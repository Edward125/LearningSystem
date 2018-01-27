﻿using Edward;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LearningSystem
{
    class p
    {
        public static string AppFolder = @".\" + Application.ProductName;

        public static string iniFilePath = AppFolder + @"\SysConfig.ini";
        public static string dbFile = AppFolder + @"\DB.sqlite";
        public static string dbConnectionString = "Data Source=" + dbFile;


        public static string FtpIP = "10.62.201.224";
        public static string FtpID = "ate";
        public static string FtpPassword = "Wcdate";
        public static string FtpFolder = @"LearnSystem";


        public enum IniSection
        {
            SysConfig
        }

        public enum DBKeyValue
        {
            usrid,
            usrpwd,
            permission            
        }

        public enum DBTable
        {
            d_usrlist
        }


        public enum PermissionKey
        {
            administrtor,
            guest
        }

        /// <summary>
        /// check app folder
        /// </summary>
        /// <returns></returns>
        public static bool checkFolder()
        {
            if (!Directory.Exists(AppFolder))
            {
                try
                {
                    Directory.CreateDirectory(AppFolder);
                }
                catch (Exception)
                {

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// create ini file
        /// </summary>
        /// <param name="inifilepath">ini file path </param>
        public static void createIniFile(string inifilepath)
        {
            IniFile.CreateIniFile(inifilepath);
            //File.SetAttributes(inifilepath, FileAttributes.Hidden);
            IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "FtpIP", FtpIP , inifilepath);
            IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "FtpID", FtpID, inifilepath);
            IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "FtpPassword",FtpPassword, inifilepath);
            IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "FtpFolder", FtpFolder, inifilepath);
        }

        /// <summary>
        /// read ini file value 
        /// </summary>
        /// <param name="inifilepath">ini file path</param>
        public static void readIniValue(string inifilepath)
        {
            FtpIP  = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "FtpIP", inifilepath);
            FtpID = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "FtpID", inifilepath);
            FtpPassword = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "FtpPassword", inifilepath);
            FtpFolder  = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "FtpFolder", inifilepath);
        }

        /// <summary>
        /// check db file ,if not exits,create it
        /// </summary>
        /// <param name="_dbfile">db file path</param>
        /// <returns></returns>
        public static bool checkDB(string _dbfile)
        {
            if (!File.Exists(_dbfile))
            {
                try
                {
                    SQLiteConnection.CreateFile(_dbfile);

                    if (!p.createUsrListTable())
                        Environment.Exit(0);                   
                    return true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Create DB Fail." + ex.Message, "Create DB Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


            }
            return true;
        }

        // string sql = "create table highscores (name varchar(20), score int)";
        /// <summary>
        /// create table 
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>create ok,return true;create ng,return false</returns>
        public static bool createTable(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(dbConnectionString);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Connect to database fail," + ex.Message);
                return false;
            }

            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Create TABLE fail," + ex.Message);
                conn.Close();
                return false;

            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool createUsrListTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS d_usrlist(
usrid varchar(255) PRIMARY KEY NOT NULL,
usrpwd varchar(255),
permission varchar(255))";

            if (createTable(sql))
                return true;
            else
                return false;

           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public static List<string> queryDB(string sql,DBKeyValue dbkeyvalue)
        {       
            List<string> files = new List<string>();
            //string sql = "SELECT * FROM d_alldepstatus";
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader re = cmd.ExecuteReader();

            if (re.HasRows)
            {
                while (re.Read())
                {                 
                    files.Add(re[dbkeyvalue.ToString()].ToString());
                }
            }
            conn.Close();
            return files;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">REPLACE INTO </param>
        /// <returns></returns>
        public  static bool insertDB(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
           

            conn.Close();
            return true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">DELETE FROM 表名称 WHERE 列名称 = 值</param>
        /// <returns></returns>
        public static bool deleteDB(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }

            conn.Close();
            return true;
        }
    }
}
