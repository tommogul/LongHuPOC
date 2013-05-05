using System;
using System.Data.Objects.DataClasses;

namespace LongHu.DataAccess.EFImp
{
    public static class EfExtension
    {
        public static void MarkCreated(this EntityObject eo, Decimal user)
        {
            var type = eo.GetType();
            var pInfo = type.GetProperty("CreatedOn");
            if (pInfo != null)
                pInfo.SetValue(eo, DateTime.Now, null);
            var cInfo = type.GetProperty("CreatedByEmployeeId");
            if (cInfo != null)
                cInfo.SetValue(eo, user, null);
            var eInfo = type.GetProperty("IsActive");
            if (eInfo != null)
                eInfo.SetValue(eo, "1", null);//2 indicates deleted
            var mInfo = type.GetProperty("ModifiedOn");
            if (mInfo != null)
                mInfo.SetValue(eo, DateTime.Now, null);
            var nInfo = type.GetProperty("ModifiedByEmployeeId");
            if (nInfo != null)
                nInfo.SetValue(eo, user, null);
        }

        public static void MarkModified(this EntityObject eo, Decimal user)
        {
            var type = eo.GetType();
            var pInfo = type.GetProperty("ModifiedOn");
            if (pInfo != null)
                pInfo.SetValue(eo, DateTime.Now, null);
            var cInfo = type.GetProperty("ModifiedByEmployeeId");
            if (cInfo != null)
                cInfo.SetValue(eo, user, null);
        }

        public static void MarkDeleted(this EntityObject eo, Decimal user)
        {
            var type = eo.GetType();

            var pInfo = type.GetProperty("IsActive");
            if (pInfo != null)
                pInfo.SetValue(eo, "0", null);//2 indicates deleted

            var eInfo = type.GetProperty("ModifiedOn");
            if (eInfo != null)
                eInfo.SetValue(eo, DateTime.Now, null);

            var cInfo = type.GetProperty("ModifiedByEmployeeId");
            if (cInfo != null)
                cInfo.SetValue(eo, user, null);
        }
    }
}

