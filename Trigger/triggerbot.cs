using System;
using System.Drawing;
using System.Windows.Forms;
using Memory;
using System.Runtime.InteropServices;
using System.Threading;
 
namespace TriggerBot
{
    public partial class funcao : func
    {
        Mem m = new Mem();
        private const string PROCESSNAME = "GTA5.exe";
        bool gta5Running;
 
        [DllImport("User32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);
 
        [DllImport("user32.dll")]
        public static extern void mouse_event(int a, int b, int c, int d);
 
        int leftDown = 0x02;
        int leftUp = 0x04;
        int CHEAT = 0;
 
        //Metódo
        void tiro(int delay)
        {
            mouse_event(leftDown, 0, 0, 0, 0);
            Thread.Sleep(1);
            mouse_event(leftUp, 0, 0, 0, 0);
            Thread.Sleep(delay);
        }
 
        void tiroAutomático()
        {
            while (true)
            {
                if (GetAsyncKeyState(Keys.RButton) < 0)
                {
                    CHEAT = m.ReadInt("GTA5.exe+1FB2380");
                    if (CHEAT >= 1 && Autoshoot_CheckBox.Checked)
                    {
                        Shoot(7);
                        Shoot(1);
                    }
                }
                Thread.Sleep(1);
            }
        }
 
        public funcao()
        {
            InitializeComponent();
        }
 
        private void funcao_Load(object sender, EventArgs e)
        {
            int PID = m.GetProcIdFromName(PROCESSNAME);
            if (PID > 0)
            {
                m.OpenProcess(PID);
                Thread AUTOSHOOT = new Thread(AutoShoot) { IsBackground = true };
                AUTOSHOOT.Start();
            }
            Author_Label.ForeColor = Color.FromArgb(169, 0, 0);
            Author_Label.Text = "Made by NightFyreTV";
        }
 
 
        private void ProcessTimer_Tick(object sender, EventArgs e)
        {
            int PID = m.GetProcIdFromName(PROCESSNAME);
            if (PID > 0)
            {
                m.OpenProcess(PID);
                Process_Label.ForeColor = Color.FromArgb(0, 169, 0);
                Process_Label.Text = "GTAV CONNECTED";
                gta5Running = true;
                return;
            }
            Process_Label.ForeColor = Color.FromArgb(169, 0, 0);
            Process_Label.Text = "GTAV NOT DETECTED";
            gta5Running = false;
        }
    }
}