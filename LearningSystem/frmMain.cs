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

        #region 窗体放大缩小

        private float X;
        private float Y;

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            try
            {
                foreach (Control con in cons.Controls)
                {

                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                    float a = Convert.ToSingle(mytag[0]) * newx;
                    con.Width = (int)a;
                    a = Convert.ToSingle(mytag[1]) * newy;
                    con.Height = (int)(a);
                    a = Convert.ToSingle(mytag[2]) * newx;
                    con.Left = (int)(a);
                    a = Convert.ToSingle(mytag[3]) * newy;
                    con.Top = (int)(a);
                    Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }


        }

        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            // this.Text = this.Width.ToString() + " " + this.Height.ToString();

        }

        #endregion



        public frmMain()
        {
            InitializeComponent();
            skinEngine1.SkinFile = p.AppFolder + @"\MacOS.ssk";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadUI();
            this.axFramerControl1.Visible = false;
           // DllRegister();
          // this.axFramerControl1.Open(@"D:\2018FA動線測試報告-Team2_Ver3.pptx");
          //  webBrowser1.Navigate(@"D:\2018FA動線測試報告-Team2_Ver3.pptx");
          //  p.checkDB(p.dbFile);

          //  string[] dl = FtpHelper.FTPGetDirectoryList(p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder);
            string[] dl = FtpHelper.FTPGetDetailList(p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder, FtpHelper.FileType.Directorys);
            
           // string[] dl = FtpHelper.FTPGetFileList (p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder);


            if (dl != null )
            {
                foreach (string item in dl)
                {
                    TreeNode node = new TreeNode(item);
                    treePPTList.Nodes.Add(node);
                    treePPTList.SelectedNode = node;

                    string[] childdl = FtpHelper.FTPGetDetailList(p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder + @"\" + item, FtpHelper.FileType.Files);


                    if (childdl != null )
                    {
                        foreach (string fs in childdl)
                        {
                            treePPTList.SelectedNode.Nodes.Add(fs);
                        }
                    }

                }

                treePPTList.ExpandAll();
            }

            grbPPT.Visible = false;



        }

        private void loadUI()
        {
            //窗体放大缩小
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            Form1_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用
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

        private void treePPTList_AfterSelect(object sender, TreeViewEventArgs e)
        {

            string ftpparentfolder = string.Empty;
            TreeNode selNode = treePPTList.SelectedNode;
            if (selNode == null)
            {
                //防止空引用
                return;
            }
            if (selNode.Parent != null)
            {
                //为有父亲节点的,
                TreeNode parentNode = selNode.Parent;//得到父亲节点

               // MessageBox.Show (parentNode.Text);
                ftpparentfolder = p.FtpFolder + @"\" + parentNode.Text;

                if (selNode.Nodes.Count == 0)
                {
                    //为没有子节点，即选中的节点为叶子节点
                   // MessageBox.Show(selNode.Text);
                    grbPPT.Visible = true;

                    string destfile = p.PPTFolder + @"\" + selNode.Text;


                    if (!File.Exists(destfile))
                    {
                        FtpHelper.FTPDownloadFile(p.FtpIP, p.FtpID, p.FtpPassword, p.PPTFolder, selNode.Text, ftpparentfolder + @"\" + selNode.Text);
                        System.IO.FileInfo fi = new FileInfo(p.PPTFolder + @"\" + selNode.Text);
                        fi.Attributes = FileAttributes.Hidden;
                    }                    

                    //MessageBox.Show(p.AppFolder + @"\" + selNode.Text);
                    axFramerControl1.Open(p.PPTFolder + @"\" + selNode.Text);


                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are u sure to exit?", "Exit or Not", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DirectoryInfo di = new DirectoryInfo(p.PPTFolder);
                foreach (var item in di.GetFiles ())
                {
                    File.Delete(item.FullName);
                }

                Environment.Exit(0);
            }
            if (dr == DialogResult.No)
                e.Cancel = true;
        }
    }
}
