/**
 * 功能：亿维定制型号（新）c++封装为托管语音.net 
 * 单氐楠  2017年1月4日09:10:04
 * 此类对主要是为了让.net调用非托管c++编写的dll,应用于昆山一检测线项目中
 * 网络模块,提供和编码器、解码器交互接口,设置及控制设备
 * 该文件包含六部分：一、宏定义；二、枚举类型定义；三、回调函数；四、接口结构类型定义；五、函数接口定义；六、数字视频服务器的配置信息结构
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NewYWCameraOper
{
    public class HHNetInterface
    {
        #region 系统常量
        public const int MAX_VIDEO_NUM = 4;//数字视频服务器最大通道数
        public const int MAX_IP_NAME_LEN = 128;	//输入IP或名字的最大长度
        public const int DVS_NAME_LEN = 32;//数字视频服务器名称长度

        #endregion


        #region 枚举
        /***
         * * 错误码
         * */
        public enum HHERR_CODE
        {
            HHERR_SUCCESS,					//操作成功
            HHERR_FAILURE,					//操作失败
            HHERR_REFUSE_REQ,				//请求被拒绝
            HHERR_USER_FULL,				//登录用户已满
            HHERR_PREVIEW_FULL,				//预览用户已满
            HHERR_TASK_FULL,				//系统任务繁忙，待会尝试连接
            HHERR_CHANNEL_NOT_EXIST,		//要打开的通道不存在或已满
            HHERR_DEVICE_NAME,				//打开的设备不存在
            HHERR_IS_TALKING,				//正在对讲
            HHERR_QUEUE_FAILUE,				//队列出错
            HHERR_USER_PASSWORD,			//用户名或密码和系统不匹配
            HHERR_SHARE_SOCKET,				//socket 错误
            HHERR_RELAY_NOT_OPEN,			//转发请求的服务还未打开
            HHERR_RELAY_MULTI_PORT,			//转发多播端口错误
            HHERR_VIEWPUSH_CHANNEL_USING,	//视频输入的通道已经被占用
            HHERR_VIEWPUSH_DECODE_TYPE,		//视频输入通道的解码格式错误，0通道(4cif,2cif,cif),1通道(2cif,cif),2通道(cif),3通道(cif)
            HHERR_AUTO_LINK_FAILURE,		//转发的自动连接失败
            HHERR_NOT_LOGON,				//设备还未登录
            HHERR_IS_SETTING,				//设备正在设置
            HHERR_COMMAND_FAILURE,			//命令失败
            HHERR_RESTRICT_ID,				//ID使用受限
            HHERR_INVALID_PARAMETER = 100,	//输入参数无效
            HHERR_LOGON_FAILURE,			//登录失败
            HHERR_TIME_OUT,					//操作超时
            HHERR_SOCKET_ERR,				//SOCKET错误
            HHERR_NOT_LINKSERVER,			//还未连接服务器
            HHERR_BUFFER_EXTCEED_LIMIT,		//使用缓冲超过限制	
            HHERR_LOW_PRIORITY,				//操作权限不足
            HHERR_BUFFER_SMALL,				//缓冲太小
            HHERR_IS_BUSY,					//系统任务正忙
            HHERR_UPDATE_FILE,				//升级文件错误
            HHERR_UPDATE_UNMATCH,			//升级文件和机器不匹配
            HHERR_PORT_INUSE,				//端口被占用
            HHERR_RELAY_DEVICE_EXIST,		//设备名已经存在
            HHERR_CONNECT_REFUSED,			//连接时被拒绝
            HHERR_PROT_NOT_SURPPORT,		//不支持该协议
            HHERR_FILE_OPEN_ERR,            //打开文件失败
            HHERR_FILE_SEEK_ERR,            //fseek失败 
            HHERR_FILE_WRITE_ERR,           //fwrite失败 
            HHERR_FILE_READ_ERR,            //fread失败 
            HHERR_FILE_CLOSING,             //正在关闭文件 
            HHERR_ALLOC_BUF_ERR
        }

        public enum NET_PROTOCOL_TYPE
        {
            NET_PROTOCOL_TCP = 0,			//TCP协议
            NET_PROTOCOL_UDP = 1,			//UDP协议
            NET_PROTOCOL_MULTI = 2,			//多播协议
        }

        public enum ENCODE_VIDEO_TYPE
        {
            EV_TYPE_NONE = 0xFFFF,
            EV_TYPE_PAL_D1 = 0x00,		//PAL制D1		704 * 576
            EV_TYPE_PAL_HD1 = 0x01,		//PAL制HalfD1	704 * 288
            EV_TYPE_PAL_CIF = 0x02,		//PAL制CIF		352 * 288
            EV_TYPE_VGA = 0x03,		//VGA			640 * 480
            EV_TYPE_HVGA = 0x04,		//HVGA			640 * 240
            EV_TYPE_CVGA = 0x05,		//CVGA			320 * 240
            EV_TYPE_PAL_QCIF = 0x06,		//PAL制QCIF		176 * 144
            EV_TYPE_QVGA = 0x07,		//QVGA			160 * 120
            EV_TYPE_NTSC_D1 = 0x08,		//N制D1			704 * 480
            EV_TYPE_NTSC_HD1 = 0x09,		//N制HalfD1		704 * 240
            EV_TYPE_NTSC_CIF = 0x0A,		//N制CIF		352 * 240
            EV_TYPE_NTSC_QCIF = 0x0E,		//N制QCIF		176 * 120

            //H.264
            EV_H264_PAL_D1 = 0x10,		//H264_2,PAL制D1		704 * 576
            EV_H264_PAL_HD1 = 0x11,		//H264_2,PAL制HalfD1	704 * 288
            EV_H264_PAL_CIF = 0x12,		//H264_2,PAL制CIF		352 * 288
            EV_H264_VGA = 0x13,		//H264_2,VGA			640 * 480
            EV_H264_HVGA = 0x14,		//H264_2,HVGA			640 * 240
            EV_H264_CVGA = 0x15,		//H264_2,CVGA			320 * 240
            EV_H264_PAL_QCIF = 0x16,		//H264_2,PAL制QCIF		176 * 144
            EV_H264_QVGA = 0x17,		//H264_2,QVGA			160 * 120
            EV_H264_NTSC_D1 = 0x18,		//H264_2,N制D1			704 * 480
            EV_H264_NTSC_HD1 = 0x19,		//H264_2,N制HalfD1		704 * 240
            EV_H264_NTSC_CIF = 0x1A,		//H264_2,N制CIF			352 * 240
            EV_H264_NTSC_QCIF = 0x1E,		//H264_2,N制QCIF		176 * 120

            //标准MPEG4
            EV_MPEG4_PAL_D1 = 0x20,		//PAL制D1				704 * 576
            EV_MPEG4_PAL_HD1 = 0x21,		//PAL制HalfD1			704 * 288
            EV_MPEG4_PAL_CIF = 0x22,		//PAL制CIF				352 * 288
            EV_MPEG4_VGA = 0x23,		//VGA					640 * 480
            EV_MPEG4_HVGA = 0x24,		//HVGA					640 * 240
            EV_MPEG4_CVGA = 0x25,		//CVGA					320 * 240
            EV_MPEG4_PAL_QCIF = 0x26,		//PAL制QCIF				176 * 144
            EV_MPEG4_QVGA = 0x27,		//QVGA					160 * 120
            EV_MPEG4_NTSC_D1 = 0x28,		//N制D1					704 * 480
            EV_MPEG4_NTSC_HD1 = 0x29,		//N制HalfD1				704 * 240
            EV_MPEG4_NTSC_CIF = 0x2A,		//N制CIF				352 * 240
            EV_MPEG4_NTSC_QCIF = 0x2E,		//N制QCIF				176 * 120

            //MJPEG
            EV_MJPEG_PAL_D1 = 0x30,     //MJPEG,PAL制D1        704 * 576
            EV_MJPEG_PAL_HD1 = 0x31,     //MJPEG,PAL制HalfD1    704 * 288
            EV_MJPEG_PAL_CIF = 0x32,     //MJPEG,PAL制CIF       352 * 288
            EV_MJPEG_VGA = 0x33,     //MJPEG,VGA            640 * 480
            EV_MJPEG_HVGA = 0x34,     //MJPEG,HVGA           640 * 240
            EV_MJPEG_CVGA = 0x35,     //MJPEG,CVGA           320 * 240
            EV_MJPEG_PAL_QCIF = 0x36,     //MJPEG,PAL制QCIF      176 * 144
            EV_MJPEG_QVGA = 0x37,     //MJPEG,QVGA           160 * 120
            EV_MJPEG_NTSC_D1 = 0x38,     //MJPEG,N制D1          704 * 480
            EV_MJPEG_NTSC_HD1 = 0x39,     //MJPEG,N制HalfD1      704 * 240
            EV_MJPEG_NTSC_CIF = 0x3A,     //MJPEG,N制CIF         352 * 240
            EV_MJPEG_NTSC_QCIF = 0x3E,     //MJPEG,N制QCIF        176 * 120

            //JPEG
            EV_JPEG_PAL_D1 = 0x40,     //JPEG,PAL制D1        704 * 576
            EV_JPEG_PAL_HD1 = 0x41,     //JPEG,PAL制HalfD1    704 * 288
            EV_JPEG_PAL_CIF = 0x42,     //JPEG,PAL制CIF       352 * 288
            EV_JPEG_VGA = 0x43,     //JPEG,VGA            640 * 480
            EV_JPEG_HVGA = 0x44,     //JPEG,HVGA           640 * 240
            EV_JPEG_CVGA = 0x45,     //JPEG,CVGA           320 * 240
            EV_JPEG_PAL_QCIF = 0x46,     //JPEG,PAL制QCIF      176 * 144
            EV_JPEG_QVGA = 0x47,     //JPEG,QVGA           160 * 120
            EV_JPEG_NTSC_D1 = 0x48,     //JPEG,N制D1          704 * 480
            EV_JPEG_NTSC_HD1 = 0x49,     //JPEG,N制HalfD1      704 * 240
            EV_JPEG_NTSC_CIF = 0x4A,     //JPEG,N制CIF         352 * 240
            EV_JPEG_NTSC_QCIF = 0x4E,     //JPEG,N制QCIF        176 * 120

            //
            EA_G722_S16B16C1 = 0x0100,	//音频，G722
            EA_G711A_S16B16C1 = 0x0200,	//音频，G711A
            EA_G711MU_S16B16C1 = 0x0300,	//音频，G711MU
            EA_ADPCM_S16B16C1 = 0x0400,	//音频，ADPCM
            EA_G726_S16B16C1 = 0x0500,	//音频，G726
            EA_BUTT_S16B16C1 = 0x0600,	//音频，BUTT
            EA_MPT_S16B16C1 = 0x0700,	//音频，MPT
        }
        /// <summary>
        /// 链接状态
        /// </summary>
        public enum CONNECT_STATUS
        {
            CONNECT_STATUS_NONE,			//未连接
            CONNECT_STATUS_OK,				//已经连接
            CONNECT_STATUS_DATA,
            CONNECT_STATUS_EXIST,
        }
        /// <summary>
        /// 设置、获取DVS参数及控制命令
        /// </summary>
        public enum HHCMD_NET 
        {
            //编码器命令 

         HHCMD_GET_ALL_PARAMETER,          //0. 得到所有编码器参数 

         HHCMD_SET_DEFAULT_PARAMETER,      //1. 恢复所有编码器默认参数 

         HHCMD_SET_RESTART_DVS,            //2. 重启编码器 

         HHCMD_GET_SYS_CONFIG,             //3. 获取系统设置 

         HHCMD_SET_SYS_CONFIG,             //4. 设置系统设置 

         HHCMD_GET_TIME,                   //5. 获取编码器时间 

         HHCMD_SET_TIME,                   //6. 设置编码器时间 

         HHCMD_GET_AUDIO_CONFIG,           //7. 获取音频设置 

         HHCMD_SET_AUDIO_CONFIG,           //8. 设置音频设置 

         HHCMD_GET_VIDEO_CONFIG,           //9. 获取视频设置 

         HHCMD_SET_VIDEO_CONFIG,           //10.设置视频设置 

         HHCMD_GET_VMOTION_CONFIG,         //11.获取移动侦测设置 

         HHCMD_SET_VMOTION_CONFIG,         //12.设置移动侦测设置 

         HHCMD_GET_VMASK_CONFIG,           //13.获取图像屏蔽设置 

         HHCMD_SET_VMASK_CONFIG,           //14.设置图像屏蔽设置 

         HHCMD_GET_VLOST_CONFIG,           //15.获取视频丢失设置 

         HHCMD_SET_VLOST_CONFIG,           //16.设置视频丢失设置 

         HHCMD_GET_SENSOR_ALARM,           //17.获取探头报警侦测设置 

         HHCMD_SET_SENSOR_ALARM,           //18.设置探头报警侦测设置 

         HHCMD_GET_USER_CONFIG,            //19.获取用户设置 

         HHCMD_SET_USER_CONFIG,            //20.设置用户设置 

         HHCMD_GET_NET_CONFIG,             //21.获取网络设置结构 

         HHCMD_SET_NET_CONFIG,             //22.设置网络设置结构 

         HHCMD_GET_COM_CONFIG,             //23.获取串口设置 

         HHCMD_SET_COM_CONFIG,             //24.设置串口设置 

         HHCMD_GET_YUNTAI_CONFIG,          //25.获取内置云台信息 

         HHCMD_SET_YUNTAI_CONFIG,          //26.设置内置云台信息 

         HHCMD_GET_VIDEO_SIGNAL_CONFIG,    //27.获取视频信号参数（亮度、色度、对比度、饱和度） 

         HHCMD_SET_VIDEO_SIGNAL_CONFIG,    //28.设置视频信号参数（亮度、色度、对比度、饱和度） 

         HHCMD_SET_PAN_CTRL,               //29.云台控制 

         HHCMD_SET_COMM_SENDDATA,          //30.透明数据传输 

         HHCMD_SET_COMM_START_GETDATA,     //31.开始采集透明数据 

         HHCMD_SET_COMM_STOP_GETDATA,      //32.停止采集透明数据 

         HHCMD_SET_OUTPUT_CTRL,            //33.继电器控制 

         HHCMD_SET_PRINT_DEBUG,            //34.调试信息开关 

         HHCMD_SET_ALARM_CLEAR,            //35.清除报警 

         HHCMD_GET_ALARM_INFO,             //36.获取报警状态和继电器状态 

         HHCMD_SET_TW2824,                 //37.设置多画面芯片参数(保留) 

         HHCMD_SET_SAVE_PARAM,              //38.设置保存参数 

         HHCMD_GET_USERINFO,               //39.获取当前登陆的用户信息 

         HHCMD_GET_DDNS,                   //40.获取DDNS 

         HHCMD_SET_DDNS,                   //41.设置DDNS 

         HHCMD_GET_CAPTURE_PIC,            //42.前端抓拍 

         HHCMD_GET_SENSOR_CAP,             //43.获取触发抓拍设置 

         HHCMD_SET_SENSOR_CAP,             //44.设置触发抓拍设置 

         HHCMD_GET_EXTINFO,                //45.获取扩展配置 

         HHCMD_SET_EXTINFO,                //46.设置扩展配置 

         HHCMD_GET_USERDATA,               //47.获取用户配置 

         HHCMD_SET_USERDATA,               //48.设置用户配置 

         HHCMD_GET_NTP,                    //49.获取NTP配置 

         HHCMD_SET_NTP,                    //50.设置NTP配置 

         HHCMD_GET_UPNP,                   //51.获取UPNP配置 

         HHCMD_SET_UPNP,                   //52.设置UPNP配置 

         HHCMD_GET_MAIL,                   //53.获取MAIL配置 

         HHCMD_SET_MAIL,                   //54.设置MAIL配置 

         HHCMD_GET_ALARMNAME,              //55.获取报警名配置 

         HHCMD_SET_ALARMNAME,              //56.设置报警名配置 

         HHCMD_GET_WFNET,                  //57.获取无线网络配置 

         HHCMD_SET_WFNET,                  //58.设置无线网络配置 

         HHCMD_GET_SEND_DEST,              //59.设置视频定向发送目标机 

         HHCMD_SET_SEND_DEST,              //60.设置视频定向发送目标机 

         HHCMD_GET_AUTO_RESET,             //61.取得定时重新注册
         HHCMD_SET_AUTO_RESET,             //62.设置定时重新注册
         HHCMD_GET_REC_SCHEDULE,           //63.取得录像策略
         HHCMD_SET_REC_SCHEDULE,           //64.设置录像策略
         HHCMD_GET_DISK_INFO,              //65.取得磁盘信息
         HHCMD_SET_MANAGE,                 //66.设置命令和操作
         HHCMD_GET_CMOS_REG,               //67.取得CMOS参数
         HHCMD_SET_CMOS_REG,               //68.设置CMOS参数
         HHCMD_SET_SYSTEM_CMD,             //69.设置执行命令
         HHCMD_SET_KEYFRAME_REQ,           //70.设置关键帧请求
         HHCMD_GET_CONFIGENCPAR,           //71.取得视频参数
         HHCMD_SET_CONFIGENCPAR,           //72.设置视频参数
         //------------------------------------------------------------

         //下面这些命令是在98系列、58系列及之后的设备使用的新命令

         //98系列、58系列及之前的设备不支持这些新命令

         //在98系列、58系列及之后的设备在获取设置以下几种参数时请切记使用新命令和新结构

         //A. 视频参数

         //B. 移动报警参数

         //C. 视频丢失报警参数

         //D. 探头报警参数

         //------------------------------------------------------------
         HHCMD_GET_ALL_PARAMETER_NEW,      //73.获取所有参数
         HHCMD_FING_LOG,                   //74.查找日志(查询方式:0－全部，1－按类型，2－按时间，3－按时间和类型 0xFF-关闭本次搜索)
         HHCMD_GET_LOG,                    //75.读取查找到的日志 
         HHCMD_GET_SUPPORT_AV_FMT,         //76.获取设备支持的编码格式、宽高及音频格式
         HHCMD_GET_VIDEO_CONFIG_NEW,       //77.获取视频参数－new
         HHCMD_SET_VIDEO_CONFIG_NEW,       //78.设置视频参数－new
         HHCMD_GET_VMOTION_CONFIG_NEW,     //79.获取移动报警参数－new
         HHCMD_SET_VMOTION_CONFIG_NEW,     //80.设置移动报警参数－new
         HHCMD_GET_VLOST_CONFIG_NEW,       //81.获取视频丢失报警参数－new
         HHCMD_SET_VLOST_CONFIG_NEW,       //82.设置视频丢失报警参数－new
         HHCMD_GET_SENSOR_ALARM_NEW,       //83.获取探头报警参数－new
         HHCMD_SET_SENSOR_ALARM_NEW,       //84.设置探头报警参数－new
         HHCMD_GET_NET_ALARM_CONFIG,       //85.获取网络故障报警参数
         HHCMD_SET_NET_ALARM_CONFIG,       //86.设置网络故障报警参数
         HHCMD_GET_RECORD_CONFIG,          //87.获取定时录像参数
         HHCMD_SET_RECORD_CONFIG,          //88.定时录像参数
         HHCMD_GET_SHOOT_CONFIG,           //89.获取定时抓拍参数
         HHCMD_SET_SHOOT_CONFIG,           //90.设置定时抓拍参数
         HHCMD_GET_FTP_CONFIG,             //91.获取FTP参数
         HHCMD_SET_FTP_CONFIG,             //92.设置FTP参数
         HHCMD_GET_RF_ALARM_CONFIG,        //93.获取无线报警参数
         HHCMD_SET_RF_ALARM_CONFIG,        //94.设置无线报警参数
         HHCMD_GET_EXT_DATA_CONFIG,        //95.获取其它扩展参数(如平台设置其它参数)
         HHCMD_SET_EXT_DATA_CONFIG,        //96.设置其它扩展参数(如平台设置其它参数)
         HHCMD_GET_FORMAT_PROCESS,         //97.获取硬盘格式化进度
         HHCMD_GET_PING_CONFIG,            //98.PING 设置获取
         HHCMD_SET_PING_CONFIG,            //99.PING 设置设置

         //解码器命令 

         DDCMD_GET_ALL_PARAMETER = 100,    //获取解码器所有设置 

         DDCMD_GET_TIME,                   //获取系统时间 

         DDCMD_SET_TIME,                   //设置系统时间 

         DDCMD_GET_SYS_CONFIG,             //获取系统配置 

         DDCMD_SET_SYS_CONFIG,             //设置系统配置 

         DDCMD_GET_NET_CONFIG,             //获取网络配置

         DDCMD_SET_NET_CONFIG,             //设置网络配置 

         DDCMD_GET_COM_CONFIG,             //获取串口配置 

         DDCMD_SET_COM_CONFIG,             //设置串口配置 

         DDCMD_GET_VIDEO_CONFIG,           //获取视频配置 

         DDCMD_SET_VIDEO_CONFIG,           //设置视频配置 

         DDCMD_GET_ALARM_OPT,              //获取报警选项 

         DDCMD_SET_ALARM_OPT,              //设置报警选项 

         DDCMD_GET_USER_INFO,              //获取用户设置信息 

         DDCMD_SET_USER_INFO,              //设置用户设置信息 

         DDCMD_GET_ALARM_RECORD,           //获取报警记录信息 

         DDCMD_GET_ADRRESS_BOOK,           //获取地址薄配置 

         DDCMD_SET_ADRRESS_BOOK,           //设置地址薄配置 

         DDCMD_SET_COMM,                   //设置发送串口数据 

         DDCMD_SET_CMD,                    //设置透明的命令 

         DDCMD_GET_YUNTAI_INFO,            //获取云台信息 

         DDCMD_GET_YUNTAI_CONFIG,          //获取云台配置 

         DDCMD_SET_YUNTAI_CONFIG,          //设置云台配置 

         DDCMD_GET_ONELINK_ADDR,           //获取解码器单路连接的信息 

         DDCMD_SET_ONELINK_ADDR,           //设置解码器单路连接的信息 

         DDCMD_GET_CYCLELINK_ADDR,         //获取解码器循环连接的信息 

         DDCMD_SET_CYCLELINK_ADDR,         //设置解码器循环连接的信息 

         DDCMD_GET_EXTINFO,                //获取扩展配置 

         DDCMD_SET_EXTINFO,                //设置扩展配置 

         DDCMD_GET_NTP,                    //获取NTP配置 

         DDCMD_SET_NTP,                    //设置NTP配置 

         DDCMD_GET_UPNP,                   //获取UPNP配置 

         DDCMD_SET_UPNP,                   //设置UPNP配置 

         DDCMD_GET_MAIL,                   //获取MAIL配置 

         DDCMD_SET_MAIL,                   //设置MAIL配置 

         DDCMD_GET_ALARMNAME,              //获取报警名配置 

         DDCMD_SET_ALARMNAME,              //设置报警名配置 

         DDCMD_GET_WFNET,                  //获取无线网络配置 

         DDCMD_SET_WFNET,                  //设置无线网络配置 

         DDCMD_GET_SEND_DEST,              //设置视频定向发送目标机 

         DDCMD_SET_SEND_DEST,              //设置视频定向发送目标机 


        }

        #endregion

        #region 结构体

        public delegate int ChannelStreamCallback(IntPtr hOpenChannel, IntPtr pStreamData, uint dwClientID, IntPtr pContext, ENCODE_VIDEO_TYPE encodeVideoType, IntPtr pAVInfo);
        /// <summary>
        /// 抓拍图片回调
        /// </summary>
        public delegate int PictureCallback(IntPtr hPictureChn, IntPtr pPicData, int nPicLen, uint dwClientID, IntPtr pContext);
        /// <summary>
        /// 打开视频通道参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HHOPEN_CHANNEL_INFO
        {
            public uint dwClientID;
            public ushort nOpenChannel;
            public NET_PROTOCOL_TYPE protocolType;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public ChannelStreamCallback funcStreamCallback;
            public IntPtr pCallbackContext;
        }
        /// <summary>
        /// 音视频参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HHAV_INFO
        {
            //视频参数
            public uint nVideoEncodeType;		//视频编码格式
            public uint nVideoHeight;			//视频图像高
            public uint nVideoWidth;			//视频图像宽
            //音频参数
            public uint nAudioEncodeType;		//音频编码格式
            public uint nAudioChannels;			//通道数
            public uint nAudioBits;				//位数
            public uint nAudioSamples;			//采样率
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HHOPEN_PICTURE_INFO
        {
            public uint dwClientID;
            public uint nOpenChannel;
            public NET_PROTOCOL_TYPE protocolType;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public PictureCallback funcPictureCallback;
            public IntPtr pCallbackContext;

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HH_CHANNEL_INFO
        {
            public IntPtr hOpenChannel;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szServerIP;
            public uint nServerPort;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szDeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string szUserName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string szUserPassword;
            public uint dwClientID;
            public CONNECT_STATUS openStatus;
            public uint nVersion;
            public uint nOpenID;
            public uint nPriority;
            public uint nOpenChannelNo;
            public uint nMulticastAddr;
            public uint nMulticastPort;
            public HHAV_INFO avInformation;
            public ENCODE_VIDEO_TYPE encodeVideoType;
            public NET_PROTOCOL_TYPE protocolType;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public ChannelStreamCallback funcStreamCallback;
            public IntPtr pCallbackContext;
            public uint dwDeviceID;	//V4.0 add
        }
        /// <summary>
        /// 音视频数据帧头
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HV_FRAME_HEAD
        {
            public short zeroFlag;//固定为0
            public char oneFlag;//固定为1
            public char streamFlag;//帧标示 0x0b视频P帧  0x0e视频I帧  0x0d音频数据
            public int nByteNum;//音视频数据长度，不包括帧头
            public int nTimestamp;//时间戳
        }
        /// <summary>
        /// 打开图片通道的信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HH_PICTURE_INFO
        {
            public IntPtr hOpenChannel;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szServerIP;
            public uint nServerPort;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szDeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string szUserName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string szUserPassword;
            public uint dwClientID;
            public CONNECT_STATUS openStatus;
            public uint nVersion;
            public uint nOpenID;
            public uint nPriority;
            public uint nOpenChannelNo;
            public uint nMulticastAddr;
            public uint nMulticastPort;
            public uint nPicWidth;
            public uint nPicHeight;
            public uint nPicBits;
            public char picFormatType;//0:JPEG,1:BMP
            public NET_PROTOCOL_TYPE protocolType;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public PictureCallback funcStreamCallback;
            public IntPtr pCallbackContext;
            public uint dwDeviceID;
        }


        #endregion

        #region 函数接口定义

        #region 服务开关及服务信息回掉
        /// <summary>
        /// 服务开启 //启动服务
        /// </summary>
        /// <param name="hNotifyWnd">设置接收SDK回送消息的窗口句柄</param>
        /// <param name="nCommandID">设置接收SDK回送消息的ID</param>
        /// <param name="dwFrameBufNum">视频BUFFER的大小，0为默认</param>
        /// <param name="bReadyRelay">是否准备转发</param>
        /// <param name="bReadyCenter">工作模式：是否准备中心模式</param>
        /// <param name="pLocalAddr">绑定地址：本地Ip地址</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_Startup(IntPtr hNotifyWnd, uint nCommandID, uint dwFrameBufNum = 0, bool bReadyRelay = false, bool bReadyCenter = false, string pLocalAddr = null);
        /// <summary>
        /// 关闭网络服务
        /// </summary>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_Cleanup();
        /// <summary>
        ///  库消息回调函数
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate int MessageNotifyCallback(uint wParam, int lParam);
        /// <summary>
        /// 改变通知消息为回调函数的方式
        /// </summary>
        /// <param name="pCallback">消息回调</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_MessageCallback(MessageNotifyCallback pCallback);
        #endregion

        #region 摄像头操作
        /// <summary>
        /// 登录服务器
        /// </summary>
        /// <param name="pServerIP">服务器地址URL(IP、域名)</param>
        /// <param name="wServerPort">通讯端口号</param>
        /// <param name="pDeviceName">DVS设备名称(转发时使用)</param>
        /// <param name="pUserName">登陆DVS使用的用户名</param>
        /// <param name="pUserPassword">登陆DVS使用的密码</param>
        /// <param name="dwClientID">回调参数(可用做连接号等，如：当非正常关闭时，应用程序可知道是哪个连接断开了)</param>
        /// <param name="hLogonServer">登录DVS返回的句柄</param>
        /// <param name="hNotifyWindow">消息通知的窗口句柄，默认通知窗口为HHNET_Startup 函数中hNotifyWnd参数句柄</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_LogonServer(string pServerIP, ushort nServerPort, string pDeviceName, string pUserName, string pUserPassword, uint dwClientID, ref IntPtr hLogonServer, IntPtr hNotifyWindow);
        /// <summary>
        /// 注销与服务器的连接
        /// </summary>
        /// <param name="hServer">登录时返回的句柄</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_LogoffServer(IntPtr hServer);

        /// <summary>
        /// 打开音视频通道
        /// </summary>
        /// <param name="pServerIP">服务器地址URL</param>
        /// <param name="wServerPort">通讯端口号</param>
        /// <param name="pDeviceName">DVS设备名称(转发时使用)</param>
        /// <param name="pUserName">登陆DVS使用的用户名</param>
        /// <param name="pUserPassword">登陆DVS使用的密码</param>
        /// <param name="pOpenInfo">打开通道的参数(参见HHOPEN_CHANNEL_INFO定义)</param>
        /// <param name="hOpenChannel">返回的通道句柄</param>
        /// <param name="hNotifyWindow">消息通知的窗口句柄</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_OpenChannel(string pServerIP, ushort wServerPort, string pDeviceName, string pUserName, string pUserPassword, IntPtr pOpenInfo, ref IntPtr hOpenChannel, IntPtr hNotifyWindow);
        /// <summary>
        /// 关闭视频通道
        /// </summary>
        /// <param name="hOpenChannel">已打开的通道句柄</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_CloseChannel(IntPtr hOpenChannel);
        /// <summary>
        /// 读取打开的音视频通道信息
        /// </summary>
        /// <param name="hOpenChannel"></param>
        /// <param name="channelInfo"></param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_ReadChannelInfo(IntPtr hOpenChannel, ref  HH_CHANNEL_INFO channelInfo); //HH_CHANNEL_INFO
        /// <summary>
        /// 初始文件生成
        /// </summary>
        /// <returns></returns>
        [DllImport("HHReadWriterSDK.dll")]
        public static extern IntPtr HHFile_InitWriter();
        /// <summary>
        /// 释放文件
        /// </summary>
        /// <param name="hWriter"></param>
        /// <returns></returns>
        [DllImport("HHReadWriterSDK.dll")]
        public static extern int HHFile_ReleaseWriter(IntPtr hWriter);
        /// <summary>
        /// 设置缓冲区大小
        /// </summary>
        /// <param name="hWriter">打开文件句柄</param>
        /// <param name="lBufferSize">缓冲区大小</param>
        /// <returns></returns>
        [DllImport("HHReadWriterSDK.dll")]
        public static extern int HHFile_SetCacheBufferSize(IntPtr hWriter, int lBufferSize);
        /// <summary>
        /// 开始录像，或正在录像时开启新的的录像
        /// </summary>
        /// <param name="hWriter">指向文件生成的句柄</param>
        /// <param name="strFileName">文件名</param>
        /// <returns></returns>
        [DllImport("HHReadWriterSDK.dll")]
        public static extern int HHFile_StartWrite(IntPtr hWriter, string strFileName);
        /// <summary>
        /// 写入帧数据
        /// </summary>
        /// <param name="hWriter"></param>
        /// <param name="pFrame"></param>
        /// <param name="lFrameSize"></param>
        /// <param name="dwEncType"></param>
        /// <returns></returns>
        [DllImport("HHReadWriterSDK.dll")]
        public static extern int HHFile_InputFrame(IntPtr hWriter, byte[] pFrame, int lFrameSize, uint dwEncType);

        /// <summary>
        /// 关闭写文件
        /// </summary>
        /// <param name="hWriter"></param>
        /// <returns></returns>
        [DllImport("HHReadWriterSDK.dll")]
        public static extern int HHFile_StopWrite(IntPtr hWriter);

        /// <summary>
        ///  打开前端设备抓图服务
        /// </summary>
        /// <param name="pServerIP">服务器地址URL(IP、域名) </param>
        /// <param name="wServerPort">通讯端口号</param>
        /// <param name="pDeviceName">DVS设备名称</param>
        /// <param name="pUserName">用户名</param>
        /// <param name="pUserPassword">密码</param>
        /// <param name="pOpenInfo">抓拍参数</param>
        /// <param name="hOpenChannel">打开的通道</param>
        /// <param name="hNotifyWindow">通知窗口</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_OpenPicture(string pServerIP, ushort wServerPort, string pDeviceName, string pUserName, string pUserPassword, IntPtr pOpenInfo, ref IntPtr hOpenChannel, IntPtr hNotifyWindow);

        /// <summary>
        /// 关闭抓图服务  hOpenPicture 打开句柄
        /// </summary>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_ClosePicture(IntPtr hOpenPicture);

        /// <summary>
        /// 通知前端设备抓图
        /// </summary>
        /// <param name="hOpenPicture">打开句柄</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_CapturePicture(IntPtr hOpenPicture);
        /// <summary>
        /// 读取图片信息
        /// </summary>
        /// <param name="hOpenPicture"></param>
        /// <param name="channelInfo"></param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_ReadPictureInfo(IntPtr hOpenPicture, ref HH_PICTURE_INFO channelInfo);

        /// <summary>
        /// 读取登录DVS的参数
        /// </summary>
        /// <param name="hServer">登录时返回的句柄</param>
        /// <param name="nConfigCommand">命令</param>
        /// <param name="pConfigBuf"> 参数数据缓冲区</param>
        /// <param name="nConfigBufSize">数据长度</param>
        /// <param name="pAppend">通道号或序号(没有序号的设为0)</param>
        /// <returns></returns>
        [DllImport("HHNetClient.dll")]
        public static extern HHERR_CODE HHNET_GetServerConfig(IntPtr hServer,HHCMD_NET nConfigCommand,ref byte[] pConfigBuf,int nConfigBufSize,ref int pAppend);

        #endregion

        #endregion
    }
}
