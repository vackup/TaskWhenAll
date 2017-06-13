using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
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

        Task<string> DoLongThingAsyncEx1()
        {
            return Task.FromException<string>(new Exception());
        }

        async Task<string> DoLongThingAsync(DateTime now)
        {
            await Task.Delay(5000);

            //return Task.FromResult(DateTime.Now.ToString());
            return now.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var inicio = DateTime.Now;

            //var tasks = this.GetTaskWithExceptions();
            var tasks = this.GetTasks();

            var task = Task.WhenAll(tasks);

            var messageEx = string.Empty;

            try
            {
                await task;
            }
            catch
            {
                messageEx = string.Join(", ", task.Exception.Flatten().InnerExceptions.Select(ex => ex.Message));
            }

            var totalSegundos = (DateTime.Now - inicio).TotalMilliseconds;

            var resultados = string.Join("|", tasks.Where(t => !t.IsFaulted).Select(t => t.Result));

            this.lblWhenAll.Text = totalSegundos.ToString();

            MessageBox.Show($"Resultados: {resultados} - Exception {messageEx}");
        }

        private List<Task<string>> GetTasks()
        {
            var tasks = new List<Task<string>>();
            
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(this.DoLongThingAsync(DateTime.Now));
            }
            return tasks;
        }

        private List<Task<string>> GetTaskWithExceptions()
        {
            var tasks = new List<Task<string>>();

            for (int i = 0; i < 5; i++)
            {
                tasks.Add(this.DoLongThingAsync(DateTime.Now));
            }

            tasks.Add(this.DoLongThingAsyncEx1());

            for (int i = 0; i < 5; i++)
            {
                tasks.Add(this.DoLongThingAsync(DateTime.Now));
            }
            return tasks;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var inicio = DateTime.Now;
            var resultadolist = new List<string>();
            var messageEx = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    resultadolist.Add(await this.DoLongThingAsync(DateTime.Now));
                }
                catch (Exception exception)
                {
                    messageEx = exception.Message;
                }
            }

            var totalSegundos = (DateTime.Now - inicio).TotalMilliseconds;

            var resultados = string.Join("|", resultadolist);

            this.lblSeparado.Text = totalSegundos.ToString();

            MessageBox.Show($"Resultados: {resultados} - Exception {messageEx}");
        }
    }
}
