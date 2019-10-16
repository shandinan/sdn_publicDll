using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace sdnFaceVerify_Test
{
    public enum FaceSDK_Rec_ImageType
    {
        /* 未知类型 */
        FACESDK_REC_IMAGETYPE_UNKNOWN = 0,
        /* 证件照 */
        FACESDK_REC_IMAGETYPE_ID_PHOTO = 1,
        /* 翻拍证件照 */
        FACESDK_REC_IMAGETYPE_FANPAI_ID_PHOTO = 2,
        /* 类证件照 */
        FACESDK_REC_IMAGETYPE_LIKE_ID_PHOTO = 3,
        /* 芯片照 */
        FACESDK_REC_IMAGETYPE_XINPIAN_ID_PHOTO = 4,
    }

    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
   [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct FaceSDK_Rec_InitParam
    {
       // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 260)]
        public byte[] configPath;
    }

    [StructLayout(LayoutKind.Sequential)]
   public struct FaceSDK_Rec_Client { }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FACE_VERIFY_Rect2D
    {
        public int x;
        public int y;
        public int Width;
        public int Height;
    }



    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FaceSDK_Rec_FeatureExtractionOption
    {
        public FaceSDK_Rec_ImageType imageType;
        /* 是否启用自动旋转 */
        public int enableAutoRotate;
        /* 是否启用自动翻转 */
        public int enableAutoFlip;
        /* 最大人脸数量， -1 表示不限制 */
        public int maxFacesAllowed;
        /* 是否为查询照 */
        public int isQueryImage;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FaceSDK_Rec_FeatureExtractResult
    {
        public FACE_VERIFY_Rect2D rect;
        public IntPtr feature;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FaceSDK_Rec_VerifyResult
    {
        public int isSamePerson;
        public double similarity;
    }

    // [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    // public struct OLIVE_VERIFY_RESULT
    // {
    //     public bool bSamePerson;
    //     public double dblSimilarity;
    //     [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
    //     public byte[] strMessage;
    // }



    public class FaceVerifyWrapper
    {
        public const int SDK_TYPE_FACE_VERIFY2 = 4;
        public const int INVALID_HANDEL = -1;

        public const int RTN_SUCC = 0;
        public const int RET_EXCEPTION = 1;
        public const int RET_NOT_SUPPORTED = 2;
        public const int RTN_LICENSE_ERROR = 3;
        public const int RTN_INVALID_PACKAGE = 4;

        const string DLLPath = @"facesdk_rec.dll";
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_Init", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private extern static int FaceSDK_Init(ref FaceSDK_Rec_InitParam pInitParam, ref IntPtr pHandle);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_Uninit", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private extern static int FaceVerify_Uninit(IntPtr pHandle);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_ExtractFeatureFromImage", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceVerify_Extract(IntPtr pHandle, IntPtr pImg, ref FaceSDK_Rec_FeatureExtractionOption option, ref IntPtr pFeatur, ref UIntPtr pFeatureCount);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_ReleaseFeatureList", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceVerify_ReleaseFeatureList(IntPtr pHandle, IntPtr pFeatureList);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_ReleaseFeature", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_Rec_ReleaseFeature(IntPtr pHandle, IntPtr pFeature);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_VerifyFeature", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private extern static int FaceSDK_Rec_VerifyFeature(IntPtr pHandle, IntPtr pFeature1, IntPtr pFeature2, ref FaceSDK_Rec_VerifyResult result);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_GetDataFromFeature", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private extern static int FaceSDK_Rec_GetDataFromFeature(IntPtr pHandle, IntPtr pFeature, ref IntPtr pData, ref UIntPtr pSize);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_ReleaseFeatureData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_Rec_ReleaseFeatureData(IntPtr pHandle, IntPtr pData);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_CreateFeatureByData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private extern static int FaceSDK_Rec_CreateFeatureByData(IntPtr pHandle, IntPtr pData, UIntPtr pSize, ref IntPtr pFeature);

        //image helper
        const string DLLPath1 = @"facesdk_common.dll";
        [DllImport(DLLPath1, EntryPoint = "FaceSDK_CreateImageFromContent", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_CreateImageFromContent(byte[] imageContent, UIntPtr nSize, ref IntPtr pImage);
        [DllImport(DLLPath1, EntryPoint = "FaceSDK_ReleaseImage", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_ReleaseImage(IntPtr pImage);
        [DllImport(DLLPath1, EntryPoint = "FaceSDK_GetContentFromImage", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_GetContentFromImage(IntPtr pImage, ref UIntPtr pSize, ref IntPtr pImageData);
        [DllImport(DLLPath1, EntryPoint = "FaceSDK_ReleaseImageData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_ReleaseImageData(IntPtr pImageData);

        //package helper
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_CreatePackageByData", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_Rec_CreatePackageByData(IntPtr pHandle, byte[] packagedata, UIntPtr nSize, ref IntPtr package);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_ReleasePackage", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_Rec_ReleasePackage(IntPtr pHandle, IntPtr package);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_GetImageCountInPackage", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_Rec_GetImageCountInPackage(IntPtr pHandle, IntPtr package, ref UIntPtr size);
        [DllImport(DLLPath, EntryPoint = "FaceSDK_Rec_GetImageFromPackage", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int FaceSDK_Rec_GetImageFromPackage(IntPtr pHandle, IntPtr package, UIntPtr nIndex, ref IntPtr image);


        public IntPtr mVerifyHandler = IntPtr.Zero;

        public Thread mDBImageThread = null;
        public Thread mQueryImageThread = null;
        public Thread mInitThread = null;

        public IntPtr mDBFeature = IntPtr.Zero;
        public IntPtr mQueryFeature = IntPtr.Zero;

        public FaceSDK_Rec_FeatureExtractResult[] buffFeatureResult1;
        public FaceSDK_Rec_FeatureExtractResult[] buffFeatureResult2;

        public Int32 buffFeatureCount1 = 0;
        public Int32 buffFeatureCount2 = 0;

        public delegate void OnFaceVerifyInitDone(int nRet);

        struct FaceVerifyInitParam
        {
            public OnFaceVerifyInitDone funcVerifyDone;
            public string strPath;
        }

        void FaceVerifyInitThread(object ParObject)
        {
            FaceVerifyInitParam param = (FaceVerifyInitParam)ParObject;

            int nRet = FaceVerifyInit(param.strPath, 2);
            if (param.funcVerifyDone != null)
            {
                param.funcVerifyDone(nRet);
            }
        }

        public void FaceVerifyInitAsync(string strPath, OnFaceVerifyInitDone funcInitDone)
        {
            if (mInitThread != null)
            {
                mInitThread.Join();
            }

            ParameterizedThreadStart ParStart = new ParameterizedThreadStart(FaceVerifyInitThread);
            mInitThread = new Thread(ParStart);
            FaceVerifyInitParam param = new FaceVerifyInitParam();
            param.funcVerifyDone = funcInitDone;
            param.strPath = strPath;
            mInitThread.Start(param);
        }
        /// <summary>
        /// 人脸比对SDK初始化
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="nReserve"></param>
        /// <returns></returns>
        public int FaceVerifyInit(string strPath, int nReserve)
        {
            if (mVerifyHandler != IntPtr.Zero)
            {
                return RTN_SUCC;
            }

            string strConfigPath = strPath + "\\config\\config.json";

            byte[] Buffer = Encoding.Default.GetBytes(strConfigPath);
            if (Buffer.Length > 260 - 2)
            {
                return RET_NOT_SUPPORTED;
            }

            int rtn = RET_EXCEPTION;

            FaceSDK_Rec_InitParam fsParam = new FaceSDK_Rec_InitParam();

            fsParam.configPath = new byte[260];


            int i;
            for (i = 0; i < Buffer.Length; i++)
            {
                fsParam.configPath[i] = Buffer[i];
            }

            fsParam.configPath[i + 1] = 0;
            fsParam.configPath[i + 2] = 0;

            rtn = FaceSDK_Init(ref fsParam, ref mVerifyHandler); //人脸识别函数初始化
            if (rtn <= -11101 && rtn >= -11105)
            {//license error
                return RTN_LICENSE_ERROR;
            }
            return rtn;
        }
        /// <summary>
        /// 人脸识别卸载
        /// </summary>
        /// <returns></returns>
        public int FaceVerifyUninit()
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return RTN_SUCC;
            }

            int rtn = FaceVerify_Uninit(mVerifyHandler);
            mVerifyHandler = IntPtr.Zero;
            return rtn;
        }
        /// <summary>
        /// 从Package得到图片
        /// </summary>
        /// <param name="packageBuffer"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public int GetImageFromPackage(byte[] packageBuffer, ref IntPtr image)
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return RET_EXCEPTION;
            }

            int rtn = RET_EXCEPTION;
            IntPtr package = IntPtr.Zero;
            UIntPtr nImageCount = UIntPtr.Zero;
            try
            {
                rtn = FaceSDK_Rec_CreatePackageByData(mVerifyHandler, packageBuffer, (UIntPtr)packageBuffer.Length, ref package);
                if (rtn == RTN_SUCC)
                {
                    rtn = FaceSDK_Rec_GetImageCountInPackage(mVerifyHandler, package, ref nImageCount);
                    if (rtn == RTN_SUCC)
                    {
                        if ((int)nImageCount > 0)
                        {
                            rtn = FaceSDK_Rec_GetImageFromPackage(mVerifyHandler, package, UIntPtr.Zero, ref image);
                        }
                        else
                        {
                            rtn = RTN_INVALID_PACKAGE;
                        }
                    }


                    FaceSDK_Rec_ReleasePackage(mVerifyHandler, package);
                    package = IntPtr.Zero;
                }
            }
            catch (System.Exception ex)
            {

            }
            return rtn;
        }

        public int ExtractFeatureFromPackageImage(IntPtr image, ref IntPtr feature, ref Int32 pFeatureCount)
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return RET_EXCEPTION;
            }

            int bRet = RET_EXCEPTION;
            try
            {
                FaceSDK_Rec_FeatureExtractionOption option;
                option.enableAutoRotate = 0;
                option.enableAutoFlip = 0;
                option.imageType = FaceSDK_Rec_ImageType.FACESDK_REC_IMAGETYPE_LIKE_ID_PHOTO;
                option.isQueryImage = 1;
                option.maxFacesAllowed = 1;

                UIntPtr uCount = UIntPtr.Zero;
                int rtn = FaceVerify_Extract(mVerifyHandler, image, ref option, ref feature, ref uCount);
                if (rtn == RTN_SUCC)
                {
                    bRet = rtn = RTN_SUCC;
                    pFeatureCount = (Int32)uCount;
                }
            }
            catch (System.Exception ex)
            {

            }
            return bRet;
        }

        public int ExtractFeature(byte[] buff, bool bQuery, int nDbType, ref IntPtr feature, ref Int32 pFeatureCount)
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return RET_EXCEPTION;
            }

            int bRet = RET_EXCEPTION;
            IntPtr image = IntPtr.Zero;
            try
            {
                FaceSDK_Rec_FeatureExtractionOption option;
                if (bQuery)
                {
                    option.enableAutoRotate = 0;
                    option.enableAutoFlip = 0;
                    option.imageType = FaceSDK_Rec_ImageType.FACESDK_REC_IMAGETYPE_LIKE_ID_PHOTO;
                    option.isQueryImage = 1;
                    option.maxFacesAllowed = 1;
                }
                else
                {
                    option.enableAutoRotate = 1;
                    option.enableAutoFlip = 0;
                    option.imageType = (FaceSDK_Rec_ImageType)nDbType;
                    option.isQueryImage = 0;
                    option.maxFacesAllowed = 1;
                }


                //IntPtr pFeatureCount;
                //FaceSDK_Rec_FeatureExtractResult feature = new FaceSDK_Rec_FeatureExtractResult();
                int rtn = FaceSDK_CreateImageFromContent(buff, (UIntPtr)buff.Length, ref image);
                if (rtn == RTN_SUCC)
                {
                    UIntPtr uCount = UIntPtr.Zero;
                    rtn = FaceVerify_Extract(mVerifyHandler, image, ref option, ref feature, ref uCount);
                    if (rtn == RTN_SUCC)
                    {
                        bRet = rtn = RTN_SUCC;
                        pFeatureCount = (Int32)uCount;
                    }
                }
                if (image != IntPtr.Zero)
                {
                    FaceSDK_ReleaseImage(image);
                    image = IntPtr.Zero;
                }
            }

            //finally
            //{
            //    if (image != IntPtr.Zero)
            //    {
            //        Marshal.FreeHGlobal(image);
            //    }
            //}
            catch (System.Exception ex)
            {
                if (image != IntPtr.Zero)
                {
                    FaceSDK_ReleaseImage(image);
                    image = IntPtr.Zero;
                }
            }
            return bRet;
        }


        public int ReleaseAllFeature()
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return RET_EXCEPTION;
            }
            //int rtn = RET_EXCEPTION;
            if (ReleaseFeature(ref buffFeatureResult1, ref  buffFeatureCount1, ref  mDBFeature) != RTN_SUCC)
                return -1;
            //mDBFeature = IntPtr.Zero;
            if (ReleaseFeature(ref buffFeatureResult2, ref  buffFeatureCount2, ref  mQueryFeature) != RTN_SUCC)
                return -1;
            return RTN_SUCC;
        }

        public int ReleaseFeature(ref FaceSDK_Rec_FeatureExtractResult[] featureList, ref Int32 pFeatureCount, ref IntPtr feature)
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return RET_EXCEPTION;
            }
            int rtn = RET_EXCEPTION;
            try
            {
                for (int i = 0; i < pFeatureCount; i++)
                    FaceSDK_Rec_ReleaseFeature(mVerifyHandler, featureList[i].feature);
                rtn = FaceVerify_ReleaseFeatureList(mVerifyHandler, feature);
                feature = IntPtr.Zero;
                pFeatureCount = 0;
            }
            catch (System.Exception ex)
            {

            }
            return rtn;
        }


        void ExtractDbImage(object ParObject)
        {
            byte[] buffer = (byte[])ParObject;
            try
            {
                if (mDBFeature != IntPtr.Zero)
                {
                    ReleaseFeature(ref buffFeatureResult1, ref  buffFeatureCount1, ref  mDBFeature);
                    mDBFeature = IntPtr.Zero;
                    buffFeatureCount1 = 0;
                }

                int rtn = ExtractFeature(buffer, false, m_nDBImageType, ref mDBFeature, ref buffFeatureCount1);
                if (rtn == RTN_SUCC && buffFeatureCount1 > 0)
                {
                    buffFeatureResult1 = new FaceSDK_Rec_FeatureExtractResult[buffFeatureCount1];
                    buffFeatureResult1[0] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(mDBFeature, typeof(FaceSDK_Rec_FeatureExtractResult));
                    int sizeFaceSDK_Rec_FeatureExtractResult1 = Marshal.SizeOf(buffFeatureResult1[0]);

                    for (int i = 1; i < buffFeatureCount1; ++i)
                    {
                        mDBFeature = new IntPtr(mDBFeature.ToInt32() + sizeFaceSDK_Rec_FeatureExtractResult1);
                        buffFeatureResult1[i] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(mDBFeature, typeof(FaceSDK_Rec_FeatureExtractResult));
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

        }

        void ExtractQueryPackage(object ParObject)
        {
            byte[] buffer = (byte[])ParObject;
            IntPtr image = IntPtr.Zero;
            try
            {
                if (mQueryFeature != IntPtr.Zero)
                {
                    ReleaseFeature(ref buffFeatureResult2, ref  buffFeatureCount2, ref  mQueryFeature);
                    mQueryFeature = IntPtr.Zero;
                    buffFeatureCount2 = 0;
                }

                int rtn = GetImageFromPackage(buffer, ref image);
                if (rtn == RTN_SUCC && image != IntPtr.Zero)
                {
                    rtn = ExtractFeatureFromPackageImage(image, ref mQueryFeature, ref buffFeatureCount2);
                    if (rtn == RTN_SUCC && buffFeatureCount2 > 0)
                    {
                        buffFeatureResult2 = new FaceSDK_Rec_FeatureExtractResult[buffFeatureCount2];
                        buffFeatureResult2[0] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(mQueryFeature, typeof(FaceSDK_Rec_FeatureExtractResult));
                        int sizeFaceSDK_Rec_FeatureExtractResult2 = Marshal.SizeOf(buffFeatureResult2[0]);

                        for (int i = 1; i < buffFeatureCount2; ++i)
                        {
                            mQueryFeature = new IntPtr(mQueryFeature.ToInt32() + sizeFaceSDK_Rec_FeatureExtractResult2);
                            buffFeatureResult2[i] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(mQueryFeature, typeof(FaceSDK_Rec_FeatureExtractResult));
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

            if (image != IntPtr.Zero)
            {
                FaceSDK_ReleaseImage(image);
                image = IntPtr.Zero;
            }
        }

        int m_nDBImageType = 1;
        public void ChangeDBImage(byte[] imagedata, int nDBImageType = 1)
        {
            if (mDBImageThread != null)
            {
                mDBImageThread.Join();
            }
            try
            {
                if (imagedata == null)
                {
                    if (mDBFeature != IntPtr.Zero)
                    {
                        ReleaseFeature(ref buffFeatureResult1, ref  buffFeatureCount1, ref  mDBFeature);
                        mDBFeature = IntPtr.Zero;
                    }

                    return;
                }

                m_nDBImageType = nDBImageType;
                ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ExtractDbImage);
                mDBImageThread = new Thread(ParStart);
                mDBImageThread.Start(imagedata);
            }
            catch (System.Exception ex)
            {

            }
        }


        public void ChangeQueryPackage(byte[] package)
        {
            if (mQueryImageThread != null)
            {
                mQueryImageThread.Join();
            }
            try
            {
                if (package == null)
                {
                    if (mQueryFeature != IntPtr.Zero)
                    {
                        ReleaseFeature(ref buffFeatureResult2, ref  buffFeatureCount2, ref mQueryFeature);
                        mQueryFeature = IntPtr.Zero;
                    }

                    return;
                }

                ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ExtractQueryPackage);
                mQueryImageThread = new Thread(ParStart);
                mQueryImageThread.Start(package);
            }
            catch (System.Exception ex)
            {

            }
        }

        public int VERIFY_RESULT_INVALID_HANLDER = -1;
        public int VERIFY_RESULT_THREAD_NOT_STARTED = -2;
        public int VERIFY_RESULT_INVALID_DB_FEATURE = -3;
        public int VERIFY_RESULT_DB_IMAGE_NOFACE = -4;
        public int VERIFY_RESULT_INVALID_QUERY_FEATURE = -5;
        public int VERIFY_RESULT_QUERY_IMAGE_NOFACE = -6;
        public int VERIFY_RESULT_VERIFY_FAILED = -7;

        public int CompareDBQuery(ref double dblSimilarity)
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return VERIFY_RESULT_INVALID_HANLDER; //算法未初始化
            }

            if (mDBImageThread == null || mQueryImageThread == null)
            {
                return VERIFY_RESULT_THREAD_NOT_STARTED; //抽特征线程未启动
            }

            mDBImageThread.Join();
            mQueryImageThread.Join();

            if (mDBFeature == IntPtr.Zero)
            {
                return VERIFY_RESULT_INVALID_DB_FEATURE;  //没有登记照特征
            }

            if (mQueryFeature == IntPtr.Zero)
            {
                return VERIFY_RESULT_INVALID_QUERY_FEATURE;  //没有查询照特征
            }

            FaceSDK_Rec_VerifyResult result = new FaceSDK_Rec_VerifyResult();
            if (buffFeatureCount2 < 1)
                return VERIFY_RESULT_DB_IMAGE_NOFACE; //登记照中没有人脸
            else if (buffFeatureCount2 < 1)
                return VERIFY_RESULT_QUERY_IMAGE_NOFACE; //查询照中没有人脸
            else if (Compare(buffFeatureResult1[0].feature, buffFeatureResult2[0].feature, ref result) != RTN_SUCC)
                return VERIFY_RESULT_VERIFY_FAILED;  //比对失败

            dblSimilarity = (float)result.similarity;

            return RTN_SUCC;

        }

        public int Compare(IntPtr pFeature1, IntPtr pFeature2, ref FaceSDK_Rec_VerifyResult result)
        {
            if (mVerifyHandler == IntPtr.Zero)
            {
                return RET_EXCEPTION;
            }
            int rtn = RET_EXCEPTION;
            try
            {
                rtn = FaceSDK_Rec_VerifyFeature(mVerifyHandler, pFeature1, pFeature2, ref result);
            }
            catch (System.Exception ex)
            {

            }
            return rtn;
        }

        public bool ComparePersonPair(byte[] DBbuff, byte[] Querybuff, ref double dblSimilarity, int dbImageType = 1)
        {
            FaceSDK_Rec_VerifyResult result = new FaceSDK_Rec_VerifyResult();
            if (mVerifyHandler == IntPtr.Zero)
            {
                dblSimilarity = 0;
                return false;
            }

            IntPtr buffFeature1 = IntPtr.Zero;
            IntPtr buffFeature2 = IntPtr.Zero;
            // FaceSDK_Rec_FeatureExtractResult[] buffFeature2 = new FaceSDK_Rec_FeatureExtractResult() [];
            Int32 buffFeatureCount1 = 0;
            Int32 buffFeatureCount2 = 0;

            if (ExtractFeature(DBbuff, false, dbImageType, ref buffFeature1, ref buffFeatureCount1) != RTN_SUCC)
                return false;
            if (ExtractFeature(Querybuff, true, 0, ref buffFeature2, ref buffFeatureCount2) != RTN_SUCC)
                return false;

            FaceSDK_Rec_FeatureExtractResult[] buffFeatureResult1 = new FaceSDK_Rec_FeatureExtractResult[buffFeatureCount1];
            FaceSDK_Rec_FeatureExtractResult[] buffFeatureResult2 = new FaceSDK_Rec_FeatureExtractResult[buffFeatureCount2];

            buffFeatureResult1[0] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature1, typeof(FaceSDK_Rec_FeatureExtractResult));
            buffFeatureResult2[0] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature2, typeof(FaceSDK_Rec_FeatureExtractResult));

            int sizeFaceSDK_Rec_FeatureExtractResult1 = Marshal.SizeOf(buffFeatureResult1[0]);
            int sizeFaceSDK_Rec_FeatureExtractResult2 = Marshal.SizeOf(buffFeatureResult2[0]);

            for (int i = 1; i < buffFeatureCount1; ++i)
            {
                buffFeature1 = new IntPtr(buffFeature1.ToInt32() + sizeFaceSDK_Rec_FeatureExtractResult1);
                buffFeatureResult1[i] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature1, typeof(FaceSDK_Rec_FeatureExtractResult));
            }
            for (int i = 1; i < buffFeatureCount2; ++i)
            {
                buffFeature2 = new IntPtr(buffFeature1.ToInt32() + sizeFaceSDK_Rec_FeatureExtractResult2);
                buffFeatureResult2[i] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature2, typeof(FaceSDK_Rec_FeatureExtractResult));
            }

            if (buffFeatureCount1 < 1 || buffFeatureCount2 < 1)
                return false;
            else if (Compare(buffFeatureResult1[0].feature, buffFeatureResult2[0].feature, ref result) != RTN_SUCC)
                return false;

            dblSimilarity = (float)result.similarity;

            if (ReleaseFeature(ref buffFeatureResult1, ref buffFeatureCount1, ref  buffFeature1) != RTN_SUCC)
                return false;
            if (ReleaseFeature(ref buffFeatureResult2, ref buffFeatureCount2, ref  buffFeature2) != RTN_SUCC)
                return false;

            return true;
        }

        public bool ComparePersonPair(Image DBImage, Image QueryImage, ref double dblSimilarity, int dbImageType = 1)
        {
            FaceSDK_Rec_VerifyResult result = new FaceSDK_Rec_VerifyResult();
            //for (int j = 0; j < 200; ++j)
            //{
            //    if (j == 0)
            //        Console.WriteLine("asfd");
            //    if(j == 50)
            //        Console.WriteLine("asfd");
            //    if (j == 99)
            //        Console.WriteLine("AFDS");
            //    if(j == 199)
            //        Console.WriteLine("AFDS");
            MemoryStream ms = new MemoryStream();
            DBImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] DBbuff = ms.GetBuffer();
            ms = new MemoryStream();
            QueryImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] Querybuff = ms.GetBuffer();
            if (mVerifyHandler == IntPtr.Zero)
            {
                dblSimilarity = 0;
                return false;
            }

            IntPtr buffFeature1 = IntPtr.Zero;
            IntPtr buffFeature2 = IntPtr.Zero;
            // FaceSDK_Rec_FeatureExtractResult[] buffFeature2 = new FaceSDK_Rec_FeatureExtractResult() [];
            Int32 buffFeatureCount1 = 0;
            Int32 buffFeatureCount2 = 0;

            if (ExtractFeature(DBbuff, false, dbImageType, ref buffFeature1, ref buffFeatureCount1) != RTN_SUCC)
                return false;
            if (ExtractFeature(Querybuff, true, 0, ref buffFeature2, ref buffFeatureCount2) != RTN_SUCC)
                return false;

            FaceSDK_Rec_FeatureExtractResult[] buffFeatureResult1 = new FaceSDK_Rec_FeatureExtractResult[buffFeatureCount1];
            FaceSDK_Rec_FeatureExtractResult[] buffFeatureResult2 = new FaceSDK_Rec_FeatureExtractResult[buffFeatureCount2];

            buffFeatureResult1[0] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature1, typeof(FaceSDK_Rec_FeatureExtractResult));
            buffFeatureResult2[0] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature2, typeof(FaceSDK_Rec_FeatureExtractResult));

            int sizeFaceSDK_Rec_FeatureExtractResult1 = Marshal.SizeOf(buffFeatureResult1[0]);
            int sizeFaceSDK_Rec_FeatureExtractResult2 = Marshal.SizeOf(buffFeatureResult2[0]);

            for (int i = 1; i < buffFeatureCount1; ++i)
            {
                buffFeature1 = new IntPtr(buffFeature1.ToInt32() + sizeFaceSDK_Rec_FeatureExtractResult1);
                buffFeatureResult1[i] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature1, typeof(FaceSDK_Rec_FeatureExtractResult));
            }
            for (int i = 1; i < buffFeatureCount2; ++i)
            {
                buffFeature2 = new IntPtr(buffFeature1.ToInt32() + sizeFaceSDK_Rec_FeatureExtractResult2);
                buffFeatureResult2[i] = (FaceSDK_Rec_FeatureExtractResult)Marshal.PtrToStructure(buffFeature2, typeof(FaceSDK_Rec_FeatureExtractResult));
            }

            if (buffFeatureCount1 < 1 || buffFeatureCount2 < 1)
                return false;
            else if (Compare(buffFeatureResult1[0].feature, buffFeatureResult2[0].feature, ref result) != RTN_SUCC)
                return false;

            dblSimilarity = (float)result.similarity;

            if (ReleaseFeature(ref buffFeatureResult1, ref  buffFeatureCount1, ref buffFeature1) != RTN_SUCC)
                return false;
            if (ReleaseFeature(ref buffFeatureResult2, ref  buffFeatureCount2, ref  buffFeature2) != RTN_SUCC)
                return false;

            return true;
        }


    }
}

