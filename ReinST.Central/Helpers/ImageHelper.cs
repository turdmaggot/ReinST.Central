using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using QRCoder;

namespace ReinST.Central.Helpers
{
    /// <summary>
    /// Helper class for images.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// This is to generate a bitmap from text. 
        /// </summary>
        /// <param name="input">Input the text which a bitmap shall be generated from.</param>
        /// <returns>Bitmap of the image generated</returns>
        public static Bitmap GenerateImageFromString(string input)
        {
            try
            {
                string text = input.Trim();
                Bitmap bitmap = new Bitmap(1, 1);
                Font font = new Font("Broadway", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                Graphics graphics = Graphics.FromImage(bitmap);
                int width = (int)graphics.MeasureString(text, font).Width;
                int height = (int)graphics.MeasureString(text, font).Height;
                bitmap = new Bitmap(bitmap, new Size(width, height));
                graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
                graphics.Flush();
                graphics.Dispose();

                return bitmap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This is to generate a base64 string from a bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap to be converted to a base64 string</param>
        /// <returns>Base64 string of the bitmap</returns>
        public static string GenerateBase64StringFromBitmap(Bitmap bitmap)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Jpeg);
                string base64stringImg = "data:image/jpg;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                ms.Dispose();
                return base64stringImg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This generates a simple QR Code from text input.
        /// </summary>
        /// <param name="input">Text to encode into a QR code.</param>
        /// <returns>Generated QR code as Bitmap.</returns>
        public static Bitmap GenerateQRCode(string input)
        {
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData data = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                return code.GetGraphic(20);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
