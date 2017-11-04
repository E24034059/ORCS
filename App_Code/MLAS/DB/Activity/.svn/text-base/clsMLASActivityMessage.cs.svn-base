using MLAS.Util;
using System.Data;

namespace MLAS.DB.Activity
{
    /// <summary>
    /// clsMLASActivityMessage 的摘要描述
    /// </summary>
    public class clsMLASActivityMessage
    {
        public clsMLASActivityMessage()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        #region SELECT
        public static DataTable MLAS_ActivityMessage_Select(object iMessageID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityMessage WHERE iMessageID LIKE '" + iMessageID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_ActivityMessage_Select(object cActivityID, object cActivityStep)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityMessage WHERE cActivityID LIKE '" + cActivityID + "'AND cActivityStep LIKE'" + cActivityStep + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static DataTable MLAS_ActivityMessage_Select(object cActivityID, object cActivityStep, object cRoleID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_ActivityMessage WHERE cActivityID LIKE '" + cActivityID + "'AND cActivityStep LIKE'" + cActivityStep + "'AND cRoleID LIKE'" + cRoleID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        #endregion
        #region INSERT
        public static int MLAS_ActivityMessage_Insert(object cActivityID, object cActivityStep, object cRoleID, object cRecipients, object cCarbonCopy, object cMessageContent)
        {
            string strSQL = "INSERT INTO  MLAS_ActivityMessage(cActivityID,cActivityStep,cRoleID,cRecipients,cCarbonCopy,cMessageContent) VALUES(@cActivityID,@cActivityStep,@cRoleID,@cRecipients,@cCarbonCopy,@cMessageContent)";
            object[] pList = { cActivityID, cActivityStep, cRoleID, cRecipients, cCarbonCopy, cMessageContent };
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL, pList);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        #endregion

        #region Delete
        public static int MLAS_ActivityMessage_DELETE(object iMessageID)
        {
            string strSQL = "DELETE MLAS_ActivityMessage WHERE iMessageID LIKE '" + iMessageID + "'";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        public static int MLAS_ActivityMessage_DELETE(object cActivityID, object cActivityStep, object cRoleID)
        {
            string strSQL = "DELETE MLAS_ActivityMessage WHERE cActivityID LIKE '" + cActivityID + "'AND cActivityStep LIKE '" + cActivityStep + "'AND cRoleID LIKE '" + cRoleID + "'";
            try
            {
                clsMLASDB MLASDB = new clsMLASDB();
                MLASDB.ExecuteNonQuery(strSQL);
            }
            catch
            {
                return -1;
            }

            return 0;
        }
        #endregion

    }
}