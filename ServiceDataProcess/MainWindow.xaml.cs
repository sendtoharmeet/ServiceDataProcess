using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceDataProcess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceMessages serviceMessages;
        public MainWindow()
        {
            serviceMessages = new ServiceMessages();
            DataContext = serviceMessages;
            InitializeComponent();
            serviceMessages.ProcessMessage = "Service Processing";
            Task.Run(async () => await ReadCsv());
        }

        private async Task ProcessData(string data)
        {
            serviceMessages.ProcessMessage += "     -  Service Processing : " + data;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://reqres.in/api/unknown?data="+ data);
                var result = await client.GetAsync(client.BaseAddress);
                string resultContent = await result.Content.ReadAsStringAsync();

                WriteoutputData(resultContent);
            }
        }

        private async Task ReadCsv()
        {
            FileStream fs = new FileStream(@"C:\Temp\data.csv", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            var str = "";
            while ((str = sr.ReadLine()) != null)
            {
                await ProcessData(str);
            }

            serviceMessages.ProcessMessage += "Service Processed";
        }

        private void WriteoutputData(string data)
        {
            string newFileName = @"C:\Temp\output_data";

            if (!File.Exists(newFileName))
            {
                File.WriteAllText(newFileName, data);
            }
            else
            {
                File.AppendAllText(newFileName, data);
            }
        }
    }
}
