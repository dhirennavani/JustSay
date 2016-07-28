using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace JustSayWindowsForms
{


    public partial class Form1 : Form
    {
        
        SpeechSynthesizer ss = new SpeechSynthesizer();
        Choices ch;
        GrammarBuilder gb;
        Grammar grammar;
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")); 
        public Form1()
        {
            
            
            InitializeComponent();
            loadGrammar();
            loadCombo();
            sre.SpeechRecognized+=new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.RecognizeCompleted+=new EventHandler<RecognizeCompletedEventArgs>(sre_RecognizeCompleted);

            sre.SetInputToDefaultAudioDevice();
           
        }


    void sre_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e) { }
            
    void    sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        string[] lines = System.IO.File.ReadAllLines(@"AppData/CommandsDataFile.txt");
        for (int i = 0; i < lines.Length; i++)
        { string[] currentline = lines[i].Split(',');
            if (currentline[0] == e.Result.Text)
            {
                    for (int j = 1; j < currentline.Length; j++)
                    {
                        MessageBox.Show(currentline[j]);
                        try
                        {
                            System.Diagnostics.Process.Start(@""" + currentline[i].Trim()+""");
                        }
                        catch (Exception except) {
                            MessageBox.Show(except.Message);
                        }

                    }
                    pictureBox1.Visible = false;
                    break;
            }
        }
   }


        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsync();
            pictureBox1.ImageLocation = @"AppData/332.GIF";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Visible = true;
            stopButton.Visible = true;
       
        }
        
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        
        private void stopButton_click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            pictureBox1.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FileName = @"AppData/CommandsDataFile.txt";
            string old;
            string n = "";
            StreamReader sr = File.OpenText(FileName);
            while ((old = sr.ReadLine()) != null)
            {
                if (!(old.Split(',')[0].Equals(commandCombo.Text)))
                {
                    n += old + Environment.NewLine;
                }
            }
            sr.Close();
            File.WriteAllText(FileName, n);
            label4.ResetText();
            loadCombo();
            loadGrammar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string FileName = @"AppData/CommandsDataFile.txt";
            StreamReader sr = File.OpenText(FileName);
            string iterator;

            while ((iterator = sr.ReadLine()) != null)
            { string[] command = iterator.Split(',');
                if (command[0] == commandCombo.Text) {
                    for (int i = 1; i < command.Length; i++) {
                        label4.Text = label4.Text +"\n"+i+": "+ command[i];
                    }
                }
            }
            sr.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"AppData/CommandsDataFile.txt"))
            {
                
                        file.WriteLine(commandBox.Text+","+filePathBox.Text);
            }
            commandBox.ResetText();
            filePathBox.ResetText();
            loadGrammar();
            loadCombo();
        }

        private void loadCombo() {
            var dataSource = new List<Language>();
            string[] lines = System.IO.File.ReadAllLines(@"AppData/CommandsDataFile.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                dataSource.Add(new Language() { Name = lines[i].Split(',')[0], Value = Name = lines[i].Split(',')[0] });
            }

            this.commandCombo.DataSource = dataSource;
            this.commandCombo.DisplayMember = "Name";

        }

        private void loadGrammar() {
            string[] lines = System.IO.File.ReadAllLines(@"AppData/CommandsDataFile.txt");
            string[] commands = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                commands[i] = lines[i].Split(',')[0];

            }
            if (commands.Length != 0)
            {
                ch = new Choices(commands);
                gb = new GrammarBuilder(ch);
                grammar = new Grammar(gb);
                sre.LoadGrammar(grammar);
            }
        }
    }

    public class Language
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
