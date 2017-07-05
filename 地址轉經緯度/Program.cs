using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace 地址轉經緯度
{
    public class Rootobject
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }

    public class Result
    {
        public Address_Components[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public bool partial_match { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

    public class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Northeast
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Southwest
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Viewport
    {
        public Northeast1 northeast { get; set; }
        public Southwest1 southwest { get; set; }
    }

    public class Northeast1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Southwest1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Address_Components
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //string APIUrl = string.Format("http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false&language=zh-tw", Console.ReadLine());
            fuck:
            Console.WriteLine("請輸入轉換文字檔之連結地址");
            string add = Console.ReadLine();

            //ReadTxtFile
            try
            {
                var file = new System.IO.StreamReader(add);
                StreamWriter str = new StreamWriter("D:\\轉檔完成檔-成功.txt");    //→→一行一行寫入文字檔中
                StreamWriter str2 = new StreamWriter("D:\\轉檔完成檔-失敗.txt");    //→→一行一行寫入文字檔中

                string line;
                var Id = 1;
                //try
                //{
                while ((line = file.ReadLine()) != null)
                {
                    //Timer time = new Timer(1500);
                    //time.Elapsed += async (sender, e) => await testc();
                    //time.Start();

                    Rootobject obj = NewMethod(line);
                    try
                    {
                        Console.WriteLine(Id + ":" + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                        str.WriteLine(Id + "," + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                    }
                    catch
                    {
                        Console.WriteLine($"第{Id}筆資料錯誤:" + line);
                        str2.WriteLine($"第{Id}筆資料錯誤" + line);

                    }
                    if (Id % 100 == 0)
                        Thread.Sleep(10);
                    Id++;
                }
                str.Close();
                str2.Close();
                Console.WriteLine("OK，請至D槽查看");
                Console.ReadLine();
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine($"第{Id}筆資料錯誤");
                //    str.WriteLine($"第{Id}筆資料錯誤");
                //    e.Data.Clear();
                //    Id++;

                //    goto fuck;
                //}


            }
            catch
            {
                Console.WriteLine("請輸入正確目標地址");
                Console.ReadLine();
                goto fuck;
            }
        }
            
        private static Rootobject NewMethod(string line)
        {
          
            string APIUrl = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&&sensor=false&language=zh-tw", line);
            var buffer = new WebClient().DownloadData(APIUrl);
            var json = Encoding.UTF8.GetString(buffer);
            var obj = JsonConvert.DeserializeObject<Rootobject>(json);
            return obj;
        }

        public static void SetTimeout(double interval, Action action , string data)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Enabled = false;
                action();
            };
            timer.Enabled = true;
        }
    }
}
