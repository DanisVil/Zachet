using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerApp
{
    public partial class Form1 : Form
    {
        private int seconds, minutes, hours;
        private const string numbers = "0123456789";
        private bool isGoing = false, paused = false;
        private int tick;
        public Form1()
        {
            InitializeComponent();
            label1.Hide();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }
        private void CalculateTime(int time)
        {
            hours = time / 3600;
            time %= 3600;
            minutes = time / 60;
            time %= 60;
            seconds = time;
            label1.Text = $"{hours}:{minutes}:{seconds}";
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            if (isGoing)
            {
                if (!paused)
                {
                    startButton.Text = "Дальше";
                    paused = true;
                    timer1.Stop();
                }
                else
                {
                    startButton.Text = "Пауза";
                    paused = false;
                    timer1.Start();
                }
            }
            else if (CheckTime())
            {
                secondsTextBox.Hide();
                minutesTextBox.Hide();
                hourTextBox.Hide();
                int s = Convert.ToInt32(secondsTextBox.Text);
                int m = Convert.ToInt32(minutesTextBox.Text);
                int h = Convert.ToInt32(hourTextBox.Text);
                int time = s + m * 60 + h * 3600;
                CalculateTime(time);
                tick = time;
                timer1.Start();
                isGoing = true;
                label1.Show();
                cancelButton.Enabled = true;
                startButton.Text = "Пауза";
            }
        }
        private bool CheckTime()
        {
            string a = new string(hourTextBox.Text.Intersect(numbers).ToArray());
            if (new string(hourTextBox.Text.Intersect(numbers).ToArray()).Equals("")&&
                new string(minutesTextBox.Text.Intersect(numbers).ToArray()).Equals("")&&
                new string(secondsTextBox.Text.Intersect(numbers).ToArray()).Equals("") ||
                hourTextBox.Text.Equals("") || secondsTextBox.Text.Equals("")|| minutesTextBox.Text.Equals(""))
            {
                MessageBox.Show("Неверный формат");
                return false;
            }
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tick > 1)
            {
                tick--;
            }
            else if (isGoing)
            {
                timer1.Stop();
                MessageBox.Show("Все!");
            }
            int time = tick;
            CalculateTime(time);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            isGoing = false;
            cancelButton.Enabled = false;
            startButton.Text = "Старт";
            paused = false;
            timer1.Stop();
            label1.Hide();
            secondsTextBox.Show();
            minutesTextBox.Show();
            hourTextBox.Show();
        }
    }
}
