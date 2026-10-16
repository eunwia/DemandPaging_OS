using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platech_finals_proj
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            lbl2.Visible = false;
            framesTxtInput.Visible = false;
            framesButton.Visible = false;

            lbl3.Visible = false;
            jobsTxtInput.Visible = false;
            jobsButton.Visible = false;

            confirmButton.Visible = false;
            editButton.Visible = false;
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
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void lbl2_Click(object sender, EventArgs e)
        {

        }

        private void memorySizeTxtInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }

        private void memorySizeButton_Click(object sender, EventArgs e)
        {
            // Get the text from the memory size input box
            string memoryInput = memorySizeTxtInput.Text;

            // Check if the input is not empty
            if (string.IsNullOrWhiteSpace(memoryInput))
            {
                MessageBox.Show("Please enter a number.", "Error");
                return; // Stop execution
            }

            // Convert the text to a number
            int memorySize;
            try
            {
                memorySize = Convert.ToInt32(memoryInput);
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter a valid number.", "Error");
                return; // Stop execution
            }

            // Check if the memory size is at least 200 MB
            if (memorySize < 1 || memorySize > 150)
            {
                MessageBox.Show("Memory size must be between 1 and 150 MB.", "Error");
            }
            else
            {
                // Show the next set of input fields
                lbl2.Visible = true;
                framesTxtInput.Visible = true;
                framesButton.Visible = true;
            }
        }

        private void framesButton_Click(object sender, EventArgs e)
        {
            // Get the text from the frames input box
            string framesInput = framesTxtInput.Text;

            // Check if the input is not empty
            if (string.IsNullOrWhiteSpace(framesInput))
            {
                MessageBox.Show("Please enter a number.", "Error");
                return; // Stop execution
            }

            // Convert the text to a number
            int frames;
            try
            {
                frames = Convert.ToInt32(framesInput);
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter a valid number.", "Error");
                return; // Stop execution
            }
            if (frames <= 0 || frames > 10)
            {
                MessageBox.Show("Input invalid! Please enter a number between 1 and 10.", "Error");
            }
            else
            {
                // Show the next set of input fields
                lbl3.Visible = true;
                jobsTxtInput.Visible = true;
                jobsButton.Visible = true;
            }
        }

        private void jobsButton_Click(object sender, EventArgs e)
        {
            // Get the text from the jobs input box
            string jobsInput = jobsTxtInput.Text;

            // Check if the input is not empty
            if (string.IsNullOrWhiteSpace(jobsInput))
            {
                MessageBox.Show("Please enter a number.", "Error");
                return; // Stop execution
            }

            // Convert the text to a number
            int jobs;
            try
            {
                jobs = Convert.ToInt32(jobsInput);
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter a valid number.", "Error");
                return; // Stop execution
            }
            if (jobs < 3 || jobs > 5)
            {
                MessageBox.Show("Input invalid! Please enter a number between 3 and 5.", "Error");
            }
            else
            {
                // Show the confirm button
                confirmButton.Visible = true;
                editButton.Visible = true;
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            int jobs;
            int frames;
            int memorySize;

            // Validate the input and convert it to an integer
            if (!int.TryParse(jobsTxtInput.Text, out jobs) || jobs <= 0)
            {
                MessageBox.Show("Invalid input! Please enter a positive number.", "Error");
                return;
            }
            // **Add the missing part**: Assign a value to 'frames' from 'framesTxtInput'
            if (!int.TryParse(framesTxtInput.Text, out frames) || frames <= 0)
            {
                MessageBox.Show("Invalid input for frames! Please enter a positive number.", "Error");
                return;
            }
            // Validate memory size input
            if (!int.TryParse(memorySizeTxtInput.Text, out memorySize) || memorySize <= 0)
            {
                MessageBox.Show("Invalid input for memory size! Please enter a positive number.", "Error");
                return;
            }
            Form3 form3 = new Form3(jobs, frames, memorySize);
            form3.Show();
            this.Hide();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // Show confirmation warning
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all fields?",
                "Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                // User clicked "No", cancel clear
                return;
            }
            memorySizeTxtInput.Text = "";
            framesTxtInput.Text = "";
            jobsTxtInput.Text = "";

            lbl2.Visible = false;
            framesTxtInput.Visible = false;
            framesButton.Visible = false;

            lbl3.Visible = false;
            jobsTxtInput.Visible = false;
            jobsButton.Visible = false;

            confirmButton.Visible = false;
            editButton.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
