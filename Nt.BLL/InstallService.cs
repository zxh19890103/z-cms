using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using Nt.BLL.Helper;
using Nt.DAL.Helper;

namespace Nt.BLL
{
    public class InstallService
    {
        #region Const
        /// <summary>
        /// 0--data source
        /// 1--initial catalog
        /// 2--user id
        /// 3--password
        /// </summary>
        const string CONNECTION_STRING_SA_PATTERN = @"data source={0};initial catalog={1};Integrated Security=False;persist security info=True;user id={2};password={3};MultipleActiveResultSets=True";

        /// <summary>
        /// 0--data source
        /// 1--initial catalog
        /// </summary>
        const string CONNECTION_STRING_WINDOWS_PATTERN = @"data source={0};initial catalog={1};Integrated Security=True;persist security info=True;MultipleActiveResultSets=True";

        const string CONNECTION_STRING_NAME_NT = "NtConnectionString";
        const string PROVIDER_NAME = "System.Data.SqlClient";
        const string SQL_SCRIPT_PATH_PATTERN = "/App_Data/Script/{0}.sql";
        #endregion

        /// <summary>
        /// 用于缓存SQL语句的容器
        /// </summary>
        private StringBuilder _sql = null;

        #region Connection String Setting
        /// <summary>
        /// data source
        /// </summary>
        private string _dataSource = string.Empty;
        public string DataSource { get { return _dataSource; } set { _dataSource = value; } }

        /// <summary>
        /// 数据库名称
        /// </summary>
        private string _dbName = string.Empty;
        public string DbName { get { return _dbName; } set { _dbName = value; } }

        /// <summary>
        /// sql server登录密码
        /// </summary>
        private string _password = string.Empty;
        public string Password { get { return _password; } set { _password = value; } }

        /// <summary>
        ///sql server 用户名
        /// </summary>
        private string _userID = string.Empty;
        public string UserID { get { return _userID; } set { _userID = value; } }

        /// <summary>
        /// 是否使用Window身份验证进行SQL  Server登录
        /// </summary>
        private bool _useWindowsAuthentication = false;
        public bool UseWindowsAuthentication { get { return _useWindowsAuthentication; } }

        private bool _dbExisting = false;
        public bool DbExisting { get { return _dbExisting; } set { _dbExisting = value; } }

        #endregion

        #region connectionString
        /// <summary>
        /// 用于连接master数据库的字符串
        /// </summary>
        private string _masterConnectionString = string.Empty;
        protected string MasterConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_masterConnectionString))
                {
                    if (_useWindowsAuthentication)
                        _masterConnectionString = string.Format(CONNECTION_STRING_WINDOWS_PATTERN, _dataSource, "master");
                    else
                        _masterConnectionString = string.Format(CONNECTION_STRING_SA_PATTERN, _dataSource, "master", _userID, _password);
                }
                return _masterConnectionString;
            }
        }

        /// <summary>
        /// 用于连接本数据库的字符串
        /// </summary>
        private string _thisDbConnectionString = string.Empty;
        protected string ThisDbConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_thisDbConnectionString))
                {
                    if (_useWindowsAuthentication)
                        _masterConnectionString = string.Format(CONNECTION_STRING_WINDOWS_PATTERN, _dataSource, _dbName);
                    else
                        _masterConnectionString = string.Format(CONNECTION_STRING_SA_PATTERN, _dataSource, _dbName, _userID, _password);
                }
                return _masterConnectionString;
            }
        }

        #endregion

        #region Constructors

        public InstallService(string dbname, string dataSource)
        {
            _dbName = dbname;
            _dataSource = dataSource;
            _useWindowsAuthentication = true;
            _sql = new StringBuilder();
        }

        public InstallService(string dbname, string dataSource,
            string userId, string password
            )
            : this(dbname, dataSource)
        {
            _userID = userId;
            _password = password;
            _useWindowsAuthentication = false;
        }

        #endregion

        string _phyPathToSaveDB;
        public string PhyPathToSaveDB
        {
            get
            {
                if (string.IsNullOrEmpty(_phyPathToSaveDB))
                {
                    string tp = WebHelper.MapPath("/App_Data/DBPathInDisk.txt");
                    if (File.Exists(tp))
                    {
                        _phyPathToSaveDB = File.ReadAllText(tp);
                        if (string.IsNullOrEmpty(_phyPathToSaveDB))
                            return "";
                    }
                }
                return _phyPathToSaveDB;
            }
        }

        /// <summary>
        /// 全部安装
        /// </summary>
        public void Install()
        {
            if (!DbExisting)
            {
                if (!string.IsNullOrEmpty(PhyPathToSaveDB)
                   && !Directory.Exists(PhyPathToSaveDB))
                    Directory.CreateDirectory(PhyPathToSaveDB);

                InstallDb();
            }
            InstallTables();
            InstallBasedata();
            InsertAllPermissionRecords();
            InstallAdminDefaultPermissionRecords();

            //向/App_Data/Connection.txt写入ConnectionString
            string connectionFilePath = WebHelper.MapPath("/App_Data/Connection.txt");
            File.WriteAllText(connectionFilePath, ThisDbConnectionString);
        }

        #region Install
        /// <summary>
        /// 安装必要的master上的存储过程
        /// </summary>
        void InstallDb()
        {
            string sql2CreateDB = string.Empty;
            if (string.IsNullOrEmpty(PhyPathToSaveDB))
            {
                sql2CreateDB = "create database [{DB}]\r\n"
                    .Replace("{DB}", DbName);
            }
            else
            {
                string mdfPath = string.Empty;
                string ldfPath = string.Empty;
                mdfPath = PhyPathToSaveDB + "{DB}.mdf";
                ldfPath = PhyPathToSaveDB + "{DB}_log.ldf";
                sql2CreateDB = "create database [{DB}] on primary\r\n(\r\nname=N'{DB}',\r\nfilename=N'{mdfPath}',\r\nsize=3072kb,\r\nmaxsize=102400kb,\r\nfilegrowth=1024kb\r\n)\r\nlog on\r\n(\r\nname=N'{DB}_log',\r\nfilename=N'{ldfPath}',\r\nsize=3072kb,\r\nmaxsize=51200kb,\r\nfilegrowth=10%\r\n)"
                    .Replace("{mdfPath}", mdfPath)
                    .Replace("{ldfPath}", ldfPath)
                    .Replace("{DB}", DbName);
            }
            _sql.Append(sql2CreateDB);
            ExecuteOnMaster();
        }

        void InstallTables()
        {
            SetQUOTED_IDENTIFIER(false);
            ExecuteScriptOnFile("DropViews");//drop all views
            ExecuteScriptOnFile("DropFK");//drop all foreign key constraints
            ExecuteScriptOnFile("DropTabs");//drop all existing tables
            ExecuteScriptOnFile("CreateTabs");//create all tables
            ExecuteScriptOnFile("CreatePK");//create all primary key constraints
            ExecuteScriptOnFile("CreateFK");//create all foreign key constraints
            ExecuteScriptOnFile("CreateViews");//create all views
        }

        void InstallBasedata()
        {
            ExecuteScriptOnFile("InsertBaseData");
        }

        #endregion

        #region Utility

        /// <summary>
        /// SET QUOTED_IDENTIFIER ON/OFF
        /// </summary>
        /// <param name="onOrOff">bool true--ON  false--OFF</param>
        void SetQUOTED_IDENTIFIER(bool onOrOff)
        {
            string sql = string.Format("SET QUOTED_IDENTIFIER {0}", onOrOff ? "ON" : "OFF");
            Execute(sql);
        }

        /// <summary>
        /// SET ANSI_NULLS ON/OFF
        /// </summary>
        /// <param name="onOrOff">bool true--ON  false--OFF</param>
        void SetANSI_NULLS(bool onOrOff)
        {
            string sql = string.Format("SET ANSI_NULLS {0}", onOrOff ? "ON" : "OFF");
            Execute(sql);
        }


        #endregion

        #region SQL Execute
        /// <summary>
        /// 清空_sql中的内容
        /// </summary>
        void Clear()
        {
            _sql.Remove(0, _sql.Length);
        }

        /// <summary>
        /// 执行保存于_sql中的SQL语句
        /// </summary>
        void Execute()
        {
            Execute(_sql.ToString());
            Clear();
        }

        void Execute(string sql)
        {
            if (sql.Length > 0)
            {
                SqlHelper.ExecuteNonQuery(
                    ThisDbConnectionString,
                    CommandType.Text,
                    sql
                    );
            }
        }

        void ExecuteOnMaster()
        {
            if (_sql.Length > 0)
            {
                SqlHelper.ExecuteNonQuery(
                    MasterConnectionString,
                    CommandType.Text,
                    _sql.ToString()
                    );
            }
            Clear();
        }


        void ExecuteScriptOnFile(string filename, bool exeOnMaster = false)
        {
            var absPath = string.Format(SQL_SCRIPT_PATH_PATTERN, filename);
            var phy_filename = WebHelper.MapPath(absPath);
            if (!File.Exists(phy_filename))
                throw new Exception(string.Format("没有发现路径为{0}的脚本文件！", absPath));
            StreamReader reader = null;
            try
            {
                reader = File.OpenText(phy_filename);
                string line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine().Trim();
                    if (line.ToUpper() == "GO")
                    {
                        if (exeOnMaster)
                            ExecuteOnMaster();
                        else
                            Execute();
                    }
                    else
                    {
                        _sql.Append(line);
                        _sql.Append("\r\n");
                    }
                }
                if (exeOnMaster)
                    ExecuteOnMaster();
                else
                    Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// install all permission records
        /// </summary>
        void InsertAllPermissionRecords()
        {
            _sql.AppendFormat("SET IDENTITY_INSERT [dbo].[Nt_Permission] ON\r\n");
            var data = PermissionRecordProvider.AllPermissionRecords;
            foreach (var item in data)
            {
                _sql.AppendFormat(string.Format("INSERT INTO [Nt_Permission](Id,Category,Name,CategoryName,SystemName)Values({0},'{1}','{2}','{3}','{4}')\r\n",
                    item.Id, item.Category, item.Name, item.CategoryName, item.SystemName));
            }
            _sql.AppendFormat("SET IDENTITY_INSERT [dbo].[Nt_Permission] OFF\r\n");
            Execute();
        }

        /// <summary>
        /// install default admin permission records
        /// </summary>
        void InstallAdminDefaultPermissionRecords()
        {
            var data = PermissionRecordProvider.AdminDefaultPermissionRecords;
            foreach (var item in data)
            {
                _sql.AppendFormat(string.Format("INSERT INTO [Nt_Permission_UserLevel_Mapping](Permission_Id,UserLevel_Id)Values({0},{1})\r\n",
                    item.Id, 2));
            }
            Execute();
        }

    }
}
