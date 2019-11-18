using System;
using System.IO;
using System.Resources;
using System.Globalization;

namespace FileTypeChecker
{
    public abstract class FileTypeMatcher
    {
        private readonly ResourceManager rm = new ResourceManager("rmc", typeof(FileTypeMatcher).Assembly);

        public enum OptionStream
        {
            DoNotReset,
            StartFromBeginOfFile,
            StartFromEndOfFile
        }

        public bool Matches(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
            {
                throw new ArgumentException(rm.GetString("StreamErrorMessage", CultureInfo.CurrentCulture), nameof(stream));
            }
            return MatchesPrivate(stream);
        }

        protected abstract bool MatchesPrivate(Stream stream);

        public abstract byte[] GetMatchingBytes();
    }
}
