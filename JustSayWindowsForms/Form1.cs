using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
//using System.

namespace JustSayWindowsForms
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        Choices ch;
        GrammarBuilder gb;
        Grammar g;
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")); 
        public Form1()
        {
            
            
            InitializeComponent();
            string[] lines = System.IO.File.ReadAllLines(@"AppData/CommandsDataFile.txt");
            string[] commands=new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                commands[i]=lines[i].Split(',')[0];
 
            }
            ch = new Choices(commands);
            gb = new GrammarBuilder(ch);
            g = new Grammar(gb);
            sre.LoadGrammar(g);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"AppData/CommandsDataFile.txt");
            for (int i = 0; i < lines.Length; i++)
            { 
            if(lines[i].Split(',')[0]=="Hi"){
                
                }
            }
            //progressBar1.Minimum = 1;
            //progressBar1.Maximum = 1000;
            //progressBar1.Visible = true;
            //progressBar1.Value = 1;
            //progressBar1.Step = 1;
            //while (i < 1000)
            //{
            //    progressBar1.PerformStep();
            //    i++;
            //}
        }
    }
}
