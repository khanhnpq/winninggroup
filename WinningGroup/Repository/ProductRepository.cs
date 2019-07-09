using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WinningGroup.Models;

namespace WinningGroup.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProductById(int id)
        {
            string json = String.Empty;
            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            using (StreamReader sr = new StreamReader($@"{currentDir}\Assets\Products\{id}.json"))
            {
                json = sr.ReadToEnd();
            }

            JObject jObject = JObject.Parse(json);
            return jObject.ToObject<Models.Product>();
        }

        public string AddProduct()
        {
            Random idRandom = new Random();
            var id = idRandom.Next(4, 99);
            var product = new Product { ID = id, Name = $@"Product{id}" };

            var json = JsonConvert.SerializeObject(product);

            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!Directory.Exists(currentDir))
            {
                Directory.CreateDirectory(currentDir);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter($@"{currentDir}\Assets\Products\{id}.json"))
            {
                file.WriteLine(json);
            }

            return json;
        }
    }
}
