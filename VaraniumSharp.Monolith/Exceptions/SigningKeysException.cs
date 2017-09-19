using System;

namespace VaraniumSharp.Monolith.Exceptions
{
    /// <summary>
    /// Exception class to use when a SigningKey issue occurs
    /// </summary>
    public class SigningKeysException : Exception
    {
        #region Constructor

        /// <summary>
        /// Construct with a message explaining the exception that occured
        /// </summary>
        /// <param name="message">Message explaining the issue</param>
        public SigningKeysException(string message)
            : base(message)
        { }

        /// <summary>
        /// Construct with a message and an inner exception containing details about when went wrong
        /// </summary>
        /// <param name="message">Message explaining the issue</param>
        /// <param name="innerException">Inner exception that caused the exception</param>
        public SigningKeysException(string message, Exception innerException)
            : base(message, innerException)
        { }

        #endregion
    }
}