using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace MLAS.DB
{
    /// <summary>
    /// clsMLASDB 的摘要描述
    /// </summary>
    public class clsMLASDB
    {
        //protected SqlDB sqldb = null;
        protected string m_strConnection = "";
        #region Constructors
        /// <summary>
        /// using default connection string in Web.config
        /// </summary>
        public clsMLASDB()
        {
            m_strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["connMLASDB"].ToString();
        }
        /// <summary>
        /// using specify connection string
        /// </summary>
        /// <param name="connstr"></param>
        public clsMLASDB(string connstr)
        {
            m_strConnection = connstr;
        }
        #endregion
        #region Get data from database
        /// <summary>
        /// Get a first DataTable for the query string.
        /// (ex: "SELECT * FROM table1 WHERE ...")
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strSQL)
        {
            return this.GetDataSet(strSQL).Tables[0];
        }
        public DataTable ExecuteDataTable(string strSQL, object[] pList)
        {
            return this.GetDataSet(strSQL, pList).Tables[0];
        }
        /// <summary>
        /// Get a DataSet for the select query string.
        /// (ex: "SELECT * FROM table1 WHERE ...")
        /// The returned DataSet will contain one table only which has no table name.
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string strSQL)
        {
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, m_strConnection);
            DataSet dsResult = new DataSet();
            try
            {

                //cmd.Connection.Open();
                sda.Fill(dsResult);

            }
            finally
            {
                //cmd.Connection.Close();
                sda.Dispose();
            }
            return dsResult;
        }

        public DataSet GetDataSet(string strSQL, object[] pList)
        {
            SqlCommand cmd = new SqlCommand(strSQL, new SqlConnection(m_strConnection));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            this.fillSqlParameters(cmd, pList);
            DataSet dsResult = new DataSet();
            try
            {
                //cmd.Connection.Open();
                sda.Fill(dsResult);
            }
            finally
            {
                //cmd.Connection.Close();
                cmd.Dispose();
                sda.Dispose();
            }
            return dsResult;
        }

        public void fillSqlParameters(SqlCommand cmd, object[] pList)
        {
            Regex r = new Regex("(@\\w+)", RegexOptions.IgnoreCase);
            MatchCollection matches = r.Matches(cmd.CommandText);
            for (int i = 0; i < matches.Count; i++)
            {
                Match m = matches[i];
                cmd.Parameters.AddWithValue(m.Value, pList[i]);
            }
        }
        /// <summary>
        /// 執行查詢，並傳回查詢所傳回的結果集中第一個資料列的第一個資料行。會忽略其他的資料行或資料列。
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public object ExecuteScalar(string strSQL)
        {
            SqlCommand cmd = new SqlCommand(strSQL);
            object objResult = null;
            try
            {
                cmd.Connection = new SqlConnection(m_strConnection);
                cmd.Connection.Open();
                objResult = cmd.ExecuteScalar();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            return objResult;
        }
        public object ExecuteScalar(string strSQL, object[] pList)
        {
            SqlCommand cmd = new SqlCommand(strSQL);
            this.fillSqlParameters(cmd, pList);
            object objResult = null;

            try
            {
                cmd.Connection = new SqlConnection(m_strConnection);
                cmd.Connection.Open();
                objResult = cmd.ExecuteScalar();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return objResult;
        }

        public DataTable GetOtherSystemURL(string strSQL)
        {
            return this.GetDataSet(strSQL).Tables[0];
        }
        #endregion
        #region Operation to database
        /// <summary>
        /// 針對連接執行 Transact-SQL 陳述式，並傳回受影響的資料列數目。
        /// </summary>
        /// <param name="strSQL"></param>
        public int ExecuteNonQuery(string strSQL)
        {
            SqlCommand cmd = new SqlCommand(strSQL);
            int rows = 0;
            try
            {
                cmd.Connection = new SqlConnection(m_strConnection);
                cmd.Connection.Open();
                rows = cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                rows = -1;
            }
            return rows;
        }

        public int ExecuteNonQuery(string strSQL, object[] pList)
        {
            SqlCommand cmd = new SqlCommand(strSQL);
            this.fillSqlParameters(cmd, pList);

            int rows = 0;
            try
            {
                cmd.Connection = new SqlConnection(m_strConnection);
                cmd.Connection.Open();
                rows = cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                rows = -1;
            }
            return rows;
        }

        /// <summary>
        /// Update database from an offline DataTable.
        /// Notice that, the DataTable and selectstr used here must
        /// be consistent with those you used in invoking getDataSet().
        /// </summary>
        /// <param name="dtObj"></param>
        /// <param name="strSQL"></param>
        public void UpdateDataTable(DataTable dtObj, string strSQL)
        {
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, m_strConnection);
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            try
            {
                sda.Update(dtObj);
            }
            finally
            {
                sda.Dispose();
                scb.Dispose();
            }
        }


        #endregion
    }
}