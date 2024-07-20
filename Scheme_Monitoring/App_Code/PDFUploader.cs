using System;
using System.IO;
using System.Web;

public class PDFUploader
{
    // Directory to save PDF files
    private static string PdfDirectory = "~/PDFs/RecomendationLetters/";
    private const int MaxPdfSize = 10 * 1024 * 1024; // 10MB in bytes

    public static string UploadPDF(HttpPostedFile file, out string errorMessage)
    {
        errorMessage = string.Empty;

        // Check if the file is a PDF
        if (!IsPDF(file))
        {
            errorMessage = "The file is not a valid PDF.";
            return null;
        }

        // Check if the file size exceeds 10MB
        if (!IsValidPDFSize(file))
        {
            errorMessage = "The file size exceeds the 10MB limit.";
            return null;
        }

        // Ensure the directory exists
        string serverPath = HttpContext.Current.Server.MapPath(PdfDirectory);
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
        string relativePath = Path.Combine(PdfDirectory, uniqueFileName).Replace("\\", "/");
        return relativePath;
    }

    public static bool DeletePDF(string relativePath)
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

    private static bool IsPDF(HttpPostedFile file)
    {
        string[] validExtensions = { ".pdf" };
        string fileExtension = Path.GetExtension(file.FileName).ToLower();

        return Array.Exists(validExtensions, extension => extension == fileExtension);
    }

    private static bool IsValidPDFSize(HttpPostedFile file)
    {
        return file.ContentLength <= MaxPdfSize;
    }

    private static string GenerateUniqueFileName(string directory, string fileName)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        string extension = Path.GetExtension(fileName);
        string uniqueFileName = fileName;

        int counter = 1;
        while (File.Exists(Path.Combine(directory, uniqueFileName)))
        {
            uniqueFileName = fileNameWithoutExtension + "_" + counter + "" + extension;
            counter++;
        }

        return uniqueFileName;
    }
}
