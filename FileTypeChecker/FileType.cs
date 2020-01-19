namespace FileTypeChecker
{
    public class FileType
    {
        public string ContentName { get; }

        public string Extension { get; }

        public static FileType Unknown { get; } = new FileType("Unknown File Extension", ".unknown", null);

        public FuzzyFileTypeMatcher FuzzyFileTypeMatcher { get; }

        public string ExtraMetadata { get; }

        public FileType(string ContentName, string Extension, FuzzyFileTypeMatcher FuzzyFileTypeMatcher)
        {
            this.ContentName = ContentName;
            this.Extension = Extension;
            this.FuzzyFileTypeMatcher = FuzzyFileTypeMatcher;
        }

        public bool Matches(byte[] StreamMinimumMatch)
        {
            return FuzzyFileTypeMatcher.Matches(StreamMinimumMatch);
        }

        public int GetMaxSignatureLength()
        {
            int MAX_LENGTH_SIG = 0;
            foreach (var FTM in FuzzyFileTypeMatcher.LisMatchByteSegment)
            {
                if (FTM.ByteSegmentTotalLength() > MAX_LENGTH_SIG)
                {
                    MAX_LENGTH_SIG = FTM.GetByteSegment().Length + FTM.Offset;
                }
            }
            return MAX_LENGTH_SIG;
        }

    }
}
