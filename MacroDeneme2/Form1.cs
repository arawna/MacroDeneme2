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
        }
        public static Process cmd;
        public static int YDeger = 5;
        public static int XDeger = 0;
        public static int UykuDeger = 25;
        public static bool Aktif = true;
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

            Thread repeaterThread = new Thread(() => ListenerThread());
            repeaterThread.SetApartmentState(ApartmentState.STA);
            repeaterThread.Start();
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
        public static void Oynat()
        {
            cmd.StandardInput.WriteLine("$p1 = [System.Windows.Forms.Cursor]::Position.X + "+ XDeger);
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine("$p2 = [System.Windows.Forms.Cursor]::Position.Y + "+YDeger);
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine("[System.Windows.Forms.Cursor]::Position = New-Object System.Drawing.Point($p1, $p2)");
            cmd.StandardInput.Flush();
            Thread.Sleep(UykuDeger);
        }
    }
}
