using System.IO;
using System.Resources;
using System.Globalization;

namespace FileTypeChecker
{
    public class FuzzyFileTypeMatcher : FileTypeMatcher
    {
        private readonly OptionStream option;
        private readonly ResourceManager rm = new ResourceManager("rmc", typeof(FileTypeMatcher).Assembly);

        private readonly MatchByteSegment MatchByteSegment;

        public FuzzyFileTypeMatcher(OptionStream option, MatchByteSegment MatchByteSegment)
        {
            this.MatchByteSegment = MatchByteSegment;
            this.option = option;
        }

        protected override bool MatchesPrivate(Stream stream)
        {
            bool AreBytesCorrectSoFar = true;
            if (stream == null || stream.ReadByte() == -1) throw new System.ArgumentException(rm.GetString("StreamErrorMessage", CultureInfo.CurrentCulture), nameof(stream));

            if (stream.Position != 0 && option == OptionStream.StartFromBeginOfFile)
            {
                stream.Seek(MatchByteSegment.Offset, SeekOrigin.Begin);
            } else if (stream.Position != 0 && option == OptionStream.StartFromEndOfFile)
            {
                stream.Seek(-(MatchByteSegment.Offset + MatchByteSegment.GetByteSegment().Length), SeekOrigin.End);
            }
            foreach (var b in MatchByteSegment.GetByteSegment())
            {
                var c = stream.ReadByte();
                if (c == -1 || (c != b))
                {
                    AreBytesCorrectSoFar = false;
                    break;
                }
            }
            return AreBytesCorrectSoFar;
        }

        public override byte[] GetMatchingBytes()
        {
            return MatchByteSegment.GetByteSegment();
        }
    }
}
