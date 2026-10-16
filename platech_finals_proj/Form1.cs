namespace platech_finals_proj
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer glowTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer slideTimer = new System.Windows.Forms.Timer();

        private int glowDirection = 1; // 1 = increasing, -1 = decreasing
        private int glowAlpha = 100;   // Starting glow transparency

        private int targetY; // For sliding the continueButton

        public Form1()
        {
            InitializeComponent();
            // Set initial form opacity for fade-in
            this.Opacity = 0;

            // Fade-in timer setup
            fadeTimer.Interval = 10;
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();

            // Slide-in animation setup
            targetY = continueButton.Top;       // Store final Y position
            continueButton.Top = this.Height;   // Start off-screen (bottom)
            slideTimer.Interval = 10;
            slideTimer.Tick += SlideTimer_Tick;
            slideTimer.Start();

            // Button glow effect setup
            continueButton.FlatStyle = FlatStyle.Flat;
            continueButton.FlatAppearance.BorderSize = 0;
            glowTimer.Interval = 5; // Speed of animation
            glowTimer.Tick += GlowTimer_Tick;
            glowTimer.Start();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); // Create an instance of Form2
            form2.Show();  // Show Form2
            this.Hide();   // Hide Form1 (Optional)
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
            Color baseColor = Color.SteelBlue;
            continueButton.BackColor = Color.FromArgb(glowAlpha, baseColor);
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.02;
            }
            else
            {
                this.Opacity = 1;
                fadeTimer.Stop();
            }
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            if (continueButton.Top > targetY)
            {
                continueButton.Top -= 10; // Move upward
            }
            else
            {
                continueButton.Top = targetY; // Snap to target position
                slideTimer.Stop();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}