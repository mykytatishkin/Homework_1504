using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace DanceProgressBar
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            if(txtNumProgressBars == null)
                button1.IsAccessible = false; button2.IsAccessible = false;
        }


        // TASK 1
        private async void button1_Click(object sender, EventArgs e)
        {
            int numProgressBars = Convert.ToInt32(txtNumProgressBars.Text);

            panelProgressBars.Controls.Clear();

            ProgressBar[] progressBars = new ProgressBar[numProgressBars];

            Random random = new Random();
            for (int i = 0; i < numProgressBars; i++)
            {
                ProgressBar progressBar = new ProgressBar();
                progressBar.Location = new Point(10, 10 + i * 30);
                progressBar.Name = "progressBar" + (i + 1).ToString();
                progressBar.Size = new Size(200, 23);
                progressBar.TabIndex = i;
                progressBar.Value = 0;
                progressBar.ForeColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                panelProgressBars.Controls.Add(progressBar);

                progressBars[i] = progressBar;
            }

            Task[] updateTasks = new Task[numProgressBars];
            for (int i = 0; i < numProgressBars; i++)
            {
                int progressBarIndex = i;
                updateTasks[i] = Task.Run(() =>
                {
                    ProgressBar progressBar = progressBars[progressBarIndex];
                    for (int value = random.Next(99); value <= 100; value++)
                    {
                        progressBar.Invoke(new Action(() =>
                        {
                            progressBar.Value = value;
                        }));

                        Task.Delay(50).Wait();
                    }
                });
            }

            await Task.WhenAll(updateTasks);
        }
        


        // TASK 2, start without debug
        private async void button2_Click(object sender, EventArgs e)
        {
            int numProgressBars = Convert.ToInt32(txtNumProgressBars.Text);

            panelProgressBars.Controls.Clear();

            ProgressBar[] progressBars = new ProgressBar[numProgressBars];

            Random random = new Random();
            for (int i = 0; i < numProgressBars; i++)
            {
                ProgressBar progressBar = new ProgressBar();
                progressBar.Location = new Point(10, 10 + i * 30);
                progressBar.Name = "progressBar" + (i + 1).ToString();
                progressBar.Size = new Size(200, 23);
                progressBar.TabIndex = i;
                progressBar.Value = 0;
                progressBar.ForeColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                panelProgressBars.Controls.Add(progressBar);

                progressBars[i] = progressBar;
            }

            Task[] updateTasks = new Task[numProgressBars];
            for (int i = 0; i < numProgressBars; i++)
            {
                int progressBarIndex = i;
                updateTasks[i] = Task.Run(() =>
                {
                    ProgressBar progressBar = progressBars[progressBarIndex];
                    for (int value = 0; value <= 100; value += random.Next(10))
                    {
                        progressBar.Invoke(new Action(() =>
                        {
                            progressBar.Value = value;
                        }));

                        Task.Delay(50).Wait(); 
                    }
                    listBox1.Items.Add("ProgressBar " + progressBar.Name + " has finished.");

                });

            }
            

            await Task.WhenAll(updateTasks);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(txtNumProgressBars.Text);
            if (n <= 1)
                return;

            int prev = 0;
            int current = 1;
            int fib = 0;

            for (int i = 2; i <= n; i++)
            {
                fib = prev + current;
                prev = current;
                current = fib;

                if (current <= n)
                    listBox1.Items.Add("Fibbonachi num: " + fib);
            }

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Вводим путь к файлу и целевое слово
            Console.WriteLine("Введите путь к файлу:");
            string filePath = tbPath.Text;

            Console.WriteLine("Введите целевое слово:");
            string targetWord = tbWord.Text;

            // Открываем файл для чтения
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int lineNumber = 1;

                // Читаем файл построчно
                while ((line = reader.ReadLine()) != null)
                {
                    // Разделяем строку на слова
                    string[] words = line.Split(' ', ',', '.', ';', ':', '-', '!', '?');

                    // Проверяем каждое слово на совпадение с целевым словом
                    foreach (string word in words)
                    {
                        if (word.Equals(targetWord, StringComparison.OrdinalIgnoreCase))
                        {
                            listBox1.Items.Add("Word \"" + targetWord + "\" was in a line " + lineNumber + ": " + line);
                        }
                    }

                    lineNumber++;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}