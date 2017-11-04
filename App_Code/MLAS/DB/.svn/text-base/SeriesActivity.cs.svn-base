using MLAS.Util;
using System.Data;

namespace MLAS.DB.SeriesActivity
{
    /// <summary>
    /// clsMLASSeriesActivityMetadata 的摘要描述
    /// </summary>
    public class clsMLASSeriesActivityMetadata
    {
        public clsMLASSeriesActivityMetadata()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region SELECT
        public static DataTable MLAS_SeriesActivityMetadata_Select_cSeriesActivityID(object cSeriesActivityID)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_SeriesActivityMetadata WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "'";
            return MLASDB.GetDataSet(strSQL).Tables[0];
        }
        public static string MLAS_SeriesActivityMetadata_Select_Parameter(object cSeriesActivityID, object cParameterName)
        {
            clsMLASDB MLASDB = new clsMLASDB();
            string strSQL = "SELECT * FROM MLAS_SeriesActivityMetadata WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cParameterName LIKE '" + cParameterName + "'";
            DataTable dtMetadata = MLASDB.GetDataSet(strSQL).Tables[0];
            if (dtMetadata.Rows.Count > 0)
            {
                return dtMetadata.Rows[0]["cParameterValue"].ToString();
            }
            else
                return "";
        }

        #endregion

        #region INSERT
        public static int MLAS_SeriesActivityMetadata_Insert(object cSeriesActivityID, object cParameterName, object cParameterValue)
        {
            string strSQL = "INSERT INTO MLAS_SeriesActivityMetadata(cSeriesActivityID,cParameterName,cParameterValue) VALUES(@cSeriesActivityID,@cParameterName,@cParameterValue)";
            object[] pList = { cSeriesActivityID, cParameterName, cParameterValue };
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

        #region Update
        public static int MLAS_SeriesActivityMetadata_Update_ParameterValue(object cSeriesActivityID, object cParameterName, object cParameterValue)
        {
            string strSQL = "UPDATE MLAS_SeriesActivityMetadata SET cParameterValue=@cParameterValue WHERE cSeriesActivityID LIKE @cSeriesActivityID AND cParameterName LIKE @cParameterName";
            object[] pList = { cParameterValue, cSeriesActivityID, cParameterName };
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
        public static int MLAS_SeriesActivityMetadata_DELETE_cSeriesActivityID(object cSeriesActivityID)
        {
            string strSQL = "DELETE MLAS_SeriesActivityMetadata WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "'";
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
        public static int MLAS_SeriesActivityMetadata_DELETE_cSeriesActivityID_PaType(object cSeriesActivityID, object cParameterName)
        {
            string strSQL = "DELETE MLAS_SeriesActivityMetadata WHERE cSeriesActivityID LIKE '" + cSeriesActivityID + "' AND cParameterName LIKE '" + cParameterName + "'";
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
