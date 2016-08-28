# VideoConverter
Videolarinizi donusturmek icin kullanilabileceginiz sinifi ve ornek uygulamayi icerir.

Video donusturmek icin ffmpeg.exe ve ffprobe.exe dosyalarina ihtiyac duymaktadir.

ffmpeg.exe ve ffprobe.exe dosyalarinin yollarini config dosyanizdan belirtebilirsiniz.
<!--<configuration>
  <appSettings>
    <add key="ffmpeg_path" value="/lib/ffmpeg/ffmpeg.exe" />
    <add key="ffprobe_path" value="/lib/ffmpeg/ffprobe.exe" />
  </appSettings>
</configuration>-->

Eger bu degerleri config dosyaniz icerisinde tanimlamazsaniz, dosyalar uygulamanizin calistigi ana dizin altinda,
"/lib/ffmpeg/ffmpeg.exe"
"/lib/ffmpeg/ffprobe.exe" yollarinda aranacaktir.
