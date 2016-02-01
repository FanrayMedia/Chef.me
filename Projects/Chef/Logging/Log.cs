using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration.Provider;

namespace Chef.Logging
{
    /// <summary>
    /// The logger for our entire system.
    /// </summary>
    public static class Log
    {
        #region Construcotr

        private static object _lock = new object();
        public static ILogProvider Provider { get; set; }

        static Log()
        {
            if (Provider == null)
            {
                lock (_lock)
                {
                    if (Provider == null)
                    {
                        string providerString = "Chef.Logging.NLogProvider, Chef";
                        Provider = Activator.CreateInstance(Type.GetType(providerString)) as ILogProvider;
                        if (Provider == null) throw new ProviderException(string.Format("Failed to load '{0}'.", providerString));
                    }
                }
            }
        }

        #endregion

        // -------------------------------------------------------------------- Info

        /// <summary>
        /// Logs information messages.
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>
        /// Using this method indicates that the process is executing correctly, but there is some interesting information to log. 
        /// It may be information that a user has logged onto a system or that a new form in the application is opening. 
        /// These are normally enabled in production environment
        /// </remarks>
        public static void Info(string message)
        {
            Provider.Info(message);
        }
        /// <summary>
        /// Logs information messages with arguments.
        /// </summary>
        /// <param name="message">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the args array.</param>
        /// <param name="args">An array containing zero or more objects to format.</param>
        /// <remarks>
        /// Using this method indicates that the process is executing correctly, but there is some interesting information to log. 
        /// It may be information that a user has logged onto a system or that a new form in the application is opening. 
        /// These are normally enabled in production environment
        /// </remarks>
        public static void Info(string message, params Object[] args)
        {
            Provider.Info(message,args);
        }

        // -------------------------------------------------------------------- Warn

        /// <summary>
        /// Logs warning message with optional eventID.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID">default 0, this value is user-defined</param>
        /// <remarks>
        /// This value indicates something unusual has occurred that may be worth investigating further; although it does not necessarily indicate an error. 
        /// It is typically for non-critical issues, which can be recovered or which are temporary failures
        /// </remarks>
        public static void Warn(string message, int eventID = 0)
        {
            Provider.Warn(message, eventID);
        }
        /// <summary>
        /// Logs warning message with arguments.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Warn(string message, params Object[] args)
        {
            Provider.Warn(message, args);
        }
        /// <summary>
        /// Logs warning message with eventID and arguments.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        public static void Warn(string message, int eventID, params Object[] args)
        {
            Provider.Warn(message, eventID, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        public static void Warn(Exception x, int eventID = 0)
        {
            Provider.Warn(x, eventID);
        }

        // -------------------------------------------------------------------- Error

        /// <summary>
        /// Logs error message.
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>
        /// This enum member has a slightly lower priority than Critical, but it still indicates something wrong in the application. 
        /// It should typically be used to flag a problem that has been handled or recovered from. 
        /// </remarks>
        public static void Error(string message)
        {
            Provider.Error(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Error(string message, params object[] args)
        {
            Provider.Error(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        public static void Error(string message, int eventID, params object[] args)
        {
            Provider.Error(message, eventID, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        public static void Error(Exception x, int eventID = 0)
        {
            Provider.Error(x, eventID);
        }

        // -------------------------------------------------------------------- Fatal

        /// <summary>
        /// Logs critical message.
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>
        /// This is the most severe and should be used sparingly, only for very serious and irrecoverable errors. 
        /// </remarks>
        public static void Fatal(string message)
        {
            Provider.Fatal(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Fatal(string message, params object[] args)
        {
            Provider.Fatal(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        public static void Fatal(string message, int eventID, params object[] args)
        {
            Provider.Fatal(message, eventID, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        public static void Fatal(Exception x, int eventID = 0)
        {
            Provider.Fatal(x, eventID);
        }

        // -------------------------------------------------------------------- 

        /// <summary>
        /// 
        /// </summary>
        public static void Flush()
        {
            Provider.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Close()
        {
            Provider.Close();
        }

        // -------------------------------------------------------------------- helper

        /// <summary>
        /// Builds a message from an exception, if the inner exception exists it builds the message on the inner one instead.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string BuildExceptionLogMessage(Exception x)
        {
            Exception logException = x;
            if (x.InnerException != null)
                logException = x.InnerException;

            string strErrorMsg = "";// Environment.NewLine + "Error in Path : " + System.Web.HttpContext.Current.Request.Path;

            // Get the QueryString along with the Virtual Path
            //strErrorMsg += Environment.NewLine + "Raw Url : " + System.Web.HttpContext.Current.Request.RawUrl;


            // Get the error message
            strErrorMsg += Environment.NewLine + "Message : " + logException.Message;

            // Source of the message
            strErrorMsg += Environment.NewLine + "Source : " + logException.Source;

            // Method where the error occurred
            strErrorMsg += Environment.NewLine + "TargetSite : " + logException.TargetSite;

            // Stack Trace of the error
            strErrorMsg += Environment.NewLine + "Stack Trace : " + logException.StackTrace;

            return strErrorMsg;
        }

        /// <summary>
        /// Builds a message from an exception, if the inner exception exists it builds the message on the inner one instead.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string BuildExceptionLogMessageHtml(Exception x)
        {
            Exception logException = x;
            if (x.InnerException != null)
                logException = x.InnerException;

            string strErrorMsg = "<br />Error in Path : " + System.Web.HttpContext.Current.Request.Path;

            // Get the QueryString along with the Virtual Path
            strErrorMsg += "<br />Raw Url : " + System.Web.HttpContext.Current.Request.RawUrl;


            // Get the error message
            strErrorMsg += "<br />Message : " + logException.Message;

            // Source of the message
            strErrorMsg += "<br />Source : " + logException.Source;

            // Method where the error occurred
            strErrorMsg += "<br />TargetSite : " + logException.TargetSite;

            // Stack Trace of the error
            strErrorMsg += "<br />Stack Trace : <br />" + logException.StackTrace.Replace(Environment.NewLine, "<br />");

            //            return "<div style=\"color:red\">" + strErrorMsg + "</div>";
            return strErrorMsg;
        }
    }
}
