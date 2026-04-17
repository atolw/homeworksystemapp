using System.Diagnostics;
using System.ComponentModel;

namespace ProcessApp
{
    public partial class Form1 : Form
    {
        Process[] processes;

        public Form1()
        {
            InitializeComponent();
          
            listBox1.ContextMenuStrip = contextMenuStrip1;

          
            ToolStripMenuItem killItem = new ToolStripMenuItem("Завершити процес");
            killItem.Click += KillItem_Click;
            contextMenuStrip1.Items.Add(killItem);
        }

        void UpdateProcessList()
        {
            
            int selectedIndex = listBox1.SelectedIndex;

            listBox1.Items.Clear();
            processes = Process.GetProcesses();
            Array.Sort(processes, (x, y) => string.Compare(x.ProcessName, y.ProcessName, StringComparison.OrdinalIgnoreCase));

            foreach (Process process in processes)
            {
                listBox1.Items.Add($"{process.Id,8}   {process.ProcessName}");
            }

            if (selectedIndex != -1 && selectedIndex < listBox1.Items.Count)
                listBox1.SelectedIndex = selectedIndex;
        }


        private void KillItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                try
                {
                    Process procToKill = processes[listBox1.SelectedIndex];
                    procToKill.Kill();
                    MessageBox.Show($"Процес {procToKill.ProcessName} завершено.");
                    UpdateProcessList(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при завершенні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Process selected = processes[listBox1.SelectedIndex];
            ProcessDetail detail = new ProcessDetail(selected); 
            detail.ShowDialog();
        }

 
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value * 1000;
            timer1.Enabled = true;
            UpdateProcessList();
        }

        private void timer1_Tick(object sender, EventArgs e) => UpdateProcessList();

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value * 1000;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listBox1.SelectedIndex == -1) e.Cancel = true;
        }
    }
}