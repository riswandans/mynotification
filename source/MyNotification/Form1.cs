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

namespace MyNotification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width - 10, screen.WorkingArea.Bottom - this.Height - 20);
            base.OnLoad(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.title.Text = File.ReadAllText(@Application.StartupPath + "\\config\\title.ini");
            this.content.Text = File.ReadAllText(@Application.StartupPath + "\\config\\content.ini");
            this.avatar.Image = Image.FromFile(@Application.StartupPath + "\\config\\avatar.png");
            this.avatar.SizeMode = PictureBoxSizeMode.Zoom;
            timer1.Enabled = true;
            timer1.Interval = 6000;
            timer1.Start();
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
    }

}
