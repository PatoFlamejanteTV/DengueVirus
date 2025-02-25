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
            //int i = 0;
            for (; ; )
            {
                /*if (i > 1000)
                {
                    i = 0;
                }*/
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
                Thread.Sleep(400);
                //Thread.Sleep(1000-i);
                //i+=10;
            }
        }
        public static void ShakeMouse()
        {
            //gets mouse pointer and shakes in both x & y directions
            Console.WriteLine("ShakeMouse");

            // get cursor
            Cursor c = new Cursor(Cursor.Current.Handle);

            // change its xy pos
            for (int i = 0; i < 100; i++)
            {
                Cursor.Position = new Point(
                    Cursor.Position.X + i, 
                    Cursor.Position.Y + i
                    );

                Thread.Sleep(100 - i);

                Cursor.Position = new Point(
                    Cursor.Position.X + i, 
                    Cursor.Position.Y - i
                    );

                Thread.Sleep(100 - i);

                Cursor.Position = new Point(
                    Cursor.Position.X - i, 
                    Cursor.Position.Y - i
                    );

                Thread.Sleep(100 - i);

                Cursor.Position = new Point(
                    Cursor.Position.X - i, 
                    Cursor.Position.Y + i
                    );

                Thread.Sleep(100 - i);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(ErrorSounds);t.Start();
            Thread.Sleep(10000);

            Thread t2 = new Thread(ShakeMouse); t2.Start();
            Thread.Sleep(4000);
            t2.Abort();
        }
    }
}
