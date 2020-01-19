using System.Collections;
using System.Collections.Generic;

namespace FileTypeChecker
{
    public class CollectionFileType
    {
        public CollectionFileType()
        {
            List = new List<FileType>();
        }

        public CollectionFileType(List<FileType> list)
        {
            List = list;
        }

        public CollectionFileType(FileType[] FTArray)
        {
            List = new List<FileType>(FTArray);
        }

        public void AddFileType(FileType ft)
        {
            List.Add(ft);
        }

        public int Count => List.Count;

        public void SortListBySegmentBytesLengthDescendently()
        {
            List.Sort((ft1, ft2) =>
                -1 * ft1.GetMaxSignatureLength().CompareTo(ft2.GetMaxSignatureLength()));
        }

        public IEnumerator GetEnumerator()
        {
            for (int index = 0; index < List.Count; ++index)
            {
                yield return List[index];
            }
        }

        public List<FileType> List { get; }

        public string[] GetAllExtensions()
        {
            List<string> ExtensionsList = new List<string>();
            foreach (FileType ft in List)
            {
                ExtensionsList.Add(ft.Extension);
            }
            return ExtensionsList.ToArray();
        }

        public string GetExtensionsAsString()
        {
            return string.Join(", ", GetAllExtensions());
        }

    }
}
