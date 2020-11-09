Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Module Gorsel
    Public Enum Dimensions
        Width
        Height
    End Enum
    Public Enum AnchorPosition
        Top
        Center
        Bottom
        Left
        Right
    End Enum

    Public param_Pic_ProcessMode As String = "1"
    Public param_Pic_Quality1 As String = "Format16bppRgb555"
    Public param_Pic_Quality2 As String = "Jpg"
    Public param_Pic_Quality3 As String = "HighQualityBicubic"
    Public Pic_QualityLevel As String = "83"
    Public param_Pic_isPicHaveFixedHeigth As String = "1"
    Public param_Pic_isPicHaveFixedWidth As String = "1"
    Public Pic_Width As String = "500"
    Public Pic_Heigth As String = "500"

    Public param_MidPic_ProcessMode As String = "1"
    Public param_MidPic_Quality1 As String = "Format16bppRgb555"
    Public param_MidPic_Quality2 As String = "Jpg"
    Public param_MidPic_Quality3 As String = "HighQualityBicubic"
    Public MidPic_QualityLevel As String = "83"
    Public param_MidPic_isPicHaveFixedHeigth As String = "1"
    Public param_MidPic_isPicHaveFixedWidth As String = "1"
    Public MidPic_Width As String = "50"
    Public MidPic_Heigth As String = "50"

    Public param_ThumbPic_ProcessMode As String = "1"
    Public param_ThumbPic_Quality1 As String = "Format16bppRgb555"
    Public param_ThumbPic_Quality2 As String = "Jpg"
    Public param_ThumbPic_Quality3 As String = "HighQualityBicubic"
    Public ThumbPic_QualityLevel As String = "83"
    Public param_ThumbPic_isPicHaveFixedHeigth As String = "1"
    Public param_ThumbPic_isPicHaveFixedWidth As String = "1"
    Public ThumbPic_Width As String = "29"
    Public ThumbPic_Heigth As String = "29"

    Public isWatermarkEnable As String = "0"
    Public WatermarkX As Integer = 100
    Public WatermakY As Integer = 120
    Public WaterMarkUrl As String = "/Tasarim/img/logo_ikon.png"

    Public Function Resim_Duzenle(Dosya As IO.Stream) As Byte()

        Dim drawing_gorsel_dosyasasi As System.Drawing.Image
        Try
            drawing_gorsel_dosyasasi = System.Drawing.Image.FromStream(Dosya)
        Catch ex As Exception
            Return Nothing
        End Try

        Dim Resim As New Bitmap(drawing_gorsel_dosyasasi)

        Dim dataBig() As Byte = Nothing


        '** Resize
        If param_Pic_ProcessMode <> "0" Then

            Dim Interpolasyon As InterpolationMode
            Dim PikselFormat As PixelFormat
            Select Case param_Pic_Quality1
                Case "Format4bppIndexed"
                    PikselFormat = PixelFormat.Format4bppIndexed
                Case "Format8bppIndexed"
                    PikselFormat = PixelFormat.Format8bppIndexed 
                Case "Format16bppGrayScale"
                    PikselFormat = PixelFormat.Format16bppGrayScale
                Case "Format16bppRgb555"
                    PikselFormat = PixelFormat.Format16bppRgb555
                Case "Format16bppRgb565"
                    PikselFormat = PixelFormat.Format16bppRgb565
                Case "Format16bppArgb1555"
                    PikselFormat = PixelFormat.Format16bppArgb1555
                Case "Format24bppRgb"
                    PikselFormat = PixelFormat.Format24bppRgb
                Case "Format32bppRgb"
                    PikselFormat = PixelFormat.Format32bppRgb
                Case "Format32bppArgb"
                    PikselFormat = PixelFormat.Format32bppArgb
                Case "Format32bppPArgb"
                    PikselFormat = PixelFormat.Format32bppPArgb
                Case "Format48bppRgb"
                    PikselFormat = PixelFormat.Format48bppRgb
                Case "Format64bppArgb"
                    PikselFormat = PixelFormat.Format64bppArgb
                Case "Format64bppPArgb"
                    PikselFormat = PixelFormat.Format64bppPArgb
                Case "Max"
                    PikselFormat = PixelFormat.Max
                Case Else
                    PikselFormat = PixelFormat.Format16bppRgb555
            End Select
            Select Case param_Pic_Quality3
                Case "Low"
                    Interpolasyon = InterpolationMode.Low
                Case "High"
                    Interpolasyon = InterpolationMode.High
                Case "Bilinear"
                    Interpolasyon = InterpolationMode.Bilinear
                Case "Bicubic"
                    Interpolasyon = InterpolationMode.Bicubic
                Case "NearestNeighbor"
                    Interpolasyon = InterpolationMode.NearestNeighbor
                Case "HighQualityBilinear"
                    Interpolasyon = InterpolationMode.HighQualityBilinear
                Case "HighQualityBicubic"
                    Interpolasyon = InterpolationMode.HighQualityBicubic
                Case Else
                    Interpolasyon = InterpolationMode.HighQualityBicubic
            End Select

            Dim MyImageFormat As ImageFormat = ImageFormat.Jpeg
            Select Case param_Pic_Quality2
                Case "Jpg"
                    MyImageFormat = ImageFormat.Jpeg
                Case "Png"
                    MyImageFormat = ImageFormat.Png
                Case "Bmp"
                    MyImageFormat = ImageFormat.Bmp
            End Select
            Dim jgpEncoder As ImageCodecInfo = GetEncoder(MyImageFormat)
            Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality

            Dim myEncoderParameters As New EncoderParameters(1)
            Dim myEncoderParameter As New EncoderParameter(myEncoder, CLng(Pic_QualityLevel))
            myEncoderParameters.Param(0) = myEncoderParameter

            If param_Pic_isPicHaveFixedHeigth = "1" And param_Pic_isPicHaveFixedWidth = "1" Then
                If param_Pic_ProcessMode = "1" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    Else
                        Dim stream As New System.IO.MemoryStream
                        Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    End If

                ElseIf param_Pic_ProcessMode = "2" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    Else
                        Dim stream As New System.IO.MemoryStream
                        ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    End If

                ElseIf param_Pic_ProcessMode = "3" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    Else
                        Dim stream As New System.IO.MemoryStream
                        FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    End If

                End If
            ElseIf param_Pic_isPicHaveFixedHeigth = "1" Then
                If param_Pic_ProcessMode = "1" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    Else
                        Dim stream As New System.IO.MemoryStream
                        Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    End If

                ElseIf param_Pic_ProcessMode = "2" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    Else
                        Dim stream As New System.IO.MemoryStream
                        ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)
                    End If

                ElseIf param_Pic_ProcessMode = "3" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    Else
                        Dim stream As New System.IO.MemoryStream
                        FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    End If

                End If
            ElseIf param_Pic_isPicHaveFixedWidth = "1" Then
                If param_Pic_ProcessMode = "1" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    Else
                        Dim stream As New System.IO.MemoryStream
                        Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    End If

                ElseIf param_Pic_ProcessMode = "2" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    Else
                        Dim stream As New System.IO.MemoryStream
                        ConstrainProportions(Resim, Pic_Heigth, Dimensions.Height, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    End If

                ElseIf param_Pic_ProcessMode = "3" Then
                    If isWatermarkEnable = "1" Then
                        Dim Resim2 As Bitmap = New Bitmap(FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat))
                        Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                        Dim stream As New System.IO.MemoryStream
                        Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    Else
                        Dim stream As New System.IO.MemoryStream
                        FixedSize(Resim, Pic_Width, Pic_Heigth, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                        stream.Position = 0
                        ReDim Preserve dataBig(stream.Length)
                        stream.Read(dataBig, 0, stream.Length)

                    End If

                End If
            Else
                If isWatermarkEnable = "1" Then
                    Dim Resim2 As Bitmap = New Bitmap(Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat))
                    Graphics.FromImage(Resim2).DrawImage(Image.FromFile(HttpContext.Current.Server.MapPath(WaterMarkUrl)), WatermarkX, WatermakY)
                    Dim stream As New System.IO.MemoryStream
                    Resim2.Save(stream, jgpEncoder, myEncoderParameters)
                    stream.Position = 0
                    ReDim Preserve dataBig(stream.Length)
                    stream.Read(dataBig, 0, stream.Length)



                Else
                    Dim stream As New System.IO.MemoryStream
                    Crop(Resim, Pic_Width, Pic_Heigth, AnchorPosition.Center, Interpolasyon, PikselFormat).Save(stream, jgpEncoder, myEncoderParameters)
                    stream.Position = 0
                    ReDim Preserve dataBig(stream.Length)
                    stream.Read(dataBig, 0, stream.Length)

                End If

            End If
        Else

            Dim stream As New System.IO.MemoryStream

            Resim.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
            stream.Position = 0

            ReDim Preserve dataBig(stream.Length)
            stream.Read(dataBig, 0, stream.Length)



        End If


        If param_MidPic_ProcessMode <> "0" Then

            Dim Interpolasyon As InterpolationMode
            Dim PikselFormat As PixelFormat
            Select Case param_MidPic_Quality1
                Case "Format4bppIndexed"
                    PikselFormat = PixelFormat.Format4bppIndexed
                Case "Format8bppIndexed"
                    PikselFormat = PixelFormat.Format8bppIndexed
                Case "Format16bppGrayScale"
                    PikselFormat = PixelFormat.Format16bppGrayScale
                Case "Format16bppRgb555"
                    PikselFormat = PixelFormat.Format16bppRgb555
                Case "Format16bppRgb565"
                    PikselFormat = PixelFormat.Format16bppRgb565
                Case "Format16bppArgb1555"
                    PikselFormat = PixelFormat.Format16bppArgb1555
                Case "Format24bppRgb"
                    PikselFormat = PixelFormat.Format24bppRgb
                Case "Format32bppRgb"
                    PikselFormat = PixelFormat.Format32bppRgb
                Case "Format32bppArgb"
                    PikselFormat = PixelFormat.Format32bppArgb
                Case "Format32bppPArgb"
                    PikselFormat = PixelFormat.Format32bppPArgb
                Case "Format48bppRgb"
                    PikselFormat = PixelFormat.Format48bppRgb
                Case "Format64bppArgb"
                    PikselFormat = PixelFormat.Format64bppArgb
                Case "Format64bppPArgb"
                    PikselFormat = PixelFormat.Format64bppPArgb
                Case "Max"
                    PikselFormat = PixelFormat.Max
                Case Else
                    PikselFormat = PixelFormat.Format16bppRgb555
            End Select
            Select Case param_MidPic_Quality3
                Case "Low"
                    Interpolasyon = InterpolationMode.Low
                Case "High"
                    Interpolasyon = InterpolationMode.High
                Case "Bilinear"
                    Interpolasyon = InterpolationMode.Bilinear
                Case "Bicubic"
                    Interpolasyon = InterpolationMode.Bicubic
                Case "NearestNeighbor"
                    Interpolasyon = InterpolationMode.NearestNeighbor
                Case "HighQualityBilinear"
                    Interpolasyon = InterpolationMode.HighQualityBilinear
                Case "HighQualityBicubic"
                    Interpolasyon = InterpolationMode.HighQualityBicubic
                Case Else
                    Interpolasyon = InterpolationMode.HighQualityBicubic
            End Select

            Dim MyImageFormat As ImageFormat = ImageFormat.Jpeg
            Select Case param_MidPic_Quality2
                Case "Jpg"
                    MyImageFormat = ImageFormat.Jpeg
                Case "Png"
                    MyImageFormat = ImageFormat.Png
                Case "Bmp"
                    MyImageFormat = ImageFormat.Bmp
            End Select
            Dim jgpEncoder As ImageCodecInfo = GetEncoder(MyImageFormat)
            Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality

            Dim myEncoderParameters As New EncoderParameters(1)
            Dim myEncoderParameter As New EncoderParameter(myEncoder, CLng(MidPic_QualityLevel))
            myEncoderParameters.Param(0) = myEncoderParameter





        Else
            Dim stream As New System.IO.MemoryStream

            Resim.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
            stream.Position = 0

            ReDim Preserve dataBig(stream.Length)
            stream.Read(dataBig, 0, stream.Length)


        End If



        Resim.Dispose()

        Return dataBig



    End Function

    Public Function ScaleByPercent(ByVal imgPhoto As Image, ByVal Percent As Integer) As Image
        Dim nPercent As Single = (CSng(Percent) / 100)
        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height
        Dim sourceX As Integer = 0
        Dim sourceY As Integer = 0

        Dim destX As Integer = 0
        Dim destY As Integer = 0
        Dim destWidth As Integer = CInt((sourceWidth * nPercent))
        Dim destHeight As Integer = CInt((sourceHeight * nPercent))

        Dim bmPhoto As New Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb)
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic

        grPhoto.DrawImage(imgPhoto, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)

        grPhoto.Dispose()
        Return bmPhoto
    End Function
    Public Function ConstrainProportions(ByVal imgPhoto As Image, ByVal Size As Integer, ByVal Dimension As Dimensions, ByVal Interpolasyon As InterpolationMode, ByVal PikselFormat As PixelFormat) As Image
        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height
        Dim sourceX As Integer = 0
        Dim sourceY As Integer = 0
        Dim destX As Integer = 0
        Dim destY As Integer = 0
        Dim nPercent As Single = 0

        Select Case Dimension
            Case Dimensions.Width
                nPercent = (CSng(Size) / CSng(sourceWidth))
                Exit Select
            Case Else
                nPercent = (CSng(Size) / CSng(sourceHeight))
                Exit Select
        End Select

        Dim destWidth As Integer = CInt((sourceWidth * nPercent))
        Dim destHeight As Integer = CInt((sourceHeight * nPercent))

        Dim bmPhoto As New Bitmap(destWidth, destHeight, PikselFormat)
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.InterpolationMode = Interpolasyon

        grPhoto.DrawImage(imgPhoto, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)

        grPhoto.Dispose()
        Return bmPhoto
    End Function
    Public Function FixedSize(ByVal imgPhoto As Image, ByVal Width As Integer, ByVal Height As Integer, ByVal Interpolasyon As InterpolationMode, ByVal PikselFormat As PixelFormat) As Image
        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height
        Dim sourceX As Integer = 0
        Dim sourceY As Integer = 0
        Dim destX As Integer = 0
        Dim destY As Integer = 0

        Dim nPercent As Single = 0
        Dim nPercentW As Single = 0
        Dim nPercentH As Single = 0

        nPercentW = (CSng(Width) / CSng(sourceWidth))
        nPercentH = (CSng(Height) / CSng(sourceHeight))

        'if we have to pad the height pad both the top and the bottom
        'with the difference between the scaled height and the desired height
        If nPercentH < nPercentW Then
            nPercent = nPercentH
            destX = CInt(((Width - (sourceWidth * nPercent)) / 2))
        Else
            nPercent = nPercentW
            destY = CInt(((Height - (sourceHeight * nPercent)) / 2))
        End If

        Dim destWidth As Integer = CInt((sourceWidth * nPercent))
        Dim destHeight As Integer = CInt((sourceHeight * nPercent))

        Dim bmPhoto As New Bitmap(Width, Height, PikselFormat)
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.Clear(Color.White)
        grPhoto.FillRectangle(Brushes.White, 0, 0, destWidth, destHeight)
        grPhoto.InterpolationMode = Interpolasyon

        grPhoto.DrawImage(imgPhoto, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)

        grPhoto.Dispose()
        Return bmPhoto
    End Function
    Public Function Crop(ByVal imgPhoto As Image, ByVal Width As Integer, ByVal Height As Integer, ByVal Anchor As AnchorPosition, ByVal Interpolasyon As InterpolationMode, ByVal PikselFormat As PixelFormat) As Image
        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height
        Dim sourceX As Integer = 0
        Dim sourceY As Integer = 0
        Dim destX As Integer = 0
        Dim destY As Integer = 0

        Dim nPercent As Single = 0
        Dim nPercentW As Single = 0
        Dim nPercentH As Single = 0

        nPercentW = (CSng(Width) / CSng(sourceWidth))
        nPercentH = (CSng(Height) / CSng(sourceHeight))

        If nPercentH < nPercentW Then
            nPercent = nPercentW
            Select Case Anchor
                Case AnchorPosition.Top
                    destY = 0
                    Exit Select
                Case AnchorPosition.Bottom
                    destY = CInt((Height - (sourceHeight * nPercent)))
                    Exit Select
                Case Else
                    destY = CInt(((Height - (sourceHeight * nPercent)) / 2))
                    Exit Select
            End Select
        Else
            nPercent = nPercentH
            Select Case Anchor
                Case AnchorPosition.Left
                    destX = 0
                    Exit Select
                Case AnchorPosition.Right
                    destX = CInt((Width - (sourceWidth * nPercent)))
                    Exit Select
                Case Else
                    destX = CInt(((Width - (sourceWidth * nPercent)) / 2))
                    Exit Select
            End Select
        End If

        Dim destWidth As Integer = CInt((sourceWidth * nPercent))
        Dim destHeight As Integer = CInt((sourceHeight * nPercent))

        Dim bmPhoto As New Bitmap(Width, Height, PikselFormat)
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.FillRectangle(Brushes.White, 0, 0, destWidth, destHeight)
        grPhoto.InterpolationMode = Interpolasyon

        grPhoto.DrawImage(imgPhoto, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)

        grPhoto.Dispose()
        Return bmPhoto
    End Function
    Public Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo

        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()

        Dim codec As ImageCodecInfo
        For Each codec In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next codec
        Return Nothing

    End Function

End Module
