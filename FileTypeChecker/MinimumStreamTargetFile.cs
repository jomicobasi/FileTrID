using System;

namespace FileTypeChecker
{
    public class MinimumStreamTargetFile
    {
        private readonly byte[] MinimumTargetFileSequence;

        private readonly int _maximumLength;

        private readonly CollectionFileType FilteredFileTypeTargets;

        public MinimumStreamTargetFile(byte[] targetFileBytes, int maximumLength, CollectionFileType FileTypeTargets)
        {
            FilteredFileTypeTargets = FileTypeTargets;
            _maximumLength = maximumLength;
            MinimumTargetFileSequence = new byte[_maximumLength];
            Array.Copy(targetFileBytes, MinimumTargetFileSequence, _maximumLength);
        }

        public CollectionFileType GetMatchingFileTypes()
        {
            CollectionFileType AllFT_Matches = new CollectionFileType();
            foreach (FileType FT in FilteredFileTypeTargets)
            {
                if (FT.Matches(MinimumTargetFileSequence))
                {
                    AllFT_Matches.AddFileType(FT);
                }
            }
            return (AllFT_Matches.Count == 0 ? new CollectionFileType(new[] { FileType.Unknown }) : AllFT_Matches);

        }
    }
}



