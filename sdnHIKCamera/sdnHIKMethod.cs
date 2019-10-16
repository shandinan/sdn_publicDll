/*
 * 此类是对封装的海康sdk方法的逻辑调用
 * 作者：单氐楠
 * 日期：2014-12-05
 * 注意：未经本人切勿修改
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdnHIKCamera
{
    public class sdnHIKMethod
    {
        /// <summary>
        /// 初始化sdk
        /// </summary>
        /// <param name="strMsg">输出结果信息</param>
        /// <returns>-1：初始化失败;1:成功;-3：异常错误</returns>
        public static int sdnInitSDK(out string strMsg)
        {
            InitSDK sdnInit = new InitSDK();
            return sdnInit.InitDll(out strMsg);
        }
        /// <summary>
        /// 摄像头登录
        /// </summary>
        /// <param name="sdnLoginParm">登录参数方法</param>
        /// <param name="chanNums">通道号</param>
        /// <returns>登录用户号</returns>
        public static int sdnLogin(LoginParm sdnLoginParm, out int[] chanNums)
        {
            CameraLogin sdn = new CameraLogin(sdnLoginParm);
            return sdn.LoginDll(out chanNums);
        }
        /// <summary>
        /// 摄像头退出类
        /// </summary>
        /// <param name="sdnLoginParm">参数类</param>
        /// <param name="m_lRealHandle">预览句柄号</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static int sdnLogout(LoginParm sdnLoginParm, int m_lRealHandle, int m_lUserID, out string strMsg)
        {
            CameraLogin sdn = new CameraLogin(sdnLoginParm);
            return sdn.LogoutDll(m_lRealHandle, m_lUserID, out strMsg);
        }
        /// <summary>
        /// 摄像头视频预览
        /// </summary>
        /// <param name="playParm">播放参数</param>
        /// <param name="strMsg">结果信息</param>
        /// <returns>预览句柄</returns>
        public static int sdnStartPreview(PlayVideoParm playParm, out string strMsg)
        {
            PreviewView sdn = new PreviewView(playParm);
            try
            {
                return sdn.PlayVideoDll(out strMsg);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                return -3;
            }
        }
        /// <summary>
        /// 带回掉函数的预览
        /// </summary>
        /// <param name="RealDataFun"></param>
        /// <param name="playParm"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static int sdnStartPreview_CallBack(CHCNetSDK.REALDATACALLBACK RealDataFun, PlayVideoParm playParm, out string strMsg)
        {
            PreviewView sdn = new PreviewView(playParm);
            try
            {
                return sdn.PlayVideoDll_CallBack(RealDataFun, out strMsg);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                return -3;
            }
        }
        /// <summary>
        /// 停止预览
        /// </summary>
        /// <param name="playParm"></param>
        /// <param name="mRealHandle"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static int sdnStopPreview(PlayVideoParm playParm, int mRealHandle, out string strMsg)
        {
            PreviewView sdn = new PreviewView(playParm);
            try
            {
                return sdn.StopPlayVideo(mRealHandle, out strMsg);
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                return -3;
            }
        }

        #region 云台控制
        /// <summary>
        /// 云台控制类，控制方向
        /// </summary>
        /// <returns></returns>
        public static bool sdnPTZControl(sdnPTZControlParam sdnPTZControlParam)
        {
          return   CHCNetSDK.NET_DVR_PTZControlWithSpeed(sdnPTZControlParam.lRealHandle, sdnPTZControlParam.dwPTZCommand, sdnPTZControlParam.dwStop, sdnPTZControlParam.dwSpeed);
        }


        #endregion

    }
}
