using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platech_finals_proj
{
    public partial class Form3 : Form
    {
        private int jobCount;  // Store the job count
        public int FrameCount { get; set; }
        public int MemorySize { get; set; } // Add this property
        private Dictionary<int, string> frameToPageMapping = new Dictionary<int, string>();
        private bool[] jobExecutedFlags;
        private readonly Dictionary<int, Color> jobColors = new Dictionary<int, Color>
        {
            { 1, Color.Red },
            { 2, Color.Green },
            { 3, Color.Blue },
            { 4, Color.Orange },
            { 5, Color.Violet }
        };


        public Form3(int jobs, int frameCount, int memorySize)
        {
            InitializeComponent();
            jobCount = jobs;
            FrameCount = frameCount;
            MemorySize = memorySize; // Store memory size

            framesPanel.Invalidate(); // Forces panel to repaint
            tableLayoutPanel2.Visible = false;
            pmtLabel.Visible = false;
            submitPMTButton.Visible = false;
            clearPMTButton.Visible = false;
            nextPMTButton.Visible = false;
            tableLayoutPanel3.Visible = false;
            fifoButton.Visible = false;
            tableLayoutPanel4.Visible = false;
            updatedJobTableLabel.Visible = false;

            int jobNumber = 0; // global variable

            panel1.BackColor = Color.Transparent;
            this.panel1.Paint += panel1_Paint;
            this.Load += Form3_Load; // defer BringToFront to after layout
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            PopulateJobNumbers();
            panel1.BringToFront();
        }
        private void PopulateJobNumbers()
        {

            jobExecutedFlags = new bool[jobCount + 1];
            // Clear previous rows (if any)
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = jobCount + 1; // +1 for the header row

            // Ensure the table has the correct number of columns
            tableLayoutPanel1.ColumnCount = 3;

            // Set equal row heights using percentages
            for (int i = 0; i <= jobCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / (jobCount + 1)));
            }

            // ----- Add Header Row -----
            string[] headers = { "Job No.", "Job Size", "Arrival Time" };

            for (int col = 0; col < headers.Length; col++)
            {
                Label headerLabel = new Label
                {
                    Text = headers[col],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Century Gothic", 9F, FontStyle.Bold),
                    Margin = Padding.Empty
                };
                tableLayoutPanel1.Controls.Add(headerLabel, col, 0);
            }

            // ----- Add Dynamic Job Rows -----
            for (int i = 1; i <= jobCount; i++)
            {
                // Column 0 - Job No.
                Label jobLabel = new Label
                {
                    Text = i.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Margin = Padding.Empty
                };
                tableLayoutPanel1.Controls.Add(jobLabel, 0, i);

                // Column 1 - Job Size
                TextBox jobSizeBox = new TextBox
                {
                    Multiline = true,                     // ✅ allow vertical fill
                    Dock = DockStyle.Fill,
                    Name = $"jobSizeBox_{i}",
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = Padding.Empty,
                    TextAlign = HorizontalAlignment.Center
                };
                jobSizeBox.Leave += AddMBUnit;
                tableLayoutPanel1.Controls.Add(jobSizeBox, 1, i);

                // Column 2 - Arrival Time
                TextBox arrivalTimeBox = new TextBox
                {
                    Multiline = true,                     // ✅ allow vertical fill
                    Dock = DockStyle.Fill,
                    Name = $"arrivalTimeBox_{i}",
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = Padding.Empty,
                    TextAlign = HorizontalAlignment.Center
                };
                arrivalTimeBox.Leave += AddMSECUnit;
                tableLayoutPanel1.Controls.Add(arrivalTimeBox, 2, i);
            }
        }
        // ADD MSEC TO EVERY LEAVE FRAME
        private void AddMSECUnit(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text.Trim();
                if (!string.IsNullOrEmpty(text) && !text.ToLower().EndsWith("msec"))
                {
                    if (int.TryParse(text, out _)) // Check if it's a valid number
                    {
                        textBox.Text = text + " msec."; // Append "msec"
                    }
                }
            }
        }
        // ADD MB TO EVERY LEAVE FRAME
        private void AddMBUnit(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text.Trim();
                if (!string.IsNullOrEmpty(text) && !text.ToLower().EndsWith("mb"))
                {
                    if (int.TryParse(text, out _)) // Check if it's a valid number
                    {
                        textBox.Text = text + " MB"; // Append "MB"
                    }
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Show confirmation warning
            DialogResult result = MessageBox.Show(
                "Are you sure you want to go back? Your current progress will be lost if you continue.",
                "Confirm Navigation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                // User clicked "No", cancel clear
                return;
            }
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        // MEMORY/RAM PANEL
        private void framesPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int totalFrames = FrameCount + 1; // Including OS Kernel
            int panelHeight = framesPanel.Height;
            int panelWidth = framesPanel.Width;
            int frameHeight = panelHeight / totalFrames;

            Pen blackPen = new Pen(Color.Black, 2);
            Font font = new Font("Century Gothic", 10, FontStyle.Bold);
            Brush textBrush = Brushes.Black;


            double memoryPerFrame = (double)(MemorySize - 10) / FrameCount; // Excluding OS Kernel
            double currentMemory = 0;

            int labelOffsetX = 1;
            int labelOffsetY = -1;

            StringFormat centerFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Draw OS Kernel
            g.FillRectangle(Brushes.LightGray, 50, 0, panelWidth - 50, frameHeight);
            g.DrawRectangle(blackPen, 50, 0, panelWidth - 50, frameHeight);
            g.DrawString("OS Kernel", font, textBrush, new RectangleF(50, 0, panelWidth - 50, frameHeight), centerFormat);

            // Draw Frames
            for (int i = 0; i < totalFrames; i++)
            {
                int yPosition = i * frameHeight;

                // Memory Label
                g.DrawString($"{Math.Round(currentMemory)} MB", font, textBrush, new PointF(labelOffsetX, yPosition + labelOffsetY));

                if (i > 0)
                {
                    // Default label and brush
                    string label = $"FRAME {i}";
                    Brush labelBrush = textBrush;

                    if (frameToPageMapping != null && frameToPageMapping.TryGetValue(i, out string jobLabel))
                    {
                        if (!string.IsNullOrWhiteSpace(jobLabel)) // 🌟 Check if it's NOT empty
                        {
                            label = jobLabel;

                            // Extract job number from label like "JOB-3, Page-0"
                            if (jobLabel.StartsWith("JOB-") && int.TryParse(jobLabel.Split('-')[1].Split(',')[0], out int jobNum))
                            {
                                if (jobColors.ContainsKey(jobNum))
                                {
                                    labelBrush = new SolidBrush(jobColors[jobNum]);
                                }
                            }
                        }
                    }

                    // Draw frame box and label
                    g.DrawRectangle(blackPen, 50, yPosition, panelWidth - 50, frameHeight);
                    g.DrawString(label, font, labelBrush, new RectangleF(50, yPosition, panelWidth - 50, frameHeight), centerFormat);
                }

                // Increment memory counter
                if (i == 0) currentMemory += 10;
                else currentMemory += memoryPerFrame;
            }

            // Final memory label
            g.DrawString($"{MemorySize} MB", font, textBrush, new PointF(labelOffsetX, panelHeight - font.Height - labelOffsetY));
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //SUBMIT JOB TABLE TO TRANSFER TO PMT TABLE
        private void submitTableButton_Click(object sender, EventArgs e)
        {
            // 🔍 Validate all job inputs
            for (int row = 1; row <= jobCount; row++)
            {
                var jobSizeBox = tableLayoutPanel1.GetControlFromPosition(1, row) as TextBox;
                var arrivalBox = tableLayoutPanel1.GetControlFromPosition(2, row) as TextBox;

                if (string.IsNullOrWhiteSpace(jobSizeBox?.Text) || string.IsNullOrWhiteSpace(arrivalBox?.Text))
                {
                    MessageBox.Show("Please input all fields before submitting.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show("Are you sure you want to submit this JOB Table?", "Confirm Submission",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // 🔛 UI Reveal
            tableLayoutPanel2.Visible = true;
            tableLayoutPanel3.Visible = true;
            pmtLabel.Visible = submitPMTButton.Visible = clearPMTButton.Visible = nextPMTButton.Visible = updatedJobTableLabel.Visible = true;
            

            // 🧾 Build PMT Summary Table Headers
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel3.Controls.Clear();
            tableLayoutPanel3.RowStyles.Clear();
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.ColumnCount = 3;

            string[] headers = { "Job Number", "Job Size", "PMT Location" };
            for (int col = 0; col < headers.Length; col++)
            {
                tableLayoutPanel3.Controls.Add(new Label
                {
                    Text = headers[col],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Century Gothic", 9, FontStyle.Bold)
                }, col, 0);
            }

            // 📦 Collect and sort jobs
            var jobs = new List<(int JobNo, int JobSize, int ArrivalTime)>();
            for (int row = 1; row <= jobCount; row++)
            {
                if (int.TryParse((tableLayoutPanel1.GetControlFromPosition(1, row) as TextBox)?.Text.Replace(" MB", ""), out int size) &&
                    int.TryParse((tableLayoutPanel1.GetControlFromPosition(2, row) as TextBox)?.Text.Replace(" msec.", ""), out int arrival))
                {
                    jobs.Add((row, size, arrival));
                }
            }

            jobs = jobs.OrderBy(j => j.ArrivalTime).ThenBy(j => j.JobNo).ToList();

            // 📥 Populate PMT Summary Table with equal row heights
            int totalRows = jobs.Count + 1; // 1 for header

            // Set fixed height for header row
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));

            // Set equal percentage height for each data row
            if (jobs.Count > 0)
            {
                float jobRowHeight = 100f / jobs.Count;

                for (int i = 0; i < jobs.Count; i++)
                {
                    var job = jobs[i];
                    int rowIndex = tableLayoutPanel3.RowCount++;
                    tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, jobRowHeight));

                    tableLayoutPanel3.Controls.Add(new Label
                    {
                        Text = $"JOB-{job.JobNo}",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Font = new Font("Century Gothic", 9)
                    }, 0, rowIndex);

                    tableLayoutPanel3.Controls.Add(new Label
                    {
                        Text = $"{job.JobSize} MB",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Font = new Font("Century Gothic", 9)
                    }, 1, rowIndex);

                    tableLayoutPanel3.Controls.Add(new Label
                    {
                        Text = "",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Font = new Font("Century Gothic", 9, FontStyle.Italic),
                        ForeColor = Color.Gray
                    }, 2, rowIndex);
                }
            }

            tableLayoutPanel3.ResumeLayout();


            // 🧩 Setup PMT Table for First Job
            if (jobs.Count == 0) return;

            var firstJob = jobs[0];
            int frameSize = MemorySize / FrameCount;
            int numPages = (int)Math.Ceiling((double)firstJob.JobSize / frameSize);

            pmtLabel.Text = $"JOB-{firstJob.JobNo} PAGE MAP TABLE";

            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.RowStyles.Clear();
            tableLayoutPanel2.RowCount = numPages + 1;
            tableLayoutPanel2.ColumnCount = 7;

            // Set fixed height for header
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));

            // Set equal height for remaining data rows
            float remainingHeight = 100f / numPages;
            for (int i = 0; i < numPages; i++)
            {
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, remainingHeight));
            }


            string[] pmtHeaders = { "Page No.", "Frame No.", "Status Bit", "Modified Bit", "Referenced Bit", "Swapped Out Bit", "Disk Location" };
            for (int col = 0; col < pmtHeaders.Length; col++)
            {
                tableLayoutPanel2.Controls.Add(new Label
                {
                    Text = pmtHeaders[col],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Century Gothic", 9, FontStyle.Bold),
                    ForeColor = Color.White
                }, col, 0);
            }

            for (int i = 0; i < numPages; i++)
            {
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / numPages));
                tableLayoutPanel2.Controls.Add(new Label { Text = i.ToString(), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 0, i + 1);

                var frameBox = new TextBox
                {
                    Name = $"frameBox_{i}",
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    TextAlign = HorizontalAlignment.Center
                };
                frameBox.Leave += (s, ev) => FormatFrameInput(s, i + 1, frameSize);
                tableLayoutPanel2.Controls.Add(frameBox, 1, i + 1);

                for (int col = 2; col < 5; col++)
                {
                    tableLayoutPanel2.Controls.Add(new Label { Text = "0", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, col, i + 1);
                }

                tableLayoutPanel2.Controls.Add(new Label
                {
                    Text = "0",
                    Name = $"swappedOut_{i}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                }, 5, i + 1);

                var diskBox = new TextBox
                {
                    Name = $"diskBox_{i}",
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    TextAlign = HorizontalAlignment.Center
                };
                diskBox.Leave += (s, ev) =>
                {
                    if (s is TextBox box && int.TryParse(box.Text, out int val) && !box.Text.EndsWith(" MB"))
                    {
                        box.Text = $"{val} MB";
                    }
                };
                tableLayoutPanel2.Controls.Add(diskBox, 6, i + 1);
            }

            tableLayoutPanel2.ResumeLayout();

            // ♻️ FIFO Unload Logic
            var jobsInRAM = new List<(int JobNum, int ArrivalTime)>();
            foreach (var kvp in frameToPageMapping)
            {
                if (kvp.Key == 0 || string.IsNullOrWhiteSpace(kvp.Value)) continue;

                string[] parts = kvp.Value.Split(',');
                if (parts[0].StartsWith("JOB-") &&
                    int.TryParse(parts[0].Replace("JOB-", ""), out int jobNum) &&
                    tableLayoutPanel1.GetControlFromPosition(2, jobNum) is TextBox arrivalBox &&
                    int.TryParse(arrivalBox.Text.Replace(" msec.", ""), out int arrivalTime) &&
                    !jobsInRAM.Any(j => j.JobNum == jobNum))
                {
                    jobsInRAM.Add((jobNum, arrivalTime));
                }
            }

            var jobToUnload = jobsInRAM.OrderBy(j => j.ArrivalTime).ThenBy(j => j.JobNum).FirstOrDefault();
            if (jobToUnload.JobNum != 0)
            {
                string oldestJob = $"JOB-{jobToUnload.JobNum}";
                var framesToClear = frameToPageMapping
                    .Where(kvp => kvp.Value.StartsWith(oldestJob))
                    .Select(kvp => kvp.Key)
                    .ToList();

                foreach (int frame in framesToClear)
                {
                    frameToPageMapping[frame] = "";

                    foreach (Label ctrl in framesPanel.Controls.OfType<Label>())
                    {
                        if (ctrl.Name == $"frameLabel{frame}")
                        {
                            ctrl.Text = $"FRAME {frame}";
                            ctrl.ForeColor = Color.Black;
                            ctrl.Font = new Font("Century Gothic", 8);
                            ctrl.TextAlign = ContentAlignment.MiddleCenter;
                            ctrl.BackColor = SystemColors.ControlLight;
                            break;
                        }
                    }
                }

                jobExecutedFlags[jobToUnload.JobNum] = true;
                framesPanel.Refresh();
            }
        }


        private void FormatFrameInput(object sender, int row, int frameSize)
        {
            TextBox frameInput = sender as TextBox;
            if (frameInput == null) return;

            string text = frameInput.Text.Trim();

            if (int.TryParse(text, out int frameNumber) && frameNumber > 0)
            {
                frameInput.Text = $"FRAME {frameNumber}"; // Format input as "FRAME X"
            }
            else
            {
                frameInput.Text = ""; // Clear invalid input
            }
        }

        // CLEAR JOB TABLE INPUTS
        private void editButton_Click(object sender, EventArgs e)
        {
            // Show confirmation warning
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear job table?",
                "Clear Job Table Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                // User clicked "No", cancel clear
                return;
            }
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        // SUBMIT PMT TABLE 
        private void submitPMTButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Are you sure you want to submit this Page Map Table?",
       "Confirm Submission",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                tableLayoutPanel4.Visible = false;

            }

            if (result == DialogResult.Yes)


                tableLayoutPanel4.Visible = true;
            updatedJobTableLabel.Visible = true;
            string labelText = pmtLabel.Text;
            int jobNumber = int.Parse(labelText.Split('-')[1].Split(' ')[0]);

            int rows = tableLayoutPanel2.RowCount;
            bool hasEmptyFrame = false;

            for (int i = 1; i < rows; i++)
            {
                Control frameControl = tableLayoutPanel2.GetControlFromPosition(1, i);
                string frameText = (frameControl as TextBox)?.Text.Replace("FRAME", "").Trim() ?? "";

                if (string.IsNullOrWhiteSpace(frameText))
                {
                    hasEmptyFrame = true;
                    break;
                }
            }

            if (hasEmptyFrame)
            {
                DialogResult fifoPrompt = MessageBox.Show(
                    "Some frame fields are empty. Click Yes to confirm using FIFO to load all jobs.",
                    "Use FIFO?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (fifoPrompt == DialogResult.Yes)
                {
                    fifoButton.Visible = true;

                    for (int i = 1; i < rows; i++)
                    {
                        Control pageLabel = tableLayoutPanel2.GetControlFromPosition(0, i);
                        Control frameControl = tableLayoutPanel2.GetControlFromPosition(1, i);
                        Control diskControl = tableLayoutPanel2.GetControlFromPosition(6, i);

                        string pageNoText = (pageLabel as Label)?.Text ?? "";
                        string frameText = (frameControl as TextBox)?.Text.Replace("FRAME", "").Trim() ?? "";
                        string diskLocationText = (diskControl as TextBox)?.Text ?? "";

                        if (!string.IsNullOrWhiteSpace(frameText) && int.TryParse(frameText, out int frameNo))
                        {
                            if (frameToPageMapping.ContainsKey(frameNo) && !string.IsNullOrWhiteSpace(frameToPageMapping[frameNo]))
                            {
                                MessageBox.Show($"Frame {frameText} is already occupied!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            frameToPageMapping[frameNo] = $"JOB-{jobNumber}, Page-{pageNoText}";

                            Control statusBitControl = tableLayoutPanel2.GetControlFromPosition(2, i);
                            if (statusBitControl is Label statusLabel)
                            {
                                statusLabel.Text = "1";
                                statusLabel.ForeColor = Color.Red;
                            }

                            foreach (Control ctrl in framesPanel.Controls)
                            {
                                if (ctrl is Label frameLabel && frameLabel.Name == $"frameLabel{frameNo}")
                                {
                                    frameLabel.Text = $"JOB-{jobNumber}\nPAGE-{pageNoText}";
                                    frameLabel.ForeColor = Color.White;
                                    frameLabel.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                                    frameLabel.TextAlign = ContentAlignment.MiddleCenter;
                                    break;
                                }
                            }
                        }
                    }

                    Control diskPage0Control = tableLayoutPanel2.GetControlFromPosition(6, 1);
                    string diskLocationPage0 = (diskPage0Control as TextBox)?.Text ?? "";

                    if (!string.IsNullOrWhiteSpace(diskLocationPage0))
                    {
                        for (int row = 1; row < tableLayoutPanel3.RowCount; row++)
                        {
                            Label jobLabel = tableLayoutPanel3.GetControlFromPosition(0, row) as Label;
                            Label locationLabel = tableLayoutPanel3.GetControlFromPosition(2, row) as Label;

                            if (jobLabel != null && locationLabel != null && jobLabel.Text == $"JOB-{jobNumber}")
                            {
                                locationLabel.Text = diskLocationPage0;
                                break;
                            }
                        }
                    }

                }
                else
                {
                    return;
                }
            }

            for (int i = 1; i < rows; i++)
            {
                Control pageLabel = tableLayoutPanel2.GetControlFromPosition(0, i);
                Control frameControl = tableLayoutPanel2.GetControlFromPosition(1, i);
                Control diskControl = tableLayoutPanel2.GetControlFromPosition(6, i);

                string pageNoText = (pageLabel as Label)?.Text ?? "";
                string frameText = (frameControl as TextBox)?.Text.Replace("FRAME", "").Trim() ?? "";
                string diskLocationText = (diskControl as TextBox)?.Text ?? "";

                if (!string.IsNullOrWhiteSpace(frameText) && int.TryParse(frameText, out int frameNo))
                {
                    frameToPageMapping[frameNo] = $"JOB-{jobNumber}, Page-{pageNoText}";

                    Control statusBitControl = tableLayoutPanel2.GetControlFromPosition(2, i);
                    if (statusBitControl is Label statusLabel)
                    {
                        statusLabel.Text = "1";
                        statusLabel.ForeColor = Color.Red;
                    }

                    foreach (Control ctrl in framesPanel.Controls)
                    {
                        if (ctrl is Label frameLabel && frameLabel.Name == $"frameLabel{frameNo}")
                        {
                            frameLabel.Text = $"JOB-{jobNumber}\nPAGE-{pageNoText}";
                            frameLabel.ForeColor = Color.White;
                            frameLabel.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            frameLabel.TextAlign = ContentAlignment.MiddleCenter;
                            break;
                        }
                    }
                }
            }


            framesPanel.Invalidate();
            panel1.Invalidate(); // Trigger repaint to draw the lines

            Control diskPage0ControlFull = tableLayoutPanel2.GetControlFromPosition(6, 1);
            string diskLocationPage0Full = (diskPage0ControlFull as TextBox)?.Text ?? "";

            for (int row = 1; row < tableLayoutPanel3.RowCount; row++)
            {
                Label jobLabel = tableLayoutPanel3.GetControlFromPosition(0, row) as Label;
                Label locationLabel = tableLayoutPanel3.GetControlFromPosition(2, row) as Label;

                if (jobLabel != null && locationLabel != null && jobLabel.Text == $"JOB-{jobNumber}")
                {
                    locationLabel.Text = diskLocationPage0Full;
                    break;
                }
            }

            if (jobExecutedFlags != null && jobNumber >= 1 && jobNumber < jobExecutedFlags.Length)
            {
                jobExecutedFlags[jobNumber] = true;
            }

            jobSubmitted = true;

            // ======= UPDATE tableLayoutPanel4 =======
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel4.Controls.Clear();
            tableLayoutPanel4.RowStyles.Clear();
            tableLayoutPanel4.ColumnStyles.Clear();
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.RowCount = rows;
            tableLayoutPanel4.AutoSize = false;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            // Add the column style (full width)
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            // Set row styles: first row fixed 30px, rest equally divided
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f)); // First row fixed height

            int remainingRows = rows - 1;
            if (remainingRows > 0)
            {
                float percentHeight = 100f / remainingRows;
                for (int i = 0; i < remainingRows; i++)
                {
                    tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, percentHeight));
                }
            }

            // First row: Job Name
            Label jobLabelHeader = new Label
            {
                Text = $"JOB-{jobNumber}",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Century Gothic", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.LightGray,
                Margin = Padding.Empty
            };
            tableLayoutPanel4.Controls.Add(jobLabelHeader, 0, 0);

            // Remaining rows: Pages
            for (int i = 1; i < rows; i++)
            {
                Control pageNoControl = tableLayoutPanel2.GetControlFromPosition(0, i);
                string pageNoText = (pageNoControl as Label)?.Text ?? "";

                Label pageLabel = new Label
                {
                    Text = $"Page-{pageNoText}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Century Gothic", 10, FontStyle.Bold),
                    Margin = Padding.Empty
                };
                tableLayoutPanel4.Controls.Add(pageLabel, 0, i);
            }

            tableLayoutPanel4.ResumeLayout();
            panel1.Invalidate(); // Triggers drawing when submission is confirmed
        }


        private void button1_Click(object sender, EventArgs e)
        {
            jobSubmitted = false;

            // Show confirmation warning
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all PMT entries?\nThis will reset textboxes and status bits.",
                "Clear PMT Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                // User clicked "No", cancel clear
                return;
            }

            // Proceed to clear TextBoxes and reset Status Bits
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is Label label && tableLayoutPanel2.GetPositionFromControl(label).Row > 0)
                {
                    int col = tableLayoutPanel2.GetPositionFromControl(label).Column;

                    // Status Bit is in column 2 — reset it to 0
                    if (col == 2) // status bit column
                    {
                        label.Text = "0";
                        label.ForeColor = Color.White; // reset color too, optional
                    }
                }
            }
        }
        private bool jobSubmitted = false;
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!jobSubmitted)
            {
                MessageBox.Show("Please submit the job before moving to the next one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            panel1.Invalidate(); // Clear lines for previous job
            tableLayoutPanel4.Controls.Clear();
            tableLayoutPanel4.RowStyles.Clear();
            tableLayoutPanel4.RowCount = 0;
            tableLayoutPanel4.ColumnCount = 0;
            tableLayoutPanel4.Visible = false;
            panel1.Invalidate();

            if (jobExecutedFlags != null)
            {
                bool allDone = true;
                for (int i = 1; i <= jobCount; i++)
                {
                    if (!jobExecutedFlags[i])
                    {
                        allDone = false;
                        break;
                    }
                }

                if (allDone)
                {
                    ImageMessageBox box = new ImageMessageBox();
                    box.ShowDialog();
                    return;
                }
            }

            int earliestArrivalTime = int.MaxValue;
            int selectedJobRow = -1;
            int selectedJobSize = 0;

            for (int row = 1; row <= jobCount; row++)
            {
                if (jobExecutedFlags[row]) continue;

                TextBox arrivalBox = (TextBox)tableLayoutPanel1.GetControlFromPosition(2, row);
                TextBox jobSizeBox = (TextBox)tableLayoutPanel1.GetControlFromPosition(1, row);

                if (arrivalBox != null && jobSizeBox != null &&
                    int.TryParse(arrivalBox.Text.Replace(" msec.", ""), out int arrival) &&
                    int.TryParse(jobSizeBox.Text.Replace(" MB", ""), out int size))
                {
                    if (arrival < earliestArrivalTime)
                    {
                        earliestArrivalTime = arrival;
                        selectedJobRow = row;
                        selectedJobSize = size;
                    }
                }
            }

            if (selectedJobRow == -1)
            {
                ImageMessageBox box = new ImageMessageBox();
                box.ShowDialog();
                return;
            }

            jobExecutedFlags[selectedJobRow] = true;

            bool allFramesUsed = true;
            for (int i = 1; i <= FrameCount; i++)
            {
                if (!frameToPageMapping.ContainsKey(i) || string.IsNullOrWhiteSpace(frameToPageMapping[i]))
                {
                    allFramesUsed = false;
                    break;
                }
            }

            if (allFramesUsed)
            {
                DialogResult result = MessageBox.Show(
                    "RAM(memory) is full, confirm to use FIFO to load all jobs in the RAM(memory)?",
                    "RAM Full - FIFO",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    int minArrivalTime = int.MaxValue;
                    int minJobNumber = int.MaxValue;
                    string oldestJob = null;

                    foreach (var kvp in frameToPageMapping)
                    {
                        if (kvp.Key == 0 || string.IsNullOrWhiteSpace(kvp.Value)) continue;

                        if (kvp.Value.StartsWith("JOB-"))
                        {
                            string[] parts = kvp.Value.Split(',');
                            string jobPart = parts[0];

                            if (int.TryParse(jobPart.Replace("JOB-", ""), out int jobNum))
                            {
                                TextBox arrivalBox = (TextBox)tableLayoutPanel1.GetControlFromPosition(2, jobNum);
                                if (arrivalBox != null && int.TryParse(arrivalBox.Text.Replace(" msec.", ""), out int arrivalTime))
                                {
                                    if (arrivalTime < minArrivalTime || (arrivalTime == minArrivalTime && jobNum < minJobNumber))
                                    {
                                        minArrivalTime = arrivalTime;
                                        minJobNumber = jobNum;
                                        oldestJob = jobPart;
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(oldestJob))
                    {
                        var keysToClear = frameToPageMapping
                            .Where(kvp => kvp.Value.StartsWith(oldestJob))
                            .Select(kvp => kvp.Key)
                            .ToList();

                        foreach (int key in keysToClear)
                        {
                            frameToPageMapping[key] = "";

                            foreach (Control ctrl in framesPanel.Controls)
                            {
                                if (ctrl is Label lbl && lbl.Name == $"frameLabel{key}")
                                {
                                    lbl.Text = $"FRAME {key}";
                                    lbl.ForeColor = Color.Black;
                                    lbl.Font = new Font("Century Gothic", 8, FontStyle.Regular);
                                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                                    lbl.BackColor = SystemColors.ControlLight;
                                    break;
                                }
                            }
                        }

                        jobExecutedFlags[minJobNumber] = true;
                        framesPanel.Invalidate();
                    }
                }
                else
                {
                    return;
                }
            }

            pmtLabel.Text = $"JOB-{selectedJobRow} PAGE MAP TABLE";
            jobSubmitted = false;

            int frameSize = MemorySize / FrameCount;
            int numPages = (int)Math.Ceiling((double)selectedJobSize / frameSize);

            // ✅ Optimize layout update with SuspendLayout and ResumeLayout
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.RowStyles.Clear();
            tableLayoutPanel2.RowCount = numPages + 1;
            tableLayoutPanel2.ColumnCount = 7;

            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            for (int i = 1; i <= numPages; i++)
            {
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / numPages));
            }

            string[] headers = { "Page No.", "Frame No.", "Status Bit", "Modified Bit", "Referenced Bit", "Swapped Out Bit", "Disk Location" };
            for (int col = 0; col < headers.Length; col++)
            {
                tableLayoutPanel2.Controls.Add(new Label
                {
                    Text = headers[col],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Century Gothic", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    Margin = Padding.Empty
                }, col, 0);
            }

            for (int i = 0; i < numPages; i++)
            {
                tableLayoutPanel2.Controls.Add(new Label
                {
                    Text = i.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Margin = Padding.Empty
                }, 0, i + 1);

                TextBox frameBox = new TextBox
                {
                    Name = $"frameBox_{i}",
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    Margin = Padding.Empty,
                    TextAlign = HorizontalAlignment.Center
                };
                frameBox.Leave += (s, ev) => FormatFrameInput(s, i + 1, frameSize);
                tableLayoutPanel2.Controls.Add(frameBox, 1, i + 1);

                for (int col = 2; col < 5; col++)
                {
                    tableLayoutPanel2.Controls.Add(new Label
                    {
                        Text = "0",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Margin = Padding.Empty
                    }, col, i + 1);
                }

                tableLayoutPanel2.Controls.Add(new Label
                {
                    Text = "0",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Margin = Padding.Empty,
                    Name = $"swappedOut_{i}"
                }, 5, i + 1);

                TextBox diskBox = new TextBox
                {
                    Name = $"diskBox_{i}",
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    Margin = Padding.Empty,
                    TextAlign = HorizontalAlignment.Center
                };
                diskBox.Leave += (s, ev) =>
                {
                    TextBox box = s as TextBox;
                    if (!box.Text.EndsWith(" MB") && int.TryParse(box.Text, out int val))
                    {
                        box.Text = $"{val} MB";
                    }
                };
                tableLayoutPanel2.Controls.Add(diskBox, 6, i + 1);
            }

            tableLayoutPanel2.ResumeLayout(); // ✅ Prevents layout thrashing
        }

        private void previousJobButton_Click(object sender, EventArgs e)
        {

        }

        private void fifoButton_Click(object sender, EventArgs e)
        {
            // 1. Confirm job number from label
            string labelText = pmtLabel.Text;
            int jobNumber = int.Parse(labelText.Split('-')[1].Split(' ')[0]);

            int rows = tableLayoutPanel2.RowCount;
            Queue<(int frameNo, int rowIndex)> fifoQueue = new Queue<(int, int)>();

            // 2. Build FIFO queue
            for (int i = 1; i < rows; i++)
            {
                Control frameControl = tableLayoutPanel2.GetControlFromPosition(1, i);
                string frameText = (frameControl as TextBox)?.Text.Replace("FRAME", "").Trim();

                if (!string.IsNullOrWhiteSpace(frameText) && int.TryParse(frameText, out int frameNo))
                {
                    fifoQueue.Enqueue((frameNo, i));
                }
            }

            // 3. Replace unloaded pages
            for (int i = 1; i < rows; i++)
            {
                var frameControl = tableLayoutPanel2.GetControlFromPosition(1, i);
                var statusControl = tableLayoutPanel2.GetControlFromPosition(2, i);
                var frameBox = frameControl as TextBox;
                var statusBit = (statusControl as Label)?.Text;

                if (frameBox != null && string.IsNullOrWhiteSpace(frameBox.Text) && statusBit == "0")
                {
                    if (fifoQueue.Count == 0) break;

                    var (oldFrameNo, oldRow) = fifoQueue.Dequeue();

                    // Clear old page
                    var oldFrameControl = tableLayoutPanel2.GetControlFromPosition(1, oldRow);
                    var oldDiskControl = tableLayoutPanel2.GetControlFromPosition(6, oldRow);
                    var oldStatusControl = tableLayoutPanel2.GetControlFromPosition(2, oldRow);
                    var oldSwappedOutControl = tableLayoutPanel2.GetControlFromPosition(5, oldRow);
                    string diskLocation = (oldDiskControl as TextBox)?.Text ?? "";

                    if (oldFrameControl is TextBox oldFrameBox) oldFrameBox.Text = "";
                    if (oldDiskControl is TextBox oldDiskBox) oldDiskBox.Text = "";
                    if (oldStatusControl is Label oldStatus)
                    {
                        oldStatus.Text = "0";
                        oldStatus.ForeColor = Color.White;
                    }
                    if (oldSwappedOutControl is Label oldSwapBit)
                    {
                        oldSwapBit.Text = "1";
                        oldSwapBit.ForeColor = Color.Yellow;
                    }

                    // Remove old mapping (optional, for cleanliness)
                    frameToPageMapping.Remove(oldFrameNo);

                    // Assign new page to old frame
                    string newPageNo = (tableLayoutPanel2.GetControlFromPosition(0, i) as Label)?.Text ?? "";

                    frameToPageMapping[oldFrameNo] = $"JOB-{jobNumber}, Page-{newPageNo}";

                    var newFrameControl = tableLayoutPanel2.GetControlFromPosition(1, i);
                    var newDiskControl = tableLayoutPanel2.GetControlFromPosition(6, i);
                    var newStatusControl = tableLayoutPanel2.GetControlFromPosition(2, i);

                    if (newFrameControl is TextBox newFrameBox) newFrameBox.Text = $"FRAME {oldFrameNo}";
                    if (newDiskControl is TextBox newDiskBox) newDiskBox.Text = diskLocation;
                    if (newStatusControl is Label newStatus)
                    {
                        newStatus.Text = "1";
                        newStatus.ForeColor = Color.Red;
                    }

                    // ✅ Update tableLayoutPanel4 label to match new page
                    var pageLabelInPanel4 = tableLayoutPanel4.GetControlFromPosition(0, i);
                    if (pageLabelInPanel4 is Label lbl) lbl.Text = $"Page-{newPageNo}";

                    fifoQueue.Enqueue((oldFrameNo, i));
                }
            }

            // 4. Final mapping update
            for (int i = 1; i < rows; i++)
            {
                var frameControl = tableLayoutPanel2.GetControlFromPosition(1, i);
                var pageLabelControl = tableLayoutPanel2.GetControlFromPosition(0, i);

                string frameText = (frameControl as TextBox)?.Text.Replace("FRAME", "").Trim();
                string pageNo = (pageLabelControl as Label)?.Text ?? "";

                if (!string.IsNullOrWhiteSpace(frameText) && int.TryParse(frameText, out int frameNo))
                {
                    frameToPageMapping[frameNo] = $"JOB-{jobNumber}, Page-{pageNo}";
                }
            }

            // 5. Redraw panels and connector lines
            framesPanel.Invalidate();
            framesPanel.Refresh();

            // ✅ Redraw connector lines
            panel1.Invalidate();
            panel1.Refresh();

            // 6. Update disk location in summary table
            Control diskPage0Control = tableLayoutPanel2.GetControlFromPosition(6, 1);
            string diskLocationPage0 = (diskPage0Control as TextBox)?.Text ?? "";

            if (!string.IsNullOrWhiteSpace(diskLocationPage0))
            {
                for (int row = 1; row < tableLayoutPanel3.RowCount; row++)
                {
                    Label jobLabel = tableLayoutPanel3.GetControlFromPosition(0, row) as Label;
                    Label locationLabel = tableLayoutPanel3.GetControlFromPosition(2, row) as Label;

                    if (jobLabel != null && locationLabel != null && jobLabel.Text == $"JOB-{jobNumber}")
                    {
                        locationLabel.Text = diskLocationPage0;
                        break;
                    }
                }
            }

            // 7. Hide FIFO button after use
            fifoButton.Visible = false;

            // ✅ Mark the job as submitted and executed
            jobSubmitted = true;

            if (jobExecutedFlags != null && jobNumber >= 1 && jobNumber < jobExecutedFlags.Length)
            {
                jobExecutedFlags[jobNumber] = true;
            }

        }


        private void restartButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "Are you sure you want to reset everything? This will delete all submitted jobs, memory usage, and reset the simulation.",
              "Confirm Reset",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // ✅ Only clear TextBox values in job input (leave headers & labels)
                for (int row = 1; row < tableLayoutPanel1.RowCount; row++) // skip header (row 0)
                {
                    for (int col = 1; col < tableLayoutPanel1.ColumnCount; col++) // skip job number column (col 0)
                    {
                        Control ctrl = tableLayoutPanel1.GetControlFromPosition(col, row);
                        if (ctrl is TextBox tb)
                        {
                            tb.Text = "";
                        }
                    }
                }

                // ✅ Reset job state flags
                jobSubmitted = false;
                if (jobExecutedFlags != null)
                {
                    for (int i = 0; i < jobExecutedFlags.Length; i++)
                    {
                        jobExecutedFlags[i] = false;
                    }
                }

                // ✅ Clear Page Map Table (PMT) and hide it
                tableLayoutPanel2.Controls.Clear();
                tableLayoutPanel2.RowStyles.Clear();
                tableLayoutPanel2.RowCount = 0;
                tableLayoutPanel2.Visible = false;

                // ✅ Clear Summary Table and hide it
                tableLayoutPanel3.Controls.Clear();
                tableLayoutPanel3.RowStyles.Clear();
                tableLayoutPanel3.RowCount = 0;
                tableLayoutPanel3.Visible = false;

                // ✅ Clear Page Reference Table (tableLayoutPanel4) and hide it
                tableLayoutPanel4.Controls.Clear();
                tableLayoutPanel4.RowStyles.Clear();
                tableLayoutPanel4.RowCount = 0;
                tableLayoutPanel4.Visible = false;

                // set the buttons and label to be not visible
                submitPMTButton.Visible = false;
                clearPMTButton.Visible = false;
                nextPMTButton.Visible = false;
                fifoButton.Visible = false;
                pmtLabel.Visible = false;
                updatedJobTableLabel.Visible = false;

                // ✅ Clear frame-to-page mapping
                frameToPageMapping.Clear();

                // ✅ Redraw RAM and connector lines
                framesPanel.Invalidate();
                panel1.Invalidate();
                panel1.Refresh();

                MessageBox.Show("Simulation has been reset.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int totalFrames = FrameCount + 1; // Including OS Kernel
            int frameHeight = framesPanel.Height / totalFrames;
            int panelWidth = framesPanel.Width;

            // 🎨 Define a palette of distinct colors
            Color[] colors = new Color[]
            {
        Color.LimeGreen, Color.DodgerBlue, Color.OrangeRed, Color.MediumPurple,
        Color.Gold, Color.Tomato, Color.DeepSkyBlue, Color.HotPink
            };

            int colorIndex = 0;

            // 🔍 Get the current job number from tableLayoutPanel4
            int currentJobNumber = -1;
            Control jobHeader = tableLayoutPanel4.GetControlFromPosition(0, 0);
            if (jobHeader is Label jobHeaderLabel)
            {
                string text = jobHeaderLabel.Text.Trim(); // e.g., "JOB-2"
                if (text.StartsWith("JOB-") && int.TryParse(text.Substring(4), out int parsedJob))
                {
                    currentJobNumber = parsedJob;
                }
            }

            foreach (var kvp in frameToPageMapping)
            {
                int frameNo = kvp.Key;
                string value = kvp.Value; // Format: "JOB-x, Page-y"

                if (frameNo <= 0 || frameNo >= totalFrames) continue;

                // Parse job number and page part
                string[] parts = value.Split(',');
                if (parts.Length != 2) continue;

                string jobPart = parts[0].Trim(); // e.g., "JOB-2"
                string pagePart = parts[1].Trim(); // e.g., "Page-0"

                if (!jobPart.StartsWith("JOB-") || !int.TryParse(jobPart.Substring(4), out int jobNum)) continue;

                // Only draw lines if this frame belongs to the currently displayed job
                if (jobNum != currentJobNumber) continue;

                // Calculate Y position of the frame in framesPanel
                int yPosition = frameNo * frameHeight;

                // Center of frame rectangle
                Point frameCenter = panel1.PointToClient(framesPanel.PointToScreen(
                    new Point(50 + (panelWidth - 50) / 2, yPosition + frameHeight / 2)));

                // Match label text in tableLayoutPanel4
                for (int row = 1; row < tableLayoutPanel4.RowCount; row++)
                {
                    Control pageLabel = tableLayoutPanel4.GetControlFromPosition(0, row);
                    if (pageLabel is Label lbl && lbl.Text.Trim() == pagePart)
                    {
                        // Center of the label
                        Point pageCenter = panel1.PointToClient(pageLabel.Parent.PointToScreen(
                            new Point(lbl.Left + lbl.Width / 2, lbl.Top + lbl.Height / 2)));

                        // Use unique color
                        Pen pen = new Pen(colors[colorIndex % colors.Length], 2);
                        colorIndex++;

                        // Draw straight line
                        g.DrawLine(pen, frameCenter, pageCenter);

                        break;
                    }
                }
            }
        }

        private void connectorPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
