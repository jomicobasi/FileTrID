using System.IO;

namespace FileTypeChecker
{
    public class FileType
    {
        public string ContentName { get; }

        public string Extension { get; }

        public static FileType Unknown { get; } = new FileType("Unknown File Extension", ".unknown", null);

        public FileTypeMatcher FileTypeMatcher { get; }

        public FileType(string ContentName, string Extension, FileTypeMatcher FileTypeMatcher)
        {
            this.ContentName = ContentName;
            this.Extension = Extension;
            this.FileTypeMatcher = FileTypeMatcher;
        }

        public bool Matches(Stream stream)
        {
            return LoopMatchers(stream);
        }

        public bool LoopMatchers(Stream stream)
        {
            return FileTypeMatcher.Matches(stream);
        }
    }
}
