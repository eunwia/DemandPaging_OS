namespace platech_finals_proj
{
    partial class ImageMessageBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageMessageBox));
            startNewSimulationButton = new Button();
            SuspendLayout();
            // 
            // startNewSimulationButton
            // 
            startNewSimulationButton.BackColor = Color.Aqua;
            startNewSimulationButton.FlatStyle = FlatStyle.Flat;
            startNewSimulationButton.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            startNewSimulationButton.Location = new Point(166, 202);
            startNewSimulationButton.Name = "startNewSimulationButton";
            startNewSimulationButton.Size = new Size(201, 28);
            startNewSimulationButton.TabIndex = 0;
            startNewSimulationButton.Text = "Press To Start a New Simulation";
            startNewSimulationButton.UseVisualStyleBackColor = false;
            startNewSimulationButton.Click += startNewSimulationButton_Click;
            // 
            // ImageMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(522, 343);
            Controls.Add(startNewSimulationButton);
            Name = "ImageMessageBox";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Simulation Finished";
            Load += ImageMessageBox_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button startNewSimulationButton;
    }
}