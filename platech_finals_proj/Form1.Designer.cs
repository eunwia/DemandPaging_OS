namespace platech_finals_proj
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            continueButton = new Button();
            SuspendLayout();
            // 
            // continueButton
            // 
            continueButton.BackColor = Color.SteelBlue;
            continueButton.FlatStyle = FlatStyle.Popup;
            continueButton.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            continueButton.ForeColor = SystemColors.ControlText;
            continueButton.Location = new Point(582, 561);
            continueButton.Name = "continueButton";
            continueButton.Size = new Size(207, 44);
            continueButton.TabIndex = 0;
            continueButton.Text = "continue";
            continueButton.UseVisualStyleBackColor = false;
            continueButton.Click += continueButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            BackgroundImage = Properties.Resources.homepage;
            ClientSize = new Size(1350, 729);
            Controls.Add(continueButton);
            Name = "Form1";
            Text = "homepage";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button continueButton;
    }
}
