using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace CustomerProductClasses
{
    // The implementation of each method in this class has changed.
    // Uses xml serialization infrastructure rather than explicitly creating xml elements.
    public static class ProductDB
    {
        private const string Path = @"..\..\..\data\Products.xml";

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            FileStream fs = new FileStream(Path, FileMode.Open);

            Type[] productTypes = { typeof(Product), typeof(Clothing), typeof(Gear) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), productTypes);
            products = (List<Product>)serializer.Deserialize(fs);
            return products;
        }

        public static void SaveProducts(List<Product> products)
        {
            Type[] productTypes = { typeof(Product), typeof(Clothing), typeof(Gear) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), productTypes);
            FileStream fs = new FileStream(Path, FileMode.Create);
            serializer.Serialize(fs, products);
            fs.Close();

        }
    }
}
