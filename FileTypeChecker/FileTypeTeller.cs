using FileTypeChecker.Properties;
using System;
using System.IO;

[assembly: CLSCompliant(true)]

namespace FileTypeChecker
{
    public class FileTypeTeller
    {
        private readonly CollectionFileType KnownFileSignatures;

        public FileTypeTeller()
        {
            KnownFileSignatures = new CollectionFileType();
            string[] lines = Resources.FILESIGNATURES.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] LineFields = line.Split(',');
                byte[] SignatureBytes = Array.ConvertAll(LineFields[1].Trim().Split(' '), byteAsString => Convert.ToByte(byteAsString, 16));
                string[] ExtensionArr = LineFields[2].Split('|');
                foreach (string ext in ExtensionArr)
                {
                    KnownFileSignatures.AddFileType(new FileType(LineFields[0], "." + ext.ToLower(), 
                        new FuzzyFileTypeMatcher(FileTypeMatcher.OptionStream.StartFromBeginOfFile, new MatchByteSegment(SignatureBytes))));
                }
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
            using (MemoryStream fileContent = new MemoryStream(rawContent))
            {
                int MaxSizematch = 0;
                foreach (FileType CurrFileType in KnownFileSignatures)
                {
                    if (MaxSizematch <= CurrFileType.FileTypeMatcher.GetMatchingBytes().Length && CurrFileType.Matches(fileContent))
                    {
                        FileTypeTargets.AddFileType(CurrFileType);
                        MaxSizematch = CurrFileType.FileTypeMatcher.GetMatchingBytes().Length;
                    }
                }
            }
            if (FileTypeTargets.List.Count == 0) { 
                return new CollectionFileType( new[] { FileType.Unknown });
            }
            return FileTypeTargets;
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
            using (MemoryStream fileContent = new MemoryStream(rawContent))
            {
                foreach (FileType FT in FileTypeTargets)
                {
                    if (FT.Matches(fileContent))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
