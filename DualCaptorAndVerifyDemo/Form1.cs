using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LivenessDetection.WindowsSDK;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace DualCaptorAndVerifyDemo
{
    public partial class Form1 : Form
    {
        public string cameraTips = "";
        FaceVerifyWrapper mFaceVerify = new FaceVerifyWrapper();
        public byte[] CapturedPackage = null;
        public byte[] DBImage = null;
        public double compareNum = 0;
        int tempNum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int rtn = 0;
            rtn = mFaceVerify.FaceVerifyInit(".", 2);
            if (rtn == FaceVerifyWrapper.RTN_LICENSE_ERROR)
            {
                MessageBox.Show("请申请license！", "人脸比对SDK");
                this.Close();
            }
            else if (rtn != FaceVerifyWrapper.RTN_SUCC)
            {
                MessageBox.Show("初始化失败！", "人脸比对SDK");
                this.Close();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                    mFaceVerify.FaceVerifyUninit();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void UploadRegistrationPhoto_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.png|All files|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fName = openFileDialog.FileName;
                    Bitmap bmp = new Bitmap(fName);
                    bmp.SetResolution(96.0F, 96.0F);
                    Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
                    Graphics draw = Graphics.FromImage(bmp2);
                    draw.DrawImage(bmp, 0, 0);
                    draw.Dispose();
                    bmp.Dispose();

                    RegistrationPhoto.Image = bmp2;
                    MemoryStream ms = new MemoryStream();
                    RegistrationPhoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    DBImage = ms.GetBuffer();
                    ms.Close();
                    MessageBox.Show("上传成功");
                }
                else
                {
                    RegistrationPhoto.Image = null;
                }

            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 开始人脸捕获
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartCapture_Click_1(object sender, EventArgs e)
        {
            this.axDualCameraLivenessControl1.Initialize(); //控件初始化
            this.axDualCameraLivenessControl1.OpenCamera(); //打开摄像头
            this.axDualCameraLivenessControl1.StartLiveness(); //开始活体检测
            this.axDualCameraLivenessControl1.Show();

        }


        private void axDualCameraLivenessControl2_OnCaptureStatus(object sender, AxDUALCAMERALIVENESSCONTROLLib._DDualCameraLivenessControlEvents_OnCaptureStatusEvent e)
        {
            Thread.Sleep(10);
            if (e.lSessionStatus == 1)
            {
                cameraTips = "捕获成功";
            }
            else
            {
                cameraTips = this.GetTipDualCamera(e.lFrameStatus);
            }
            this.label5.Text = cameraTips;
        }


        private void axDualCameraLivenessControl2_OnCaptureSuccessCallbackHandler(object sender, EventArgs e)
        {

           // byte[] byteFaceImage = Convert.FromBase64String(this.axDualCameraLivenessControl1.CapturedFaceImageContent);
          //  byte[] byteInfredFaceImage = Convert.FromBase64String(this.axDualCameraLivenessControl1.CapturedFaceImageInfContent);
            CapturedPackage = Convert.FromBase64String(this.axDualCameraLivenessControl1.livenessVerificationPackage);
            //VisiblePhoto.Image = Bitmap.FromStream(new MemoryStream(byteFaceImage));
           // InfredPhoto.Image = Bitmap.FromStream(new MemoryStream(byteInfredFaceImage));
            MessageBox.Show("捕获成功");
        }


        public string GetTipDualCamera(int FrameStatus)
        {
            string strRet = "请正对摄像头,并除去遮挡物";
            switch (FrameStatus)
            {
                case 1:
                    strRet = "请正对摄像头,并除去遮挡物";
                    break;
                case 2:
                    strRet = "请保持ATM前中只有一人";
                    break;
                case 300:
                    strRet = "请注视摄像头";
                    break;
                case 401:
                    strRet = "请稍许靠后站立";
                    break;
                case 402:
                    strRet = "请稍许靠前站立";
                    break;
                case 501:
                    strRet = "请摘下眼镜";
                    break;
                case 502:
                    strRet = "请摘下口罩";
                    break;
                case 503:
                    strRet = "请摘下眼镜";
                    break;
                case 600:
                    strRet = "请保持严肃";
                    break;
                case 700:
                    strRet = "请维持人脸静止，不要晃动";
                    break;

                case 0:
                    strRet = "";
                    break;
                default:
                    break;
            }

            return strRet;
        }


        private void StopCapture_Click(object sender, EventArgs e)
        {
            this.axDualCameraLivenessControl1.Hide();
            this.axDualCameraLivenessControl1.StopLiveness();
            this.axDualCameraLivenessControl1.CloseCamera();
        }

        //是否人证一致
        private void Verify_Click(object sender, EventArgs e)
        {
            mFaceVerify.ChangeQueryPackage(CapturedPackage);
            mFaceVerify.ChangeDBImage(DBImage, 1);
            this.tempNum = this.mFaceVerify.CompareDBQuery(ref compareNum);
//            this.tempNum = this.mFaceVerify.ComparePersonPair(,DBImage ref compareNum);
            
            if (compareNum > 66)
            {
                this.label7.Text = "是同一个人";
            }
            else
            {
                this.label7.Text = "不是同一个人";
            }
            this.label8.Text = compareNum.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}
