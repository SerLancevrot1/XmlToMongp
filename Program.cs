using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace XmlJson
{
    class Array
    {
        public string ID { get; set; }
        public double First { get; set; }
        public double Second { get; set; }
        public double Thid { get; set; }
        public double Fourth { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            XmlToMongo xml = new XmlToMongo();
            xml.ReadXml();
        }
    }

    class XmlToMongo
    {
        public void ReadXml()
        {
            List<Array> arr = new List<Array>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\WBD\\source\\repos\\XmlJson\\XmlJson\\XMLFile1.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Array user = new Array();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "ID")
                        user.ID = childnode.InnerText;
                    if (childnode.Name == "First")
                        user.First = Double.Parse(childnode.InnerText);
                    if (childnode.Name == "Second")
                        user.Second = Double.Parse(childnode.InnerText);
                    if (childnode.Name == "Thid")
                        user.Thid = Double.Parse(childnode.InnerText);
                    if (childnode.Name == "Fourth")
                        user.Fourth = Double.Parse(childnode.InnerText);
                }
                SaveDocs(user);
                arr.Add(user);
            }
            foreach (Array u in arr)
            {
                Console.WriteLine($"{u.ID}, 2117={u.First}  2122={u.Second}   2125={u.Thid}  2130={u.Fourth}");
                Console.WriteLine();
            }
            Console.Read();
        }

        static async Task SaveDocs(Array post)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            string date = new DateTime(year, month, 1).ToShortDateString();

            MongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase DB = client.GetDatabase("testXml");
            IMongoCollection<Array> collection = DB.GetCollection<Array>(date);
            await collection.InsertOneAsync(post);
        }
    }
}
