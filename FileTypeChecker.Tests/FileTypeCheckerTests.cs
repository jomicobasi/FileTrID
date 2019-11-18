using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using FileTypeChecker.Tests.Properties;


namespace FileTypeChecker.Tests
{
    [TestFixture]
    public static class FileTypeCheckerTests
    {
        [TestFixture]
        protected class WhenTheFileIsKnown
        {
            private byte[] bitmap;

            private byte[] pdf;

            private FileTypeTeller checker;

            [TestFixtureSetUp]
            public void SetUp()
            {
                using (var tempBmp = new MemoryStream())
                {
                    // LAND.bmp is from http://www.fileformat.info/format/bmp/sample/
                    Resources.LAND.Save(tempBmp, ImageFormat.Bmp);
                    bitmap = tempBmp.ToArray();
                }
                // http://boingboing.net/2015/03/23/free-pdf-advanced-quantum-the.html
                pdf = Resources.advancedquantumthermodynamics;
                checker = new FileTypeTeller();
            }

            [Test]
            public void ItDetectsPDFs()
            {
                CollectionFileType fileExtensions = checker.GetFileExtension(pdf);
                Assert.IsTrue(fileExtensions.GetExtensionsAsString().Contains(".pdf"));
            }

            [Test]
            public void ItDetectsBMPs()
            {
                CollectionFileType fileExtensions = checker.GetFileExtension(bitmap);
                Assert.IsTrue(fileExtensions.GetExtensionsAsString().Contains(".bmp"));
            }

            [Test]
            public void ItIsIndeedBMP()
            {
                bool boolIsExtension = checker.IsFileExtensionCorrect(".bmp",bitmap);
                Assert.IsTrue(boolIsExtension);
            }

            [Test]
            public void ItIsNotBMP()
            {
                bool boolIsExtension = checker.IsFileExtensionCorrect(".pdf", bitmap);
                Assert.IsFalse(boolIsExtension);
            }
        }

    }
}
