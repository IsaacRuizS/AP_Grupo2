using System;
using System.IO;

namespace RF.Core.Helpers
{
    public static class FileHelper
    {
        public static string GenerateUniqueFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            string extension = Path.GetExtension(fileName);
            string name = Path.GetFileNameWithoutExtension(fileName);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            
            return $"{name}_{timestamp}{extension}";
        }
    }
}
