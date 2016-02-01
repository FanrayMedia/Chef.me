using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Chef.Logging
{
    /// <summary>
    /// Logger based on NLog for when running outside of Azure environment.
    /// </summary>
    /// <remarks>
    /// NLog <see cref="https://github.com/nlog/nlog/wiki/Tutorial"/>
    /// </remarks>
    public class NLogProvider : ILogProvider
    {
        #region Constructor

        private Logger _logger;

        /// <summary>
        /// Initialize logger, only called on creation.
        /// </summary>
        public NLogProvider()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params Object[] args)
        {
            _logger.Info(message, args);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        public void Warn(string message, int eventID = 0)
        {
            _logger.Warn(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Warn(string message, params Object[] args)
        {
            _logger.Warn(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        public void Warn(string message, int eventID, params Object[] args)
        {
            _logger.Warn(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        public void Warn(Exception x, int eventID = 0)
        {
            _logger.Warn(Log.BuildExceptionLogMessage(x));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        public void Error(string message, int eventID = 0)
        {
            _logger.Error(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        public void Error(string message, int eventID, params object[] args)
        {
            _logger.Error(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        public void Error(Exception x, int eventID = 0)
        {
            _logger.Error(Log.BuildExceptionLogMessage(x));
        }

        


        //public void Debug(string message)
        //{
        //    _logger.Debug(message);
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        public void Fatal(string message, int eventID = 0)
        {
            _logger.Fatal(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="args"></param>
        public void Fatal(string message, int eventID, params object[] args)
        {
            _logger.Fatal(message, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="eventID"></param>
        public void Fatal(Exception x, int eventID = 0)
        {
            _logger.Fatal(Log.BuildExceptionLogMessage(x));
        }


        /// <summary>
        /// Flush is necessary for TraceSource impl, not used here
        /// </summary>
        public void Flush()
        {
        }

        /// <summary>
        /// see Flush
        /// </summary>
        public void Close()
        {
        }
    }
}
