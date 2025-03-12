using System;
using System.IO;
using System.Web;

public class ImageUploader
{
    // Directory to save images
    private static string ImageDirectory = "~/Images/CrematoriumImages/";
    private const int MaxImageSize = 2 * 1024 * 1024; // 2MB in bytes

    public static string UploadImage(HttpPostedFile file, out string errorMessage)
    {
        errorMessage = string.Empty;

        // Check if the file is an image
        if (!IsImage(file))
        {
            errorMessage = "The file is not a valid image.";
            return null;
        }

        // Check if the file size exceeds 2MB
        if (!IsValidImageSize(file))
        {
            errorMessage = "The file size exceeds the 2MB limit.";
            return null;
        }

        // Ensure the directory exists
        string serverPath = HttpContext.Current.Server.MapPath(ImageDirectory);
        if (!Directory.Exists(serverPath))
        {
            Directory.CreateDirectory(serverPath);
        }



        // Generate a unique file name if there is a collision
        string uniqueFileName = GenerateUniqueFileName(serverPath, Path.GetFileName(file.FileName));

        // Save the file to the server
        string filePath = Path.Combine(serverPath, uniqueFileName);
        file.SaveAs(filePath);

        // Return the relative path for database storage
        string relativePath = Path.Combine(ImageDirectory, uniqueFileName).Replace("\\", "/");
        return relativePath;
    }

    public static string UploadImageWithSizeAndPath(HttpPostedFile file, string ImageDirectory, int MaxImageSize, out string errorMessage)
    {
        errorMessage = string.Empty;

        // Check if the file is a Image
        if (!IsImage(file))
        {
            errorMessage = "The file is not a valid Image.";
            return null;
        }


        // Check if the file size exceeds 10MB
        if (!IsValidImageSizeWithMaxImageSize(file, MaxImageSize))
        {
            errorMessage = "The file size exceeds the " + (MaxImageSize / 1024 / 1024).ToString() + " MB limit.";
            return null;
        }

        // Ensure the directory exists
        string serverPath = HttpContext.Current.Server.MapPath(ImageDirectory);
        if (!Directory.Exists(serverPath))
        {
            Directory.CreateDirectory(serverPath);
        }

        // Generate a unique file name if there is a collision
        string uniqueFileName = GenerateUniqueFileName(serverPath, Path.GetFileName(file.FileName));

        // Save the file to the server
        string filePath = Path.Combine(serverPath, uniqueFileName);
        file.SaveAs(filePath);

        // Return the relative path for database storage
        string relativePath = Path.Combine(ImageDirectory, uniqueFileName).Replace("\\", "/");
        return relativePath;
    }

    private static bool IsValidImageSizeWithMaxImageSize(HttpPostedFile file, int MaxImageSize)
    {
        return file.ContentLength <= MaxImageSize;
    }

    public static bool DeleteImage(string relativePath)
    {
        try
        {
            string serverPath = HttpContext.Current.Server.MapPath(relativePath);
            if (File.Exists(serverPath))
            {
                File.Delete(serverPath);
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool IsImage(HttpPostedFile file)
    {
        string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        string fileExtension = Path.GetExtension(file.FileName).ToLower();

        return Array.Exists(validExtensions, extension => extension == fileExtension);
    }

    private static bool IsValidImageSize(HttpPostedFile file)
    {
        return file.ContentLength <= MaxImageSize;
    }

    private static string GenerateUniqueFileName(string directory, string fileName)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        string extension = Path.GetExtension(fileName);
        string uniqueFileName = fileName;

        int counter = 1;
        while (File.Exists(Path.Combine(directory, uniqueFileName)))
        {
            uniqueFileName = fileNameWithoutExtension+"_"+counter+""+extension;
            counter++;
        }

        return uniqueFileName;
    }
}
