using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ReadText
{
    class Program
    {
        private static object JsonConvert;

        static void Main(string[] args)
        {
            //int counter = 0;
            //string line;

            //// Read the file and display it line by line.
            //System.IO.StreamReader file =
            //   new System.IO.StreamReader("E:\\test.txt");
            //while ((line = file.ReadLine()) != null)
            //{
            //    Console.WriteLine(line);
            //    counter++;
            //}

            //file.Close();

            //// Suspend the screen.
            //Console.ReadLine();

            ////Timer timer = new Timer(5000);
            ////timer.Elapsed += async (sender, e) => await HandleTimer();
            ////timer.Start();
            ////Console.Write("Press any key to exit... ");
            ////Console.ReadKey();

            //------------------------------------
            //var file = new System.IO.StreamReader("E:\\test.txt");
            //string line;
            //var Id = 1;
            //while ((line = file.ReadLine()) != null)
            //{
            //    Timer time = new Timer(1500);
            //    time.Elapsed += async (sender, e) => await NewMethod(line);
            //    time.Start();
            //Console.WriteLine(line);
            //}
            //Console.ReadLine();
            //------------------------------------
            var file = new System.IO.StreamReader("E:\\test.txt");
            string line;

            if ((line = file.ReadLine()) != null)
            {
                line = NewMethod(file);
            }
            else
            {
            Console.ReadLine();
                return;
            }


        }

        private static string NewMethod(System.IO.StreamReader file)
        {
            string line;
            

            while ((line = file.ReadLine()) != null)
            {
                SetTimeout(1000, delegate
            {
                Console.WriteLine(line);
             });}
            return line;
        }

        public static void SetTimeout(double interval, Action action)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Enabled = false;
                action();
            };
            timer.Enabled = true;
        }



        ////public static void SetInterval(double interval, Action<ElapsedEventArgs> action)   //在指定時間週期重複執行指定的表達式
        ////{
        ////    System.Timers.Timer timer = new System.Timers.Timer(interval);
        ////    timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArg s e)
        ////    {
        ////        action(e);
        ////    };
        ////    timer.Enabled = true;
        ////}



        ////private static Task HandleTimer()
        ////{
        ////    Console.WriteLine("你好");
        ////    throw new NotImplementedException();
        ////}


    }
}

