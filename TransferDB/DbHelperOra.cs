using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace TransferDB.DBUtility
{
    /// <summary>
    /// Copyright (C) 2004-2008 LiTianPing 
    /// 数据访问基础类(基于Oracle)
    /// 可以用户可以修改满足自己项目的需要。
    /// </summary>
    public abstract class DbHelperOra
    {
        /// <summary>
        /// 数据库连接字符串(app.config来配置)，可以动态更改connectionString支持多数据库.
        /// </summary>		
        public static string connectionString = ConfigurationManager.ConnectionStrings["strCoonOracle"].ToString();//本地数据库
        public static string connString = ConfigurationManager.ConnectionStrings["strconnOrcl"].ToString();  //远程服务器上的数据库
        //   public static string connectionString = Common.DESEncrypt.Decrypt( ConfigurationManager.ConnectionStrings["strCoonOracle"].ToString());//本地数据库
        //  public static string connString = Common.DESEncrypt.Decrypt(ConfigurationManager.ConnectionStrings["strconnOrcl"].ToString());  //远程服务器上的数据库

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet QueryVeh(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(SQLString, connection);
                    OracleDataAdapter command = new OracleDataAdapter();//(SQLString, connection);
                    command.SelectCommand = cmd;
                    command.Fill(ds);
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DbHelperOra()
        {
        }

        #region 公用方法

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Exists(string strSql, params OracleParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (OracleException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (OracleException E)
                {

                    tx.Rollback();
                    return -1;
                    throw new Exception(E.Message);
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(SQLString, connection);
                // OracleParameter myParameter = new OracleParameter("@content",OracleType.NVarChar); //
                OracleParameter myParameter = new OracleParameter("@content", OracleDbType.NVarchar2);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (OracleException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(strSQL, connection);
                //  OracleParameter myParameter = new OracleParameter("@fs", OracleType.LongRaw);
                OracleParameter myParameter = new OracleParameter("@fs", OracleDbType.LongRaw);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (OracleException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (OracleException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string strSQL)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand(strSQL, connection);
            try
            {
                connection.Open();
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query_1(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(SQLString, connection);
                    OracleDataAdapter command = new OracleDataAdapter();//(SQLString, connection);
                    command.SelectCommand = cmd;
                    command.Fill(ds);
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(SQLString, connection);
                    OracleDataAdapter command = new OracleDataAdapter();//(SQLString, connection);
                    command.SelectCommand = cmd;
                    command.Fill(ds);
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }



        #endregion

        #region 连接多数据库

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql_1(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (OracleException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (OracleException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OracleParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    OracleCommand cmd = new OracleCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (OracleException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string SQLString, params OracleParameter[] cmdParms)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (OracleException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query_1(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (OracleException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) &&
                        (parm.Value == null))
                    {
                        parm.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parm);
                }
            }
        }
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdtype, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdtype; //CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }



        #endregion

        #region 执行存储过程

        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static OracleDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleDataReader returnReader;
            connection.Open();
            OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OracleDataAdapter sqlDA = new OracleDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OracleDataAdapter sqlDA = new OracleDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static OracleCommand BuildQueryCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }


        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                int result;
                connection.Open();
                OracleCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static OracleCommand BuildIntCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new OracleParameter("ReturnValue",
                OracleDbType.Int32, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        #endregion

        #region 执行分页存储过程

        /// <summary>
        /// 执行存储过程Pagination分页
        /// </summary>
        /// <param name="TableName">表名或视图名或一个select语句</param>
        /// <param name="Fields">返回的字段</param>
        /// <param name="WhereStr">Where条件（and id=1 and name=222）</param>
        /// <param name="OrderStr">排序（order by Id desc,time desc）</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="RecordCount">输出总记录数(不需返回时为0)</param>
        /// <returns></returns>
        public static DataTable RunPagination(string TableName, string WhereStr, string Fields, string OrderColumn, string OrderType, int PageIndex, int PageSize, out int RecordCount, out int totalPages, bool IsCount)
        {
            OracleParameter[] Paras =
            {
                new OracleParameter(":SqlTable",OracleDbType.Varchar2,1000),
                 new OracleParameter(":SqlWhere",OracleDbType.Varchar2,1000),
                new OracleParameter(":SqlField",OracleDbType.Varchar2,1000),
                 new OracleParameter(":SqlOrder",OracleDbType.Varchar2,200),
                new OracleParameter(":SqlOrderType",OracleDbType.Varchar2,200),
                 new OracleParameter(":PageIndex",OracleDbType.Int32),
                new OracleParameter(":PageSize",OracleDbType.Int32),
                new OracleParameter(":RecordCount",OracleDbType.Int32),
                new OracleParameter(":totalPages",OracleDbType.Int32),
                new OracleParameter(":cur",OracleDbType.RefCursor)
            };
            Paras[0].Value = TableName;
            Paras[1].Value = WhereStr;
            Paras[2].Value = Fields;
            Paras[3].Value = OrderColumn;
            Paras[4].Value = OrderType;
            Paras[5].Value = PageIndex;
            Paras[6].Value = PageSize;
            Paras[7].Direction = ParameterDirection.Output;
            Paras[8].Direction = ParameterDirection.Output;
            Paras[9].Direction = ParameterDirection.Output;
            DataSet PageSet = RunProcedure("page_query", Paras, "pageindex");
            RecordCount = IsCount ? Convert.ToInt32(Paras[7].Value.ToString()) : 0;
            totalPages = IsCount ? Convert.ToInt32(Paras[8].Value.ToString()) : 0;
            return PageSet.Tables["pageindex"];
        }

        #endregion

        #region 插入新的一条数据并返回相应的id
        /// <summary>
        /// 向数据库插入一条新的数据并返回该数据相应的自增长ID
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="colValues">插入的值（自增ID，系统默认添加，之后的值要与列名对应）</param>
        /// <param name="tableName">要插入数据的表名</param>
        /// <param name="squName">要插入数据的名的序列名</param>
        /// <returns></returns>
        public static int Add_GetId(string columsName, string colValues, string tableName, string squName)
        {
            OracleParameter[] parms ={
                                         new OracleParameter(":p_columns",OracleDbType.Varchar2),
                                         new OracleParameter(":p_colvalues",OracleDbType.Varchar2),
                                         new OracleParameter(":p_tablename",OracleDbType.Varchar2),
                                         new OracleParameter(":p_squename",OracleDbType.Varchar2),
                                         new OracleParameter(":p_newid",OracleDbType.Int32)
                                     };
            parms[0].Value = columsName;
            parms[1].Value = colValues;
            parms[2].Value = tableName;
            parms[3].Value = squName;
            parms[4].Value = ParameterDirection.Output;
            OracleDataReader returnReader = RunProcedure("ADD_GETID", parms);
            returnReader.Close();
            if (parms[4].Value != null)
                return Convert.ToInt32(parms[4].Value.ToString());
            return -1;
        }

        #endregion
    }
}
