using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using XmlLoader.Models;

namespace XmlLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("123");
            var datas = loadData();
            // Console.Write(datas);
            showData(datas);
            Console.ReadKey();
        }

        static List<OpenData> loadData()
        {
            var xml = XElement.Load("data.xml");
            var nodes = xml.Descendants("row").ToList();
            List<OpenData> result = new List<OpenData>();

            for(var i=0; i<nodes.Count; i++)
            {
                var node = nodes[i];
                OpenData item = new OpenData();

                item.行政區名 = node.Element("Col1").Value;
                item.郵遞區號 = int.Parse(node.Element("Col2").Value);
                item.中心點經度 = node.Element("Col3").Value;
                item.中心點緯度 = node.Element("Col4").Value;


                result.Add(item);
            }
            return result;
        }
        private static string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }
        public static void showData(List<OpenData> nodes)
        {
            Console.WriteLine("共" + nodes.Count + "筆資料");
            nodes.ForEach(group =>
            {
                if (group.郵遞區號 >= 800 && group.郵遞區號 <= 852) {
                    var name = group.行政區名;
                    var number = group.郵遞區號;
                    var lnt = group.中心點經度;
                    var lng = group.中心點緯度;
                    Console.WriteLine("行政區名：" + name + "\t郵遞區號：" + number);
                    //Console.WriteLine("郵遞區號：" + number);
                    Console.WriteLine("中心點經度：" + lnt);
                    Console.WriteLine("中心點緯度：" + lng);
                    Console.WriteLine("-----");
                    }
            });
            Console.WriteLine("以上僅列出高雄市部分");
        }
    }
}
