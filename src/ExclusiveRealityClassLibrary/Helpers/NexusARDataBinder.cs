using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Internal;
using Castle.Components.Binder;
using Castle.MonoRail.Framework;
using ExclusiveReality.Crud;
using ExclusiveReality.HttpHandlers;
using ExclusiveReality.Models;
using ExclusiveReality.Models.Base;
using NHibernate.Expression;

namespace ExclusiveReality.Helpers
{
    public static class NexusARDataBinder
    {
        public static void BindNestedObjects(ref object instance, string prefix, Controller controller)
        {
            if (instance == null)
            {
                return;
            }

            var arFormHelper = new NexusARFormHelper();
            ActiveRecordModel model = ActiveRecordModel.GetModel(instance.GetType());
            Collection<PropertyInfo> props = NexusAttributeHelper<NestedAttribute>.GetPropertiesWhereIs(instance);

            foreach (PropertyInfo prop in props)
            {
                object nestedInstance = prop.GetValue(instance, null);
                if (nestedInstance == null)
                {
                    nestedInstance = Activator.CreateInstance(prop.PropertyType);
                }

                Collection<PropertyInfo> belongToAtts =
                    NexusAttributeHelper<BelongsToAttribute>.GetPropertiesWhereIs(nestedInstance);

                foreach (PropertyInfo subProp in nestedInstance.GetType().GetProperties())
                {
                    try
                    {
                        if (subProp.PropertyType == typeof (byte[]))
                        {
                            var file =
                                controller.Request.Files[prefix + "." + prop.Name + "." + subProp.Name] as
                                HttpPostedFile;
                            if (file.ContentLength > 0)
                            {
                                var buffer = new byte[file.ContentLength];
                                file.InputStream.Read(buffer, 0, file.ContentLength);
                                buffer = EstateImageHttpHandler.GetResizedImage(buffer, 640, 480);
                                subProp.SetValue(nestedInstance, buffer, null);
                            }
                        }
                        else
                        {
                            if (belongToAtts.Contains(subProp))
                            {
                                object belongToInstance = ActiveRecordMediator.FindOne(subProp.PropertyType,
                                                                                       Expression.Eq("Id",
                                                                                                     int.Parse(
                                                                                                         controller.Form
                                                                                                             [
                                                                                                             prefix
                                                                                                             + "."
                                                                                                             + prop.Name
                                                                                                             + "."
                                                                                                             +
                                                                                                             subProp.
                                                                                                                 Name
                                                                                                             + ".Id"])));
                                subProp.SetValue(nestedInstance, belongToInstance, null);
                            }
                            else
                            {
                                subProp.SetValue(nestedInstance,
                                                 Convert.ChangeType(
                                                     controller.Form[prefix + "." + prop.Name + "." + subProp.Name],
                                                     subProp.PropertyType), null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Response.Write(ex);
                    }
                }
                prop.SetValue(instance, nestedInstance, null);
            }
        }


        public static void BindExistsOneToOneObjects(ref object instance, string prefix, Controller controller)
        {
            if (instance == null)
            {
                return;
            }

            var arFormHelper = new NexusARFormHelper();
            ActiveRecordModel model = ActiveRecordModel.GetModel(instance.GetType());
            Collection<PropertyInfo> props = NexusAttributeHelper<OneToOneAttribute>.GetPropertiesWhereIs(instance);

            foreach (PropertyInfo prop in props)
            {
                object oneToOneInstance = Activator.CreateInstance(prop.PropertyType);

                foreach (PropertyInfo subProp in oneToOneInstance.GetType().GetProperties())
                {
                    try
                    {
                        if (subProp.PropertyType == typeof (byte[]))
                        {
                            var file =
                                controller.Request.Files[prefix + "." + prop.Name + "." + subProp.Name] as
                                HttpPostedFile;

                            if (file.ContentLength > 0)
                            {
                                var buffer = new byte[file.ContentLength];
                                file.InputStream.Read(buffer, 0, file.ContentLength);
                                buffer = EstateImageHttpHandler.GetResizedImage(buffer, 640, 480);
                                subProp.SetValue(oneToOneInstance, buffer, null);
                            }
                        }
                        else
                        {
                            subProp.SetValue(oneToOneInstance,
                                             Convert.ChangeType(
                                                 controller.Form[prefix + "." + prop.Name + "." + subProp.Name],
                                                 subProp.PropertyType), null);
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Response.Write(ex);
                    }
                }
                ActiveRecordMediator.Update(oneToOneInstance);


                Type localeARType = arFormHelper.GetLocaleType(prop.PropertyType);
                //controller.Response.Write("localeARType=" + localeARType + "<br>");

                /***************lokalizace*****************/
                if (localeARType != null)
                {
                    string localePrefix = "OneToOneProperty_" + prop.Name + "_LocalizationInstances";

                    var localeBuilder = new TreeBuilder();
                    var localeNode =
                        localeBuilder.BuildSourceNode(controller.Form).GetChildNode(localePrefix) as IndexedNode;

                    foreach (Node node in localeNode.ChildNodes)
                    {
                        var cNode = (node as CompositeNode);
                        object localeInstance = ActiveRecordMediator.FindByPrimaryKey(localeARType,
                                                                                      Convert.ToInt32(
                                                                                          controller.Form[
                                                                                              localePrefix + "["
                                                                                              + cNode.Name + "].Id"]));
                        //controller.Response.Write(controller.Form[localePrefix + "[" + cNode.Name + "].Id"] + "<br>");
                        //controller.Response.Write("(localeInstance != null)=" + (localeInstance != null) + "<br>");

                        Type localeInstanceType = localeInstance.GetType();

                        foreach (Node subnode in cNode.ChildNodes)
                        {
                            PropertyInfo subProp = localeInstanceType.GetProperty(subnode.Name);
                            if (subProp != null && subnode.Name != "Id"
                                && CrudProvider.IsPrimitive(subProp.PropertyType) && subProp.CanWrite)
                            {
                                //controller.Response.Write("subProp.Name=" + subProp.Name + "<br>");
                                subProp.SetValue(localeInstance,
                                                 controller.Form[localePrefix + "[" + cNode.Name + "]." + subnode.Name],
                                                 null);
                                //controller.Response.Write(subnode.Name + "=" + controller.Form[localePrefix + "[" + cNode.Name + "]." + subnode.Name] + "<br>");
                            }
                        }
                        ActiveRecordMediator.Update(localeInstance);
                    }
                }
                /***************lokalizace*****************/


                prop.SetValue(instance, oneToOneInstance, null);
            }
        }


        public static void BindNewOneToOneObjects(ref object instance, string prefix, Controller controller)
        {
            if (instance == null)
            {
                return;
            }

            var arFormHelper = new NexusARFormHelper();
            ActiveRecordModel model = ActiveRecordModel.GetModel(instance.GetType());
            Collection<PropertyInfo> props = NexusAttributeHelper<OneToOneAttribute>.GetPropertiesWhereIs(instance);

            foreach (PropertyInfo prop in props)
            {
                try
                {
                    object oneToOneInstance = Activator.CreateInstance(prop.PropertyType);

                    foreach (PropertyInfo subProp in oneToOneInstance.GetType().GetProperties())
                    {
                        try
                        {
                            if (subProp.PropertyType == instance.GetType())
                            {
                                subProp.SetValue(oneToOneInstance, instance, null);
                            }
                            else if (subProp.PropertyType == typeof (byte[]))
                            {
                                var file =
                                    controller.Request.Files[prefix + "." + prop.Name + "." + subProp.Name] as
                                    HttpPostedFile;

                                if (file.ContentLength > 0)
                                {
                                    var buffer = new byte[file.ContentLength];
                                    file.InputStream.Read(buffer, 0, file.ContentLength);
                                    buffer = EstateImageHttpHandler.GetResizedImage(buffer, 640, 480);
                                    subProp.SetValue(oneToOneInstance, buffer, null);
                                }
                            }
                            else
                            {
                                subProp.SetValue(oneToOneInstance,
                                                 Convert.ChangeType(
                                                     controller.Form[prefix + "." + prop.Name + "." + subProp.Name],
                                                     subProp.PropertyType), null);
                            }
                        }
                        catch (Exception ex)
                        {
                            HttpContext.Current.Response.Write(ex);
                        }
                    }
                    ActiveRecordMediator.Create(oneToOneInstance);


                    Type localeARType = arFormHelper.GetLocaleType(prop.PropertyType);

                    /***************lokalizace*****************/
                    if (localeARType != null)
                    {
                        string localePrefix = "OneToOneProperty_" + prop.Name + "_LocalizationInstances";

                        var localeBuilder = new TreeBuilder();
                        var localeNode =
                            localeBuilder.BuildSourceNode(controller.Form).GetChildNode(localePrefix) as IndexedNode;

                        foreach (Node node in localeNode.ChildNodes)
                        {
                            var cNode = (node as CompositeNode);
                            object localeInstance = Activator.CreateInstance(localeARType);
                            Type localeInstanceType = localeInstance.GetType();

                            foreach (Node subnode in cNode.ChildNodes)
                            {
                                PropertyInfo subProp = localeInstanceType.GetProperty(subnode.Name);
                                if (subProp != null && subProp.CanWrite)
                                {
                                    if (CrudProvider.IsPrimitive(subProp.PropertyType))
                                    {
                                        subProp.SetValue(localeInstance,
                                                         controller.Form[
                                                             localePrefix + "[" + cNode.Name + "]." + subnode.Name],
                                                         null);
                                    }
                                    else if (subProp.Name == oneToOneInstance.GetType().Name)
                                    {
                                        subProp.SetValue(localeInstance, oneToOneInstance, null);
                                    }
                                }
                            }
                            foreach (PropertyInfo oneToOneProp in localeInstanceType.GetProperties())
                            {
                                if (oneToOneProp.PropertyType == oneToOneInstance.GetType())
                                {
                                    oneToOneProp.SetValue(localeInstance, oneToOneInstance, null);
                                }
                                if (oneToOneProp.Name == "Culture")
                                {
                                    oneToOneProp.SetValue(localeInstance,
                                                          BusinessObjectBase<Culture>.GetById(Convert.ToInt32(cNode.Name)),
                                                          null);
                                }
                            }

                            ActiveRecordMediator.Create(localeInstance);
                        }
                    }
                    /***************lokalizace*****************/


                    prop.SetValue(instance, oneToOneInstance, null);
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write(ex);
                }
            }
        }
    }
}