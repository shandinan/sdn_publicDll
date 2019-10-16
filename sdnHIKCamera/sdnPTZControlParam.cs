/**
 * 海康球机云台控制参数类
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdnHIKCamera
{
    public class sdnPTZControlParam
    {
        private int _lRealHandle;//预览句柄
        private uint _dwPTZCommand;//控制命令
        private uint _dwStop;//动作开始结束 0－开始；1－停止 
        private uint _dwSpeed;//云台控制的速度，用户按不同解码器的速度控制值设置。取值范围[1,7] 
        /// <summary>
        /// 预览句柄
        /// </summary>
        public int lRealHandle { get; set; }
        /// <summary>
        /// 控制命令
        /// </summary>
        public uint dwPTZCommand { get; set; }
        /// <summary>
        /// 动作开始结束 0－开始；1－停止 
        /// </summary>
        public uint dwStop { get; set; }
        /// <summary>
        /// 云台控制的速度，用户按不同解码器的速度控制值设置。取值范围[1,7] 
        /// </summary>
        public uint dwSpeed { get; set; }
    }
}
