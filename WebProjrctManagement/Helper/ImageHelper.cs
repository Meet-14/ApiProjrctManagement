namespace WebProjrctManagement.Helper
{
    public static class ImageHelper
    {
        public static string SaveImageToFile(IFormFile imageFile, string directoryPath)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string fileExtension = Path.GetExtension(imageFile.FileName);
            if (string.IsNullOrWhiteSpace(fileExtension))
                fileExtension = ".jpeg";

            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string fullPath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return fileName; // Return only the file name
        }
    }
}

