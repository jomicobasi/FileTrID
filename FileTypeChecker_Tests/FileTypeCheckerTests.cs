using FileTypeChecker.Tests.Properties;
using NUnit.Framework;
using System.Drawing.Imaging;
using System.IO;

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
                Assert.IsTrue(fileExtensions.GetExtensionsAsString().Contains("PDF"));
            }

            [Test]
            public void ItDetectsBMPs()
            {
                CollectionFileType fileExtensions = checker.GetFileExtension(bitmap);
                Assert.IsTrue(fileExtensions.GetExtensionsAsString().Contains("BMP"));
            }

            [Test]
            public void ItIsIndeedBMP()
            {
                bool boolIsExtension = checker.IsFileExtensionCorrect("BMP", bitmap);
                Assert.IsTrue(boolIsExtension);
            }

            [Test]
            public void ItIsNotBMP()
            {
                bool boolIsExtension = checker.IsFileExtensionCorrect("PDF", bitmap);
                Assert.IsFalse(boolIsExtension);
            }
        }

    }
}
