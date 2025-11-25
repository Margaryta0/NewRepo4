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
    public class JsonRepository<T, TId> : IRepositiry<T, TId> where T : class, IEntity<TId>
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
        private List<T> LoadAllInternal()
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

        private void SaveAllInternal(List<T> items)
        {
            try
            {
                string json = JsonSerializer.Serialize(items ?? new List<T>(), _jsonOptions);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка збереження даних у файл {_filePath}: {ex.Message}", ex);
            }
        }

        public T GetById(TId id)
        {
            var items = LoadAllInternal();
            return items.FirstOrDefault(item => EqualityComparer<TId>.Default.Equals(item.Id, id));
        }

        public List<T> GetAll()
        {
            return LoadAllInternal();
        }

        public void Add(T entity)
        {
            var items = LoadAllInternal();

            if (items.Any(item => EqualityComparer<TId>.Default.Equals(item.Id, entity.Id)))
            {
            }

            items.Add(entity);
            SaveAllInternal(items);
        }

        public void Update(T entity)
        {
            var items = LoadAllInternal();

            var existingItem = items.FirstOrDefault(item => EqualityComparer<TId>.Default.Equals(item.Id, entity.Id));

            if (existingItem == null)
            {
                throw new KeyNotFoundException($"Елемент з ID {entity.Id} не знайдено для оновлення.");
            }

            int index = items.IndexOf(existingItem);
            items[index] = entity;

            SaveAllInternal(items);
        }

        public void Delete(TId id)
        {
            var items = LoadAllInternal();

            var itemToRemove = items.FirstOrDefault(item => EqualityComparer<TId>.Default.Equals(item.Id, id));

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                SaveAllInternal(items);
            }
        }

        public TId GetNextId()
        {
            if (typeof(TId) == typeof(int))
            {
                var items = LoadAllInternal();
                int maxId = items.Any() ? items.Cast<IEntity<int>>().Max(d => d.Id) : 0;
                return (TId)(object)(maxId + 1);
            }

            return default(TId);
        }
    }
}
