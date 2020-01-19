using FileTypeChecker.Properties;
using System;
using System.Collections.Generic;
using System.Text.Json;

[assembly: CLSCompliant(true)]

namespace FileTypeChecker
{
    public class FileTypeTeller
    {
        private readonly CollectionFileType KnownFileSignatures = new CollectionFileType();

        private MinimumStreamTargetFile MSTF;

        public FileTypeTeller()
        {
            List<FileTypeSpecifications> JSONFileSpecs = JsonSerializer.Deserialize<List<FileTypeSpecifications>>(Resources.FILESIGNATURES_JSON);
            foreach (var spec in JSONFileSpecs)
            {
                List<MatchByteSegment> OneFileByteSegments = new List<MatchByteSegment>();
                foreach (SegmentAndOffset SegByteOff in spec.SegmentBytesAndOffset)
                {
                    OneFileByteSegments.Add(new MatchByteSegment(
                        Array.ConvertAll(SegByteOff.SegmentStringByte.Trim().Split(' '), byteAsString => Convert.ToByte(byteAsString, 16)), SegByteOff.Offset)
                    );
                }
                KnownFileSignatures.AddFileType(new FileType(string.Join(",", spec.FileTypeDescriptions), string.Join(",", spec.Extensions),
                            new FuzzyFileTypeMatcher(OneFileByteSegments)));
            }
            KnownFileSignatures.SortListBySegmentBytesLengthDescendently();
        }

        public FileTypeTeller(CollectionFileType KnownFileSignatures)
        {
            this.KnownFileSignatures = KnownFileSignatures;
        }

        public CollectionFileType GetFileExtension(byte[] rawContent)
        {
            CollectionFileType FileTypeTargets = new CollectionFileType();

            int MaximumLengthForMatch = 0;
            foreach (FileType CurrFileType in KnownFileSignatures)
            {
                int CurrFileSignatureLength = CurrFileType.GetMaxSignatureLength();
                int MinResult = Math.Min(CurrFileSignatureLength, rawContent.Length);
                if (MinResult == CurrFileSignatureLength)
                {
                    FileTypeTargets.AddFileType(CurrFileType);
                    MaximumLengthForMatch = Math.Max(MaximumLengthForMatch, CurrFileSignatureLength);
                }
            }
            MSTF = new MinimumStreamTargetFile(rawContent, MaximumLengthForMatch, FileTypeTargets);
            return MSTF.GetMatchingFileTypes();
        }

        public bool IsFileExtensionCorrect(string extension, byte[] rawContent)
        {
            if (extension == null)
            {
                return false;
            }
            CollectionFileType FileTypeTargets = new CollectionFileType(KnownFileSignatures.List.FindAll(
                delegate (FileType ft)
                {
                    return Array.IndexOf(ft.Extension.Split(','), extension) >= 0;
                }
            ));
            if (FileTypeTargets.List.Count == 0)
            {
                return false;
            }
            foreach (FileType FT in FileTypeTargets)
            {
                if (FT.Matches(rawContent))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
