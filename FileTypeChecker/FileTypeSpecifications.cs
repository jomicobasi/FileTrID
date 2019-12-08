using System.Collections.Generic;

namespace FileTypeChecker
{
    public class FileTypeSpecifications
    {
        public List<string> FileTypeDescriptions { get; set; }

        public List<SegmentAndOffset> SegmentBytesAndOffset { get; set; }

        public List<string> Extensions { get; set; }

    }
}
