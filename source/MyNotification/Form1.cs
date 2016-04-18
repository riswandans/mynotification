using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;
using Microsoft.Win32;

namespace MyNotification
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer _voice;

        public Form1()
        {
            InitializeComponent();
            _voice = new SpeechSynthesizer();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width - 10, screen.WorkingArea.Bottom - this.Height - 20);
            base.OnLoad(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Load your setting */
            this.BackgroundImage = Image.FromFile(@Application.StartupPath + "\\config\\background.png");
            this.title.Text = File.ReadAllText(@Application.StartupPath + "\\config\\title.ini");
            this.content.Text = File.ReadAllText(@Application.StartupPath + "\\config\\content.ini");
            this.avatar.Image = Image.FromFile(@Application.StartupPath + "\\config\\avatar.png");
            this.avatar.SizeMode = PictureBoxSizeMode.Zoom;

            /* Auto hide when 6 sec */
            timer1.Enabled = true;
            timer1.Interval = 6000;
            timer1.Start();

            /* Speech */
            _voice.SpeakAsync(content.Text);

            /* Set Startup */
            SetStartup();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetStartup()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            registry.SetValue("MyNotification", Application.ExecutablePath.Replace("/", "\\"));
        }
}

}
