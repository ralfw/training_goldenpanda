using System.IO;
using System.Threading.Tasks;

namespace AppointmentData
{
    public static class FileWriter
    {
        public static async Task WriteFileContentAsync(string filePath, string content)
        {
            using (var writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(content);
                await writer.FlushAsync();
            }
        }
    }
}
