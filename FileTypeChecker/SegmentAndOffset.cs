namespace FileTypeChecker
{
    public class SegmentAndOffset
    {
        private const int V = 1;
        private string SegmentStringByte;
        private int Offset = V;

        public string GetSegmentStringByte() { return SegmentStringByte; }

        public void SetSegmentStringByte(string SegmentStringByte)
        {
            this.SegmentStringByte = SegmentStringByte;
        }

        public int GetOffset() { return Offset; }

        public void SetOffset(int Offset)
        {
            this.Offset = Offset;
        }

    }
}
