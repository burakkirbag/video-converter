# VideoConverter
Videolarinizi donusturmek icin kullanilabileceginiz sinifi ve ornek uygulamayi icerir.

Video donusturmek icin ffmpeg.exe ve ffprobe.exe dosyalarina ihtiyac duymaktadir.

ffmpeg.exe ve ffprobe.exe dosyalarinin yollarini config dosyanizdan belirtebilirsiniz.<br><br>
configuration<br>
  appSettings<br>
    add key="ffmpeg_path" value="/lib/ffmpeg/ffmpeg.exe"<br>
    add key="ffprobe_path" value="/lib/ffmpeg/ffprobe.exe"<br>
  appSettings<br>
configuration<br>

Eger bu degerleri config dosyaniz icerisinde tanimlamazsaniz, dosyalar uygulamanizin calistigi ana dizin altinda,<br>
"/lib/ffmpeg/ffmpeg.exe"<br>
"/lib/ffmpeg/ffprobe.exe" yollarinda aranacaktir.
