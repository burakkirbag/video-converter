namespace VideoConverter.Core
{
    public class Stream
    {
        public int index { get; set; }
        public string codec_name { get; set; }
        public string codec_long_name { get; set; }
        public string profile { get; set; }
        public string codec_type { get; set; }
        public string codec_time_base { get; set; }
        public string codec_tag_string { get; set; }
        public string codec_tag { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int has_b_frames { get; set; }
        public string sample_aspect_ratio { get; set; }
        public string display_aspect_ratio { get; set; }
        public string pix_fmt { get; set; }
        public int level { get; set; }
        public string r_frame_rate { get; set; }
        public string avg_frame_rate { get; set; }
        public string time_base { get; set; }
        public int start_pts { get; set; }
        public string start_time { get; set; }
        public int duration_ts { get; set; }
        public string duration { get; set; }
        public string nb_frames { get; set; }
        public Disposition disposition { get; set; }
        public string sample_fmt { get; set; }
        public string sample_rate { get; set; }
        public int? channels { get; set; }
        public int? bits_per_sample { get; set; }
        public string bit_rate { get; set; }
    }
}
