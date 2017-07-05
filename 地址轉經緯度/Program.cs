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
    class Program
    {
        static void Main(string[] args)
        {
            again:
            Console.WriteLine("請輸入轉換文字檔之連結地址");
            string add = Console.ReadLine();
            try
            {
                //ReadTxtFile
                var file = new System.IO.StreamReader(add);
                //Create Success File & Fail File
                StreamWriter SuccessFile = new StreamWriter("D:\\轉檔完成檔-成功.txt");   
                StreamWriter FailFile = new StreamWriter("D:\\轉檔完成檔-失敗.txt");      

                string line;
                var Id = 1;
                while ((line = file.ReadLine()) != null)
                {
                    Rootobject obj = NewMethod(line);
                    try
                    {
                        Console.WriteLine(Id + ":" + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                        SuccessFile.WriteLine(Id + "," + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                    }
                    catch
                    {
                        Console.WriteLine($"第{Id}筆資料錯誤:" + line);
                        FailFile.WriteLine($"第{Id}筆資料錯誤" + line);

                    }
                    if (Id % 100 == 0)
                        Thread.Sleep(1);
                    Id++;
                }
                SuccessFile.Close();
                FailFile.Close();
                Console.WriteLine("OK，請至D槽查看");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("請輸入正確目標地址");
                Console.ReadLine();
                goto again;
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

        //public static void SetTimeout(double interval, Action action , string data)
        //{
        //    System.Timers.Timer timer = new System.Timers.Timer(interval);
        //    timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
        //    {
        //        timer.Enabled = false;
        //        action();
        //    };
        //    timer.Enabled = true;
        //}
    }
}
