using System.Diagnostics;

namespace ProcessApp
{
    public partial class ProcessDetail : Form
    {
        private Process _currentProcess;

        public ProcessDetail(Process proc)
        {
            InitializeComponent();
            _currentProcess = proc;     
            LoadProcessData();
        }

        private void LoadProcessData()
        {
            try
            {
                label1.Text = $"Назва: {_currentProcess.ProcessName}";
                label2.Text = $"ID: {_currentProcess.Id}";
                label3.Text = $"Потоки: {_currentProcess.Threads.Count}";
                label4.Text = $"Старт: {_currentProcess.StartTime.ToLongTimeString()}";
                             
                int count = Process.GetProcessesByName(_currentProcess.ProcessName).Length;
                label5.Text = $"Копій запущено: {count}";
            }
            catch (Exception ex)
            {
                label4.Text = "Старт: Немає доступу";
                label5.Text = $"Копій запущено: Немає доступу";
                MessageBox.Show($"Частина даних недоступна: {ex.Message}");
            }
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _currentProcess.Kill();
                MessageBox.Show("Процес успішно зупинено.");
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося вбити процес: {ex.Message}");
            }
        }
              
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}