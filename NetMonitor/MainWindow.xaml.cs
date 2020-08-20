using System;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace NetMonitor
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        long preByteSend = 0;
        long preByteRec = 0;

        long DSpeed, USpeed;

        IPv4InterfaceStatistics iFace;

        private void Timer_Tick(object sender, System.EventArgs e)
        { 
            iFace = NetworkInterface.GetAllNetworkInterfaces()[0].GetIPv4Statistics();

            USpeed = (iFace.BytesSent - preByteSend) / 1024;
            DSpeed = (iFace.BytesReceived - preByteRec) / 1024;

            preByteSend = NetworkInterface.GetAllNetworkInterfaces()[0].GetIPv4Statistics().BytesSent;
            preByteRec = NetworkInterface.GetAllNetworkInterfaces()[0].GetIPv4Statistics().BytesReceived;

            if (DSpeed > 1000) // enter download speed
            {
                DownloadSpeed.Content = "Download: " + Math.Round((double)DSpeed / 1024, 2) + " MB/s";
            }
            else
            {
                DownloadSpeed.Content = "Download: " + Math.Round((double)DSpeed, 2) + " KB/s";
            }

            if (USpeed > 1000) // enter upload speed
            {
                UploadSpeed.Content = "Upload " + Math.Round((double)USpeed / 1024, 2) + " MB/s";
            }
            else
            {
                UploadSpeed.Content = "Upload " + Math.Round((double)USpeed, 2) + " KB/s";
            }
        }

        private void Grid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e) // close application 
        {
            Application.Current.Shutdown();
        }
    }
}
