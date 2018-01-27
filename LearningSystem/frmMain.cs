using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LearningSystem
{
    public partial class frmMain : Form
    {


        #region Help


        //https://www.cnblogs.com/zsi/p/dsoframer-register-on-64-bit-windows.html
        //http://blog.csdn.net/hnsdgxylh/article/details/51162031
        //http://blog.csdn.net/u013457167/article/details/42682569
        //https://www.cnblogs.com/hfzsjz/p/4127867.html
        //https://www.cnblogs.com/liping13599168/archive/2009/09/13/1565801.html
        //http://blog.csdn.net/andrewniu/article/details/54583723



        #endregion



        #region dll註冊

        [DllImport("dsoframer.ocx")]
        public static extern int DllRegisterServer();//注册时用
        [DllImport("dsoframer.ocx")]
        public static extern int DllUnregisterServer();//取消注册时用

        #endregion





        public frmMain()
        {
            InitializeComponent();
            skinEngine1.SkinFile = p.AppFolder + @"\MacOS.ssk";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadUI();
           // DllRegister();
           this.axFramerControl1.Open(@"D:\2018FA動線測試報告-Team2_Ver3.pptx");
          //  webBrowser1.Navigate(@"D:\2018FA動線測試報告-Team2_Ver3.pptx");
        }

        private void loadUI()
        {
            // skinEngine1.SkinFile = p.AppFolder + @"\MacOS.ssk";
            this.Text = "Compare FTP files & DB Files,Ver:" + Application.ProductVersion + "(Edward_song@yeah.net)";
        }





        /// <summary>
        /// 
        /// </summary>
        private void DllRegister()
        {
            string localpath = Application.StartupPath + @"\dsoframer.ocx";
            string targetpath = @"C:\Windows\System32\dsoframer.ocx";
            if (!File.Exists(targetpath))
            {
                File.Copy(localpath, targetpath);
            }

            int i = DllRegisterServer(); //if (i >= 0) { //注册成功! } else { //注册失败} }
            if (i > 0)
            {
                MessageBox.Show("註冊成功");
            }
            else
            {
                MessageBox.Show("註冊失敗");
            }


        }
    }
}
