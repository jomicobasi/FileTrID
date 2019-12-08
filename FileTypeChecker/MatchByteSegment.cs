namespace FileTypeChecker
{
    public class MatchByteSegment
    {
        private readonly byte[] ByteString;

        public int Offset { get; }

        public MatchByteSegment(byte[] ByteString, int Offset = 0)
        {
            this.Offset = Offset;
            this.ByteString = ByteString;
        }

        public byte[] GetByteSegment() { return ByteString; }

        public int ByteSegmentTotalLength() => Offset + ByteString.Length;

    }
}
