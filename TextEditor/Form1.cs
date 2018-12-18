using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainLayer.Text.Length < 1)
            {
                Form1 frm1 = new Form1();
                this.Visible = false;
                frm1.Show();
            }
            if (mainLayer.Text.Length > 0)
            {
                askToSave();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(openFile.FileName.ToString());
                // System.IO.StreamWriter file = new System.IO.StreamWriter(openFile.FileName.ToString());
                //  file.WriteLine(mainLayer.Text);
                //  file.Close();
                // file.ReadLine(mainLayer.Text);
                String tekst = file.ReadToEnd();
                mainLayer.Text = tekst;
                file.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileMethod();
        }

        //method for generic save files.
        public void saveFileMethod()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFile.FileName.ToString());
                file.WriteLine(mainLayer.Text);
                file.Close();
            }

        }


        //method for ask users if they want to save file --> used messageBox.
        public void askToSave()
        {
            DialogResult result = MessageBox.Show("Do you want to save changes?",
                "Notepad", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                Form1 frm1 = new Form1();
                this.Visible = false;
                frm1.Show();
            }
            else
            {
                saveFileMethod();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainLayer.Text.Length < 1)
            {
                this.Visible = false;
            }
            else
            {
                askToSave();
            }
        }
      

        // Show and accept choosen fonts and styles.
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();


            if (mainLayer.SelectedText.Length > 0)
            {
                mainLayer.HideSelection = false;
                this.mainLayer.SelectionFont = fontDialog1.Font;

            }
            else
            {
                this.mainLayer.SelectionFont = fontDialog1.Font;
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
          String search=  Microsoft.VisualBasic.Interaction.InputBox("Search specific word");
            if (mainLayer.Text.Contains(search))
            {
                highlightText(mainLayer, search);
            }
            else
            {
                MessageBox.Show("Word doesn't exist!");
            }


        }
            
        public void highlightText(RichTextBox rtb, string word)
        {
            int counter = 0;
            while(counter < rtb.Text.Length)
            {
                int startPosition = rtb.Find(word, counter, RichTextBoxFinds.None);
                counter++;
                if(startPosition != -1)
                {

                    rtb.SelectionStart = startPosition;
                    rtb.SelectionLength = word.Length;
                    rtb.SelectionBackColor = Color.Yellow;
                    mainLayer.Text.Replace("pa", "novo");
                }
                else
                {
                    break;
                    counter += startPosition;
                }
            }

        }

        //Replace word method

        private void replaceWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String searchReplaceWord = Microsoft.VisualBasic.Interaction.InputBox("Search specific word for replace");

            if (mainLayer.Text.Contains(searchReplaceWord))
            {
                String askForNewWord = Microsoft.VisualBasic.Interaction.InputBox("New word?");

                mainLayer.Text = mainLayer.Text.Replace(searchReplaceWord, askForNewWord);

            }
            else
            {
                MessageBox.Show("Word doesn't exist!");
            }

        }

        private void siteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String sourcePageInput = Microsoft.VisualBasic.Interaction.InputBox("Page URL");

            try
            {
                WebClient client = new WebClient();
                string htmlCode = client.DownloadString(sourcePageInput);
                mainLayer.Text = htmlCode;
            }
            catch (Exception E)
            {

                MessageBox.Show("Triggered Error,try again!");
            }
        }

        //ask to save on closing.
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (mainLayer.Text.Length > 0)
            {
                askToSave();
            }
        }

        //view print dialog.
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();  
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            mainLayer.SelectionColor = colorDialog1.Color; 
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Simple text editor " + "\n" + "Version 1.0" + "\n" + "Github acc : https://github.com/simo061991" , "About TextEditor");
        }
    }
    }







