using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateTXT
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] lines = { i.ToString() };
            //System.IO.File.WriteAllLines(@"D:\\WriteLines.txt", lines);   //←←一次寫入

            StreamWriter str = new StreamWriter(@"D:\\WriteLines.txt");    //→→一行一行寫入文字檔中
            for (var i = 0; i <= 100; i++)
            {
                str.WriteLine(i);
            }
            str.Close();
        }
    }
}
