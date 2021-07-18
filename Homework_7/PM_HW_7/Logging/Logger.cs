using System;
using System.Diagnostics;

namespace PM_HW_7.Logging
{
    public class Logger : ILogger
    {
        /// <summary>
        /// Method to Log standard messages
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            Debug.Write($"Logging :::::: {message}");
        }

        /// <summary>
        /// Method to Log expetions
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        public void Log(Exception exception, string message)
        {
            Debug.Write($"Error occured! Message:{message}\n\nException: {exception}");
        }
    }
}