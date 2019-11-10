namespace Y.Core
{
    public class Picture : BaseEntity
    {
        public string MimeType { get; set; }

        public string FileName { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }

        public bool IsNew { get; set; }

    }
}
