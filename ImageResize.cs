using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic

internal static partial class Gorsel
{
    public enum Dimensions
    {
        Width,
        Height
    }

    public enum AnchorPosition
    {
        Top,
        Center,
        Bottom,
        Left,
        Right
    }

    public static string param_Pic_ProcessMode = "1";
    public static string param_Pic_Quality1 = "Format16bppRgb555";
    public static string param_Pic_Quality2 = "Jpg";
    public static string param_Pic_Quality3 = "HighQualityBicubic";
    public static string Pic_QualityLevel = "83";
    public static string param_Pic_isPicHaveFixedHeigth = "1";
    public static string param_Pic_isPicHaveFixedWidth = "1";
    public static string Pic_Width = "500";
    public static string Pic_Heigth = "500";
    public static string param_MidPic_ProcessMode = "1";
    public static string param_MidPic_Quality1 = "Format16bppRgb555";
    public static string param_MidPic_Quality2 = "Jpg";
    public static string param_MidPic_Quality3 = "HighQualityBicubic";
    public static string MidPic_QualityLevel = "83";
    public static string param_MidPic_isPicHaveFixedHeigth = "1";
    public static string param_MidPic_isPicHaveFixedWidth = "1";
    public static string MidPic_Width = "50";
    public static string MidPic_Heigth = "50"; 
    public static string param_ThumbPic_ProcessMode = "1";
    public static string param_ThumbPic_Quality1 = "Format16bppRgb555";
    public static string param_ThumbPic_Quality2 = "Jpg";
    public static string param_ThumbPic_Quality3 = "HighQualityBicubic";
    public static string ThumbPic_QualityLevel = "83";
    public static string param_ThumbPic_isPicHaveFixedHeigth = "1";
    public static string param_ThumbPic_isPicHaveFixedWidth = "1";
    public static string ThumbPic_Width = "29";
    public static string ThumbPic_Heigth = "29";
    public static string isWatermarkEnable = "0";
    public static int WatermarkX = 100;
    public static int WatermakY = 120;
    public static string WaterMarkUrl = "/Tasarim/img/logo_ikon.png";

    public static byte[] Resim_Duzenle(Stream Dosya)
    {
        Global.System.Drawing.Image drawing_gorsel_dosyasasi;
        try
        {
            drawing_gorsel_dosyasasi = System.Drawing.Image.FromStream(Dosya);
        }
        catch (Exception ex)
        {
            return null;
        }

        var Resim = new Bitmap(drawing_gorsel_dosyasasi);
        byte[] dataBig = null;


        // ** Resize
        if (param_Pic_ProcessMode != "0")
        {
            InterpolationMode Interpolasyon;
            PixelFormat PikselFormat;
            switch (param_Pic_Quality1 ?? "")
            {
                case "Format4bppIndexed":
                    {
                        PikselFormat = PixelFormat.Format4bppIndexed;
                        break;
                    }

                case "Format8bppIndexed":
                    {
                        PikselFormat = PixelFormat.Format8bppIndexed;
                        break;
                    }

                case "Format16bppGrayScale":
                    {
                        PikselFormat = PixelFormat.Format16bppGrayScale;
                        break;
                    }

                case "Format16bppRgb555":
                    {
                        PikselFormat = PixelFormat.Format16bppRgb555;
                        break;
                    }

                case "Format16bppRgb565":
                    {
                        PikselFormat = PixelFormat.Format16bppRgb565;
                        break;
                    }

                case "Format16bppArgb1555":
                    {
                        PikselFormat = PixelFormat.Format16bppArgb1555;
                        break;
                    }

                case "Format24bppRgb":
                    {
                        PikselFormat = PixelFormat.Format24bppRgb;
                        break;
                    }

                case "Format32bppRgb":
                    {
                        PikselFormat = PixelFormat.Format32bppRgb;
                        break;
                    }

                case "Format32bppArgb":
                    {
                        PikselFormat = PixelFormat.Format32bppArgb;
                        break;
                    }

                case "Format32bppPArgb":
                    {
                        PikselFormat = PixelFormat.Format32bppPArgb;
                        break;
                    }

                case "Format48bppRgb":
                    {
                        PikselFormat = PixelFormat.Format48bppRgb;
                        break;
                    }

                case "Format64bppArgb":
                    {
                        PikselFormat = PixelFormat.Format64bppArgb;
                        break;
                    }

                case "Format64bppPArgb":
                    {
                        PikselFormat = PixelFormat.Format64bppPArgb;
                        break;
                    }

                case "Max":
                    {
                        PikselFormat = PixelFormat.Max;
                        break;
                    }

                default:
                    {
                        PikselFormat = PixelFormat.Format16bppRgb555;
                        break;
                    }
            }

            switch (param_Pic_Quality3 ?? "")
            {
                case "Low":
                    {
                        Interpolasyon = InterpolationMode.Low;
                        break;
                    }

                case "High":
                    {
                        Interpolasyon = InterpolationMode.High;
                        break;
                    }

                case "Bilinear":
                    {
                        Interpolasyon = InterpolationMode.Bilinear;
                        break;
                    }

                case "Bicubic":
                    {
                        Interpolasyon = InterpolationMode.Bicubic;
                        break;
                    }

                case "NearestNeighbor":
                    {
                        Interpolasyon = InterpolationMode.NearestNeighbor;
                        break;
                    }

                case "HighQualityBilinear":
                    {
                        Interpolasyon = InterpolationMode.HighQualityBilinear;
                        break;
                    }

                case "HighQualityBicubic":
                    {
                        Interpolasyon = InterpolationMode.HighQualityBicubic;
                        break;
                    }

                default:
                    {
                        Interpolasyon = InterpolationMode.HighQualityBicubic;
                        break;
                    }
            }

            ImageFormat MyImageFormat = ImageFormat.Jpeg;
            switch (param_Pic_Quality2 ?? "")
            {
                case "Jpg":
                    {
                        MyImageFormat = ImageFormat.Jpeg;
                        break;
                    }

                case "Png":
                    {
                        MyImageFormat = ImageFormat.Png;
                        break;
                    }

                case "Bmp":
                    {
                        MyImageFormat = ImageFormat.Bmp;
                        break;
                    }
            }

            var jgpEncoder = GetEncoder(MyImageFormat);
            Global.System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);
            var myEncoderParameter = new EncoderParameter(myEncoder, Conversions.ToLong(Pic_QualityLevel));
            myEncoderParameters.Param(0) = myEncoderParameter;
            if (param_Pic_isPicHaveFixedHeigth == "1" & param_Pic_isPicHaveFixedWidth == "1")
            {
                if (param_Pic_ProcessMode == "1")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
                else if (param_Pic_ProcessMode == "2")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
                else if (param_Pic_ProcessMode == "3")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
            }
            else if (param_Pic_isPicHaveFixedHeigth == "1")
            {
                if (param_Pic_ProcessMode == "1")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
                else if (param_Pic_ProcessMode == "2")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
                else if (param_Pic_ProcessMode == "3")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
            }
            else if (param_Pic_isPicHaveFixedWidth == "1")
            {
                if (param_Pic_ProcessMode == "1")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
                else if (param_Pic_ProcessMode == "2")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
                else if (param_Pic_ProcessMode == "3")
                {
                    if (isWatermarkEnable == "1")
                    {
                        var Resim2 = new Bitmap(FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat));
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                        var stream = new MemoryStream();
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                    else
                    {
                        var stream = new MemoryStream();
                        FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                        stream.Position = 0L;
                        Array.Resize(ref dataBig, (int)(stream.Length + 1));
                        stream.Read(dataBig, 0, (int)stream.Length);
                    }
                }
            }
            else if (isWatermarkEnable == "1")
            {
                var Resim2 = new Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat));
                Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY);
                var stream = new MemoryStream();
                Resim2.Save(stream, jgpEncoder, myEncoderParameters);
                stream.Position = 0L;
                Array.Resize(ref dataBig, (int)(stream.Length + 1));
                stream.Read(dataBig, 0, (int)stream.Length);
            }
            else
            {
                var stream = new MemoryStream();
                Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters);
                stream.Position = 0L;
                Array.Resize(ref dataBig, (int)(stream.Length + 1));
                stream.Read(dataBig, 0, (int)stream.Length);
            }
        }
        else
        {
            var stream = new MemoryStream();
            Resim.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0L;
            Array.Resize(ref dataBig, (int)(stream.Length + 1));
            stream.Read(dataBig, 0, (int)stream.Length);
        }

        if (param_MidPic_ProcessMode != "0")
        {
            InterpolationMode Interpolasyon;
            PixelFormat PikselFormat;
            switch (param_MidPic_Quality1 ?? "")
            {
                case "Format4bppIndexed":
                    {
                        PikselFormat = PixelFormat.Format4bppIndexed;
                        break;
                    }

                case "Format8bppIndexed":
                    {
                        PikselFormat = PixelFormat.Format8bppIndexed;
                        break;
                    }

                case "Format16bppGrayScale":
                    {
                        PikselFormat = PixelFormat.Format16bppGrayScale;
                        break;
                    }

                case "Format16bppRgb555":
                    {
                        PikselFormat = PixelFormat.Format16bppRgb555;
                        break;
                    }

                case "Format16bppRgb565":
                    {
                        PikselFormat = PixelFormat.Format16bppRgb565;
                        break;
                    }

                case "Format16bppArgb1555":
                    {
                        PikselFormat = PixelFormat.Format16bppArgb1555;
                        break;
                    }

                case "Format24bppRgb":
                    {
                        PikselFormat = PixelFormat.Format24bppRgb;
                        break;
                    }

                case "Format32bppRgb":
                    {
                        PikselFormat = PixelFormat.Format32bppRgb;
                        break;
                    }

                case "Format32bppArgb":
                    {
                        PikselFormat = PixelFormat.Format32bppArgb;
                        break;
                    }

                case "Format32bppPArgb":
                    {
                        PikselFormat = PixelFormat.Format32bppPArgb;
                        break;
                    }

                case "Format48bppRgb":
                    {
                        PikselFormat = PixelFormat.Format48bppRgb;
                        break;
                    }

                case "Format64bppArgb":
                    {
                        PikselFormat = PixelFormat.Format64bppArgb;
                        break;
                    }

                case "Format64bppPArgb":
                    {
                        PikselFormat = PixelFormat.Format64bppPArgb;
                        break;
                    }

                case "Max":
                    {
                        PikselFormat = PixelFormat.Max;
                        break;
                    }

                default:
                    {
                        PikselFormat = PixelFormat.Format16bppRgb555;
                        break;
                    }
            }

            switch (param_MidPic_Quality3 ?? "")
            {
                case "Low":
                    {
                        Interpolasyon = InterpolationMode.Low;
                        break;
                    }

                case "High":
                    {
                        Interpolasyon = InterpolationMode.High;
                        break;
                    }

                case "Bilinear":
                    {
                        Interpolasyon = InterpolationMode.Bilinear;
                        break;
                    }

                case "Bicubic":
                    {
                        Interpolasyon = InterpolationMode.Bicubic;
                        break;
                    }

                case "NearestNeighbor":
                    {
                        Interpolasyon = InterpolationMode.NearestNeighbor;
                        break;
                    }

                case "HighQualityBilinear":
                    {
                        Interpolasyon = InterpolationMode.HighQualityBilinear;
                        break;
                    }

                case "HighQualityBicubic":
                    {
                        Interpolasyon = InterpolationMode.HighQualityBicubic;
                        break;
                    }

                default:
                    {
                        Interpolasyon = InterpolationMode.HighQualityBicubic;
                        break;
                    }
            }

            ImageFormat MyImageFormat = ImageFormat.Jpeg;
            switch (param_MidPic_Quality2 ?? "")
            {
                case "Jpg":
                    {
                        MyImageFormat = ImageFormat.Jpeg;
                        break;
                    }

                case "Png":
                    {
                        MyImageFormat = ImageFormat.Png;
                        break;
                    }

                case "Bmp":
                    {
                        MyImageFormat = ImageFormat.Bmp;
                        break;
                    }
            }

            var jgpEncoder = GetEncoder(MyImageFormat);
            Global.System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);
            var myEncoderParameter = new EncoderParameter(myEncoder, Conversions.ToLong(MidPic_QualityLevel));
            myEncoderParameters.Param(0) = myEncoderParameter;
        }
        else
        {
            var stream = new MemoryStream();
            Resim.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0L;
            Array.Resize(ref dataBig, (int)(stream.Length + 1));
            stream.Read(dataBig, 0, (int)stream.Length);
        }

        Resim.Dispose();
        return dataBig;
    }

    public static Image ScaleByPercent(Image imgPhoto, int Percent)
    {
        float nPercent = Percent / 100f;
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;
        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);
        var bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
        grPhoto.Dispose();
        return bmPhoto;
    }

    public static Image ConstrainProportions(Image imgPhoto, int Size, Dimensions Dimension, InterpolationMode Interpolasyon, PixelFormat PikselFormat)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;
        float nPercent = 0f;
        switch (Dimension)
        {
            case Dimensions.Width:
                {
                    nPercent = Size / (float)sourceWidth;
                    break;
                }

            default:
                {
                    nPercent = Size / (float)sourceHeight;
                    break;
                }
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);
        var bmPhoto = new Bitmap(destWidth, destHeight, PikselFormat);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.InterpolationMode = Interpolasyon;
        grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
        grPhoto.Dispose();
        return bmPhoto;
    }

    public static Image FixedSize(Image imgPhoto, int Width, int Height, InterpolationMode Interpolasyon, PixelFormat PikselFormat)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;
        float nPercent = 0f;
        float nPercentW = 0f;
        float nPercentH = 0f;
        nPercentW = Width / (float)sourceWidth;
        nPercentH = Height / (float)sourceHeight;

        // if we have to pad the height pad both the top and the bottom
        // with the difference between the scaled height and the desired height
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = (int)((Width - sourceWidth * nPercent) / 2f);
        }
        else
        {
            nPercent = nPercentW;
            destY = (int)((Height - sourceHeight * nPercent) / 2f);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);
        var bmPhoto = new Bitmap(Width, Height, PikselFormat);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.Clear(Color.White);
        grPhoto.FillRectangle(Brushes.White, 0, 0, destWidth, destHeight);
        grPhoto.InterpolationMode = Interpolasyon;
        grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
        grPhoto.Dispose();
        return bmPhoto;
    }

    public static Image Crop(Image imgPhoto, int Width, int Height, AnchorPosition Anchor, InterpolationMode Interpolasyon, PixelFormat PikselFormat)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;
        float nPercent = 0f;
        float nPercentW = 0f;
        float nPercentH = 0f;
        nPercentW = Width / (float)sourceWidth;
        nPercentH = Height / (float)sourceHeight;
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentW;
            switch (Anchor)
            {
                case AnchorPosition.Top:
                    {
                        destY = 0;
                        break;
                    }

                case AnchorPosition.Bottom:
                    {
                        destY = (int)(Height - sourceHeight * nPercent);
                        break;
                    }

                default:
                    {
                        destY = (int)((Height - sourceHeight * nPercent) / 2f);
                        break;
                    }
            }
        }
        else
        {
            nPercent = nPercentH;
            switch (Anchor)
            {
                case AnchorPosition.Left:
                    {
                        destX = 0;
                        break;
                    }

                case AnchorPosition.Right:
                    {
                        destX = (int)(Width - sourceWidth * nPercent);
                        break;
                    }

                default:
                    {
                        destX = (int)((Width - sourceWidth * nPercent) / 2f);
                        break;
                    }
            }
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);
        var bmPhoto = new Bitmap(Width, Height, PikselFormat);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.FillRectangle(Brushes.White, 0, 0, destWidth, destHeight);
        grPhoto.InterpolationMode = Interpolasyon;
        grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
        grPhoto.Dispose();
        return bmPhoto;
    }

    public static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (var codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }

        return default;
    }
}
