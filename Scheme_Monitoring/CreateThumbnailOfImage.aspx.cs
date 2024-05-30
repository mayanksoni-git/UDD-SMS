using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

public partial class CreateThumbnailOfImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ErrorMessage.Visible = false;
        try
        {
            if (FileUpload1.HasFile)
            {
                // Save the uploaded image to a stream
                Stream uploadedImageStream = FileUpload1.PostedFile.InputStream;
                using (Bitmap originalImage = new Bitmap(uploadedImageStream))
                {
                    // Create the thumbnail
                    int thumbnailWidth = 100; // Set the width for the thumbnail
                    int thumbnailHeight = (int)(originalImage.Height * (100.0 / originalImage.Width)); // Calculate height proportionally

                    using (Bitmap thumbnailImage = CreateThumbnail(originalImage, thumbnailWidth, thumbnailHeight))
                    {
                        // Define the directory to save the thumbnail
                        string thumbnailsDir = Server.MapPath("~/Thumbnails");
                        if (!Directory.Exists(thumbnailsDir))
                        {
                            Directory.CreateDirectory(thumbnailsDir);
                        }

                        // Define the path to save the thumbnail
                        string thumbnailFileName = Path.GetFileNameWithoutExtension(FileUpload1.FileName) + "_thumb.png";
                        string thumbnailPath = Path.Combine(thumbnailsDir, thumbnailFileName);

                        // Save the thumbnail to the directory
                        thumbnailImage.Save(thumbnailPath, ImageFormat.Png);

                        // Display the thumbnail in an Image control
                        ThumbnailImage.ImageUrl = "~/Thumbnails/" + thumbnailFileName;
                    }
                }
            }
            else
            {
                ErrorMessage.Text = "Please select a file to upload.";
                ErrorMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage.Text = "An error occurred: " + ex.Message;
            ErrorMessage.Visible = true;
        }
    }

    private Bitmap CreateThumbnail(Image originalImage, int width, int height)
    {
        // Create a new blank image with the specified size
        Bitmap thumbnailImage = new Bitmap(width, height);
        using (Graphics graphics = Graphics.FromImage(thumbnailImage))
        {
            // Set the quality of the interpolation
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            // Calculate the aspect ratio and resize dimensions
            float originalAspectRatio = (float)originalImage.Width / originalImage.Height;
            float thumbnailAspectRatio = (float)width / height;

            int resizedWidth, resizedHeight;
            if (originalAspectRatio > thumbnailAspectRatio)
            {
                resizedWidth = width;
                resizedHeight = (int)(width / originalAspectRatio);
            }
            else
            {
                resizedWidth = (int)(height * originalAspectRatio);
                resizedHeight = height;
            }

            // Draw the original image on the new image with the specified size
            graphics.DrawImage(originalImage, 0, 0, resizedWidth, resizedHeight);
        }
        return thumbnailImage;
    }
}