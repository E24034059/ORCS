using System;
using System.Data;
using Hints.DB;
using suro.util;
using System.Data.SqlClient;


namespace Hints.DB
{
    /// <summary>
    /// clsHintsDB 的摘要描述。
    /// </summary>
    public class clsHintsDB
    {
        protected SqlDB sqldb = null;

        public clsHintsDB()
        {
            sqldb = new SqlDB(System.Configuration.ConfigurationManager.ConnectionStrings["connHintsDB"].ToString());
        }

        public clsHintsDB(string strConnectionString)
        {
            sqldb = new SqlDB(strConnectionString);
        }

        #region 此clsHintsDB所提供的進皆方法


        //		public void UpdateDataTable(DataTable dt,System.Data.SqlClient.SqlCommand sqlcom)
        //		{
        //			sqldb.UpdateDataTable(dt,sqlcom);			
        //		}

        /// <summary>
        /// 透過SqlCommand物件取得DataSet物件
        /// </summary>
        /// <param name="sqlcom">SqlCommand物件</param>
        /// <returns>傳回DataSet</returns>
        public DataSet getDataSet(System.Data.SqlClient.SqlCommand sqlcom)
        {
            return sqldb.getDataSet(sqlcom);
        }

        //		public void fillSqlParameters(System.Data.SqlClient.SqlCommand sqlcom,object[] pList)
        //		{
        //			sqldb.fillSqlParameters(sqlcom,pList);
        //		}

        //		public void ExecuteNonQuery(System.Data.SqlClient.SqlCommand sqlcom)
        //		{
        //			sqldb.ExecuteNonQuery(sqlcom);
        //		}

        //		public object ExecuteScalar(System.Data.SqlClient.SqlCommand sqlcom)
        //		{
        //			return sqldb.ExecuteScalar(sqlcom);
        //		}

        public void fillSqlParameters(SqlCommand cmd, object[] pList)
        {
            sqldb.fillSqlParameters(cmd, pList);
        }



        /// <summary>
        /// 利用SqlCommand將DataTable的物件更新回資料庫
        /// </summary>
        /// <param name="dt">DataTable的物件</param>
        /// <param name="cmd">SqlCommand物件</param>
        public void UpdateDataTable(DataTable dt, SqlCommand cmd)
        {
            sqldb.Update(dt, cmd);
        }

        /// <summary>
        /// 將DataTable物件更新回資料庫
        /// </summary>
        /// <param name="dt">欲更新回資料庫的DataTable物件</param>
        /// <param name="cmdstr">
        /// 更新此DataTable物件所需的SQL指令
        /// 例如:
        /// DataTable dt = clsHintsDB.getDataSet(" SELECT * FROM any_table WHERE any_column='condition_value' ").Tables[0];
        /// 執行過程中對此 dt 物件作任何修改動作
        /// 則最後欲更新回資料庫,所下的指令如下:
        /// clsHintsDB.UpdateDataTable(dt," SELECT * FROM any_table ");
        /// 則參數cmdstr = " SELECT * FROM any_table ";
        /// </param>
        public bool UpdateDataTable(DataTable dt, string cmdstr)
        {
            try
            {

                return sqldb.Update(dt, cmdstr);

            }
            catch (Exception e)
            {
                string hh = e.ToString();
                string jj = hh;
                return false;
            }

        }

        /// <summary>
        /// 根據SQL語法,取得DataSet物件
        /// </summary>
        /// <param name="cmdstr">SQL指令</param>
        /// <param name="pList">
        /// SQL指令中的變數以object[]傳入
        /// string strUserID = "alivs";
        /// string strCaseID = "Case1248746545642460000";
        /// string strSQL = "SELECT * FROM TempLogForHeader WHERE cUserID=@cUserID AND cCaseID=@cCaseID";//在此SQL語法中@cUserID,@cCaseID分別為變數
        /// object[] pList = {strUserID,strCaseID};//因為上面SQL語法有兩個變數,所以在此需按照SQL語法中的變數順序傳入兩個變數的值
        /// DataSet ds = clsHintsDB.getDataSet(strSQL,pList);
        /// </param>
        /// <returns>DataSet物件</returns>
        public DataSet getDataSet(string cmdstr, object[] pList)
        {
            return sqldb.getDataSet(cmdstr, pList);
        }

        /// <summary>
        /// 根據SQL語法,取得DataSet物件
        /// </summary>
        /// <param name="cmdstr">SQL指令</param>
        /// <returns>DataSet物件</returns>
        public DataSet getDataSet(string cmdstr)
        {
            try
            {
                return sqldb.getDataSet(cmdstr);
            }
            catch (Exception e)
            {
                string hh = e.ToString();
                return sqldb.getDataSet(cmdstr);
            }
        }

        /// <summary>
        /// 執行DELETE,INSERT,UPDATE等SQL指令
        /// </summary>
        /// <param name="cmdstr">DELETE,INSERT,UPDATE等SQL指令</param>
        /// <param name="pList">
        /// SQL指令中的變數以object[]傳入
        /// string strSQL = "DELETE TempLogForHeader WHERE cUserID=@cUserID AND cCaseID=@cCaseID";//在此SQL語法中@cUserID,@cCaseID分別為變數
        /// object[] pList = {strUserID,strCaseID};//因為上面SQL語法有兩個變數,所以在此需按照SQL語法中的變數順序傳入兩個變數的值
        /// clsHintsDB.ExecuteNonQuery(strSQL,pList);
        /// </param>
        public void ExecuteNonQuery(string cmdstr, object[] pList)
        {
            sqldb.ExecuteNonQuery(cmdstr, pList);
        }

        /// <summary>
        /// 執行DELETE,INSERT,UPDATE等SQL指令
        /// </summary>
        /// <param name="cmdstr">DELETE,INSERT,UPDATE等SQL指令</param>
        public void ExecuteNonQuery(string cmdstr)
        {
            sqldb.ExecuteNonQuery(cmdstr);
        }

        /// <summary>
        /// 從資料庫擷取單一值
        /// </summary>
        /// <param name="cmdstr">
        /// SELECT等SQL指令,且此SQL指令只會傳回一列一行的資料,例如：
        /// string strSQL = "SELECT COUNT(*) FROM HinsUser";
        /// stirng result = clsHintsDB.ExecuteScalar(strSQL,pList).ToString();
        /// </param>
        /// SQL指令中的變數以object[]傳入
        /// string strSQL = "SELECT COUNT(*) FROM region WHERE cUserID=@cUserID AND cCaseID=@cCaseID";//在此SQL語法中@cUserID,@cCaseID分別為變數
        /// object[] pList = {strUserID,strCaseID};//因為上面SQL語法有兩個變數,所以在此需按照SQL語法中的變數順序傳入兩個變數的值
        /// stirng result = clsHintsDB.ExecuteScalar(strSQL,pList).ToString();
        /// <returns>傳回直以object傳回,視需求轉型</returns>
        public object ExecuteScalar(string cmdstr, object[] pList)
        {
            try
            {
                return sqldb.ExecuteScalar(cmdstr, pList);
            }
            catch
            {
                return new object();
            }
        }

        /// <summary>
        /// 從資料庫擷取單一值
        /// </summary>
        /// <param name="cmdstr">
        /// SELECT等SQL指令,且此SQL指令只會傳回一列一行的資料,例如：
        /// string strSQL = "SELECT COUNT(*) FROM HinsUser";
        /// stirng result = clsHintsDB.ExecuteScalar(strSQL,pList).ToString();
        /// </param>
        /// <returns>傳回直以object傳回,視需求轉型</returns>
        public object ExecuteScalar(string cmdstr)
        {
            try
            {
                return sqldb.ExecuteScalar(cmdstr);
            }
            catch
            {
                string hh = "";
                return null;
            }
        }

        #endregion

        #region 4個基本SQL語法指令 SELECT,DELETE,UPDATE,INSERT

        /// <summary>
        /// 執行SQL語法的Update指令
        /// </summary>
        /// <param name="strTableName">欲更新資料的資料表名稱</param>
        /// <param name="sryReferenceFieldNameWithValue">
        ///  篩選資料的準則,例如[cCaseName='Chin']或[sGrade > 50]
        ///  字串陣列中的每個元素代表每一個篩選條件,例如[cCaseName='Chin']
        /// </param>
        /// <param name="aryUpdateFieldNameWithValue">
        /// 更新資料的欄位及其更新的值	
        /// 例如[cCaseName = 'Chin']或[sGrade = 50]
        /// 字串陣列中的每個元素形式如同"cCaseName='Chin'"
        /// </param>
        /// <returns>傳回執行結果,若執行成功傳回"執行成功!"字串,若執行失敗傳回錯誤訊息</returns>
        public string UpdateCommand(string strTableName, string[] aryReferenceFieldNameWithValue, string[] aryUpdateFieldNameWithValue)
        {
            string strResultMessage = "執行成功!";
            string strSQL_aryReferenceFieldNameWithValue = "";
            int count = 0;
            foreach (object obj in aryReferenceFieldNameWithValue)
            {
                strSQL_aryReferenceFieldNameWithValue += count > 0 ? " AND (" + obj + " ) " : " (" + obj + " ) ";
                count++;
            }
            string strSQL = "SELECT * FROM " + strTableName + " WHERE " + strSQL_aryReferenceFieldNameWithValue;
            DataTable dt = sqldb.getDataSet(strSQL).Tables[0];

            string tmpFieldName = ""; //欲更新資料的欄位名稱
            string tmpFieldValue = "";//欲更新資料的欄位值

            foreach (DataRow dr in dt.Rows)
            {
                foreach (object obj in aryUpdateFieldNameWithValue)
                {
                    tmpFieldName = obj.ToString().Split('=')[0].Trim();
                    tmpFieldValue = obj.ToString().Split('=')[1].Trim();
                    if (tmpFieldValue.LastIndexOf("'") == tmpFieldValue.Length - 1)
                    {
                        tmpFieldValue = tmpFieldValue.Substring(0, tmpFieldValue.Length - 1);
                        if (tmpFieldValue.IndexOf("'") == 0)
                        {
                            tmpFieldValue = tmpFieldValue.Substring(1);
                        }
                    }
                    dr[tmpFieldName] = tmpFieldValue;
                }
            }
            try
            {
                this.UpdateDataTable(dt, "SELECT * FROM " + strTableName);
            }
            catch (Exception ex)
            {
                strResultMessage = ex.ToString();
            }
            return strResultMessage;
        }

        /// <summary>
        /// 執行SQL語法的Insert指令
        /// </summary>
        /// <param name="strTableName">欲新增資料的資料表名稱</param>
        /// <param name="aryInsertValue">
        /// 1.欲新增新增資料列每各欄位的值
        /// 2.在此須特別注意,每各欄位皆須插入資料,換句話說,所插入的資料表有n欄位則aryInsertValue就需有n個元素,
        ///   若不插入資料也需插入"NULL"的字串,表示不在相對欄位插入資料,但須特別注意不插入資料的欄位是否為索引鍵
        ///   否則會出錯
        /// </param>
        /// <returns>傳回執行結果,若執行成功傳回"執行成功!"字串,若執行失敗傳回錯誤訊息</returns>
        public string InsertCommand(string strTableName, string[] aryInsertValue)
        {
            string strResultMessage = "執行成功!";
            string strSQL = " SELECT * FROM " + strTableName + " WHERE 1=0";
            DataTable dt = getDataSet(strSQL).Tables[0];
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            int count = 0;
            foreach (object insertValue in aryInsertValue)
            {
                if (insertValue.ToString() != "NULL" && insertValue.ToString() != "'NULL'")
                {
                    newRow[count] = insertValue.ToString();
                }
                count++;
            }
            try
            {
                UpdateDataTable(dt, "SELECT * FROM " + strTableName);
            }
            catch (Exception ex)
            {
                strResultMessage = ex.ToString();
            }
            return strResultMessage;
        }

        /// <summary>
        /// 執行SQL語法的DELETE指令
        /// </summary>
        /// <param name="strTableName">資料表名稱</param>
        /// <param name="sryReferenceFieldNameWithValue">篩選資料的準則,例如[cCaseName='Chin']或[sGrade > 50]
        ///  字串陣列中的每個元素代表每一個篩選條件,例如[cCaseName='Chin']
        /// </param>
        /// <returns>傳回執行結果,若執行成功傳回"執行成功!"字串,若執行失敗傳回錯誤訊息</returns>
        public string DeleteCommand(string strTableName, string[] aryReferenceFieldNameWithValue)
        {
            string strResultMessage = "執行成功!";
            string strSQL_aryReferenceFieldNameWithValue = "";
            int count = 0;
            foreach (object obj in aryReferenceFieldNameWithValue)
            {
                strSQL_aryReferenceFieldNameWithValue += count > 0 ? " AND (" + obj + " ) " : " (" + obj + " ) ";
                count++;
            }
            string strSQL = "DELETE " + strTableName + " WHERE " + strSQL_aryReferenceFieldNameWithValue;
            try
            {
                this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strResultMessage = ex.ToString();
            }
            return strResultMessage;
        }

        /// <summary>
        /// 執行SQL語法的SELECT指令,在此只能查詢資料庫中的一個資料表
        /// </summary>
        /// <param name="strTableName">資料表名稱</param>
        /// <param name="aryQueryField">篩選出每筆資料所需要的欄位</param>
        /// <param name="sryReferenceFieldNameWithValue">篩選資料的準則,例如[cCaseName='Chin']或[sGrade > 50]
        ///  字串陣列中的每個元素代表每一個篩選條件,例如[cCaseName='Chin']
        /// </param>
        /// <returns></returns>
        public DataTable SelectCommand(string strTableName, string[] aryQueryField, string[] aryReferenceFieldNameWithValue)
        {
            string strSQL_QueryField = "";
            string strSQL_aryReferenceFieldNameWithValue = "";

            int count = 0;
            foreach (object obj in aryQueryField)
            {
                strSQL_QueryField += count > 0 ? " , " + obj : " " + obj;
                count++;
            }

            count = 0;
            foreach (object obj in aryReferenceFieldNameWithValue)
            {
                strSQL_aryReferenceFieldNameWithValue += count > 0 ? " AND (" + obj + " ) " : " (" + obj + " ) ";
                count++;
            }
            string strSQL = "SELECT DISTINCT " + strSQL_QueryField + " FROM " + strTableName + " WHERE " + strSQL_aryReferenceFieldNameWithValue;
            DataTable dt = sqldb.getDataSet(strSQL).Tables[0];
            return dt;
        }

        /// <summary>
        /// 執行SQL語法的SELECT指令,在此只能查詢資料庫中的一個資料表
        /// </summary>
        /// <param name="strTableName">資料表名稱</param>
        /// <param name="sryReferenceFieldNameWithValue">篩選資料的準則,例如[cCaseName='Chin']或[sGrade > 50]
        ///  字串陣列中的每個元素代表每一個篩選條件,例如[cCaseName='Chin']
        /// </param>
        /// <returns></returns>
        public DataTable SelectCommand(string strTableName, string[] aryReferenceFieldNameWithValue)
        {
            string strSQL_aryReferenceFieldNameWithValue = "";

            int count = 0;
            foreach (object obj in aryReferenceFieldNameWithValue)
            {
                strSQL_aryReferenceFieldNameWithValue += count > 0 ? " AND (" + obj + " ) " : " (" + obj + " ) ";
                count++;
            }
            string strSQL = "SELECT * FROM " + strTableName + " WHERE " + strSQL_aryReferenceFieldNameWithValue;
            DataTable dt = sqldb.getDataSet(strSQL).Tables[0];
            return dt;
        }

        #endregion


        //internal void Update(DataTable dt, string p)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}
    }

    public enum DBType { SQLDB, OLEDB };
}