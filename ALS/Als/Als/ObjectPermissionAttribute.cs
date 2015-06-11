using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Windows;

namespace Als
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Assembly,
        AllowMultiple = true, Inherited = false)]
    [Serializable]
    sealed public class ObjectPermissionAttribute : CodeAccessSecurityAttribute
    {
        private String m_Name = null;
        private bool m_unrestricted = false;
        private ObjectPermissionAccess permAccess;
        private ObjectPermissionAccess currentPermAccess;
        public ObjectPermissionAttribute(SecurityAction action)
            : base(action)
        {
            currentPermAccess = ((App)Application.Current).CurrentUserInfo.PermissionAccess;
        }

        public String NameOfObject
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public ObjectPermissionAccess RequiredPermissionAccess
        {
            set { permAccess = value; }
            get { return permAccess; }
        }

        public override IPermission CreatePermission()
        {           
            if (m_unrestricted)
            {
                throw new ArgumentException("Unrestricted permissions not allowed in identity permissions.");
            }
            else
            {
                return new ObjectPermission(m_Name, permAccess, currentPermAccess);
            }
        }
    }
}
