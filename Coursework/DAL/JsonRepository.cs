using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace DAL
{
    public class JsonRepository<T> : IRepositiry<T> where T : class
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonRepository(string fileName)
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, fileName);

            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true
            };
        }

        public void SaveAll(List<T> items)
        {
            try
            {
                if (items == null)
                    items = new List<T>();

                string json = JsonSerializer.Serialize(items, _jsonOptions);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка збереження даних у файл {_filePath}: {ex.Message}", ex);
            }
        }

        public List<T> LoadAll()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<T>();

                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                    return new List<T>();

                var items = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);
                return items ?? new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка завантаження даних з файлу {_filePath}: {ex.Message}", ex);
            }
        }

        public bool FileExists()
        {
            return File.Exists(_filePath);
        }

        public void DeleteFile()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
