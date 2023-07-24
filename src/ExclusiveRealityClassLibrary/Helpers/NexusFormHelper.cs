using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using Castle.Components.Binder;
using Castle.Components.Validator;
using Castle.Core;
using Castle.Core.Logging;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Helpers;
using Castle.MonoRail.Framework.Helpers.ValidationStrategy;
using ExclusiveReality.Utils;
using IValidator=Castle.Components.Validator.IValidator;

namespace ExclusiveReality.Helpers
{
    public class NexusFormHelper : AbstractHelper, IServiceEnabledComponent
    {
        protected static readonly BindingFlags FieldFlags = (BindingFlags.GetField | BindingFlags.Public
                                                             | BindingFlags.Instance | BindingFlags.IgnoreCase);

        protected static readonly BindingFlags PropertyFlags = (BindingFlags.GetProperty | BindingFlags.Public
                                                                | BindingFlags.Instance | BindingFlags.IgnoreCase);

        protected static readonly BindingFlags PropertyFlags2 = (BindingFlags.GetProperty | BindingFlags.Public
                                                                 | BindingFlags.Instance | BindingFlags.DeclaredOnly
                                                                 | BindingFlags.IgnoreCase);

        private readonly Stack objectStack = new Stack();
        private string currentFormId;

        private int formCount;
        private bool isValidationDisabled;
        protected ILogger logger = NullLogger.Instance;

        private BrowserValidationConfiguration validationConfig;
        private IBrowserValidatorProvider validatorProvider = new PrototypeWebValidator();

        private bool IsValidationEnabled
        {
            get
            {
                if (this.isValidationDisabled)
                {
                    return false;
                }
                return ((this.objectStack.Count == 0) || ((FormScopeInfo) this.objectStack.Peek()).IsValidationEnabled);
            }
        }

        #region IServiceEnabledComponent Members

        public void Service(IServiceProvider provider)
        {
            var service = (ILoggerFactory) provider.GetService(typeof (ILoggerFactory));
            if (service != null)
            {
                this.logger = service.Create(typeof (NexusFormHelper));
            }
            var provider2 = (IBrowserValidatorProvider) provider.GetService(typeof (IBrowserValidatorProvider));
            if (provider2 != null)
            {
                this.validatorProvider = provider2;
            }
        }

        #endregion

        private static void AddChecked(IDictionary attributes)
        {
            attributes["checked"] = "checked";
        }

        public string AjaxFormTag(IDictionary parameters)
        {
            this.currentFormId = CommonUtils.ObtainEntryAndRemove(parameters, "id", "form" + ++this.formCount);
            this.validationConfig = this.validatorProvider.CreateConfiguration(parameters);
            string str = this.IsValidationEnabled
                             ? this.validationConfig.CreateAfterFormOpened(this.currentFormId)
                             : string.Empty;
            string str2 = base.UrlHelper.For(parameters);
            parameters["form"] = true;
            if (parameters.Contains("onsubmit"))
            {
                string str3 = CommonUtils.ObtainEntryAndRemove(parameters, "onsubmit");
                if (str3.StartsWith("return ", StringComparison.InvariantCultureIgnoreCase))
                {
                    str3 = str3.Substring(7);
                }
                if (str3.EndsWith(";", StringComparison.InvariantCultureIgnoreCase))
                {
                    str3 = str3.Remove(str3.Length - 1);
                }
                string str4 = CommonUtils.ObtainEntryAndRemove(parameters, "condition", string.Empty);
                if (!string.IsNullOrEmpty(str4))
                {
                    str4 = str4 + " && ";
                }
                str4 = str4 + str3;
                parameters["condition"] = str4;
            }
            bool flag = parameters.Contains("method");
            string str5 = CommonUtils.ObtainEntryAndRemove(parameters, "method", "post");
            parameters["url"] = str2;
            if (flag)
            {
                parameters["method"] = str5;
            }
            string str6 = new AjaxHelper().RemoteFunction(parameters);
            return
                (string.Format(
                     "<form id='{1}' method='{2}' {3} onsubmit=\"{0}; return false;\" enctype=\"multipart/form-data\">",
                     new object[] {str6, this.currentFormId, str5, base.GetAttributes(parameters)}) + str);
        }

        private static void ApplyFilterOptions(IDictionary attributes)
        {
            string str = CommonUtils.ObtainEntryAndRemove(attributes, "forbid", string.Empty);
            attributes["onKeyPress"] = "return monorail_formhelper_inputfilter(event, [" + str + "]);";
        }

        private static void ApplyNumberOnlyOptions(IDictionary attributes)
        {
            string str = CommonUtils.ObtainEntryAndRemove(attributes, "exceptions", string.Empty);
            string str2 = CommonUtils.ObtainEntryAndRemove(attributes, "forbid", string.Empty);
            attributes["onKeyPress"] = "return monorail_formhelper_numberonly(event, [" + str + "], [" + str2 + "]);";
        }

        protected virtual void ApplyValidation(InputElementType inputType, string target, ref IDictionary attributes)
        {
            bool flag = CommonUtils.ObtainEntryAndRemove(attributes, "disablevalidation", "false") == "true";
            if ((this.IsValidationEnabled || !flag)
                && ((base.Controller.Validator != null) && (this.validationConfig != null)))
            {
                if (attributes == null)
                {
                    attributes = new HybridDictionary(true);
                }
                IValidator[] validatorArray = this.CollectValidators(RequestContext.All, target);
                IBrowserValidationGenerator generator = this.validatorProvider.CreateGenerator(this.validationConfig,
                                                                                               inputType, attributes);
                foreach (IValidator validator in validatorArray)
                {
                    if (validator.SupportsBrowserValidation)
                    {
                        validator.ApplyBrowserValidation(this.validationConfig, inputType, generator, attributes, target);
                    }
                }
            }
        }

        private static bool AreEqual(object left, object right)
        {
            if ((left == null) || (right == null))
            {
                return false;
            }
            if ((left is string) && (right is string))
            {
                return (string.Compare(left.ToString(), right.ToString()) == 0);
            }
            if (left.GetType() == right.GetType())
            {
                return right.Equals(left);
            }
            var convertible = left as IConvertible;
            if (convertible != null)
            {
                try
                {
                    return convertible.ToType(right.GetType(), null).Equals(right);
                }
                catch (Exception) {}
            }
            return left.ToString().Equals(right.ToString());
        }

        private void AssertIsValidArray(object instance, string property, int index)
        {
            Type type = instance.GetType();
            var list = instance as IList;
            bool flag = false;
            if ((list == null) && type.IsGenericType)
            {
                Type[] genericArguments = type.GetGenericArguments();
                Type type2 = typeof (IList<>).MakeGenericType(genericArguments);
                Type c = type.GetGenericTypeDefinition().MakeGenericType(genericArguments);
                flag = type2.IsAssignableFrom(c);
            }
            if (!flag && (list == null))
            {
                throw new RailsException(
                    "The property {0} is being accessed as an indexed property but does not seem to implement IList. In fact the type is {1}",
                    new object[] {property, type.FullName});
            }
            if (index < 0)
            {
                throw new RailsException("The specified index '{0}' is outside the bounds of the array. Property {1}",
                                         new object[] {index, property});
            }
        }

        public string Button(string value)
        {
            return this.Button(value, null);
        }

        public string Button(string value, IDictionary attributes)
        {
            return this.CreateInputElement("button", value, attributes);
        }

        public string ButtonElement(string innerText)
        {
            return this.ButtonElement(innerText, "submit", null);
        }

        public string ButtonElement(string innerText, string type)
        {
            return this.ButtonElement(innerText, type, null);
        }

        public string ButtonElement(string innerText, string type, IDictionary attributes)
        {
            return string.Format("<button type=\"{0}\" {1}>{2}</button>", type, base.GetAttributes(attributes),
                                 innerText);
        }

        private IValidator[] CollectValidators(RequestContext requestContext, string target)
        {
            var validators = new List<IValidator>();
            this.ObtainTargetProperty(requestContext, target,
                                      delegate(PropertyInfo property)
                                      {
                                          validators.AddRange(Controller.Validator.GetValidators(
                                                                  property.DeclaringType, property));
                                      });
            return validators.ToArray();
        }

        private static string CreateHtmlId(string name)
        {
            var builder = new StringBuilder(name.Length);
            bool flag = false;
            foreach (char ch in name)
            {
                switch (ch)
                {
                    case '[':
                    case ']':
                    case '.':
                        if (flag)
                        {
                            builder.Append('_');
                            flag = false;
                        }
                        break;

                    default:
                        flag = true;
                        builder.Append(ch);
                        break;
                }
            }
            return builder.ToString();
        }

        protected static string CreateHtmlId(IDictionary attributes, string target)
        {
            return CreateHtmlId(attributes, target, true);
        }

        protected static string CreateHtmlId(IDictionary attributes, string target, bool removeEntry)
        {
            string str;
            if (removeEntry)
            {
                str = CommonUtils.ObtainEntryAndRemove(attributes, "id");
            }
            else
            {
                str = CommonUtils.ObtainEntry(attributes, "id");
            }
            if (str == null)
            {
                str = CreateHtmlId(target);
            }
            return str;
        }

        public CheckboxList CreateCheckboxList(string target, IEnumerable dataSource)
        {
            return this.CreateCheckboxList(target, dataSource, null);
        }

        public CheckboxList CreateCheckboxList(string target, IEnumerable dataSource, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            return new CheckboxList(this, target, this.ObtainValue(target), dataSource, attributes);
        }

        protected virtual string CreateInputElement(string type, string value, IDictionary attributes)
        {
            return string.Format("<input type=\"{0}\" value=\"{1}\" {2}/>", type, FormatIfNecessary(value, attributes),
                                 base.GetAttributes(attributes));
        }

        protected virtual string CreateInputElement(string type, string target, object value, IDictionary attributes)
        {
            if (value == null)
            {
                value = CommonUtils.ObtainEntryAndRemove(attributes, "defaultValue");
            }
            string id = CreateHtmlId(attributes, target);
            return this.CreateInputElement(type, id, target, FormatIfNecessary(value, attributes), attributes);
        }

        protected virtual string CreateInputElement(string type, string id, string target, string value,
                                                    IDictionary attributes)
        {
            value = FormatIfNecessary(value, attributes);
            value = this.SafeHtmlEncode(value);
            if ((attributes != null) && attributes.Contains("mask"))
            {
                string str = CommonUtils.ObtainEntryAndRemove(attributes, "mask");
                string str2 = CommonUtils.ObtainEntryAndRemove(attributes, "mask_separator", "-");
                string str3 = CommonUtils.ObtainEntryAndRemove(attributes, "onBlur", "void(0)");
                string str4 = CommonUtils.ObtainEntryAndRemove(attributes, "onKeyUp", "void(0)");
                string str5 = "return monorail_formhelper_mask(event,this,'" + str + "','" + str2 + "');";
                attributes["onBlur"] = "javascript:" + str3 + ";" + str5;
                attributes["onKeyUp"] = "javascript:" + str4 + ";" + str5;
            }
            return string.Format("<input type=\"{0}\" id=\"{1}\" name=\"{2}\" value=\"{3}\" {4}/>",
                                 new object[] {type, id, target, value, base.GetAttributes(attributes)});
        }

        public void DisableValidation()
        {
            this.isValidationDisabled = true;
        }

        public string EndFormTag()
        {
            string str = string.Empty;
            if (this.validationConfig != null)
            {
                str = this.IsValidationEnabled
                          ? this.validationConfig.CreateBeforeFormClosed(this.currentFormId)
                          : string.Empty;
            }
            return (str + "</form>");
        }

        public static Pair<int, string>[] EnumToPairs(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumType must be an Enum", "enumType");
            }
            Array values = Enum.GetValues(enumType);
            string[] names = Enum.GetNames(enumType);
            var list = new List<Pair<int, string>>();
            int num = 0;
            foreach (string str in names)
            {
                int first = Convert.ToInt32(values.GetValue(num++));
                list.Add(new Pair<int, string>(first, TextHelper.PascalCaseToWord(str)));
            }
            return list.ToArray();
        }

        public string FileField(string target)
        {
            return this.FileField(target, null);
        }

        public string FileField(string target, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return this.CreateInputElement("file", target, string.Empty, attributes);
        }

        public string FilteredTextField(string target, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(target);
            attributes = (attributes != null) ? attributes : new Hashtable();
            ApplyFilterOptions(attributes);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return this.CreateInputElement("text", target, obj2, attributes);
        }

        protected static string FormatIfNecessary(object value, IDictionary attributes)
        {
            string format = CommonUtils.ObtainEntryAndRemove(attributes, "textformat");
            if ((value != null) && (format != null))
            {
                var formattable = value as IFormattable;
                if (formattable != null)
                {
                    value = formattable.ToString(format, null);
                }
            }
            else if (value == null)
            {
                value = string.Empty;
            }
            return value.ToString();
        }

        public string FormTag(IDictionary parameters)
        {
            string url = null;
            if (CommonUtils.ObtainEntryAndRemove(parameters, "noaction", "false") == "false")
            {
                url = base.UrlHelper.For(parameters);
            }
            return this.FormTag(url, parameters);
        }

        public string FormTag(string url, IDictionary parameters)
        {
            string str3;
            string str = CommonUtils.ObtainEntryAndRemove(parameters, "method", "post");
            this.currentFormId = CommonUtils.ObtainEntryAndRemove(parameters, "id", "form" + ++this.formCount);
            this.validationConfig = this.validatorProvider.CreateConfiguration(parameters);
            string str2 = this.IsValidationEnabled
                              ? this.validationConfig.CreateAfterFormOpened(this.currentFormId)
                              : string.Empty;
            if (url != null)
            {
                str3 = "<form action='" + url + "' method='" + str + "' id='" + this.currentFormId + "' "
                       + base.GetAttributes(parameters) + ">";
            }
            else
            {
                str3 = "<form method='" + str + "' id='" + this.currentFormId + "' " + base.GetAttributes(parameters)
                       + ">";
            }
            return (str3 + str2);
        }

        protected virtual string GenerateSelect(string target, object selectedValue, IEnumerable dataSource,
                                                IDictionary attributes)
        {
            string str = CreateHtmlId(target);
            this.ApplyValidation(InputElementType.Select, target, ref attributes);
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            var writer2 = new HtmlTextWriter(writer);
            string content = null;
            string str3 = null;
            string str4 = target;
            if (attributes != null)
            {
                content = CommonUtils.ObtainEntryAndRemove(attributes, "firstoption");
                str3 = CommonUtils.ObtainEntryAndRemove(attributes, "firstoptionvalue");
                if (attributes.Contains("name"))
                {
                    str4 = (string) attributes["name"];
                    attributes.Remove("name");
                }
                if (attributes.Contains("id"))
                {
                    str = (string) attributes["id"];
                    attributes.Remove("id");
                }
            }
            OperationState state = SetOperation.IterateOnDataSource(selectedValue, dataSource, attributes);
            writer2.WriteBeginTag("select");
            writer2.WriteAttribute("id", str);
            writer2.WriteAttribute("name", str4);
            writer2.Write(" ");
            writer2.Write(base.GetAttributes(attributes));
            writer2.Write('>');
            writer2.WriteLine();
            if (content != null)
            {
                writer2.WriteBeginTag("option");
                writer2.WriteAttribute("value", (str3 == null) ? "0" : this.SafeHtmlEncode(str3));
                writer2.Write('>');
                writer2.Write(this.SafeHtmlEncode(content));
                writer2.WriteEndTag("option");
                writer2.WriteLine();
            }
            foreach (SetItem item in state)
            {
                writer2.WriteBeginTag("option");
                if (item.IsSelected)
                {
                    writer2.Write(" selected=\"selected\"");
                }
                writer2.WriteAttribute("value", this.SafeHtmlEncode(item.Value));
                writer2.Write('>');
                writer2.Write(this.SafeHtmlEncode(item.Text));
                writer2.WriteEndTag("option");
                writer2.WriteLine();
            }
            writer2.WriteEndTag("select");
            return writer.ToString();
        }

        private static object GetArrayElement(object instance, int index)
        {
            var list = instance as IList;
            if (((list == null) && (instance != null)) && instance.GetType().IsGenericType)
            {
                Type type = instance.GetType();
                Type[] genericArguments = type.GetGenericArguments();
                Type type2 = type.GetGenericTypeDefinition().MakeGenericType(genericArguments);
                var num = (int) type2.GetProperty("Count").GetValue(instance, null);
                if ((num == 0) || ((index + 1) > num))
                {
                    return null;
                }
                return type2.GetProperty("Item").GetValue(instance, new object[] {index});
            }
            if (((list != null) && (list.Count != 0)) && ((index + 1) <= list.Count))
            {
                return list[index];
            }
            return null;
        }

        public string HiddenField(string target)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(target);
            return this.CreateInputElement("hidden", target, obj2, null);
        }

        public string HiddenField(string target, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(target);
            string id = CreateHtmlId(attributes, target);
            obj2 = (obj2 != null) ? obj2 : string.Empty;
            return this.CreateInputElement("hidden", id, target, obj2.ToString(), attributes);
        }

        public string HiddenField(string target, object value)
        {
            return this.CreateInputElement("hidden", target, value, null);
        }

        public string HiddenField(string target, object value, IDictionary attributes)
        {
            return this.CreateInputElement("hidden", target, value, attributes);
        }

        public string CheckboxField(string target)
        {
            return this.CheckboxField(target, null);
        }

        public string CheckboxField(string target, IDictionary attributes)
        {
            bool flag;
            target = this.RewriteTargetIfWithinObjectScope(target);
            object left = this.ObtainValue(target);
            string right = CommonUtils.ObtainEntryAndRemove(attributes, "trueValue", "true");
            if (right != "true")
            {
                flag = AreEqual(left, right);
            }
            else
            {
                flag = (((left != null) && (left is bool)) && ((bool) left)) || (!(left is bool) && (left != null));
            }
            if (flag)
            {
                if (attributes == null)
                {
                    attributes = new HybridDictionary(true);
                }
                AddChecked(attributes);
            }
            this.ApplyValidation(InputElementType.Checkbox, target, ref attributes);
            string id = CreateHtmlId(attributes, target);
            string str3 = id + "H";
            string str4 = CommonUtils.ObtainEntryAndRemove(attributes, "falseValue", "false");
            return (this.CreateInputElement("checkbox", id, target, right, attributes)
                    + this.CreateInputElement("hidden", str3, target, str4, null));
        }

        internal string CheckboxItem(int index, string target, string suffix, SetItem item, IDictionary attributes)
        {
            if (item.IsSelected)
            {
                AddChecked(attributes);
            }
            else
            {
                RemoveChecked(attributes);
            }
            target = string.Format("{0}[{1}]", target, index);
            string id = CreateHtmlId(attributes, target, true);
            string str2 = target;
            if ((suffix != null) && (suffix != string.Empty))
            {
                str2 = str2 + "." + suffix;
            }
            return this.CreateInputElement("checkbox", id, str2, item.Value, attributes);
        }

        private static bool CheckForExistenceAndExtractIndex(ref string property, out int index)
        {
            bool flag = property.IndexOf('[') != -1;
            index = -1;
            if (flag)
            {
                int startIndex = property.IndexOf('[') + 1;
                int length = property.IndexOf(']', startIndex) - startIndex;
                string str = property.Substring(startIndex, length);
                try
                {
                    index = Convert.ToInt32(str);
                }
                catch (Exception)
                {
                    throw new RailsException("Could not convert (param {0}) index to Int32. Value is {1}",
                                             new object[] {property, str});
                }
                property = property.Substring(0, startIndex - 1);
            }
            return flag;
        }

        public string InstallScripts()
        {
            return base.RenderScriptBlockToSource("/MonoRail/Files/FormHelperScript");
        }

        protected internal static bool IsPresent(object value, object initialSetValue, ValueGetter propertyOnInitialSet,
                                                 bool isMultiple)
        {
            if (!isMultiple)
            {
                object right = initialSetValue;
                if (propertyOnInitialSet != null)
                {
                    right = propertyOnInitialSet.GetValue(initialSetValue);
                }
                return AreEqual(value, right);
            }
            foreach (object obj3 in (IEnumerable) initialSetValue)
            {
                object obj4 = obj3;
                if (propertyOnInitialSet != null)
                {
                    obj4 = propertyOnInitialSet.GetValue(obj3);
                }
                if (AreEqual(value, obj4))
                {
                    return true;
                }
            }
            return false;
        }

        public string LabelFor(string target, string label)
        {
            return this.LabelFor(target, label, null);
        }

        public string LabelFor(string target, string label, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            string str = CreateHtmlId(attributes, target);
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            var writer2 = new HtmlTextWriter(writer);
            writer2.WriteBeginTag("label");
            writer2.WriteAttribute("for", str);
            string str2 = base.GetAttributes(attributes);
            if (str2 != string.Empty)
            {
                writer2.Write(' ');
            }
            writer2.Write(str2);
            writer2.Write('>');
            writer2.Write(label);
            writer2.WriteEndTag("label");
            return writer.ToString();
        }

        public string NumberField(string target)
        {
            return this.NumberField(target, null);
        }

        public string NumberField(string target, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(target);
            attributes = (attributes != null) ? attributes : new Hashtable();
            ApplyNumberOnlyOptions(attributes);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return this.CreateInputElement("text", target, obj2, attributes);
        }

        public string NumberFieldValue(string target, object value)
        {
            return this.NumberFieldValue(target, value, null);
        }

        public string NumberFieldValue(string target, object value, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            attributes = attributes ?? new Hashtable();
            ApplyNumberOnlyOptions(attributes);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return this.CreateInputElement("text", target, value, attributes);
        }

        protected object ObtainRootInstance(RequestContext context, string target)
        {
            object obj2 = null;
            if ((context == RequestContext.All) || (context == RequestContext.PropertyBag))
            {
                obj2 = base.Controller.PropertyBag[target];
            }
            if ((obj2 == null) && ((context == RequestContext.All) || (context == RequestContext.Flash)))
            {
                obj2 = base.Controller.Context.Flash[target];
            }
            if ((obj2 == null) && ((context == RequestContext.All) || (context == RequestContext.Session)))
            {
                obj2 = base.Controller.Context.Session[target];
            }
            if ((obj2 == null) && ((context == RequestContext.All) || (context == RequestContext.Params)))
            {
                obj2 = base.Controller.Params[target];
            }
            if ((obj2 != null) || ((context != RequestContext.All) && (context != RequestContext.Request)))
            {
                return obj2;
            }
            return base.Controller.Context.Items[target];
        }

        protected object ObtainRootInstance(RequestContext context, string target, out string[] pieces)
        {
            int num;
            pieces = target.Split(new[] {'.'});
            string property = pieces[0];
            bool flag = CheckForExistenceAndExtractIndex(ref property, out num);
            object instance = this.ObtainRootInstance(context, property);
            if (instance == null)
            {
                return null;
            }
            if (flag)
            {
                this.AssertIsValidArray(instance, property, num);
            }
            if ((flag || (pieces.Length != 1)) && flag)
            {
                instance = GetArrayElement(instance, num);
            }
            return instance;
        }

        private Type ObtainRootType(RequestContext context, string target, out string[] pieces)
        {
            pieces = target.Split(new[] {'.'});
            var type = (Type) base.Controller.PropertyBag[pieces[0] + "type"];
            if (type == null)
            {
                object obj2 = this.ObtainRootInstance(context, target, out pieces);
                if (obj2 != null)
                {
                    type = obj2.GetType();
                }
            }
            return type;
        }

        protected PropertyInfo ObtainTargetProperty(RequestContext context, string target, Action<PropertyInfo> action)
        {
            string[] strArray;
            Type type = this.ObtainRootType(context, target, out strArray);
            if ((type != null) && (strArray.Length > 1))
            {
                return this.QueryPropertyInfoRecursive(type, strArray, 1, action);
            }
            return null;
        }

        protected object ObtainValue(string target)
        {
            return this.ObtainValue(RequestContext.All, target);
        }

        protected object ObtainValue(RequestContext context, string target)
        {
            string[] strArray;
            object rootInstance = this.ObtainRootInstance(context, target, out strArray);
            if ((rootInstance != null) && (strArray.Length > 1))
            {
                return this.QueryPropertyRecursive(rootInstance, strArray, 1);
            }
            return rootInstance;
        }

        public string PasswordField(string target)
        {
            return this.PasswordField(target, null);
        }

        public string PasswordField(string target, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(target);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return this.CreateInputElement("password", target, obj2, attributes);
        }

        public string PasswordNumberField(string target)
        {
            return this.PasswordNumberField(target, null);
        }

        public string PasswordNumberField(string target, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(target);
            attributes = (attributes != null) ? attributes : new Hashtable();
            ApplyNumberOnlyOptions(attributes);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return this.CreateInputElement("password", target, obj2, attributes);
        }

        public void Pop()
        {
            this.objectStack.Pop();
        }

        public void Push(string target)
        {
            this.Push(target, null);
        }

        public void Push(string target, IDictionary parameters)
        {
            string str = CommonUtils.ObtainEntryAndRemove(parameters, "disablevalidation", "false");
            if (this.ObtainValue(target) != null)
            {
                this.objectStack.Push(new FormScopeInfo(target, str != "true"));
            }
            else
            {
                if (this.ObtainValue(target + "type") == null)
                {
                    throw new ArgumentException("target could not be evaluated during Push operation. Target: " + target);
                }
                this.objectStack.Push(new FormScopeInfo(target, str != "true"));
            }
        }

        private PropertyInfo QueryPropertyInfoRecursive(Type type, string[] propertyPath, int piece,
                                                        Action<PropertyInfo> action)
        {
            int num;
            string property = propertyPath[piece];
            bool flag = CheckForExistenceAndExtractIndex(ref property, out num);
            PropertyInfo info = type.GetProperty(property, ResolveFlagsToUse(type));
            if (info == null)
            {
                if (this.logger.IsErrorEnabled)
                {
                    this.logger.Error("No public property '{0}' found on type '{1}'",
                                      new object[] {property, type.FullName});
                }
                return null;
            }
            if (!info.CanRead)
            {
                throw new BindingException("Property '{0}' for type '{1}' can not be read",
                                           new object[] {info.Name, type.FullName});
            }
            if (info.GetIndexParameters().Length != 0)
            {
                throw new BindingException("Property '{0}' for type '{1}' has indexes, which are not supported",
                                           new object[] {info.Name, type.FullName});
            }
            if (action != null)
            {
                action(info);
            }
            type = info.PropertyType;
            if (typeof (ICollection).IsAssignableFrom(type))
            {
                return null;
            }
            if (flag)
            {
                if (type.IsGenericType)
                {
                    Type[] genericArguments = type.GetGenericArguments();
                    if (genericArguments.Length != 1)
                    {
                        throw new BindingException("Expected the generic indexed property '{0}' to be of 1 element",
                                                   new object[] {type.Name});
                    }
                    type = genericArguments[0];
                }
                if (type.IsArray)
                {
                    type = type.GetElementType();
                }
            }
            if ((piece + 1) == propertyPath.Length)
            {
                return info;
            }
            return this.QueryPropertyInfoRecursive(type, propertyPath, piece + 1, action);
        }

        protected object QueryPropertyRecursive(object rootInstance, string[] propertyPath, int piece)
        {
            int num;
            string property = propertyPath[piece];
            Type type = rootInstance.GetType();
            bool flag = CheckForExistenceAndExtractIndex(ref property, out num);
            PropertyInfo info = type.GetProperty(property, ResolveFlagsToUse(type));
            object instance = null;
            if (info == null)
            {
                FieldInfo field = type.GetField(property, FieldFlags);
                if (field != null)
                {
                    instance = field.GetValue(rootInstance);
                }
            }
            else
            {
                if (!info.CanRead)
                {
                    throw new BindingException("Property '{0}' for type '{1}' can not be read",
                                               new object[] {info.Name, type.FullName});
                }
                if (info.GetIndexParameters().Length != 0)
                {
                    throw new BindingException("Property '{0}' for type '{1}' has indexes, which are not supported",
                                               new object[] {info.Name, type.FullName});
                }
                instance = info.GetValue(rootInstance, null);
            }
            if (flag && (instance != null))
            {
                this.AssertIsValidArray(instance, property, num);
                instance = GetArrayElement(instance, num);
            }
            if ((instance != null) && ((piece + 1) != propertyPath.Length))
            {
                return this.QueryPropertyRecursive(instance, propertyPath, piece + 1);
            }
            return instance;
        }

        public string RadioField(string target, object valueToSend)
        {
            return this.RadioField(target, valueToSend, null);
        }

        public string RadioField(string target, object valueToSend, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object right = this.ObtainValue(target);
            if (AreEqual(valueToSend, right))
            {
                if (attributes == null)
                {
                    attributes = new HybridDictionary(true);
                }
                AddChecked(attributes);
            }
            return this.CreateInputElement("radio", target, valueToSend, attributes);
        }

        private static void RemoveChecked(IDictionary attributes)
        {
            attributes.Remove("checked");
        }

        private static BindingFlags ResolveFlagsToUse(Type type)
        {
            if (!type.Assembly.FullName.StartsWith("DynamicAssemblyProxyGen")
                && !type.Assembly.FullName.StartsWith("DynamicProxyGenAssembly2"))
            {
                return PropertyFlags;
            }
            return PropertyFlags2;
        }

        protected string RewriteTargetIfWithinObjectScope(string target)
        {
            if (this.objectStack.Count == 0)
            {
                return target;
            }
            return (((FormScopeInfo) this.objectStack.Peek()).RootTarget + "." + target);
        }

        private string SafeHtmlEncode(string content)
        {
            if (base.Controller.Context != null)
            {
                return HtmlEncode(content);
            }
            return content;
        }

        public string Select(string target, IEnumerable dataSource)
        {
            return this.Select(target, dataSource, null);
        }

        public string Select(string target, IEnumerable dataSource, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object selectedValue = this.ObtainValue(target);
            return this.Select(target, selectedValue, dataSource, attributes);
        }

        public string Select(string target, object selectedValue, IEnumerable dataSource, IDictionary attributes)
        {
            return this.GenerateSelect(target, selectedValue, dataSource, attributes);
        }

        public string Submit(string value)
        {
            return this.Submit(value, null);
        }

        public string Submit(string value, IDictionary attributes)
        {
            return this.CreateInputElement("submit", value, attributes);
        }

        public string TextArea(string target)
        {
            return this.TextArea(target, null);
        }

        public string TextArea(string target, IDictionary attributes)
        {
            return this.TextArea(target, attributes, false);
        }

        public string TextArea(string target, IDictionary attributes, bool ensureParagraph)
        {
            string str = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(str);
            if (ensureParagraph)
            {
                if (obj2 == null || String.IsNullOrEmpty(obj2.ToString()))
                {
                    obj2 = "<p></p>";
                }
                obj2 = this.EnsureParagraphOverContent(obj2.ToString());
            }
            return this.TextAreaValue(target, obj2, attributes);
        }

        public string EnsureParagraphOverContent(string content)
        {
            if (!content.StartsWith("<"))
            {
                return ("<p>" + content + "</p>");
            }
            return content;
        }

        public string TextAreaValue(string target, object value, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            value = (value == null) ? "" : HtmlEncode(value.ToString());
            string str = CreateHtmlId(target);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return string.Format("<textarea id=\"{0}\" name=\"{1}\" {2}>{3}</textarea>",
                                 new object[]
                                 {str, target, base.GetAttributes(attributes), FormatIfNecessary(value, attributes)});
        }

        public string TextField(string target)
        {
            return this.TextField(target, null);
        }

        public string TextField(string target, IDictionary attributes)
        {
            target = this.RewriteTargetIfWithinObjectScope(target);
            object obj2 = this.ObtainValue(target);
            this.ApplyValidation(InputElementType.Text, target, ref attributes);
            return this.CreateInputElement("text", target, obj2, attributes);
        }

        public string TextFieldValue(string target, object value)
        {
            return this.TextFieldValue(target, value, null);
        }

        public string TextFieldValue(string target, object value, IDictionary attributes)
        {
            return this.CreateInputElement("text", target, value, attributes);
        }

        public void UsefValidate()
        {
            this.UseWebValidatorProvider(new FValidateWebValidator());
        }

        public void UsePrototypeValidation()
        {
            this.UseWebValidatorProvider(new PrototypeWebValidator());
        }

        public void UseWebValidatorProvider(IBrowserValidatorProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            this.validatorProvider = provider;
        }

        public void UseZebdaValidation()
        {
            this.UseWebValidatorProvider(new ZebdaWebValidator());
        }

        #region Nested type: CheckboxList

        public sealed class CheckboxList : IEnumerable, IEnumerator
        {
            private readonly IDictionary attributes;
            private readonly IEnumerator enumerator;
            private readonly NexusFormHelper helper;
            private readonly OperationState operationState;
            private readonly string target;
            private bool hasItem;
            private bool hasMovedNext;
            private int index = -1;

            public CheckboxList(NexusFormHelper helper, string target, object initialSelectionSet,
                                IEnumerable dataSource, IDictionary attributes)
            {
                if (dataSource == null)
                {
                    throw new ArgumentNullException("dataSource");
                }
                this.helper = helper;
                this.target = target;
                this.attributes = attributes ?? new HybridDictionary(true);
                this.operationState = SetOperation.IterateOnDataSource(initialSelectionSet, dataSource, attributes);
                this.enumerator = this.operationState.GetEnumerator();
            }

            public SetItem CurrentSetItem
            {
                get { return (this.enumerator.Current as SetItem); }
            }

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return this;
            }

            #endregion

            #region IEnumerator Members

            public bool MoveNext()
            {
                this.hasMovedNext = true;
                this.hasItem = this.enumerator.MoveNext();
                if (this.hasItem)
                {
                    this.index++;
                }
                return this.hasItem;
            }

            public void Reset()
            {
                this.index = -1;
                this.enumerator.Reset();
            }

            public object Current
            {
                get { return this.CurrentSetItem.Item; }
            }

            #endregion

            public string Item()
            {
                return this.Item(null);
            }

            public string Item(string id)
            {
                if (!this.hasMovedNext)
                {
                    throw new InvalidOperationException("Before rendering a checkbox item, you must use MoveNext");
                }
                if (!this.hasItem)
                {
                    return string.Empty;
                }
                if (id != null)
                {
                    this.attributes["id"] = id;
                }
                return this.helper.CheckboxItem(this.index, this.target, this.operationState.TargetSuffix,
                                                this.CurrentSetItem, this.attributes);
            }
        }

        #endregion

        #region Nested type: DataRowValueGetter

        public class DataRowValueGetter : ValueGetter
        {
            private readonly string columnName;

            public DataRowValueGetter(string columnName)
            {
                this.columnName = columnName;
            }

            public override string Name
            {
                get { return this.columnName; }
            }

            public override object GetValue(object instance)
            {
                var row = (DataRow) instance;
                return row[this.columnName];
            }
        }

        #endregion

        #region Nested type: DataRowViewValueGetter

        public class DataRowViewValueGetter : ValueGetter
        {
            private readonly string columnName;

            public DataRowViewValueGetter(string columnName)
            {
                this.columnName = columnName;
            }

            public override string Name
            {
                get { return this.columnName; }
            }

            public override object GetValue(object instance)
            {
                var view = (DataRowView) instance;
                return view[this.columnName];
            }
        }

        #endregion

        #region Nested type: EnumValueGetter

        public class EnumValueGetter : ValueGetter
        {
            private readonly Type enumType;

            public EnumValueGetter(Type enumType)
            {
                this.enumType = enumType;
            }

            public override string Name
            {
                get { return string.Empty; }
            }

            public override object GetValue(object instance)
            {
                return Enum.Format(this.enumType, Enum.Parse(this.enumType, Convert.ToString(instance)), "d");
            }
        }

        #endregion

        #region Nested type: FormScopeInfo

        private class FormScopeInfo
        {
            private readonly bool isValidationEnabled;
            private readonly string target;

            public FormScopeInfo(string target, bool isValidationEnabled)
            {
                this.target = target;
                this.isValidationEnabled = isValidationEnabled;
            }

            public bool IsValidationEnabled
            {
                get { return this.isValidationEnabled; }
            }

            public string RootTarget
            {
                get { return this.target; }
            }
        }

        #endregion

        #region Nested type: NoActionGetter

        public class NoActionGetter : ValueGetter
        {
            public override string Name
            {
                get { return string.Empty; }
            }

            public override object GetValue(object instance)
            {
                return null;
            }
        }

        #endregion

        #region Nested type: ReflectionValueGetter

        public class ReflectionValueGetter : ValueGetter
        {
            private readonly PropertyInfo propInfo;

            public ReflectionValueGetter(PropertyInfo propInfo)
            {
                this.propInfo = propInfo;
            }

            public override string Name
            {
                get { return this.propInfo.Name; }
            }

            public override object GetValue(object instance)
            {
                try
                {
                    return this.propInfo.GetValue(instance, null);
                }
                catch (TargetException)
                {
                    PropertyInfo property = instance.GetType().GetProperty(this.Name);
                    if (property == null)
                    {
                        throw;
                    }
                    return property.GetValue(instance, null);
                }
            }
        }

        #endregion

        #region Nested type: ValueGetter

        public abstract class ValueGetter
        {
            public abstract string Name { get; }
            public abstract object GetValue(object instance);
        }

        #endregion

        #region Nested type: ValueGetterAbstractFactory

        public class ValueGetterAbstractFactory
        {
            public static ValueGetter Create(Type targetType, string keyName)
            {
                if (targetType == null)
                {
                    return new NoActionGetter();
                }
                if (targetType == typeof (DataRow))
                {
                    return new DataRowValueGetter(keyName);
                }
                if (targetType == typeof (DataRowView))
                {
                    return new DataRowViewValueGetter(keyName);
                }
                if (targetType.IsEnum)
                {
                    return new EnumValueGetter(targetType);
                }
                PropertyInfo property = targetType.GetProperty(keyName, ResolveFlagsToUse(targetType));
                if (property != null)
                {
                    return new ReflectionValueGetter(property);
                }
                return null;
            }
        }

        #endregion
    }
}