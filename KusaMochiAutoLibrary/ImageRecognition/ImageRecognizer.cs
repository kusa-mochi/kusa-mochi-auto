using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using DupImageLib;

namespace KusaMochiAutoLibrary.ImageRecognition
{
    public class ImageRecognizer
    {
        #region Public Methods

        public bool IsImageFound(string imageFilePath, double recognitionThreshold = 0.7)
        {
            System.Drawing.Size screenSize = GetScreenResolution();
            System.Drawing.Size compareSize = new System.Drawing.Size(16, 16);

            Image image = Bitmap.FromFile(imageFilePath);
            System.Drawing.Size originalImageSize = new System.Drawing.Size(image.Width, image.Height);
            image = new Bitmap(image, compareSize);
            MemoryStream sourceImageStream = new MemoryStream();
            image.Save(sourceImageStream, ImageFormat.Bmp);
            Bitmap screenImage = new Bitmap(screenSize.Width, screenSize.Height);

            int nHorizontalRaster = screenSize.Width - originalImageSize.Width + 1;
            int nVerticalRaster = screenSize.Height - originalImageSize.Height + 1;
            int dRow = originalImageSize.Height / 16;
            int dColumn = originalImageSize.Width / 16;

            using (Graphics g = Graphics.FromImage(screenImage))
            using (MemoryStream targetStream = new MemoryStream())
            {
                g.CopyFromScreen(0, 0, 0, 0, screenSize, CopyPixelOperation.SourceCopy);
                //screenImage.Save("abababa.bmp", ImageFormat.Bmp);
                for (int iRow = 0; iRow < nVerticalRaster; iRow += dRow)
                {
                    for (int iColumn = 0; iColumn < nHorizontalRaster; iColumn += dColumn)
                    {
                        Bitmap targetImage = screenImage.Clone(
                            new Rectangle(iColumn, iRow, originalImageSize.Width, originalImageSize.Height),
                            System.Drawing.Imaging.PixelFormat.Format32bppArgb
                            );
                        targetImage = new Bitmap(targetImage, compareSize);
                        targetImage.Save(targetStream, ImageFormat.Bmp);

                        float diff = CompareImages(sourceImageStream, targetStream);
                        if (diff > recognitionThreshold) return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region Private Methods

        private float CompareImages(Stream image1, Stream image2)
        {
            image1.Position = 0L;
            image2.Position = 0L;
            ulong hash1 = _imageHashes.CalculateDifferenceHash64(image1);
            ulong hash2 = _imageHashes.CalculateDifferenceHash64(image2);
            return ImageHashes.CompareHashes(hash1, hash2);
        }

        private System.Drawing.Size GetScreenResolution()
        {
            Window MainWindow = System.Windows.Application.Current.MainWindow;
            PresentationSource MainWindowPresentationSource = PresentationSource.FromVisual(MainWindow);
            Matrix m = MainWindowPresentationSource.CompositionTarget.TransformToDevice;
            double dpiWidthFactor = m.M11;
            double dpiHeightFactor = m.M22;
            double ScreenHeight = SystemParameters.PrimaryScreenHeight * dpiHeightFactor;
            double ScreenWidth = SystemParameters.PrimaryScreenWidth * dpiWidthFactor;

            return new System.Drawing.Size(
                (int)ScreenWidth,
                (int)ScreenHeight
                );
        }

        #endregion

        #region Fields

        private ImageHashes _imageHashes = new ImageHashes();

        #endregion
    }
}
