using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platech_finals_proj
{
    public partial class ImageMessageBox : Form
    {
        private System.Windows.Forms.Timer glowTimer = new System.Windows.Forms.Timer();
        private int glowDirection = 1; // 1 = increasing, -1 = decreasing
        private int glowAlpha = 100;   // Starting glow transparency

        public ImageMessageBox()
        {
            InitializeComponent();

            // Style for better glow appearance
            startNewSimulationButton.FlatStyle = FlatStyle.Flat;
            startNewSimulationButton.FlatAppearance.BorderSize = 0;

            // Timer setup for pulsing effect
            glowTimer.Interval = 5; // Speed of animation
            glowTimer.Tick += GlowTimer_Tick;
            glowTimer.Start();
        }

        private void ImageMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void startNewSimulationButton_Click(object sender, EventArgs e)
        {
            // Delay the closing of other forms until after Form1 is shown
            this.Hide(); // Hide current form immediately to avoid flashing

            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) =>
            {
                // When Form1 is closed, close the entire app
                Application.Exit();
            };
            form1.Show();

            // Close all other forms except Form1
            foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
            {
                if (openForm != form1)
                {
                    openForm.Hide(); // Hide instead of close to prevent app termination
                }
            }
        }
        private void GlowTimer_Tick(object sender, EventArgs e)
        {
            // Adjust glowAlpha between 100 and 255
            glowAlpha += glowDirection * 5;

            if (glowAlpha >= 255)
            {
                glowAlpha = 255;
                glowDirection = -1;
            }
            else if (glowAlpha <= 100)
            {
                glowAlpha = 100;
                glowDirection = 1;
            }

            // Apply the glow color to the button's background
            Color baseColor = Color.Aqua;
            startNewSimulationButton.BackColor = Color.FromArgb(glowAlpha, baseColor);
        }
    }
}
