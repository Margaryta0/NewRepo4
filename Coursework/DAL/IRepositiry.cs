using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepositiry<T> where T : class
    {
        void SaveAll(List<T> items);

        List<T> LoadAll();

        bool FileExists();

        void DeleteFile();

        string GetFilePath();
    }
}