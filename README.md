# ASPNET Image Resizer and Optimizer Without Quality Loss (Watermark Option)

Save Resized / Watermarked image directly to SQL database :

<code>Resim_Duzenle(FileUpload1.PostedFile.InputStream)</code>

VB.NET Example : 

<code>
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
  </code>
