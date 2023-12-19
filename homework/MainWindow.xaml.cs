using SAMP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerSearcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private List<Thread> threadList = new List<Thread>();

        public void Worker(object param)
        {
            int port = (int)param;
            String ip = null;
            while ((ip = IPManager.GetIP()) != null)
            {
                Dispatcher.Invoke(() =>
                {
                    IPStats stats = IPManager.GetStats();
                    StatusLabel.Content = String.Format("{0}/{1}", stats.ipChecked, stats.totalIp);
                });
                try
                {
                    Query query = new Query(ip, port);
                    query.Send('i');
                    String[] data = query.Store(query.Recieve());
                    String result = String.Format("{0}:{1} ({2}, {3}/{4})\n", ip, port, data[3], data[1], data[2]);
                    Dispatcher.Invoke(() =>
                    {
                        ResultBox.Text += result;
                    });
                    using (StreamWriter sw = File.AppendText("./result.txt"))
                    {
                        sw.Write(result);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public void SoftwareWorker()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Dispatcher.Invoke(() =>
                {
                    if (threadList.Count > 0)
                    {
                        threadList.RemoveAll(x => !x.IsAlive);
                        if (threadList.Count == 0)
                        {
                            LaunchButton.Content = "Начать";
                        }
                    }
                });
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Thread thr = new Thread(new ThreadStart(SoftwareWorker));
            thr.Start();
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            if (threadList.Count > 0)
            {
                for (int i = 0; i < threadList.Count; i++)
                {
                    threadList[i].Abort();
                }
                threadList = new List<Thread>();
                LaunchButton.Content = "Начать";
            }
            else
            {
                LaunchButton.Content = "Завершить";
                IPManager.Reset();
                foreach (String line in IpRangeBox.Text.Split('\n'))
                {
                    String ipRange = line.Replace("\r", "").Replace("\t", "");
                    String[] ips = ipRange.Split('-');
                    IPManager.AddIPRange(new IPRange(ips[0], ips[1]));
                }
                int port = int.Parse(PortBox.Text);
                int threads = int.Parse(ThreadBox.Text);
                for (int i = 0; i < threads; i++)
                {
                    Thread thr = new Thread(new ParameterizedThreadStart(Worker));
                    thr.Start(port);
                    threadList.Add(thr);
                    Thread.Sleep(10);
                }
            }
        }
    }
}
