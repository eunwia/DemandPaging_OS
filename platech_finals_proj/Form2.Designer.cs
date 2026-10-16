namespace platech_finals_proj
{
    partial class Form2
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
            Button memorySizeButton;
            backButton = new Button();
            lbl1 = new Label();
            memorySizeTxtInput = new TextBox();
            lbl2 = new Label();
            framesTxtInput = new TextBox();
            lbl3 = new Label();
            jobsTxtInput = new TextBox();
            jobsButton = new Button();
            confirmButton = new Button();
            editButton = new Button();
            memorySizeButton = new Button();
            framesButton = new Button();
            SuspendLayout();
            // 
            // memorySizeButton
            // 
            memorySizeButton.BackColor = Color.SteelBlue;
            memorySizeButton.FlatStyle = FlatStyle.Popup;
            memorySizeButton.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            memorySizeButton.ForeColor = SystemColors.ActiveCaptionText;
            memorySizeButton.Location = new Point(801, 247);
            memorySizeButton.Name = "memorySizeButton";
            memorySizeButton.Size = new Size(112, 23);
            memorySizeButton.TabIndex = 3;
            memorySizeButton.Text = "SUBMIT";
            memorySizeButton.UseVisualStyleBackColor = false;
            memorySizeButton.Click += memorySizeButton_Click;
            // 
            // backButton
            // 
            backButton.BackColor = Color.White;
            backButton.BackgroundImage = Properties.Resources.backbutton;
            backButton.BackgroundImageLayout = ImageLayout.None;
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backButton.ForeColor = Color.Black;
            backButton.ImageAlign = ContentAlignment.MiddleRight;
            backButton.Location = new Point(12, 12);
            backButton.Name = "backButton";
            backButton.Size = new Size(75, 31);
            backButton.TabIndex = 0;
            backButton.Text = "BACK";
            backButton.TextAlign = ContentAlignment.MiddleRight;
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.BackColor = Color.Transparent;
            lbl1.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl1.ForeColor = SystemColors.ButtonHighlight;
            lbl1.Location = new Point(451, 223);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(320, 22);
            lbl1.TabIndex = 1;
            lbl1.Text = "Enter memory size (in MB, 150 MB):";
            lbl1.Click += lbl1_Click;
            // 
            // memorySizeTxtInput
            // 
            memorySizeTxtInput.Location = new Point(451, 248);
            memorySizeTxtInput.Multiline = true;
            memorySizeTxtInput.Name = "memorySizeTxtInput";
            memorySizeTxtInput.Size = new Size(344, 23);
            memorySizeTxtInput.TabIndex = 2;
            memorySizeTxtInput.TextChanged += memorySizeTxtInput_TextChanged;
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.BackColor = Color.Transparent;
            lbl2.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl2.ForeColor = SystemColors.ButtonHighlight;
            lbl2.Location = new Point(451, 285);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(379, 22);
            lbl2.TabIndex = 4;
            lbl2.Text = "Enter number of frames (max 10 frames):";
            lbl2.Click += lbl2_Click;
            // 
            // framesTxtInput
            // 
            framesTxtInput.Location = new Point(451, 310);
            framesTxtInput.Multiline = true;
            framesTxtInput.Name = "framesTxtInput";
            framesTxtInput.Size = new Size(344, 23);
            framesTxtInput.TabIndex = 5;
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.BackColor = Color.Transparent;
            lbl3.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl3.ForeColor = SystemColors.ButtonHighlight;
            lbl3.Location = new Point(451, 346);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(420, 22);
            lbl3.TabIndex = 7;
            lbl3.Text = "Enter number of Jobs to be loaded (3-5 jobs):";
            // 
            // jobsTxtInput
            // 
            jobsTxtInput.Location = new Point(451, 371);
            jobsTxtInput.Multiline = true;
            jobsTxtInput.Name = "jobsTxtInput";
            jobsTxtInput.Size = new Size(344, 23);
            jobsTxtInput.TabIndex = 8;
            // 
            // jobsButton
            // 
            jobsButton.BackColor = Color.SteelBlue;
            jobsButton.FlatStyle = FlatStyle.Popup;
            jobsButton.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            jobsButton.ForeColor = SystemColors.ActiveCaptionText;
            jobsButton.Location = new Point(801, 371);
            jobsButton.Name = "jobsButton";
            jobsButton.Size = new Size(112, 23);
            jobsButton.TabIndex = 9;
            jobsButton.Text = "SUBMIT";
            jobsButton.UseVisualStyleBackColor = false;
            jobsButton.Click += jobsButton_Click;
            // 
            // confirmButton
            // 
            confirmButton.BackColor = Color.SteelBlue;
            confirmButton.FlatStyle = FlatStyle.Popup;
            confirmButton.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            confirmButton.ForeColor = SystemColors.ActiveCaptionText;
            confirmButton.Location = new Point(451, 420);
            confirmButton.Name = "confirmButton";
            confirmButton.Size = new Size(462, 23);
            confirmButton.TabIndex = 10;
            confirmButton.Text = "CONFIRM";
            confirmButton.UseVisualStyleBackColor = false;
            confirmButton.Click += confirmButton_Click;
            // 
            // editButton
            // 
            editButton.BackColor = Color.White;
            editButton.FlatStyle = FlatStyle.Popup;
            editButton.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            editButton.ForeColor = SystemColors.ControlText;
            editButton.Location = new Point(451, 449);
            editButton.Name = "editButton";
            editButton.Size = new Size(462, 23);
            editButton.TabIndex = 11;
            editButton.Text = "EDIT";
            editButton.UseVisualStyleBackColor = false;
            editButton.Click += editButton_Click;
            // 
            // framesButton
            // 
            framesButton.BackColor = Color.SteelBlue;
            framesButton.FlatStyle = FlatStyle.Popup;
            framesButton.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            framesButton.ForeColor = SystemColors.ActiveCaptionText;
            framesButton.Location = new Point(801, 310);
            framesButton.Name = "framesButton";
            framesButton.Size = new Size(112, 23);
            framesButton.TabIndex = 6;
            framesButton.Text = "SUBMIT";
            framesButton.UseVisualStyleBackColor = false;
            framesButton.Click += framesButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.Form2_bg;
            ClientSize = new Size(1350, 729);
            Controls.Add(editButton);
            Controls.Add(confirmButton);
            Controls.Add(jobsButton);
            Controls.Add(jobsTxtInput);
            Controls.Add(lbl3);
            Controls.Add(framesButton);
            Controls.Add(framesTxtInput);
            Controls.Add(lbl2);
            Controls.Add(memorySizeButton);
            Controls.Add(memorySizeTxtInput);
            Controls.Add(lbl1);
            Controls.Add(backButton);
            Name = "Form2";
            Text = "page 2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button backButton;
        private Label lbl1;
        private TextBox memorySizeTxtInput;
        private Button memorySizeButton;
        private Label lbl2;
        private TextBox framesTxtInput;
        private Button framesButton;
        private Label lbl3;
        private TextBox jobsTxtInput;
        private Button jobsButton;
        private Button confirmButton;
        private Button editButton;
    }
}