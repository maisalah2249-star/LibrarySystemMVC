namespace LibrarySystem.PL.Utilities
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);
            Directory.CreateDirectory(folderPath);

            string fileName = $"{Guid.NewGuid()}-{file.FileName}";
            string filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}