using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace VideoConverter.Core
{
    public class VideoConverter
    {
        #region Private Properties
        private string _ffmpegPath
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ffmpeg_path"] != null)
                    return System.Configuration.ConfigurationManager.AppSettings["ffmpeg_path"];
                else
                    return System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/lib/ffmpeg/ffmpeg.exe";
            }
        }

        private string _ffprobePath
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ffprobe_path"] != null)
                    return System.Configuration.ConfigurationManager.AppSettings["ffprobe_path"];
                else
                    return System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/lib/ffmpeg/ffprobe.exe";
            }
        }
        #endregion

        #region Public Methods
        public Info GetInfo(string sourceFile)
        {
            return _getInfo(sourceFile);
        }
        public bool Convert(string sourceFile, string destinationFile)
        {
            #region
            try
            {
                Info info = _getInfo(sourceFile);

                Stream videoStream = info.streams.Where(q => q.codec_type == "video").FirstOrDefault();
                Stream audioStream = info.streams.Where(q => q.codec_type == "audio").FirstOrDefault();

                if (videoStream != null)
                {
                    string audioBitRate = videoStream.bit_rate;

                    if (audioStream != null)
                        audioBitRate = audioStream.bit_rate;

                    return _convert(sourceFile, destinationFile, videoStream.width, videoStream.height, videoStream.bit_rate, videoStream.duration, videoStream.avg_frame_rate, audioBitRate);
                }
                else
                    return false;
            }
            catch { return false; }
            #endregion
        }
        #endregion

        #region Private Methods
        Info _getInfo(string sourceFile)
        {
            #region
            try
            {
                string args = string.Format("-i {0} -v quiet -print_format json -show_streams", sourceFile);

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_ffprobePath);
                p.StartInfo.Arguments = args;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
                p.Start();

                string output = p.StandardOutput.ReadToEnd().Replace("\r\n", "\n");
                p.WaitForExit();

                Info videoInfo = new JavaScriptSerializer().Deserialize<Info>(output);
                return videoInfo;
            }
            catch { return null; }
            #endregion
        }

        bool _convert(string sourceFile, string destinationFile, int width, int height, string bitRate, string videoDuration, string frameRate, string audioBitRate)
        {
            #region
            try
            {
                if (string.IsNullOrEmpty(bitRate))
                {
                    int frameRateVal1 = System.Convert.ToInt32(frameRate.Split('/')[0].Trim());
                    int frameRateVal2 = System.Convert.ToInt32(frameRate.Split('/')[1].Trim());
                    bitRate = width + " * " + height + " * " + (frameRateVal1 / frameRateVal2) + " * 0.15";
                }

                if (string.IsNullOrEmpty(audioBitRate))
                    audioBitRate = "128000";

                string args = string.Format("-i \"{0}\" -b:v {1} -vcodec libx264 -preset slow -pix_fmt yuvj420p -vf scale={2}:{3} -threads 0 -b:a {4} \"{5}\"", sourceFile, bitRate, width, height, audioBitRate, destinationFile);

                using (Process convertProcess = new Process())
                {
                    ProcessStartInfo p = new ProcessStartInfo();
                    convertProcess.StartInfo.Arguments = args;
                    convertProcess.StartInfo.FileName = _ffmpegPath;
                    convertProcess.StartInfo.CreateNoWindow = false;
                    convertProcess.StartInfo.UseShellExecute = false;
                    convertProcess.StartInfo.RedirectStandardOutput = true;
                    convertProcess.StartInfo.RedirectStandardError = true;
                    convertProcess.Start();
                    using (StreamReader sbOutPut = convertProcess.StandardError)
                    {
                        System.Text.StringBuilder output = new System.Text.StringBuilder();

                        using (StreamReader objStreamReader = convertProcess.StandardError)
                        {
                            while (!convertProcess.WaitForExit(1000))
                            {
                                output.Append(objStreamReader.ReadToEnd().ToString());
                            }

                            if (convertProcess.ExitCode == 0)
                            {
                                convertProcess.Close();
                                if (objStreamReader != null)
                                {
                                    objStreamReader.Close();
                                }
                                return true;
                            }
                            else
                            {
                                convertProcess.Close();
                                string errorMessage = output.ToString();
                                if (objStreamReader != null)
                                    objStreamReader.Close();

                                return false;
                            }
                        }
                    }
                }
            }
            catch { return false; }
            #endregion
        }
        #endregion
    }
}
