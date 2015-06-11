using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Als
{
    [Serializable]    
    public class ObjectAccessDeniedException : Exception
    {
        public ObjectAccessDeniedException() { }
        public ObjectAccessDeniedException(string message) : base(message) { }
        public ObjectAccessDeniedException(string message, Exception inner) : base(message, inner) { }
        protected ObjectAccessDeniedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
        public string AccessObject
        { get; set; }
        public string UserName
        { get { return ""; } }
        public ObjectPermissionAccess RequiredPermissionLevel
        { get; set; }
        public ObjectPermissionAccess CurrentPermissionLevel
        { get; set; }
    }
}
