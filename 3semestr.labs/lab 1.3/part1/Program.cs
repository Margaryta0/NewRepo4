using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace part1
{
    internal class Program
    {
        static void Main(string[] args)
        {


            MyString[] arr = new MyString[4];
            arr[0] = new MyString("Megi");
            arr[1] = new MyString("Olena");
            arr[2] = new MyString("Tatti");
            arr[3] = new MyString("Ruvim");

            Console.WriteLine("Array: ");
            foreach (var item in arr)
                Console.WriteLine($"{item.Output()} ({item.Length})");

            BinarySerialize(arr, "binary.dat");
            var restoredBin = BinaryDeserialize("binary.dat");

            Console.WriteLine("\nArray from binary: ");
            foreach (var item in restoredBin)
                Console.WriteLine(item.Output());

            XmlSerialize(arr, "data.xml");
            var restoredXml = XmlDeserialize("data.xml");

            Console.WriteLine("\nRestored XML: ");
            foreach (var item in restoredXml)
                Console.WriteLine(item.Output());

            JsonSerialize(arr, "data.json");
            var restoredJson = JsonDeserialize("data.json");

            Console.WriteLine("\nRestored JSON:");
            foreach (var item in restoredJson)
                Console.WriteLine(item.Output());

            List<MyString> list = new List<MyString>(arr);
            JsonSerializeList(list, "list.json");
            var restoredList = JsonDeserializeList("list.json");

            Console.WriteLine("\nRestored JSON List:");
            foreach (var item in restoredList)
                Console.WriteLine(item.Output());

            Console.WriteLine($"Array: {arr.Length}");
            Console.WriteLine($"Collection: {list.Count}");
        }

        static void BinarySerialize(MyString[] arr, string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                bf.Serialize(fs, arr);
            }
        }

        static MyString[] BinaryDeserialize(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                return (MyString[])bf.Deserialize(fs);
            }
        }

        static void XmlSerialize(MyString[] arr, string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(MyString[]));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                xs.Serialize(fs, arr);
            }
        }

        static MyString[] XmlDeserialize(string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(MyString[]));
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                return (MyString[])xs.Deserialize(fs);
            }
        }

        static void JsonSerialize(MyString[] arr, string filename)
        {
            string json = JsonConvert.SerializeObject(arr, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        static MyString[] JsonDeserialize(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<MyString[]>(json);
        }


        static void JsonSerializeList(List<MyString> list, string filename)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        static List<MyString> JsonDeserializeList(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<List<MyString>>(json);
        }
    }
}
