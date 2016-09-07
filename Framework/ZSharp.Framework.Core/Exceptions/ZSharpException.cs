using System;
using System.Runtime.Serialization;

namespace ZSharp.Framework
{
    /// <summary>
    /// Base exception type for those are thrown by Abp system for Abp specific exceptions.
    /// </summary>
    [Serializable]
    public class ZSharpException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        public ZSharpException()
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        public ZSharpException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public ZSharpException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ZSharpException(string message, ZSharpException innerException)
            : base(message, innerException)
        {
        }
    }
}
