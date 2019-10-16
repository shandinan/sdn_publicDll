using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sdnKDCamera;

namespace winForm_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                long iErro = 0;
                bool bl = IPCSdk.IPC_InitDll("ipcsdk.dll", 3300, 0, ref iErro);
               
                //string strVersion="";
             //   bl = IPCSdk.IPC_GetVersion(out strVersion, 1000, ref iErro);
               // MessageBox.Show(strVersion);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
