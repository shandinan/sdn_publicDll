using Authorizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Decrypt
{
    public partial class Decrypt :sdnControls.sdnSkinForm.SkinForm
    {
        string privateKey = "";
        string publicKey = "";

        public Decrypt()
        {
            InitializeComponent();
            string strPathPrivate = Application.StartupPath+@"\config\private.txt";
            string strPathPublic = Application.StartupPath+@"\config\public.txt";
            using (StreamReader sr1 = new StreamReader(strPathPrivate))
            {
                sr1.Peek();
                privateKey = sr1.ReadToEnd();
                sr1.Close();
            }
            using (StreamReader sr1 = new StreamReader(strPathPublic))
            {
                sr1.Peek();
                publicKey = sr1.ReadToEnd();
                sr1.Close();
            }
        }
        /// <summary>
        /// 生成授权码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
          

            if (string.IsNullOrEmpty(txtClientId.Text))
            {
                MessageBox.Show("客户端标识不能为空");
                return;
            }
            {
                System.Security.Cryptography.RSACryptoServiceProvider p = new System.Security.Cryptography.RSACryptoServiceProvider();
                p.FromXmlString(this.privateKey);//私钥
                var ep = p.ExportParameters(true);

                var d = Convert.ToBase64String(ep.D);
                var n = Convert.ToBase64String(ep.Modulus);
                this.txtSqm.Text = RC2RSA.Encrypt(this.GetClientAuthorization(), d, n); //授权码
            }

            {

                System.Security.Cryptography.RSACryptoServiceProvider p = new System.Security.Cryptography.RSACryptoServiceProvider();
                p.FromXmlString(this.publicKey);
                var ep = p.ExportParameters(false);
                var exponent = Convert.ToBase64String(ep.Exponent);
                var n = Convert.ToBase64String(ep.Modulus);
                this.txtSqMsg.Text = RC2RSA.Decrypt(this.txtSqm.Text, exponent, n);
            }
        }

        /// <summary>
        /// 得到客户端识别码
        /// </summary>
        /// <returns></returns>
        private string GetClientAuthorization()
        {

            StringBuilder authorizationBuilder = new StringBuilder();
            authorizationBuilder.AppendFormat("ClientIdentity:{0}", this.txtClientId.Text).AppendLine(); //客户端识别码
            authorizationBuilder.AppendFormat("FromDate:{0}", this.dtStart.Value.ToString("yyyy/MM/dd")).AppendLine(); //起始时间
            authorizationBuilder.AppendFormat("ToDate:{0}", this.dtEnd.Value.ToString("yyyy/MM/dd")).AppendLine(); //结束时间
            return authorizationBuilder.ToString(); //得到客户端识别码
        }
    }
}
