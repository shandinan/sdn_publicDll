/*
 * 此类是摄像头截图参数类
 * 作者：单氐楠
 * 日期：2014-12-05
 * 注意：未经本人同意切换修改
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdnHIKCamera
{
    public class CaptureParms
    {
        // 通道号 默认为1
        private int _iChannelNum;
        /// <summary>
        /// 通道号 默认为1
        /// </summary>
        public int iChannelNum
        {
            set { this._iChannelNum = value; }
            get { return _iChannelNum; }
        }
        private int _m_lUserID;
        /// <summary>
        /// 视频登录ID
        /// </summary>
        public int m_lUserID
        {
            set { this._m_lUserID = value; }
            get { return _m_lUserID; }
        }
        private string _strFilePath;
        /// <summary>
        /// 文件存放路径
        /// </summary>
        public string strFilePath
        {
            set { this._strFilePath = value; }
            get { return this._strFilePath; }
        }

        private string _strFileName;
        /// <summary>
        /// 保存图片文件名
        /// </summary>
        public string strFileName
        {
            set { this._strFileName = value; }
            get { return _strFileName; }
        }
    }
}
