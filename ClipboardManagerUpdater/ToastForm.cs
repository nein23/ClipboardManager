using ClipboardManagerUpdater.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ClipboardManagerUpdater
{
    public partial class ToastForm : Form
    {
        private bool enteredForm = false;
        private int xPos;
        private Timer timer;
        private string fileName;
        private int maxX;

        public ToastForm(string title, string text, string fileName)
        {
            this.fileName = fileName;

            InitializeComponent();

            Rectangle wa = Screen.PrimaryScreen.WorkingArea;
            xPos = wa.Right - this.Width;
            this.Location = new Point(wa.Right, wa.Bottom - this.Height - 12);
            maxX = wa.Right;

            label_title.Text = title;
            label_text.Text = text;

            this.MouseEnter += AboutForm_MouseEnter;
            this.MouseLeave += AboutForm_MouseLeave;

            pictureBox_icon.MouseEnter += AboutForm_MouseEnter;
            pictureBox_icon.MouseLeave += AboutForm_MouseLeave;

            label_title.MouseEnter += AboutForm_MouseEnter;
            label_title.MouseLeave += AboutForm_MouseLeave;

            label_text.MouseEnter += AboutForm_MouseEnter;
            label_text.MouseLeave += AboutForm_MouseLeave;

            label_URL.MouseEnter += AboutForm_MouseEnter;
            label_URL.MouseLeave += AboutForm_MouseLeave;
            label_URL.Cursor = Cursors.Hand;

            label_linked.MouseEnter += AboutForm_MouseEnter;
            label_linked.MouseLeave += AboutForm_MouseLeave;

            pictureBox_exit.MouseEnter += PictureBox_exit_MouseEnter;
            pictureBox_exit.MouseLeave += PictureBox_exit_MouseLeave;

            this.Click += exit_Click;
            pictureBox_icon.Click += exit_Click;
            label_title.Click += exit_Click;
            label_text.Click += exit_Click;
            label_URL.Click += exit_Click;
            label_linked.Click += exit_Click;
            pictureBox_exit.Click += exit_Click;

            if (fileName != null)
            {
                label_linked.Click += label_URL_Restart_Click;
                label_linked.Text = "Start Clipboard Manager";
                label_linked.Cursor = Cursors.Hand;
            }

            label_URL.Click += label_URL_Click;

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += FadeInTimer_Tick;
            timer.Enabled = true;
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X - 20, this.Location.Y);
            if (Location.X < xPos)
            {
                timer.Enabled = false;
                this.Location = new Point(xPos, this.Location.Y);
            }
        }

        private void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X + 20, this.Location.Y);
            if (Location.X >= maxX)
            {
                timer.Enabled = false;
                this.Close();
            }
        }

        private void AboutForm_MouseEnter(object sender, EventArgs e)
        {
            if (!enteredForm)
            {
                pictureBox_exit.Image = Resources.toast_exit_white;
                enteredForm = true;
            }
        }

        private void AboutForm_MouseLeave(object sender, EventArgs e)
        {
            if (enteredForm)
            {
                pictureBox_exit.Image = null;
                enteredForm = false;
            }
        }

        private void PictureBox_exit_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_exit.Image = Resources.toast_exit_grey;
        }

        private void PictureBox_exit_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_exit.Image = Resources.toast_exit_white;
        }

        private void label_URL_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/nein23/ClipboardManager");
        }

        private void label_URL_Restart_Click(object sender, EventArgs e)
        {
            if(fileName != null) Process.Start(fileName);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += FadeOutTimer_Tick;
            timer.Enabled = true;
        }
    }
}
