using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sdnFaceVerify_Test
{
    public partial class FaceVerify : Form
    {

        public string cameraTips = "";
        FaceVerifyWrapper mFaceVerify = new FaceVerifyWrapper();
        public byte[] CapturedPackage = null;
        public byte[] DBImage = null;
        public double compareNum = 0;
        int tempNum = 0;

        public FaceVerify()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 开始捕获
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            sdnLiveness.Initialize();//初始化控件
            sdnLiveness.OpenCamera();//打开摄像头
            sdnLiveness.StartLiveness();//开始活体检测
            sdnLiveness.Show();
        }
        /// <summary>
        /// 拍照完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sdnLiveness_OnCaptureStatus(object sender, AxDUALCAMERALIVENESSCONTROLLib._DDualCameraLivenessControlEvents_OnCaptureStatusEvent e)
        {
            if (e.lSessionStatus == 1)
            {
                cameraTips = "捕获成功";
            }
            else
            {
                cameraTips = this.GetTipDualCamera(e.lFrameStatus);
            }
            this.lbVerify.Text = cameraTips;
        }
        /// <summary>
        /// 回掉函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sdnLiveness_OnCaptureSuccessCallbackHandler(object sender, EventArgs e)
        {
            byte[] byteFaceImage = Convert.FromBase64String(this.sdnLiveness.CapturedFaceImageContent);
            byte[] byteInfredFaceImage = Convert.FromBase64String(this.sdnLiveness.CapturedFaceImageInfContent);
            CapturedPackage = Convert.FromBase64String(this.sdnLiveness.livenessVerificationPackage);
         //   VisiblePhoto.Image = Bitmap.FromStream(new MemoryStream(byteFaceImage));
          //  InfredPhoto.Image = Bitmap.FromStream(new MemoryStream(byteInfredFaceImage));
            MessageBox.Show("捕获成功");
        }
        /// <summary>
        /// 活体检测时摄像头提示
        /// </summary>
        /// <param name="FrameStatus"></param>
        /// <returns></returns>
        public string GetTipDualCamera(int FrameStatus)
        {
            string strRet = "请正对摄像头,并除去遮挡物";
            switch (FrameStatus)
            {
                case 1:
                    strRet = "请正对摄像头,并除去遮挡物";
                    break;
                case 2:
                    strRet = "请保持镜头前中只有一人";
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
        /// <summary>
        /// 停止活体监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopCapture_Click(object sender, EventArgs e)
        {
            sdnLiveness.StartLiveness(); //停止活体检测
            sdnLiveness.CloseCamera();   //关闭摄像头
            sdnLiveness.UnInitialize(); //释放SDK
        }
        /// <summary>
        /// 上传登记照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 人证比对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FaceVerify_Load(object sender, EventArgs e)
        {
            int rtn = 0;
          //  rtn = mFaceVerify.FaceVerifyInit(".", 2);
            rtn = mFaceVerify.FaceVerifyInit("", 2);
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
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FaceVerify_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mFaceVerify.FaceVerifyUninit();
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
