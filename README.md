# ASPNET Image Resizer and Optimizer Without Quality Loss (Watermark Option)

### Save Resized / Watermarked image directly to SQL database :

<code>Resim_Duzenle(FileUpload1.PostedFile.InputStream)</code>

VB.NET Example : 

```csharp
   Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("sp_sistem_KULLANICILAR", cn) 
                    With cmd
                        .Connection.Open()
                        .CommandType = CommandType.StoredProcedure 
                        .Parameters.Add("@Resim", SqlDbType.Image)
                        .Parameters("@Resim").Value = IIf(FileUpload1.HasFile, Resim_Duzenle(FileUpload1.PostedFile.InputStream), IIf(Not FileUpload1.HasFile And Request.QueryString.Count = 0, System.IO.File.ReadAllBytes(Server.MapPath("~/Tasarim/img/users/user-1.png")), DBNull.Value)) 
                        .ExecuteNonQuery()
                    End With 
            End Using
End Using
```

C# Example : 

```csharp
 using (var cn = new SqlConnection(ConnectionString))
    {
        using (var cmd = new SqlCommand("sp_sistem_KULLANICILAR", cn))
        {
            cmd.Connection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Resim", SqlDbType.Image);
            cmd.Parameters("@Resim").Value = Interaction.IIf(FileUpload1.HasFile, Resim_Duzenle(FileUpload1.PostedFile.InputStream), Interaction.IIf(!FileUpload1.HasFile & Request.QueryString.Count == 0, File.ReadAllBytes(Server.MapPath("~/Tasarim/img/users/user-1.png")), DBNull.Value));
            cmd.ExecuteNonQuery();
        }
    }
```        

### Customize Resize Settings via variables

```vb
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
```
