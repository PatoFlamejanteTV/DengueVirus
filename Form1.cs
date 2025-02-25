using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DengueVirus
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void ErrorSounds()
        {
            Console.WriteLine("ErrorSounds");
            int i = 0;
            for (; ; )
            {
                if (i > 1000)
                {
                    i = 0;
                }
                int num = new Random().Next(2);
                if (num != 0)
                {
                    if (num == 1)
                    {
                        SystemSounds.Hand.Play();
                    }
                }
                else
                {
                    SystemSounds.Exclamation.Play();
                }
                Thread.Sleep(1000-i);
                i+=10;
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(ErrorSounds);t.Start();
            Thread.Sleep(100000);
            t.Abort();
        }
    }
}
