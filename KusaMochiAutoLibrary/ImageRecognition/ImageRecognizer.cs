using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace KusaMochiAutoLibrary.ImageRecognition
{
    public class ImageRecognizer
    {
        #region Public Methods

        public bool IsImageFound(string imageFilePath, int recognitionThreshold = 10)
        {
            using var queryImage = new Mat(imageFilePath, ImreadModes.Color);

            Bitmap screenBitmap = GetScreenCapture(System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using var targetImage = BitmapConverter.ToMat(screenBitmap);

            using var orb = ORB.Create(1000);
            using var descriptors1 = new Mat();
            using var descriptors2 = new Mat();
            orb.DetectAndCompute(queryImage, null, out var keyPoints1, descriptors1);
            orb.DetectAndCompute(targetImage, null, out var keyPoints2, descriptors2);

            using var bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
            var matches = bf.Match(descriptors1, descriptors2);

            var goodMatches = matches
                .Where(x => x.Distance <= recognitionThreshold)
                .OrderBy(x => x.Distance)
                .Take(1024)
                .ToArray();

            return goodMatches.Length > 0;

            //if (goodMatches.Length == 0)
            //{
            //    return false;
            //}

            //var srcPts = goodMatches.Select(m => keyPoints1[m.QueryIdx].Pt).Select(p => new Point2d(p.X, p.Y));
            //var dstPts = goodMatches.Select(m => keyPoints2[m.TrainIdx].Pt).Select(p => new Point2d(p.X, p.Y));

            //using var homography = Cv2.FindHomography(srcPts, dstPts, HomographyMethods.Ransac, 5, null);

            //int h = queryImage.Height, w = queryImage.Width;
            //var targetImageBounds = new[]
            //{
            //    new Point2d(0, 0),
            //    new Point2d(0, h-1),
            //    new Point2d(w-1, h-1),
            //    new Point2d(w-1, 0),
            //};
            //var targetImageBoundsTransformed = Cv2.PerspectiveTransform(targetImageBounds, homography);

            ////using var view = targetImage.Clone();
            ////var drawingPoints = targetImageBoundsTransformed.Select(p => (OpenCvSharp.Point)p).ToArray();
            ////Cv2.Polylines(view, new[] { drawingPoints }, true, Scalar.Red, 3);

            ////using (new OpenCvSharp.Window("view", view))
            ////{
            ////    Cv2.WaitKey();
            ////}

            //return true;
        }

        #endregion

        #region Private Methods

        private System.Drawing.Size GetScreenResolution()
        {
            System.Windows.Window MainWindow = System.Windows.Application.Current.MainWindow;
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

        private Bitmap GetScreenCapture(System.Drawing.Imaging.PixelFormat pixelFormat)
        {
            System.Drawing.Size screenSize = GetScreenResolution();
            return GetScreenCapture(pixelFormat, new Rectangle(0, 0, screenSize.Width, screenSize.Height));
        }

        private Bitmap GetScreenCapture(System.Drawing.Imaging.PixelFormat pixelFormat, Rectangle rect)
        {
            Bitmap output = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(output))
            {
                g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            }
            output = ConvertPixelFormat(output, pixelFormat);
            return output;
        }

        private Bitmap ConvertPixelFormat(Bitmap sourceImage, System.Drawing.Imaging.PixelFormat pixelFormat)
        {
            Bitmap output = sourceImage.Clone(
                new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                pixelFormat
                );
            return output;
        }

        #endregion

        #region Fields

        #endregion
    }
}
