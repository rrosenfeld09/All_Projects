using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApplication {
    public class JsonToFile<T> {
        public static List<T> ReadJson() {
            string filename = $"JsonData/{typeof(T).Name}.json";
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<T>)serializer.Deserialize(file, typeof(List<T>));
            }
        }
    }
}