﻿using System;
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
using KusaMochiAutoLibrary.NativeFunctions;

namespace KusaMochiAutoLibrary.ImageRecognition
{
    public class ImageRecognizer
    {
        #region Public Methods

        /// <summary>
        /// return positions on a target image that contain a query image.
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="recognitionThreshold"></param>
        /// <returns></returns>
        public List<Point2d> GetImagePosition(string imageFilePath, double recognitionThreshold = -1.0)
        {
            double th = recognitionThreshold < 0.0 ? _recognitionThreshold : recognitionThreshold;

            using var queryImage = new Mat(imageFilePath, ImreadModes.Color);

            Bitmap screenBitmap = GetScreenCapture(System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using var targetImage = BitmapConverter.ToMat(screenBitmap);

            var result = new Mat();
            Cv2.MatchTemplate(targetImage, queryImage, result, TemplateMatchModes.CCoeffNormed);
            Cv2.Threshold(result, result, th, 1.0, ThresholdTypes.Binary);

            List<Point2d> output = new List<Point2d>();
            for (int iRow = 0; iRow < result.Height; iRow++)
            {
                for (int iColumn = 0; iColumn < result.Width; iColumn++)
                {
                    if (result.At<int>(iRow, iColumn) != 0)
                    {
                        output.Add(new Point2d { X = iColumn, Y = iRow });
                    }
                }
            }

            return output;
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

        private double _recognitionThreshold = 0.85;

        #endregion
    }
}
