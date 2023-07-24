using System;
using System.Text;
using System.Web;
using System.Reflection;
using System.IO;
using System.Collections;

namespace ExclusiveReality.Helpers
{
    public static class Logger
    {
        private static bool toHttpResponse = false;
        private static LogingState state = LogingState.Errors;

        public static void EnterFunction(MethodBase method, params object[] pars)
        {
            if (state == LogingState.All || state == LogingState.Entering)
            {
                var sb = new StringBuilder();
                sb.Append(method.DeclaringType.Name + "." + method.Name + "(");
                var parsInfo = method.GetParameters();
                for (int x = 0; x < parsInfo.Length; x++)
                {
                    if (pars.Length > x)
                    {
                        if (x != 0)
                            sb.Append(", ");
                        sb.Append(parsInfo[x].ParameterType);
                        sb.Append(" ");
                        sb.Append(parsInfo[x].Name);
                        sb.Append("=");
                        if (pars[x] is ICollection) // vypis polozek případného pole
                        {
                            try
                            {
                                var arr = pars[x] as ICollection;
                                if (arr != null)
                                {
                                    sb.Append("{");
                                    int index = 0;
                                    foreach (object item in arr)
                                    {
                                        sb.Append(item);
                                        if ((index + 1) < arr.Count)
                                            sb.Append(",");
                                        index++;
                                    }
                                    sb.Append("}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Error(MethodBase.GetCurrentMethod(), ex);
                            }
                        }
                        else
                            sb.Append(pars[x]);
                    }
                }
                sb.Append(")");

                Write(method, LogMessagetype.Enter, sb.ToString());
            }
        }
        public static void Diagnostic(MethodBase method, object message)
        {
            if (state == LogingState.All || state == LogingState.Diagnosting || state == LogingState.Debuging)
                Write(method, LogMessagetype.Diagnostic, message);
        }
        public static void Debug(MethodBase method, object message)
        {
            if (state == LogingState.All || state == LogingState.Debuging)
                Write(method, LogMessagetype.Debug, message);
        }
        public static void Error(MethodBase method, object message)
        {
            Write(method, LogMessagetype.Error, message);
        }

        private static void Write(MethodBase method, LogMessagetype logType, object message)
        {
            string logFile = HttpContext.Current.Server.MapPath("/logs/" + method.DeclaringType.Assembly.FullName.Split(new[] {','})[0] + ".log");
            try
            {
                File.AppendAllText(logFile, string.Format("{0}		{1}			{2}				{3}{4}", DateTime.Now.ToString("HH:mm:ss:fff"), logType,
                                                 method.DeclaringType.Name, message, Environment.NewLine), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (toHttpResponse)
                HttpContext.Current.Response.Write(DateTime.Now.ToUniversalTime() + "	" + logType + "	" + message + "<br />" + Environment.NewLine);
        }



        public enum LogingState { All, Entering, Diagnosting, Debuging, Errors }
        public enum LogMessagetype { Debug, Diagnostic, Enter, Error }
    }
}
