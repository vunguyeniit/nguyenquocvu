using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Responses;

namespace SoKHCNVTAPI.Services;

public interface IFileService
{
    Task<FileUploadSummary> UploadFileAsync(IFormFile file, string type);
}

public class FileService : IFileService
{
    private const string UploadsSubDirectory = "Assets/Uploads";

    private readonly IEnumerable<string> allowedExtensions = new List<string> { ".zip", ".rar", ".png", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx" };

    public async Task<FileUploadSummary> UploadFileAsync(IFormFile file, string type)
    {
        var fileCount = 0;
        long totalSizeInBytes = 0;

        if(type == "")
        {
            type = "Khac";
        }
        var boundary = GetBoundary(MediaTypeHeaderValue.Parse(file.ContentType));
        var multipartReader = new MultipartReader(boundary, file.OpenReadStream());
        var section = await multipartReader.ReadNextSectionAsync();

        var filePaths = new List<string>();
        var notUploadedFiles = new List<string>();
        while (section != null)
        {
            var fileSection = section.AsFileSection();
            if (fileSection != null)
            {
                totalSizeInBytes += await SaveFileAsync(fileSection, type, filePaths, notUploadedFiles);
                fileCount++;
            }

            section = await multipartReader.ReadNextSectionAsync();
        }

        return new FileUploadSummary
        {
            TotalFilesUploaded = fileCount,
            TotalSizeUploaded = ConvertSizeToString(totalSizeInBytes),
            FilePaths = filePaths,
            NotUploadedFiles = notUploadedFiles
        };
    }

    private async Task<long> SaveFileAsync(FileMultipartSection fileSection, string type, IList<string> filePaths, IList<string> notUploadedFiles)
    {
        if (fileSection != null && fileSection.FileStream != null)
        {
            var extension = Path.GetExtension(fileSection.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                notUploadedFiles.Add(fileSection.FileName);
                return 0;
            }

            string finalSubDirectory = Path.Combine(UploadsSubDirectory, type, Utils.GetUUID());
            Directory.CreateDirectory(finalSubDirectory);

            var filePath = Path.Combine(finalSubDirectory, fileSection.FileName);

            await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024);

            await fileSection.FileStream.CopyToAsync(stream);

            filePaths.Add(GetFullFilePath(fileSection));

            return fileSection.FileStream.Length;
        }
        return 0;
    }

    private string GetFullFilePath(FileMultipartSection fileSection)
    {
        return !string.IsNullOrEmpty(fileSection.FileName)
            ? Path.Combine(Directory.GetCurrentDirectory(), UploadsSubDirectory, fileSection.FileName)
            : string.Empty;
    }

    private string ConvertSizeToString(long bytes)
    {
        var fileSize = new decimal(bytes);
        var kilobyte = new decimal(1024);
        var megabyte = new decimal(1024 * 1024);
        var gigabyte = new decimal(1024 * 1024 * 1024);

        return fileSize switch
        {
            _ when fileSize < kilobyte => "Less then 1KB",
            _ when fileSize < megabyte =>
                $"{Math.Round(fileSize / kilobyte, fileSize < 10 * kilobyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}KB",
            _ when fileSize < gigabyte =>
                $"{Math.Round(fileSize / megabyte, fileSize < 10 * megabyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}MB",
            _ when fileSize >= gigabyte =>
                $"{Math.Round(fileSize / gigabyte, fileSize < 10 * gigabyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}GB",
            _ => "n/a"
        };
    }

    private string GetBoundary(MediaTypeHeaderValue contentType)
    {
        var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

        if (string.IsNullOrWhiteSpace(boundary))
        {
            throw new InvalidDataException("Missing content-type boundary.");
        }

        return boundary;
    }
}