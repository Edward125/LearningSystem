using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace LearningSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            if (!p.checkFolder())
            {
                MessageBox.Show("Create app folder fail,program will exit.", "Create Folder Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                System.Threading.Thread.Sleep(1000);
                // SplashForm.CloseSplash();
                Environment.Exit(0);
            }

            if (!File.Exists(@".\IrisSkin4.dll"))
            {
                if (!downloadIrisSkin4())
                {
                    System.Threading.Thread.Sleep(1000);
                    // SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }

            if (!File.Exists(@".\System.Data.SQLite.dll"))
            {
                if (!downloadSqliteDll())
                {
                    System.Threading.Thread.Sleep(1000);
                    //SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }

            if (!File.Exists(@".\SQLite.Interop.dll"))
            {
                if (!downloadSqliteInterop())
                {
                    System.Threading.Thread.Sleep(1000);
                    // SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }

            if (!File.Exists(p.AppFolder + @"\MacOS.ssk"))
            {
                if (!downloadSkin())
                {
                    System.Threading.Thread.Sleep(1000);
                    //SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }

            //if (!p.checkDB(p.dbFile))
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    //SplashForm.CloseSplash();
            //    Environment.Exit(0);
            //}

            if (!File.Exists(p.iniFilePath))
                p.createIniFile(p.iniFilePath);
            p.readIniValue(p.iniFilePath);
            {
                System.Threading.Thread.Sleep(1000);
                // Environment.Exit(0);
            }


#if DEBUG

            if (!p.checkDB(p.dbFile))
            {
                System.Threading.Thread.Sleep(1000);
                //SplashForm.CloseSplash();
                Environment.Exit(0);
            }

            string sql = "REPLACE INTO " + p.DBTable.d_usrlist.ToString() +
                "(" + p.DBKeyValue.usrid.ToString() + "," + p.DBKeyValue.usrpwd.ToString() + "," + p.DBKeyValue.permission.ToString() +
                ") VALUES ('D0805G260','D0805G260','" + p.PermissionKey.administrtor.ToString() + "')";

            p.insertDB(sql);

            Application.Run(new frmMain());
            return;

#else


            bool dbinftp = false;

            if (!File.Exists(p.dbFile))
            {
                string[] _f = FtpHelper.FTPGetFileList(p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder);

                foreach (var item in _f)
                {
                    if (item == "DB.sqlite")
                    {
                        dbinftp = true;
                        break;
                    }
                }

                if (dbinftp)
                    FtpHelper.FTPDownloadFile(p.FtpIP, p.FtpID, p.FtpPassword, p.AppFolder, "DB.sqlite",  p.FtpFolder +@"\DB.sqlite");

                string dblocal = p.AppFolder + @"\" + "DB.sqlite";
                if (File.Exists(dblocal))
                {
                    FileInfo fi = new FileInfo(dblocal);
                    fi.Attributes = FileAttributes.Hidden;
                }
            }

#endif
            System.Threading.Thread.Sleep(1000);

            Application.Run(new frmLogin());


        }


        /// <summary>
        /// download irisskin4.dll
        /// </summary>
        /// <returns></returns>
        private static bool downloadIrisSkin4()
        {
            string filePath = @".\IrisSkin4.dll";
            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.IrisSkin4;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    //  File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }

        private static bool downloadSqliteDll()
        {
            string filePath = @".\System.Data.SQLite.dll";

            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.System_Data_SQLite;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    //  File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }

        private static bool downloadSqliteInterop()
        {
            string filePath = @".\SQLite.Interop.dll";

            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.SQLite_Interop;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    //  File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// download skin file
        /// </summary>
        /// <returns></returns>
        private static bool downloadSkin()
        {
            string filePath = p.AppFolder + @"\MacOS.ssk";

            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.MacOS;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            return true;

        }
    }
}
