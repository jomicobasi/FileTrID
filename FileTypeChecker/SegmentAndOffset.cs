namespace FileTypeChecker
{
    public class SegmentAndOffset
    {
        public string SegmentStringByte { get; set; }

        public int Offset { get; }

        public SegmentAndOffset(string SegmentStringByte, int Offset)
        {
            this.SegmentStringByte = SegmentStringByte;
            this.Offset = Offset;
        }
    }
}
