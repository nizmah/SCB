using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;

namespace Als
{
    

    [Serializable()]
    sealed public class ObjectPermission : CodeAccessPermission, IUnrestrictedPermission
    {
    
        private bool m_Unrestricted;
        private ObjectPermissionAccess reqPerm;
        private ObjectPermissionAccess currPerm;
        private string securableObjectName;
        public ObjectPermission(string objectName,ObjectPermissionAccess requirePermissionAccess, ObjectPermissionAccess currentPermissionAccess)
        {
            reqPerm = requirePermissionAccess;
            currPerm = currentPermissionAccess;
            securableObjectName = objectName;
            
            if (currentPermissionAccess < requirePermissionAccess)
            {
                ObjectAccessDeniedException exc = new ObjectAccessDeniedException("Access denied");
                exc.AccessObject = objectName;
                exc.CurrentPermissionLevel = currentPermissionAccess;
                exc.RequiredPermissionLevel = requirePermissionAccess;
                throw exc;
            }
        }

        public ObjectPermission(PermissionState state)
        {
            throw new ArgumentException("Invalid permission state.");
        }

        public ObjectPermissionAccess RequiredAccess
        {
            get { return reqPerm; }
            set { reqPerm = value; }
        }
        public ObjectPermissionAccess CurrentAccess
        {
            get { return currPerm; }
            set { currPerm = value; }
        }
        public string ObjectName
        {
            get { return securableObjectName; }
            set { securableObjectName = value; }
        }


        public override IPermission Copy()
        {
            return new ObjectPermission(securableObjectName, reqPerm, currPerm);
        }
        public bool IsUnrestricted()
        {
            // Always false, unrestricted state is not allowed.
            return m_Unrestricted;
        }

        private bool VerifyType(IPermission target)
        {
            return (target is ObjectPermission);
        }
        public override bool IsSubsetOf(IPermission target)
        {

            if (target == null)
            {

                return false;
            }
            try
            {
                ObjectPermission operand = (ObjectPermission)target;
                
                // The following check for unrestricted permission is only included as an example for
                // permissions that allow the unrestricted state. It is of no value for this permission.
                return currPerm <= operand.CurrentAccess;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException(String.Format("Argument_WrongType", this.GetType().FullName));
            }
        }
        public override IPermission Intersect(IPermission target)
        {
            if (target == null)
            {
                return null;
            }

            if (!VerifyType(target))
            {
                throw new ArgumentException(String.Format("Argument is wrong type.", this.GetType().FullName));
            }

            ObjectPermission operand = (ObjectPermission)target;

            if (operand.IsSubsetOf(this)) return operand.Copy();
            else if (this.IsSubsetOf(operand)) return this.Copy();
            else
            {
                ObjectPermissionAccess val = (ObjectPermissionAccess)Math.Min((Int32)currPerm, (Int32)((ObjectPermission)target).CurrentAccess);
                if (val == 0) return null;

                // Return a new object with the intersected permission value.

                return new ObjectPermission(securableObjectName,reqPerm,val);

            }
        }
        public override IPermission Union(IPermission target)
        {
            if (target == null)
            {
                return this;
            }
            if (!VerifyType(target))
            {
                throw new ArgumentException(String.Format("Argument WrongType", this.GetType().FullName));
            }

            ObjectPermission operand = (ObjectPermission)target;

            if (operand.IsSubsetOf(this)) return this.Copy();
            else if (this.IsSubsetOf(operand)) return operand.Copy();
            else
            {
                return new ObjectPermission(securableObjectName,reqPerm,(ObjectPermissionAccess)
                 Math.Max((Int32)currPerm, (Int32)operand.CurrentAccess));

            }
        }

        public override void FromXml(SecurityElement e)
        {
            m_Unrestricted = false;
            String elName = e.Attribute("Name");
            securableObjectName = elName == null ? null : elName;

            return;
        }
        public override SecurityElement ToXml()
        {
            SecurityElement esd = new SecurityElement("IPermission");
            String name = typeof(ObjectPermission).AssemblyQualifiedName;
            esd.AddAttribute("Class", name);
            if (securableObjectName != null) esd.AddAttribute("Name", securableObjectName);
            return esd;
        }
    }

    //[ObjectPermissionAttribute(ObjectPermissionAccess.Administrator, ObjectPermissionAccess.Administrator)]

    /// <summary>
    /// Class is used to expose RequiredAccessPermission and CurrentAccessPermission for ObjectPermission.
    /// </summary>
    
   
    
   
}
