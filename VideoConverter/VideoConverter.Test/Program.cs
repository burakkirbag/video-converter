using System;

namespace VideoConverter.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoConverter.Core.VideoConverter converter = new Core.VideoConverter();

            /* Config(App.Config, Web.Config) dosyasi icerisinde 
             * "ffmpeg_path" ve "ffprobe_path" anahtarlarini ekleyerek, ffmpeg ve ffprobe exelerinin yollarini degistirebilirsiniz.
             * Bu anahtarlari config dosyanizda tanimlanadiginiz taktirde, uygulamanin calistigi ana dizin altinda "/lib/ffmpeg/ffmpeg.exe" ve "/lib/ffmpeg/ffprobe.exe" yollarinda exeleri ariyacaktir.
             */

            // Kaynak Dosya Yolu Tam Olarak Belirtilmeli
            string sourceFile = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/uploads/temp/test.avi";

            // Cevirilecek Dosya Yolu Tam Olarak Belirtilmeli (Dosya ismine cevirilmek istenen uzanti yazilmali.)
            string destFile = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/uploads/test.mp4";

            // Dosya basarili olarak cevirildiginde true donecektir.
            var result = converter.Convert(sourceFile, destFile);
            if (result)
                Console.WriteLine("Success Completed !");
            else
                Console.WriteLine("Failed !");

            Console.ReadLine();
        }
    }
}
