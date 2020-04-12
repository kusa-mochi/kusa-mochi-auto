using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using DupImageLib;

namespace KusaMochiAutoLibrary.ImageRecognition
{
    public class ImageRecognizer
    {
        #region Public Methods

        public bool IsImageFound(string imageFilePath)
        {
            int screenWidth = (int)SystemParameters.VirtualScreenWidth;
            int screenHeight = (int)SystemParameters.VirtualScreenHeight;

            Image imageBitmap = Bitmap.FromFile(imageFilePath);
            int width = imageBitmap.Width;
            int height = imageBitmap.Height;
            MemoryStream imageStream = new MemoryStream();
            imageBitmap.Save(imageStream, ImageFormat.Bmp);

            int nHorizontalRaster = screenWidth - width + 1;
            int nVerticalRaster = screenHeight - height + 1;

            for (int iRow = 0; iRow < nVerticalRaster; iRow += 3)
            {
                for (int iColumn = 0; iColumn < nHorizontalRaster; iColumn += 3)
                {
                    // get a partial screen capture as a memory stream.
                    Graphics g = Graphics.FromImage(
                        new Bitmap(width, height, PixelFormat.Format32bppArgb)
                        );
                    g.CopyFromScreen(iColumn, iRow, 0, 0, new System.Drawing.Size(width, height), CopyPixelOperation.SourceCopy);
                    Bitmap bitmap = new Bitmap(width, height, g);
                    MemoryStream targetImageStream = new MemoryStream();
                    bitmap.Save(targetImageStream, ImageFormat.Bmp);

                    // get a value of difference between image streams.
                    float diff = CompareImages(imageStream, targetImageStream);
                    if (diff > 0.7) return true;
                }
            }

            return false;

            //Rectangle rect = new Rectangle(0, 0, width, height);
            //Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            //Graphics g = Graphics.FromImage(bmp);
            //g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
        }

        #endregion

        #region Private Methods

        private float CompareImages(Stream image1, Stream image2)
        {
            image1.Position = 0L;
            image2.Position = 0L;
            //ulong[] hash1 = _imageHashes.CalculateDifferenceHash256(image1);
            ulong hash2 = _imageHashes.CalculateDifferenceHash64(image2);
            //return ImageHashes.CompareHashes(hash1, hash2);
            return 0.0f;
        }

        #endregion

        #region Fields

        private ImageHashes _imageHashes = new ImageHashes();

        #endregion
    }
}
