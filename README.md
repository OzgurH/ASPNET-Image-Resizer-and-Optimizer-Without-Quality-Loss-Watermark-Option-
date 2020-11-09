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
        
