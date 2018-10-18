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
            //Console.Write(datas);
            Console.WriteLine("空氣品質指標(AQI)");
            Console.Write("輸入0顯示完整資訊，輸入1顯示以空汙指標物分類，輸入2顯示以狀態分類:");
            var input = Console.Read();
            //Console.Write(input);
            showData(datas, input);
            Console.Write("請按任意鍵結束...");
            Console.ReadKey();
        }

        static List<OpenData> loadData()
        {
            List<OpenData> result = new List<OpenData>();

            var xml = XElement.Load("data.xml");
            var nodes = xml.Descendants("row").ToList();

            //Console.Write(nodes.Count);
            

            for(var i=0; i<nodes.Count; i++)
            {
                var node = nodes[i];
                OpenData item = new OpenData();

                item.縣市 = node.Element("Col2").Value;
                item.空氣品質指標 = int.Parse(node.Element("Col3").Value);
                item.空氣汙染指標物 = node.Element("Col4").Value;
                item.狀態 = node.Element("Col5").Value;

                result.Add(item);
            }
            return result;
        }

        private static string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }
        public static void showData(List<OpenData> nodes, int f)
        {
            Console.WriteLine("共 " + nodes.Count + " 筆資料");
            if (f == 48) {                 
                nodes.ForEach(group =>
                {
                    var country = group.縣市;
                    var AQI = group.空氣品質指標;
                    var pollutant = group.空氣汙染指標物;
                    var status = group.狀態;
                    if (pollutant == "")
                        pollutant = "無";
                    Console.WriteLine("縣市名：" + country + "\t空氣品質指標：" + AQI);
                    //Console.WriteLine("郵遞區號：" + number);
                    Console.WriteLine("空氣汙染指標物：" + pollutant);
                    Console.WriteLine("狀態：" + status);
                    //Console.WriteLine("-----");
                });
            }
            else if(f == 49){ 
                nodes.GroupBy(node => node.空氣汙染指標物).ToList()
                    .ForEach(group =>
                    {
                        var key = group.Key;
                        if (key == "")
                            key = "無";
                        var groupDatas = group.ToList();
                        var msg = $"空氣汙染指標物:{key},共有{groupDatas.Count()}筆資料";
                        Console.WriteLine(msg);
                    });
            }
            else {
                nodes.GroupBy(node => node.狀態).ToList()
                    .ForEach(group =>
                    {
                        var key = group.Key;
                        var groupDatas = group.ToList();
                        var msg = $"狀態:{key},共有{groupDatas.Count()}筆資料";
                        Console.WriteLine(msg);
                    });
            }
        }
    }
}
