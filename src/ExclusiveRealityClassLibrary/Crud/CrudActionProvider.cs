using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Binder;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using ExclusiveReality.Models;
using System.Reflection;
using Castle.ActiveRecord.Framework.Internal;
using System.Globalization;
using System.IO;
using System.Collections;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models.Attributes;
using System.Text.RegularExpressions;
using Castle.ActiveRecord.Queries;

namespace ExclusiveReality.Crud
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CrudAttribute : Attribute
    {
        private readonly Type activeRecordType;

        public CrudAttribute(Type activeRecordType)
        {
            this.activeRecordType = activeRecordType;
        }

        public Type ActiveRecordType
        {
            get { return activeRecordType; }
        }
    }

    /// <summary>
    /// Includes dynamic actions on the controller
    /// </summary>
    public class CrudProvider : IDynamicActionProvider
    {
        public void IncludeActions(Controller controller)
        {
            Type controllerType = controller.GetType();

            object[] atts = controllerType.GetCustomAttributes(typeof(CrudAttribute), false);

            if (atts.Length == 0)
                throw new Exception("CrudAttribute not used on " + controllerType.Name);

            CrudAttribute crudAtt = (CrudAttribute)atts[0];
            Type arType = crudAtt.ActiveRecordType;

            /***************lokalizace*****************/
            Type localeARType = null;
            object[] localeTmpAtts = arType.GetCustomAttributes(typeof(LocalizedPropertiesTypeAttribute), true);
            if (localeTmpAtts.Length > 0)
                localeARType = (localeTmpAtts[0] as ExclusiveReality.Models.Attributes.LocalizedPropertiesTypeAttribute).ARType;
            /***************lokalizace*****************/


            controller.DynamicActions["index"] = new IndexAction();
            controller.DynamicActions["list"] = new ListAction(arType, localeARType);
            controller.DynamicActions["detail"] = new DetailAction(arType, localeARType);
            controller.DynamicActions["new"] = new NewAction(arType, localeARType);
            controller.DynamicActions["create"] = new CreateAction(arType, localeARType);
            controller.DynamicActions["edit"] = new EditAction(arType, localeARType);
            controller.DynamicActions["update"] = new UpdateAction(arType, localeARType);
            controller.DynamicActions["confirmdelete"] = new ConfirmDeleteAction(arType, localeARType);
            controller.DynamicActions["delete"] = new DeleteAction(arType, localeARType);
        }

        public static bool IsPrimitive(Type type)
        {
            return (type.IsPrimitive ||
                    type == typeof(String) ||
                    type == typeof(Decimal) ||
                    type == typeof(Single) ||
                    type == typeof(Double) ||
                    type == typeof(Byte) ||
                    type == typeof(SByte) ||
                    type == typeof(bool) ||
                    type == typeof(Enum) ||
                    type == typeof(DateTime));
        }

        public static bool ViewExists(string name, Controller controller)
        {
            if (Directory.Exists(controller.Context.Server.MapPath("\\views\\" + controller.ViewFolder)))
                foreach (string f in Directory.GetFiles(controller.Context.Server.MapPath("\\views\\" + controller.ViewFolder)))
                    if (Path.GetFileNameWithoutExtension(f) == name)
                        return true;
            return false;
        }

    }

    public class BaseAction
    {
        protected readonly Type arType;
        protected readonly ActiveRecordModel model;
        protected readonly string prefix;
        protected readonly Type localeARType;
        protected readonly ActiveRecordModel localeModel;
        protected readonly string localePrefix;
        protected readonly ExclusiveReality.Models.Attributes.ClassLocalizationAttribute classLocalization;

        public BaseAction(Type arType, Type localeARType)
        {
            this.arType = arType;
            this.localeARType = localeARType;

            if (arType != null)
            {
                this.model = ActiveRecordModel.GetModel(arType);
                this.prefix = "Instance";
            }

            /***************lokalizace*****************/
            if (localeARType != null)
            {
                this.localeModel = ActiveRecordModel.GetModel(localeARType);
                this.localePrefix = "LocalizationInstances";
            }
            /***************lokalizace*****************/

            /***************popis AR tridy pro formulare*****************/
            object[] classLocalizationTmpAtts = arType.GetCustomAttributes(typeof(ClassLocalizationAttribute), true);
            if (classLocalizationTmpAtts.Length > 0)
                classLocalization = (classLocalizationTmpAtts[0] as ClassLocalizationAttribute);
            /***************popis AR tridy pro formulare*****************/
        }

        public void ExecuteBase(Controller controller)
        {
            controller.PropertyBag["AllCultures"] = Culture.FindAll();
            controller.PropertyBag["AllCultureInfo"] = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            controller.PropertyBag["Now"] = DateTime.Now;
            
            controller.PropertyBag["Model"] = this.model;
            controller.PropertyBag["Prefix"] = this.prefix;
            controller.PropertyBag["LocaleModel"] = this.localeModel;
            controller.PropertyBag["LocalePrefix"] = this.localePrefix;

            controller.PropertyBag["ClassLocalization"] = this.classLocalization;
        }
    }

    public class IndexAction : IDynamicAction
    {
        public void Execute(Controller controller)
        {
            if (CrudProvider.ViewExists("index", controller))
                controller.RenderView("index");
            else
                controller.Redirect(controller.Name, "list");
        }
    }

    public class ListAction : BaseAction, IDynamicAction
    {
        public ListAction(Type arType, Type localeARType)
            : base(arType, localeARType){}

        public void Execute(Controller controller)
        {
            ExecuteBase(controller);


            // aktualizace URL stránek s detailem nemovitosti
            if (!String.IsNullOrEmpty(controller.Params["updateEstatesUrl"]) && controller.Params["updateEstatesUrl"] == "1")
            {
                Estate[] estates = Estate.FindAll();
                foreach (Estate estate in estates)
                {
                    foreach (EstateCulture estateCulture in estate.LocalizedObjects)
                    {
                        estateCulture.DetailPageUrl = estate.GetUrl(estateCulture.Culture.Id);
                        estateCulture.Update();
                    }
                }
            }
            // aktualizace URL stránek s detailem nemovitosti
            else if (!String.IsNullOrEmpty(controller.Params["updateProjectsUrl"]) && controller.Params["updateProjectsUrl"] == "1")
            {
                DeveloperProject[] projects = DeveloperProject.FindAll();
                foreach (DeveloperProject project in projects)
                {
                    foreach (DeveloperProjectCulture projectCulture in project.LocalizedObjects)
                    {
                        projectCulture.DetailPageUrl = project.GetUrl(projectCulture.Culture.Id);
                        projectCulture.Update();
                    }
                }
            }


            string where = "";
            if (!String.IsNullOrEmpty(controller.Params["filter_key"]) && !String.IsNullOrEmpty(controller.Params["filter_value"]))
            {
                if (controller.Params["filter_value"] == "null")
                {
                    where = "m." + controller.Params["filter_key"] + " is null";
                }
                else if (controller.Params["filter_value"] == "not null")
                {
                    where = "m." + controller.Params["filter_key"] + " is not null";
                }
                else
                {
                    where = "m." + controller.Params["filter_key"] + " like '" + controller.Params["filter_value"] + "%'";
                }
            }
            string where1 = "";
            if (!String.IsNullOrEmpty(controller.Params["filter_key1"]) && !String.IsNullOrEmpty(controller.Params["filter_value1"]))
            {
                if (controller.Params["filter_value1"] == "null")
                {
                    where1 = "m." + controller.Params["filter_key1"] + " is null";
                }
                else if (controller.Params["filter_value1"] == "not null")
                {
                    where1 = "m." + controller.Params["filter_key1"] + " is not null";
                }
                else
                {
                    where1 = "m." + controller.Params["filter_key1"] + " like '" + controller.Params["filter_value1"] + "%'";
                }
            }

            if (!String.IsNullOrEmpty(where) || !String.IsNullOrEmpty(where1))
            {
                string whereTmp = " where ";

                if (!String.IsNullOrEmpty(where))
                {
                    whereTmp += where;
                }

                if (!String.IsNullOrEmpty(where1))
                {
                    if (!String.IsNullOrEmpty(where))
                    {
                        whereTmp += " and ";
                    }
                    whereTmp += where1;
                }

                where = whereTmp;
            }


            string sort = "";
            if (!String.IsNullOrEmpty(controller.Params["sort_key"]))
                sort = " order by " + controller.Params["sort_key"] + " " + (controller.Params["sort_dir"] == "asc" ? "asc" : "desc");


            var tmpResult = new ActiveRecordBase[0];
            try
            {
                tmpResult = new SimpleQuery<ActiveRecordBase>(arType, "from " + arType.Name + " m" + where + sort).Execute();
            }
            catch (Exception ex)
            {
                controller.Response.Write(ex);
            }


            List<object> orderedList = new List<object>();
            foreach (object item in tmpResult)
                orderedList.Add(item);
            if (String.IsNullOrEmpty(controller.Params["sort_key"]))
                orderedList.Reverse();
            controller.PropertyBag["items"] = orderedList.ToArray();

            if (CrudProvider.ViewExists("list", controller))
                controller.RenderView("list");
            else
                controller.RenderSharedView("admin_exclusivereal/common/list");
        }
    }

    public class DetailAction : BaseAction, IDynamicAction
    {
        public DetailAction(Type arType, Type localeARType)
            : base(arType, localeARType) { }

        public void Execute(Controller controller)
        {
            ExecuteBase(controller);

            int id = Convert.ToInt32(controller.Query["Id"]);

            controller.PropertyBag[prefix] = ActiveRecordMediator.FindByPrimaryKey(arType, id, false);
            if (localeARType != null && controller.PropertyBag[prefix] != null)
            {
                var localeItems = ActiveRecordMediator.FindAllByProperty(localeARType, arType.Name + ".Id", id) as object[];
                controller.PropertyBag[localePrefix] = localeItems;



                foreach (OneToOneModel otoModel in ActiveRecordModel.GetModel(arType).OneToOnes)
                {
                    object oneToOneInstance = ActiveRecordMediator.FindByPrimaryKey(otoModel.OneToOneAtt.MapType, id, false);
                    Type oneToOneLocaleARType = null;

                    object[] localizedPropertiesTypeTmpAtts = otoModel.OneToOneAtt.MapType.GetCustomAttributes(typeof(LocalizedPropertiesTypeAttribute), true);
                    if (localizedPropertiesTypeTmpAtts.Length > 0)
                        oneToOneLocaleARType = (localizedPropertiesTypeTmpAtts[0] as ExclusiveReality.Models.Attributes.LocalizedPropertiesTypeAttribute).ARType;

                    if (oneToOneLocaleARType != null && oneToOneInstance != null)
                    {
                        var oneToOneLocaleItems = ActiveRecordMediator.FindAllByProperty(oneToOneLocaleARType, otoModel.OneToOneAtt.MapType.Name + ".Id", oneToOneInstance.GetType().GetProperty("Id").GetValue(oneToOneInstance, null)) as object[];
                        controller.PropertyBag["OneToOneProperty_" + otoModel.Property.Name + "_" + localePrefix] = oneToOneLocaleItems;
                    }
                }



            }


            if (CrudProvider.ViewExists("detail", controller))
                controller.RenderView("detail");
            else
                controller.RenderSharedView("admin_exclusivereal/common/detail");
        }
    }

    public class NewAction : BaseAction, IDynamicAction
    {
        public NewAction(Type arType, Type localeARType)
            : base(arType, localeARType){}

        public void Execute(Controller controller)
        {
            base.ExecuteBase(controller);

            object objectInstance = Activator.CreateInstance(arType);

            foreach (PropertyInfo nestedType in NexusAttributeHelper<NestedAttribute>.GetPropertiesWhereIs(objectInstance))
            {
                object nestedInstance = Activator.CreateInstance(nestedType.PropertyType);
                objectInstance.GetType().GetProperty(nestedType.Name).SetValue(objectInstance, nestedInstance, null);
            }

            foreach (OneToOneModel otoModel in ActiveRecordModel.GetModel(arType).OneToOnes)
            {
                object oneToOneInstance = Activator.CreateInstance(otoModel.OneToOneAtt.MapType);
                objectInstance.GetType().GetProperty(otoModel.Property.Name).SetValue(objectInstance, oneToOneInstance, null);
            }

            controller.PropertyBag[this.prefix] = objectInstance;






            if (CrudProvider.ViewExists("new", controller))
                controller.RenderView("new");
            else
                controller.RenderSharedView("admin_exclusivereal/common/new");
        }
    }

    public class CreateAction : BaseAction, IDynamicAction
    {
        public CreateAction(Type arType, Type localeARType)
            : base(arType, localeARType){}

        public void Execute(Controller controller)
        {
            ExecuteBase(controller);

            object instance = null;

            try
            {
                var arController =
                    (ARSmartDispatcherController)controller;

                var binder = (ARDataBinder)arController.Binder;
                binder.AutoLoad = AutoLoadBehavior.OnlyNested;

                var builder = new TreeBuilder();
                instance = binder.BindObject(
                    arType, prefix,
                    builder.BuildSourceNode(controller.Form));

                NexusARDataBinder.BindNestedObjects(ref instance, prefix, controller);

                NexusARDataBinder.BindNewOneToOneObjects(ref instance, prefix, controller);

                if (model.OneToOnes.Count == 0)
                    ActiveRecordMediator.Create(instance);


                /***************lokalizace*****************/
                if (localeARType != null)
                {
                    var localeBuilder = new TreeBuilder();
                    var localeNode = localeBuilder.BuildSourceNode(controller.Form).GetChildNode(localePrefix) as IndexedNode;

                    foreach (Node node in localeNode.ChildNodes)
                    {
                        var cNode = (node as CompositeNode);
                        object localeInstance = Activator.CreateInstance(localeARType);
                        Type localeInstanceType = localeInstance.GetType();



                        foreach (Node subnode in cNode.ChildNodes)
                        {
                            PropertyInfo prop = localeInstanceType.GetProperty(subnode.Name);
                            if (prop != null && subnode.Name != "Id" && prop.CanWrite)
                            {
                                if (CrudProvider.IsPrimitive(prop.PropertyType))
                                {
                                    prop.SetValue(localeInstance, controller.Form[localePrefix + "[" + cNode.Name + "]." + subnode.Name], null);
                                }
                                else if (prop.Name == instance.GetType().Name)
                                {
                                    prop.SetValue(localeInstance, instance, null);
                                }
                                else if (prop.Name == "Culture")
                                {
                                    prop.SetValue(localeInstance, Culture.GetById(Convert.ToInt32(cNode.Name)), null);
                                }
                                //controller.Response.Write(subnode.Name + "=" + controller.Form[localePrefix + "[" + cNode.Name + "]." + subnode.Name] + "<br>");
                            }
                        }
                        ActiveRecordMediator.Create(localeInstance);
                    }
                }
                /***************lokalizace*****************/

                if (!String.IsNullOrEmpty(controller.Form["NextStep"]) && Regex.IsMatch(controller.Form["NextStep"], "^[0-9]+$", RegexOptions.IgnoreCase))
                {
                    var attr = new Hashtable();
                    PropertyInfo idProp = instance.GetType().GetProperty("Id");
                    attr["Id"] = idProp.GetValue(instance, null) + "";
                    attr["CurrentStep"] = controller.Form["NextStep"] + "";
                    attr["OriginalAction"] = "new";
                    controller.Redirect(controller.Name, "edit", attr);
                }
                else
                    controller.Redirect(controller.Name, "list");
            }
            catch (Exception ex)
            {
                controller.Flash["errormessage"] = ex.Message;
                controller.Flash[prefix] = instance;

                controller.Response.Write(ex);
                controller.CancelView();
                //controller.Redirect(controller.Name, "new");
            }
        }
    }

    public class EditAction : BaseAction, IDynamicAction
    {
        public EditAction(Type arType, Type localeARType)
            : base(arType, localeARType){}

        public void Execute(Controller controller)
        {
            ExecuteBase(controller);

            int id = Convert.ToInt32(controller.Query["Id"]);

            controller.PropertyBag[prefix] = ActiveRecordMediator.FindByPrimaryKey(arType, id, false);
            if (localeARType != null && controller.PropertyBag[prefix] != null)
            {
                var localeItems = ActiveRecordMediator.FindAllByProperty(localeARType, arType.Name + ".Id", id) as object[];
                controller.PropertyBag[localePrefix] = localeItems;



                foreach (OneToOneModel otoModel in ActiveRecordModel.GetModel(arType).OneToOnes)
                {
                    object oneToOneInstance = ActiveRecordMediator.FindByPrimaryKey(otoModel.OneToOneAtt.MapType, id, false);
                    Type oneToOneLocaleARType = null;

                    object[] localizedPropertiesTypeTmpAtts = otoModel.OneToOneAtt.MapType.GetCustomAttributes(typeof(LocalizedPropertiesTypeAttribute), true);
                    if (localizedPropertiesTypeTmpAtts.Length > 0)
                        oneToOneLocaleARType = (localizedPropertiesTypeTmpAtts[0] as ExclusiveReality.Models.Attributes.LocalizedPropertiesTypeAttribute).ARType;

                    if (oneToOneLocaleARType != null && oneToOneInstance != null)
                    {
                        var oneToOneLocaleItems = ActiveRecordMediator.FindAllByProperty(oneToOneLocaleARType, otoModel.OneToOneAtt.MapType.Name + ".Id", oneToOneInstance.GetType().GetProperty("Id").GetValue(oneToOneInstance, null)) as object[];
                        controller.PropertyBag["OneToOneProperty_" + otoModel.Property.Name + "_" + this.localePrefix] = oneToOneLocaleItems;
                    }
                }



            }


            if(CrudProvider.ViewExists("edit", controller))
                controller.RenderView("edit");
            else
                controller.RenderSharedView("admin_exclusivereal/common/edit");
        }
    }

    public class UpdateAction : BaseAction, IDynamicAction
    {
        public UpdateAction(Type arType, Type localeARType)
            : base(arType, localeARType){}

        public void Execute(Controller controller)
        {
            ExecuteBase(controller);

            object instance = null;

            try
            {
                var arController = (ARSmartDispatcherController)controller;

                var binder = (ARDataBinder)arController.Binder;
                binder.AutoLoad = AutoLoadBehavior.Always;

                var builder = new TreeBuilder();

                instance = binder.BindObject(arType, prefix, builder.BuildSourceNode(controller.Form));

                NexusARDataBinder.BindNestedObjects(ref instance, prefix, controller);

                NexusARDataBinder.BindExistsOneToOneObjects(ref instance, prefix, controller);

                ActiveRecordMediator.Update(instance);

                /***************lokalizace*****************/
                if (localeARType != null)
                {
                    var localeBuilder = new TreeBuilder();
                    var localeNode = localeBuilder.BuildSourceNode(controller.Form).GetChildNode(localePrefix) as IndexedNode;

                    foreach (Node node in localeNode.ChildNodes)
                    {
                        var cNode = (node as CompositeNode);
                        object localeInstance = ActiveRecordMediator.FindByPrimaryKey(localeARType, Convert.ToInt32(controller.Form[localePrefix + "[" + cNode.Name + "].Id"]));
                        //controller.Response.Write(controller.Form[localePrefix + "[" + cNode.Name + "].Id"] + "<br>");

                        Type localeInstanceType = localeInstance.GetType();

                        foreach (Node subnode in cNode.ChildNodes)
                        {
                            PropertyInfo prop = localeInstanceType.GetProperty(subnode.Name);
                            if (prop != null && subnode.Name != "Id" && CrudProvider.IsPrimitive(prop.PropertyType) && prop.CanWrite)
                            {
                                //controller.Response.Write("prop.Name=" + prop.Name + "<br>");
                                prop.SetValue(localeInstance, controller.Form[localePrefix + "[" + cNode.Name + "]." + subnode.Name], null);
                                //controller.Response.Write(subnode.Name + "=" + controller.Form[localePrefix + "[" + cNode.Name + "]." + subnode.Name] + "<br>");
                            }
                        }
                        ActiveRecordMediator.Update(localeInstance);
                    }
                }
                /***************lokalizace*****************/





                //foreach (string item in controller.Form)
                //    controller.Response.Write(item + "=" + controller.Form[item] + "<br>");

                //controller.CancelView();
                if (!String.IsNullOrEmpty(controller.Form["NextStep"]) && Regex.IsMatch(controller.Form["NextStep"], "^[0-9]+$", RegexOptions.IgnoreCase))
                {
                    if (!String.IsNullOrEmpty(controller.Form["save"]))
                        controller.Redirect(controller.Name, "list");
                    else
                    {
                        var attr = new Hashtable();
                        PropertyInfo idProp = instance.GetType().GetProperty("Id");
                        attr["Id"] = idProp.GetValue(instance, null) + "";
                        attr["CurrentStep"] = controller.Form["NextStep"] + "";
                        attr["OriginalAction"] = controller.Form["OriginalAction"];

                        controller.Redirect(controller.Name, "edit", attr);
                    }
                }
                else
                    controller.Redirect(controller.Name, "list");
            }
            catch (Exception ex)
            {
                controller.Flash["errormessage"] = ex.Message;
                controller.Flash[prefix] = instance;

                controller.Response.Write(ex);
                controller.CancelView();
                //controller.Redirect(controller.Name, "edit", controller.Form);
            }
        }
    }

    public class ConfirmDeleteAction : BaseAction, IDynamicAction
    {
        public ConfirmDeleteAction(Type arType, Type localeARType)
            : base(arType, localeARType){}

        public void Execute(Controller controller)
        {
            ExecuteBase(controller);

            int id = Convert.ToInt32(controller.Query["Id"]);

            controller.PropertyBag[prefix] = ActiveRecordMediator.FindByPrimaryKey(arType, id);
            if (CrudProvider.ViewExists("confirmdelete", controller))
                controller.RenderView("confirmdelete");
            else
                controller.RenderSharedView("admin_exclusivereal/common/confirmdelete");
        }
    }

    public class DeleteAction : BaseAction, IDynamicAction
    {
        public DeleteAction(Type arType, Type localeARType)
            : base(arType, localeARType){}

        public void Execute(Controller controller)
        {
            ExecuteBase(controller);

            object instance = null;

            try
            {
                int id = Convert.ToInt32(controller.Form[prefix + ".Id"]);

                instance = ActiveRecordMediator.FindByPrimaryKey(arType, id);
                Logger.Debug(MethodBase.GetCurrentMethod(), instance);

                ActiveRecordBase arInstance = (instance as ActiveRecordBase);
                if (arInstance != null)
                {
                    Logger.Debug(MethodBase.GetCurrentMethod(), "arInstance.Delete()");
                    arInstance.Delete();
                }
                else
                {
                    Logger.Debug(MethodBase.GetCurrentMethod(), "ActiveRecordMediator.Delete(instance)");
                    ActiveRecordMediator.Delete(instance);
                }


                controller.Redirect(controller.Name, "list");
            }
            catch (Exception ex)
            {
                controller.Flash["errormessage"] = ex.Message;
                controller.Flash[prefix] = instance;

                controller.Response.Write(ex);
                controller.CancelView();
                //controller.Redirect(controller.Name, "confirmdelete", controller.Query);
            }
        }
    }
}
