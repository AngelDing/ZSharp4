using System;
using System.Runtime.Serialization;

namespace ZSharp.Framework
{
    /// <summary>
    /// This exception is thrown if a problem on ABP initialization progress.
    /// </summary>
    [Serializable]
    public class ZSharpInitializationException : ZSharpException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ZSharpInitializationException()
        {
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public ZSharpInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public ZSharpInitializationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ZSharpInitializationException(string message, ZSharpException innerException)
            : base(message, innerException)
        {
        }
    }
}
