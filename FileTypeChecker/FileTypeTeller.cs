using FileTypeChecker.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: CLSCompliant(true)]

namespace FileTypeChecker
{
    public class FileTypeTeller
    {
        private readonly CollectionFileType KnownFileSignatures;

        private MinimumStreamTargetFile MSTF;

        public FileTypeTeller()
        {
            List<FileTypeSpecifications> JSONFileSpecs = JsonConvert.DeserializeObject<List<FileTypeSpecifications>>(Resources.FILESIGNATURES_JSON);
            foreach (var spec in JSONFileSpecs)
            {
                List<MatchByteSegment> OneFileByteSegments = new List<MatchByteSegment>();
                foreach (SegmentAndOffset SegByteOff in GetSegmentBytes(spec))
                {
                    OneFileByteSegments.Add(new MatchByteSegment(
                        Array.ConvertAll(SegByteOff.SegmentStringByte.Trim().Split(' '), byteAsString => Convert.ToByte(byteAsString, 16)), SegByteOff.Offset)
                    );
                }
                KnownFileSignatures.AddFileType(new FileType(string.Join(",", spec.FileDescriptions), "." + string.Join(",", spec.Extensions),
                            new FuzzyFileTypeMatcher(OneFileByteSegments)));
            }
            KnownFileSignatures.SortListBySegmentBytesLengthDescendently();
        }

        private static IList<SegmentAndOffset> GetSegmentBytes(FileTypeSpecifications spec)
        {
            return spec.SegmentBytesAndOffset;
        }

        public FileTypeTeller(CollectionFileType KnownFileSignatures)
        {
            this.KnownFileSignatures = KnownFileSignatures;
        }

        public CollectionFileType GetFileExtension(byte[] rawContent)
        {
            CollectionFileType FileTypeTargets = new CollectionFileType();

            int MaxSizematch = 0;
            foreach (FileType CurrFileType in KnownFileSignatures)
            {
                int MAX_LENGTH = CurrFileType.GetMaxSignatureLength();
                if (MaxSizematch <= MAX_LENGTH)
                {
                    FileTypeTargets.AddFileType(CurrFileType);
                    MaxSizematch = MAX_LENGTH;
                }
            }
            MSTF = new MinimumStreamTargetFile(rawContent, MaxSizematch, FileTypeTargets); 
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
                    return ft.Extension.Equals(extension);
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
