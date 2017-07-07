using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 批量地址經緯度轉換器_Power_By_Google_Map_Api
{
    

    public partial class Form1 : Form
    {
        string fildadd;
        string saveadd;

        public Form1()
        {
            InitializeComponent();
            button1.Text = "選取檔案";
            button2.Text = "存檔位置";
            button3.Text = "確認轉換";
            label2.Text = "轉換檔案";
            label3.Text = "存儲位置";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);   //讀檔案用的
                fildadd = openFileDialog1.FileName.ToString();
                label2.Text = fildadd;
                sr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "文字文件|*.txt";
            saveFileDialog1.Title = "選擇儲存目標";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveadd = saveFileDialog1.FileName.ToString();
                label3.Text = saveadd;
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = Properties.Resources._2012070508485491;
            //pictureBox1.Image = Properties.Resources.END;
            label5.Text = "轉換中...";
            NewMethod();
        }

        private void NewMethod()
        {
            var file = new System.IO.StreamReader(fildadd);
            StreamWriter SuccessFile = new StreamWriter(saveadd.Remove(saveadd.Length - 4) + "Success_File.txt");
            StreamWriter FailFile = new StreamWriter(saveadd.Remove(saveadd.Length - 4) + "Fail_File.txt");
            string line;
            int Id = 1;
            while ((line = file.ReadLine()) != null)
            {
                label4.Text = $"目前進度:{Id}筆";
                Rootobject obj = GetDataFromMapApi(line);
                try
                {
                    //label4.Text = (Id + ":" + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                    SuccessFile.WriteLine(Id + "," + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                }
                catch
                {
                    //label4.Text = ($"第{Id}筆資料錯誤:" + line);
                    FailFile.WriteLine($"第{Id}筆資料錯誤" + line);

                }
                Id++;
                if (Id % 100 == 0)
                    Thread.Sleep(1);
                //測試測試測試測試測試測試測試測試測試測試測試測試測試測試
            }
            SuccessFile.Close();
            FailFile.Close();
            MessageBox.Show("轉檔完成");
        }

        private static Rootobject GetDataFromMapApi(string line)
        {
            string APIUrl = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&&sensor=false&language=zh-tw", line);
            var buffer = new WebClient().DownloadData(APIUrl);
            var json = Encoding.UTF8.GetString(buffer);
            var obj = JsonConvert.DeserializeObject<Rootobject>(json);
            return obj;
        }

        
    }
}
