namespace FileTypeChecker
{
    public class MatchByteSegment
    {
        private readonly byte[] ByteString;

        public int Offset { get; }

        public MatchByteSegment(byte[] byteString, int offset = 0)
        {
            Offset = offset;
            ByteString = byteString;
        }

        public byte[] GetByteSegment() { return ByteString; }

        public int ByteSegmentTotalLength() => Offset + ByteString.Length;

    }
}
