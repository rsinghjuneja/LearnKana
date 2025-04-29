using System.Diagnostics;

using Newtonsoft.Json;

namespace LearnKana.Shared
{
    public static class JsonDatabase
    {
        public static JsonSerializerSettings JsonSerializerSettings { get; } = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
        };


        public static async Task<T?> ReadFileAsync<T>(string directory, string filename, CancellationToken? token = default) where T : class
            => await ReadFileAsync<T>(directory, filename, JsonSerializerSettings, token);
        public static async Task<T?> ReadFileAsync<T>(string directory, string filename, JsonSerializerSettings? settings = null, CancellationToken? token = default) where T : class
        {
            string filePath = Path.Combine(directory, filename);
            Debug.WriteLine($"Reading file: [{filePath}]");

            try
            {
                if (!File.Exists(filePath))
                {
                    Debug.WriteLine("File does not exist.");
                    return default;
                }

                using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync(token ?? CancellationToken.None);

                token?.ThrowIfCancellationRequested();
                T? obj = JsonConvert.DeserializeObject<T>(json, settings ?? JsonSerializerSettings);
                return obj;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return default;
        }

        public static async Task<bool> SaveFileAsync<T>(T? file, string directory, string filename, CancellationToken? token = default) where T : class
            => await SaveFileAsync(file, directory, filename, JsonSerializerSettings, token);
        public static async Task<bool> SaveFileAsync<T>(T? file, string directory, string filename, JsonSerializerSettings? settings = null, CancellationToken? token = default) where T : class
        {
            try
            {
                token?.ThrowIfCancellationRequested();
                string json = JsonConvert.SerializeObject(file, settings ?? JsonSerializerSettings);
                await WriteJsonAsync(json, directory, filename, token);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return false;
        }

        public static async Task<bool> SaveContentAsync(string? contents, string directory, string filename, CancellationToken? token = default)
        {
            try
            {
                await WriteJsonAsync(contents, directory, filename, token);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return false;
        }

        private static async Task WriteJsonAsync(string? contents, string directory, string filename, CancellationToken? token = default)
        {
            token?.ThrowIfCancellationRequested();
            string filePath = Path.Combine(directory, filename);
            await File.WriteAllTextAsync(filePath, contents, token ?? CancellationToken.None);
        }
    }
}