using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MacroDeneme2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormClosing += Form1_FormClosing;
        }
        public static Process cmd;
        public static int YDeger = 5;
        public static int XDeger = 0;
        public static int UykuDeger = 25;
        public static bool Aktif = true;
        public static Thread repeaterThread;
        private void Form1_Load(object sender, EventArgs e)
        {
            cmd = new Process();
            cmd.StartInfo.FileName = "powershell.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("Add-Type -AssemblyName System.Windows.Forms");
            cmd.StandardInput.Flush();

            textBox1.Text = YDeger.ToString();
            textBox2.Text = XDeger.ToString();
            textBox3.Text = UykuDeger.ToString();

            repeaterThread = new Thread(() => ListenerThread());
            repeaterThread.SetApartmentState(ApartmentState.STA);
            repeaterThread.Start();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            repeaterThread.Abort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDeger = Int32.Parse(textBox2.Text);
            YDeger = Int32.Parse(textBox1.Text);
            UykuDeger = Int32.Parse(textBox3.Text);
        }

        public static void ListenerThread()
        {
            while (true)
            {
                if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                {
                    if (Aktif)
                    {
                        Oynat();
                    }
                    
                }
                if((Control.MouseButtons & MouseButtons.XButton1) == MouseButtons.XButton1)
                {
                    Aktif = !Aktif;
                }
            }
        }
        public static string XText = "X6G31YL8G9jLgjMBfkRvHKluBq6ABOZk1iMrrC4zy6dnIQ1dhIrGW5aUTqtBi/QOu5xfQYCOwp3cknH3IxjMgQ==";
        public static string YText = "kSxVQ0cyeImFoyIgBhQRmf8xIrzpQlumplVr6ATsocfIodYSCxYyW8TOjZ09wmfGwubdOQTd4z6a6Q5HctaigQ==";
        public static string MoveText = "rhGiwrtHM51RGtDdxANu7lzwGkuzDBG67KXAAm9gYu6noEafLGHDCIiHYGbOADADxJ2e6lzcT4FBAdKQqALjcdcqikegIcoKuFceXIUAmli/WYy/iFNrFFt2b1xsC51M";
        public static void Oynat()
        {
            cmd.StandardInput.WriteLine(XText + XDeger);
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine(YText + YDeger);
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine(MoveText);
            cmd.StandardInput.Flush();
            Thread.Sleep(UykuDeger);
        }
    }
}
