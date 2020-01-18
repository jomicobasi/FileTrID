using System.Collections.Generic;

namespace FileTypeChecker
{
    public class FuzzyFileTypeMatcher
    {
        public FuzzyFileTypeMatcher(List<MatchByteSegment> MatchByteSegmentList)
        {
            LisMatchByteSegment = MatchByteSegmentList;
        }

        public bool Matches(byte[] StreamMinimumSequence)
        {
            if (StreamMinimumSequence == null) return false;
            bool AreBytesCorrectSoFar = true;
            foreach (var SSB in LisMatchByteSegment)
            {
                byte[] SignatureSequence = SSB.GetByteSegment();
                for (int i = 0; i < SignatureSequence.Length; ++i)
                {
                    if (StreamMinimumSequence[i] != SignatureSequence[i])
                    {
                        AreBytesCorrectSoFar = false;
                        break;
                    }
                }
            }
            return AreBytesCorrectSoFar;
        }

        public List<MatchByteSegment> LisMatchByteSegment { get; }
    }
}
