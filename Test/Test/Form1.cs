using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Diagnostics;
using System.IO;


namespace Test
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recognizer;

        bool islisteninng = false;
        public Form1()
        {
            InitializeComponent();

            recognizer = new SpeechRecognitionEngine();

            Choices commands = new Choices();

            commands.Add(new string[]
            {
                "open word",
                "open notepad",
                "open calculator",
                "open paint",
                "open file explorer",
                "open excel",
                "open powerpoint",
                "open edge",
                "open chrome",
                "open settings",
                "open capcut"
            });

            GrammarBuilder gb = new GrammarBuilder();

            gb.Append(commands);

            Grammar grammar = new Grammar(gb);

            recognizer.LoadGrammar(grammar);

            recognizer.SetInputToDefaultAudioDevice();

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string command = e.Result.Text;

            textBox1.Text = "Recognized: " + command;

            ExecuteCommand(command);
        }

        private void ExecuteCommand(string command)
        {
            try
            {
                switch (command)
                {
                    case "open word":
                        Process.Start("winword");
                        break;
                    case "open notepad":
                        Process.Start("notepad");
                        break;
                    case "open calculator":
                        Process.Start("calc");
                        break;
                    case "open paint":
                        Process.Start("paint");
                        break;
                    case "open file explorer":
                        Process.Start("explorer");
                        break;
                    case "open excel":
                        Process.Start("excel");
                        break;
                    case "open powerpoint":
                        Process.Start("powerpnt");
                        break;
                    case "open edge":
                        Process.Start("msedge");
                        break;
                    case "open chrome":
                        Process.Start("chrome");
                        break;
                    case "open settings":
                        Process.Start("ms-settings: ");
                        break;
                    case "open capcut":
                        Process.Start("capcut");
                        break;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(
                    "Unable to open application.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            label2.Text = "Status: Listening";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recognizer.RecognizeAsyncStop();
            label2.Text = "Status: Stopped";
            textBox1.Text = "";
        }
    }
}
