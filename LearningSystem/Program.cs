using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace LearningSystem
{
    static class Program
    {

        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;


        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端 
        }




    //导入判断网络是否连接的 .dll  
      [DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]  
     //判断网络状况的方法,返回值true为连接，false为未连接  
      public extern static bool InternetGetConnectedState(out int conState, int reder); 
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Process instance = RunningInstance();
            if (instance != null)
            {
                HandleRunningInstance(instance);
                Environment.Exit(0);
            }



            int n = 0;  
            if (InternetGetConnectedState(out n, 0))  
            {  
             //  MessageBox.Show("yes");  
            }  
           else 
            {
                MessageBox.Show("Your pc may not connect network,pls check and retry", "Network Invaild", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                System.Threading.Thread.Sleep(1000);
                // SplashForm.CloseSplash();
                Environment.Exit(0);
            }  


   



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


            if (!File.Exists( @".\dsoframer.ocx"))
            {
                if (!downloadDSOFramer ())
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


            if (!RegControl())
            {
                System.Threading.Thread.Sleep(1000);
                //SplashForm.CloseSplash();
                Environment.Exit(0);
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


        private static bool downloadDSOFramer()
        {
            string filePath = @".\dsoframer.ocx";

            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.dsoframer;
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


         public static  bool RegControl()
        {

            try
            {
                Assembly thisExe = Assembly.GetExecutingAssembly();
                //System.IO.Stream myS = thisExe.GetManifestResourceStream("NameSpaceName.dsoframer.ocx");

                string oripath = @".\dsoframer.ocx";

                //string sPath = “该ocx文件的+ @"/dsoframer.ocx";
                string destpath32 = @"C:\Windows\System32\dsoframer.ocx";
                string destpath64 = @"C:\Windows\SysWOW64\dsoframer.ocx";





                if (System.Environment.Is64BitOperatingSystem)
                {

                    if (!File.Exists(destpath64))
                    {
                        File.Copy(oripath, destpath64, true);
                        ProcessStartInfo psi = new ProcessStartInfo("regsvr32", "/s " + destpath64 );
                        //ProcessStartInfo psi = new ProcessStartInfo("regsvr32", " " + oripath);
                        Process.Start(psi);
                    }
                }
                else
                {
                    if (!File.Exists(destpath32))
                    {
                        File.Copy(oripath, destpath32, true);
                        ProcessStartInfo psi = new ProcessStartInfo("regsvr32", "/s " + destpath32);
                        //ProcessStartInfo psi = new ProcessStartInfo("regsvr32", " " + oripath);
                        Process.Start(psi);
                    }
                    
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
      
        }


    }
}
