// Copyright 2004-2007 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Internal;
using Castle.MonoRail.ActiveRecordScaffold;
using ExclusiveReality.Models;
using ExclusiveReality.Models.Attributes;
using CommonOperationUtils=ExclusiveReality.Utils.CommonOperationUtils;

namespace ExclusiveReality.Helpers
{
    public class NexusARFormHelper : NexusFormHelper
    {
        private static readonly int[] Days = {
                                                 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                                                 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                                                 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31
                                             };

        private static readonly int[] Months = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};

        private static readonly int[] Years;
        private readonly IDictionary model2nestedInstance = new Hashtable();
        private readonly StringBuilder stringBuilder = new StringBuilder(1024);

        static NexusARFormHelper()
        {
            int lastYear = DateTime.Now.Year;

            Years = new int[lastYear - 1950 + 50];

            for (int year = 1950; year < lastYear + 50; year++)
            {
                Years[year - 1950] = year;
            }
        }

        public ICollection GetModelHierarchy(ActiveRecordModel model, object instance)
        {
            var list = new ArrayList();

            ActiveRecordModel hierarchy = model;

            while (hierarchy != null)
            {
                list.Add(hierarchy);

                hierarchy = ActiveRecordModel.GetModel(hierarchy.Type.BaseType);
            }

            hierarchy = model;

            while (hierarchy != null)
            {
                foreach (NestedModel nested in hierarchy.Components)
                {
                    object nestedInstance = nested.Property.GetValue(instance, null);

                    if (nestedInstance == null)
                    {
                        nestedInstance = CreationUtil.Create(nested.Property.PropertyType);
                    }

                    if (nestedInstance != null)
                    {
                        this.model2nestedInstance[nested.Model] = nestedInstance;
                    }

                    list.Add(nested.Model);
                }

                hierarchy = ActiveRecordModel.GetModel(hierarchy.Type.BaseType);
            }

            return list;
        }

        public ICollection GetOneToOneModelHierarchy(ActiveRecordModel model, object instance)
        {
            var list = new ArrayList();

            foreach (OneToOneModel oneToOne in model.OneToOnes)
            {
                ActiveRecordModel oneToOneModel = ActiveRecordModel.GetModel(oneToOne.Property.PropertyType);
                list.Add(oneToOneModel);
            }

            return list;
        }

        public bool HasLocaleModel(ActiveRecordModel mainModel)
        {
            return (this.GetLocaleModel(mainModel) != null);
        }

        public ActiveRecordModel GetLocaleModel(ActiveRecordModel mainModel)
        {
            if (mainModel == null)
            {
                return null;
            }

            ActiveRecordModel result = null;
            Type localeType = this.GetLocaleType(mainModel.Type);
            if (localeType != null)
            {
                result = ActiveRecordModel.GetModel(localeType);
            }

            return result;
        }

        public Type GetLocaleType(Type mainType)
        {
            Type result = null;

            if (mainType == null)
            {
                return result;
            }

            object[] localizedPropertiesTypeAttributes =
                mainType.GetCustomAttributes(typeof (LocalizedPropertiesTypeAttribute), true);
            if (localizedPropertiesTypeAttributes.Length > 0)
            {
                result = (localizedPropertiesTypeAttributes[0] as LocalizedPropertiesTypeAttribute).ARType;
            }

            return result;
        }

        private void RenderAppropriateControl(ActiveRecordModel model,
                                              Type propType, string propName, PropertyInfo property,
                                              object value, bool unique, bool notNull, String columnType, int length)
        {
            IDictionary htmlAttributes = new Hashtable();

            if (propType == typeof (String))
            {
                var atts = new Hashtable();
                switch (this.GetPropertyFormBehavior(property))
                {
                    case FormControlType.WYSIWYG:
                        string wimPropName = propName.Replace(".", "").Replace("[", "_").Replace("]", "_");
                        atts.Add("class", wimPropName);
                        this.stringBuilder.AppendLine("<script type=\"text/javascript\">");
                        this.stringBuilder.AppendLine("jQuery(function() {");
                        this.stringBuilder.AppendLine("    jQuery('." + wimPropName + "').wymeditor({");
                        this.stringBuilder.AppendLine("        stylesheet: 'screen.css'");
                        this.stringBuilder.AppendLine("    });");
                        this.stringBuilder.AppendLine("});");
                        this.stringBuilder.AppendLine("</script>");

                        this.stringBuilder.AppendFormat(TextArea(propName, atts, true));
                        break;
                    case FormControlType.Hidden:
                        this.stringBuilder.AppendFormat(HiddenField(propName, htmlAttributes));
                        break;
                    default:
                        if (String.Compare("stringclob", columnType, true) == 0)
                        {
                            atts.Add("rows", 10);
                            atts.Add("style", "width:100%;");

                            this.stringBuilder.AppendFormat(TextArea(propName, atts));
                        }
                        else
                        {
                            if (length > 0)
                            {
                                htmlAttributes["maxlength"] = length.ToString();
                            }

                            this.stringBuilder.AppendFormat(TextField(propName, htmlAttributes));
                        }
                        break;
                }
            }
            else if (propType == typeof (Int16) || propType == typeof (Int32) || propType == typeof (Int64))
            {
                this.stringBuilder.AppendFormat(NumberField(propName, htmlAttributes));
            }
            else if (propType == typeof (Single) || propType == typeof (Double))
            {
                this.stringBuilder.AppendFormat(NumberField(propName, htmlAttributes));
            }
            else if (propType == typeof (DateTime))
            {
                if (length > 0)
                {
                    htmlAttributes["maxlength"] = length.ToString();
                }

                this.stringBuilder.AppendFormat(TextField(propName, htmlAttributes));
                //stringBuilder.AppendFormat(Select(propName + "month", Months, htmlAttributes));
                //stringBuilder.AppendFormat(Select(propName + "day", Days, htmlAttributes));
                //stringBuilder.AppendFormat(Select(propName + "year", Years, htmlAttributes));
            }
            else if (propType == typeof (bool))
            {
                this.stringBuilder.Append(CheckboxField(propName, htmlAttributes));
            }
            else if (propType == typeof (byte[]))
            {
                this.stringBuilder.Append(FileField(propName));
                this.stringBuilder.Append("(*.jpg)");
                if (value != null)
                {
                    EstatePictures picturesInstance = null;
                    PropertyInfo photoProp = null;
                    foreach (PropertyInfo p in value.GetType().GetProperties())
                    {
                        if (p.Name == property.DeclaringType.Name && p.PropertyType == property.DeclaringType)
                        {
                            photoProp = p;
                        }
                    }
                    if (photoProp != null)
                    {
                        picturesInstance = photoProp.GetValue(value, null) as EstatePictures;
                    }

                    if (picturesInstance != null)
                    {
                        var fileContent = property.GetValue(picturesInstance, null) as byte[];
                        if (fileContent != null)
                        {
                            this.stringBuilder.Append(" <a href=\"/estateimage.axd?t=" + value.GetType().Name + "&p="
                                                      + property.Name + "&id="
                                                      + value.GetType().GetProperty("Id").GetValue(value, null)
                                                      + "\" target=\"_blank\">Náhled</a>");
                            this.stringBuilder.Append(" <a href=\"/estateimage.axd?clear=true&t=" + value.GetType().Name
                                                      + "&p=" + property.Name + "&id="
                                                      + value.GetType().GetProperty("Id").GetValue(value, null)
                                                      + "\" target=\"_blank\">Vymazat</a>");
                        }
                    }
                }
            }
            else if (propType == typeof (Enum))
            {
                // TODO: Support flags as well

                String[] names = Enum.GetNames(propType);

                IList options = new ArrayList();

                foreach (String name in names)
                {
                    options.Add(String.Format("{0} {1}\r\n", RadioField(propName, name, htmlAttributes),
                                              LabelFor(name, name, htmlAttributes)));
                }
            }
        }

        private static string CreatePropName(ActiveRecordModel model, String prefix, String name)
        {
            string propName;

            if (model.IsNestedType)
            {
                propName = String.Format("{0}.{1}.{2}", prefix, model.Type.Name, name);
            }
            else
            {
                propName = String.Format("{0}.{1}", prefix, name);
            }

            return propName;
        }

        public string GetLocalizedName(Type mainType, Type nestedType)
        {
            String result = String.Empty;
            object[] localizationAttributes = null;


            foreach (PropertyInfo prop in mainType.GetProperties())
            {
                if (prop.PropertyType == nestedType)
                {
                    localizationAttributes = prop.GetCustomAttributes(typeof (PropertyLocalizationAttribute), false);
                    if (localizationAttributes.Length == 0)
                    {
                        localizationAttributes = prop.GetCustomAttributes(typeof (PropertyLocalizationAttribute), true);
                    }
                    foreach (object item in localizationAttributes)
                    {
                        var att = item as PropertyLocalizationAttribute;
                        if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == att.TwoLetterISOLanguageName)
                        {
                            result = att.Text;
                        }
                    }
                }
            }


            if (String.IsNullOrEmpty(result))
            {
                localizationAttributes = nestedType.GetCustomAttributes(typeof (ClassLocalizationAttribute), false);
                if (localizationAttributes.Length == 0)
                {
                    localizationAttributes = nestedType.GetCustomAttributes(typeof (ClassLocalizationAttribute), true);
                }
                foreach (object item in localizationAttributes)
                {
                    var att = item as ClassLocalizationAttribute;
                    if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == att.TwoLetterISOLanguageName)
                    {
                        result = att.ItemText;
                    }
                }
            }

            if (String.IsNullOrEmpty(result))
            {
                result = nestedType.Name;
            }

            return result;
        }

        public string GetLocalizedPropertyName(string propName, object instance)
        {
            if (instance == null)
            {
                return String.Empty;
            }

            foreach (PropertyInfo prop in instance.GetType().GetProperties())
            {
                if (prop.Name == propName)
                {
                    return this.GetLocalizedPropertyName(prop);
                }
            }

            return String.Empty;
        }

        public string GetLocalizedPropertyName(PropertyInfo prop)
        {
            String result = String.Empty;
            object[] localizationAttributes = null;

            localizationAttributes = prop.GetCustomAttributes(typeof (PropertyLocalizationAttribute), false);
            if (localizationAttributes.Length == 0)
            {
                localizationAttributes = prop.GetCustomAttributes(typeof (PropertyLocalizationAttribute), true);
            }
            foreach (object item in localizationAttributes)
            {
                var att = item as PropertyLocalizationAttribute;
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == att.TwoLetterISOLanguageName)
                {
                    result = att.Text;
                }
            }

            if (String.IsNullOrEmpty(result) && !this.CanHandleType(prop.PropertyType))
            {
                localizationAttributes = prop.PropertyType.GetCustomAttributes(typeof (ClassLocalizationAttribute),
                                                                               false);
                if (localizationAttributes.Length == 0)
                {
                    localizationAttributes = prop.PropertyType.GetCustomAttributes(typeof (ClassLocalizationAttribute),
                                                                                   true);
                }
                foreach (object item in localizationAttributes)
                {
                    var att = item as ClassLocalizationAttribute;
                    if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == att.TwoLetterISOLanguageName)
                    {
                        result = att.ItemText;
                    }
                }
            }

            if (String.IsNullOrEmpty(result))
            {
                result = prop.Name;
            }

            return result;
        }

        public string GetLocalizedClassName(Type type)
        {
            String result = String.Empty;
            object[] localizationAttributes = null;

            if (String.IsNullOrEmpty(result))
            {
                localizationAttributes = type.GetCustomAttributes(typeof (ClassLocalizationAttribute), false);
                if (localizationAttributes.Length == 0)
                {
                    localizationAttributes = type.GetCustomAttributes(typeof (ClassLocalizationAttribute), true);
                }
                foreach (object item in localizationAttributes)
                {
                    var att = item as ClassLocalizationAttribute;
                    if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == att.TwoLetterISOLanguageName)
                    {
                        result = att.ItemText;
                    }
                }
            }

            if (String.IsNullOrEmpty(result))
            {
                result = type.Name;
            }

            return result;
        }

        public string GetLocalizedPropertyNameWithValue(string propName, object instance, string format)
        {
            if (instance == null)
            {
                return String.Empty;
            }

            foreach (PropertyInfo prop in instance.GetType().GetProperties())
            {
                if (prop.Name == propName)
                {
                    object value = prop.GetValue(instance, null);
                    if (this.GetPropertyFormBehavior(prop) == FormControlType.WYSIWYG)
                    {
                        value = EnsureParagraphOverContent((value == null ? "" : value.ToString()));
                    }
                    return String.Format(format, this.GetLocalizedPropertyName(prop), value);
                }
            }
            return String.Empty;
        }

        public FormControlType GetPropertyFormBehavior(PropertyInfo prop)
        {
            if (prop == null)
            {
                return FormControlType.Auto;
            }

            object[] formGeneratorBehaviorAttributes = prop.GetCustomAttributes(
                typeof (FormGeneratorBehaviorAttribute), false);
            if (formGeneratorBehaviorAttributes.Length == 0)
            {
                formGeneratorBehaviorAttributes = prop.GetCustomAttributes(typeof (FormGeneratorBehaviorAttribute), true);
            }
            foreach (object item in formGeneratorBehaviorAttributes)
            {
                var att = item as FormGeneratorBehaviorAttribute;
                if (att != null)
                {
                    return att.FormControlType;
                }
            }

            return FormControlType.Auto;
        }

        public string GetFormatedValue(string propertyName, object instance, string format)
        {
            if (String.IsNullOrEmpty(format))
            {
                format = "{0} : {1}";
            }
            PropertyInfo prop = instance.GetType().GetProperty(propertyName);
            if (prop != null)
            {
                object value = prop.GetValue(instance, null);
                if (value != null)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(value)))
                    {
                        return String.Format(format, this.GetLocalizedPropertyName(prop), value);
                    }
                }
            }
            return String.Empty;
        }

        public string GetFormatedPrice(object price)
        {
            if (price == null || !(price is int))
            {
                return String.Empty;
            }
            CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(1029);

            string result = int.Parse(price.ToString()).ToString("N").Replace(",00", "");

            Thread.CurrentThread.CurrentCulture = oldCulture;

            return result;
        }

        public ActiveRecordModel GetModelFromType(Type arType)
        {
            if (arType == null)
            {
                return null;
            }

            return ActiveRecordModel.GetModel(arType);
        }

        #region CanHandle methods

        public bool CanHandle(FieldModel field)
        {
            return this.CanHandleType(field.Field.FieldType);
        }

        public bool CanHandle(PropertyModel propModel)
        {
            return this.CanHandleType(propModel.Property.PropertyType) && propModel.Property.PropertyType.IsPublic;
        }

        public bool CanHandle(PropertyInfo propInfo)
        {
            return this.CanHandleType(propInfo.PropertyType) && propInfo.PropertyType.IsPublic;
        }

        public bool CanHandle(OneToOneModel model)
        {
            return this.CheckModelAndKeyAreAccessible(model.OneToOneAtt.MapType);
        }

        public bool CanHandle(BelongsToModel model)
        {
            return this.CheckModelAndKeyAreAccessible(model.BelongsToAtt.Type);
        }

        public bool CanHandle(HasManyModel model)
        {
            if (!model.HasManyAtt.Inverse)
            {
                return this.CheckModelAndKeyAreAccessible(model.HasManyAtt.MapType);
            }
            return false;
        }

        public bool CanHandle(HasAndBelongsToManyModel model)
        {
            if (!model.HasManyAtt.Inverse)
            {
                return this.CheckModelAndKeyAreAccessible(model.HasManyAtt.MapType);
            }
            return false;
        }

        private bool CheckModelAndKeyAreAccessible(Type type)
        {
            ActiveRecordModel otherModel = ActiveRecordModel.GetModel(type);

            PrimaryKeyModel keyModel = this.ObtainPKProperty(otherModel);

            if (otherModel == null || keyModel == null)
            {
                return false;
            }

            return true;
        }

        private PrimaryKeyModel ObtainPKProperty(ActiveRecordModel model)
        {
            if (model == null)
            {
                return null;
            }

            ActiveRecordModel curModel = model;

            while (curModel != null)
            {
                PrimaryKeyModel keyModel = curModel.PrimaryKey;

                if (keyModel != null)
                {
                    return keyModel;
                }

                curModel = curModel.Parent;
            }

            return null;
        }

        private bool CanHandleType(Type type)
        {
            return (type.IsPrimitive ||
                    type == typeof (String) ||
                    type == typeof (Decimal) ||
                    type == typeof (Single) ||
                    type == typeof (Double) ||
                    type == typeof (Byte) ||
                    type == typeof (Byte[]) ||
                    type == typeof (SByte) ||
                    type == typeof (SByte[]) ||
                    type == typeof (bool) ||
                    type == typeof (Enum) ||
                    type == typeof (DateTime));
        }

        #endregion

        #region CreateControl methods

        public String CreateControl(ActiveRecordModel model, String prefix,
                                    String propertyName, object instance)
        {
            foreach (object item in model.Properties)
            {
                var prop = item as PropertyModel;
                if (prop.Property.Name == propertyName)
                {
                    return CreateControl(model, prefix, prop, instance);
                }
            }

            foreach (object item in model.BelongsTo)
            {
                var prop = item as BelongsToModel;
                if (prop.Property.Name == propertyName)
                {
                    return CreateControl(model, prefix, prop, instance);
                }
            }

            foreach (object item in model.HasMany)
            {
                var prop = item as HasManyModel;
                if (prop.Property.Name == propertyName)
                {
                    return CreateControl(model, prefix, prop, instance);
                }
            }

            foreach (object item in model.HasAndBelongsToMany)
            {
                var prop = item as HasAndBelongsToManyModel;
                if (prop.Property.Name == propertyName)
                {
                    return CreateControl(model, prefix, prop, instance);
                }
            }

            return String.Empty;
        }


        internal String CreateControl(ActiveRecordModel model, String prefix,
                                      FieldModel fieldModel, object instance)
        {
            this.stringBuilder.Length = 0;

            FieldInfo fieldInfo = fieldModel.Field;

            String propName = CreatePropName(model, prefix, fieldInfo.Name);

            if (fieldInfo.FieldType == typeof (DateTime))
            {
                this.stringBuilder.Append(LabelFor(propName + "day", fieldInfo.Name + ": &nbsp;"));
            }
            else
            {
                this.stringBuilder.Append(LabelFor(propName, fieldInfo.Name + ": &nbsp;"));
            }

            FieldAttribute propAtt = fieldModel.FieldAtt;

            this.RenderAppropriateControl(model, fieldInfo.FieldType, propName, null, null,
                                          propAtt.Unique, propAtt.NotNull, propAtt.ColumnType, propAtt.Length);

            return this.stringBuilder.ToString();
        }

        internal String CreateControl(ActiveRecordModel model, String prefix,
                                      PropertyModel propertyModel, object instance)
        {
            this.stringBuilder.Length = 0;

            PropertyInfo prop = propertyModel.Property;

            // Skip non standard properties
            if (!prop.CanWrite || !prop.CanRead)
            {
                return String.Empty;
            }

            // Skip indexers
            if (prop.GetIndexParameters().Length != 0)
            {
                return String.Empty;
            }

            String propName = CreatePropName(model, prefix, prop.Name);

            if (prop.PropertyType == typeof (DateTime))
            {
                this.stringBuilder.Append(LabelFor(propName + "day", this.GetLocalizedPropertyName(prop) + ": &nbsp;"));
            }
            else
            {
                this.stringBuilder.Append(LabelFor(propName, this.GetLocalizedPropertyName(prop) + ": &nbsp;"));
            }

            PropertyAttribute propAtt = propertyModel.PropertyAtt;

            this.RenderAppropriateControl(model, prop.PropertyType, propName, prop, instance,
                                          propAtt.Unique, propAtt.NotNull, propAtt.ColumnType, propAtt.Length);

            return this.stringBuilder.ToString();
        }

        internal String CreateControl(ActiveRecordModel model, String prefix,
                                      PropertyInfo prop, object instance)
        {
            this.stringBuilder.Length = 0;

            // Skip non standard properties
            if (!prop.CanWrite || !prop.CanRead)
            {
                return String.Empty;
            }

            // Skip indexers
            if (prop.GetIndexParameters().Length != 0)
            {
                return String.Empty;
            }

            String propName = CreatePropName(model, prefix, prop.Name);

            if (prop.PropertyType == typeof (DateTime))
            {
                this.stringBuilder.Append(LabelFor(propName + "day", this.GetLocalizedPropertyName(prop) + ": &nbsp;"));
            }
            else
            {
                this.stringBuilder.Append(LabelFor(propName, this.GetLocalizedPropertyName(prop) + ": &nbsp;"));
            }

            this.RenderAppropriateControl(model, prop.PropertyType,
                                          propName, prop, null, false, false, null, 0);

            return this.stringBuilder.ToString();
        }

        internal String CreateControl(ActiveRecordModel model, String prefix,
                                      BelongsToModel belongsToModel, object instance)
        {
            this.stringBuilder.Length = 0;

            PropertyInfo prop = belongsToModel.Property;

            prefix += "." + prop.Name;

            ActiveRecordModel otherModel = ActiveRecordModel.GetModel(belongsToModel.BelongsToAtt.Type);

            PrimaryKeyModel keyModel = this.ObtainPKProperty(otherModel);

            if (otherModel == null || keyModel == null)
            {
                return "Model not found or PK not found";
            }

            object[] items = CommonOperationUtils.FindAll(otherModel.Type);

            String propName = CreatePropName(model, prefix, keyModel.Property.Name);

            this.stringBuilder.Append(LabelFor(propName, this.GetLocalizedPropertyName(prop) + ": &nbsp;"));

            IDictionary attrs = new HybridDictionary(true);

            attrs["value"] = keyModel.Property.Name;

            if (!belongsToModel.BelongsToAtt.NotNull)
            {
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs")
                {
                    attrs.Add("firstOption", "Prázdná hodnota");
                }
                else
                {
                    attrs.Add("firstOption", "Empty");
                }
                attrs.Add("firstOptionValue", "");
            }

            this.stringBuilder.Append(Select(propName, items, attrs));

            return this.stringBuilder.ToString();
        }

        public String CreateControlNested(ActiveRecordModel model, String prefix,
                                          BelongsToModel belongsToModel, object instance)
        {
            this.stringBuilder.Length = 0;

            PropertyInfo prop = belongsToModel.Property;

            ActiveRecordModel otherModel = ActiveRecordModel.GetModel(belongsToModel.BelongsToAtt.Type);

            PrimaryKeyModel keyModel = this.ObtainPKProperty(otherModel);

            if (otherModel == null || keyModel == null)
            {
                return "Model not found or PK not found";
            }

            object[] items = CommonOperationUtils.FindAll(otherModel.Type);

            String propName = prefix + "." + model.Type.Name + "." + prop.Name + ".Id";

            this.stringBuilder.Append(LabelFor(propName, this.GetLocalizedPropertyName(prop) + ": &nbsp;"));

            IDictionary attrs = new HybridDictionary(true);

            attrs["value"] = keyModel.Property.Name;

            if (!belongsToModel.BelongsToAtt.NotNull)
            {
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs")
                {
                    attrs.Add("firstOption", "Prázdná hodnota");
                }
                else
                {
                    attrs.Add("firstOption", "Empty");
                }
                attrs.Add("firstOptionValue", "");
            }

            this.stringBuilder.Append(Select(propName, items, attrs));

            return this.stringBuilder.ToString();
        }

        internal String CreateControl(ActiveRecordModel model, String prefix,
                                      HasManyModel hasManyModel, object instance)
        {
            this.stringBuilder.Length = 0;

            PropertyInfo prop = hasManyModel.Property;

            prefix += "." + prop.Name;

            ActiveRecordModel otherModel = ActiveRecordModel.GetModel(hasManyModel.HasManyAtt.MapType);

            PrimaryKeyModel keyModel = this.ObtainPKProperty(otherModel);

            if (otherModel == null || keyModel == null)
            {
                return "Model not found or PK not found";
            }

            object[] source = CommonOperationUtils.FindAll(otherModel.Type);

            this.stringBuilder.Append(this.GetLocalizedPropertyName(prop) + ": &nbsp;");
            this.stringBuilder.Append("<br/>\r\n");

            IDictionary attrs = new HybridDictionary(true);

            attrs["value"] = keyModel.Property.Name;

            CheckboxList list = CreateCheckboxList(prefix, source, attrs);

            foreach (object item in list)
            {
                this.stringBuilder.Append(list.Item());

                this.stringBuilder.Append(item.ToString());

                this.stringBuilder.Append("<br/>\r\n");
            }

            return this.stringBuilder.ToString();
        }

        internal String CreateControl(ActiveRecordModel model, String prefix,
                                      HasAndBelongsToManyModel hasAndBelongsModel, object instance)
        {
            this.stringBuilder.Length = 0;

            PropertyInfo prop = hasAndBelongsModel.Property;

            prefix += "." + prop.Name;

            ActiveRecordModel otherModel = ActiveRecordModel.GetModel(hasAndBelongsModel.HasManyAtt.MapType);

            PrimaryKeyModel keyModel = this.ObtainPKProperty(otherModel);

            if (otherModel == null || keyModel == null)
            {
                return "Model not found or PK not found";
            }

            object[] source = CommonOperationUtils.FindAll(otherModel.Type);

            this.stringBuilder.Append(this.GetLocalizedPropertyName(prop) + ": &nbsp;");
            this.stringBuilder.Append("<br/>\r\n");

            IDictionary attrs = new HybridDictionary(true);

            attrs["value"] = keyModel.Property.Name;

            CheckboxList list = CreateCheckboxList(prefix, source, attrs);

            foreach (object item in list)
            {
                this.stringBuilder.Append(list.Item());

                this.stringBuilder.Append(item.ToString());

                this.stringBuilder.Append("<br/>\r\n");
            }

            return this.stringBuilder.ToString();
        }

        #endregion
    }
}