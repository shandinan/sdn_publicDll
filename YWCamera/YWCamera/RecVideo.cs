using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YWCamera
{
    public class RecVideo
    {
        private YWCamreaOper.CameraParam camParm;
        private YWCamreaOper.OperCamera operCamera = null;//定义操作类变量
        private int userNo = 0; //用户自定义消息号
        public RecVideo(YWCamreaOper.CameraParam cp,int UserNum)
        {
            this.camParm = cp;
            operCamera = new YWCamreaOper.OperCamera(camParm);//实例化摄像头操作类
            this.userNo = UserNum;//给用户自定义消息号赋值
        }

        /// <summary>
        /// 1 初始化SDK
        /// </summary>
        private void InitSDK()
        {
            operCamera.ClientInit(userNo);
        }
        /// <summary>
        /// 卸载SDK
        /// </summary>
        private void FreeSDK()
        {
            operCamera.ClientFree();
        }

        //2 预览摄像头
        private void StartRec()
        {

        }
        //3 摄像头录像(如果要拍照启动拍照线程)
        //4 录像停止
        //5 关闭预览
        //6 协助SDK
    }
}
