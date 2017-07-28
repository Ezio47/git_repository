using log4net;
using Springer.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Springer.Common
{
    public class MediaFileFormatConverter
    {
        private static readonly ILog logs = LogHelper.GetInstance();
        /// <summary>
        /// 音频运行格式转换( .amr => .mp3 ) (3g2==>.mp4)
        /// </summary>
        /// <param name="ffmpegVirtualPath"></param>
        /// <param name="sourceFile">源文件物理路径</param>
        /// <param name="fileVirtualPath">目标文件物理路径</param>
        /// <param name="type">需转化文件类型</param>
        /// <returns></returns>
        public static bool ConvertAudioAndVideo(string ffmpegVirtualPath, string sourceFile, string fileVirtualPath, string type)
        {
            //取得ffmpeg.exe的物理路径
            string ffmpeg = Path.GetFullPath(ffmpegVirtualPath);
            if (!File.Exists(ffmpeg))
            {
                logs.Error("找不到格式转换程序！");
                return false;
            }
            if (!File.Exists(sourceFile))
            {
                logs.Error("找不到源文件！");
                return false;
            }
          //  string destFile = Path.GetFullPath(fileVirtualPath);
            string destFile = fileVirtualPath;
            System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            /*ffmpeg参数说明
             * -i 1.avi   输入文件
             * -ab/-ac <比特率> 设定声音比特率，前面-ac设为立体声时要以一半比特率来设置，比如192kbps的就设成96，转换 
                均默认比特率都较小，要听到较高品质声音的话建议设到160kbps（80）以上
             * -ar <采样率> 设定声音采样率，PSP只认24000
             * -b <比特率> 指定压缩比特率，似乎ffmpeg是自动VBR的，指定了就大概是平均比特率，比如768，1500这样的   --加了以后转换不正常
             * -r 29.97 桢速率（可以改，确认非标准桢率会导致音画不同步，所以只能设定为15或者29.97）
             * s 320x240 指定分辨率
             * 最后的路径为目标文件
             */
            if (type == "2")//视频
            {
                //               上面介绍了转换过程中的音视频的配置参数，综合上面，我们在转换的时候通常的命令如下：
                //高品质：ffmpeg -i infile -ab 128 -acodec libmp3lame -ac 1 -ar 22050 -r 29.97 -qscale 6 -y outfile
                //低品质：ffmpeg -i infile -ab 128 -acodec libmp3lame -ac 1 -ar 22050 -r 29.97 -b 512 -y outfile
                FilestartInfo.Arguments = " -i " + sourceFile + " -ab 128 -acodec libmp3lame -ac 1 -ar 22050  -r 29.97 -qscale 6 -y " + destFile;
            }
            else if (type == "3")//音频
            {
               // FilestartInfo.Arguments = " -i " + sourceFile + " -vn -ar 8 -ac 2 -ab 192 -f mp3 " + destFile;
                FilestartInfo.Arguments = " -i " + sourceFile + " " + destFile;
            }

            try
            {
                //转换
                System.Diagnostics.Process.Start(FilestartInfo);
            }
            catch
            {
                logs.Error("格式转换失败！");
                return false;
            }
            return true;
        }
    }
}
