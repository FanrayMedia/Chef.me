using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chef.Logging
{
    /// <summary>
    /// Log provider contract.
    /// </summary>
    /// <remarks>
    /// Our system can run in both Azure and non-Azure environment, former relies on System.Diagnostic.TraceSource and logs to WADLogTable,
    /// latter relies on NLog and logs to ~/Content/Site.log text file. 
    /// <br /><br />
    /// 
    /// From the TraceSource Primer <see cref=""/> and NLog Tutorial <see cref=""/>, I come up the following level of log methods:<br />
    /// •Critical. This is the most severe member of the enum. It should be used sparingly, only for very serious and irrecoverable errors. 
    /// •Error. This enum member has a slightly lower priority than Critical, but it still indicates something wrong in the application. It should typically be used to flag a problem that has been handled or recovered from. 
    /// •Warning. This value indicates something unusual has occurred that may be worth investigating further; although it does not necessarily indicate an error. 
    /// •Information: This value indicates that the process is executing correctly, but there is some interesting information to include in the tracing output file. It may be information that a user has logged onto a system or that a new form in the application is opening. 
    /// •Verbose: This is the loosest of all the severity related values in the enum. It should be used for information that is not indicating anything wrong with the application and is likely to appear in vast quantities. For example, when instrumenting all methods in a type to trace their beginning and ending, it is typical to use the verbose event type. 
    /// •Stop, Start, Suspend, Resume, Transfer: These event types are not indications of severity, but mark the trace event as relating to the logical flow of the application. They are known as activity event types and mark a logical operation’s starting or stopping, or transferring control to another logical operation. They are very useful event types but fall outside the scope of this already long blog. They will be discussed in more detailing in a future post.
    ///
    /// • Trace - very detailed logs, which may include high-volume information such as protocol payloads. This log level is typically only enabled during development (is this similar to verbose?)
    /// • Debug - debugging information, less detailed than trace, typically not enabled in production environment.
    /// • Info - information messages, which are normally enabled in production environment
    /// • Warn - warning messages, typically for non-critical issues, which can be recovered or which are temporary failures
    /// • Error - error messages 
    /// • Fatal - very serious errors 
    /// 
    /// The second parameter in the trace methods is the event ID number. This integer identifies the nature of the tracing message. 
    /// It is typical for an application to designate different event ids to mean different things. For example, the number 1 may mean 
    /// that this trace event marks the beginning of a method and the number 2 may indicate that the event marks the end of a method. 
    /// The event ID is just another way to differentiate tracing messages.f tracing information - something that is done a lot when 
    /// instrumenting an application with tracing.<br /> <br />
    /// 
    /// As mentioned above, there is also a TraceInformation method which takes a string as its only parameter. This method will simply 
    /// call TraceEvent with an event type of information and an event ID of 0. It's a shorthand way of tracing information - something 
    /// that is done a lot when instrumenting an application with tracing.
    /// </remarks>
    public interface ILogProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the args array.</param>
        /// <param name="args">An array containing zero or more objects to format.</param>
        void Info(string message, params Object[] args);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        void Warn(string message, int eventID = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Warn(string message, params Object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        void Warn(string message, int eventID, params Object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        void Warn(Exception x, int eventID = 0);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        void Error(string message, int eventID = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Error(string message, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        void Error(string message, int eventID, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        void Error(Exception x, int eventID = 0);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        void Fatal(string message, int eventID = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Fatal(string message, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        void Fatal(string message, int eventID, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        void Fatal(Exception x, int eventID = 0);


        //void Debug(string message);

        /// <summary>
        /// 
        /// </summary>
        void Flush();
        /// <summary>
        /// 
        /// </summary>
        void Close();
    }
}
