using System.Collections.Generic;

namespace FileTypeChecker
{
    public class FileTypeSpecifications
    {
        public IList<string> FileDescriptions { get; }

        public IList<SegmentAndOffset> SegmentBytesAndOffset { get; }

        public IList<string> Extensions { get; }

        public FileTypeSpecifications(IList<string> FileTypeDescriptions, IList<SegmentAndOffset> SegmentBytes, IList<string> Extensions)
        {
            FileDescriptions = FileTypeDescriptions;
            SegmentBytesAndOffset = SegmentBytes;
            this.Extensions = Extensions;
        }
    }
}
