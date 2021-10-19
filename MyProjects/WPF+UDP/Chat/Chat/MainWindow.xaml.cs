using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace Chat
{
    public partial class MainWindow : Window
    {
        private Connection _connection;
        private ConnectionData _connectionData;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChatLoaded(object sender, RoutedEventArgs e)
        {
            _connection = new Connection();

            _connection.Show();

            _connection.connectBttn.Click += new RoutedEventHandler(Connect);
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            int localPort = Convert.ToInt32(_connection.localPortTxtBox.Text);
            int remotePort = Convert.ToInt32(_connection.remotePortTxtBox.Text);
            string userName = _connection.usernameTxtBox.Text;

            _connectionData = new ConnectionData(localPort, remotePort, userName);

            Task.Run(() => ReceiveMessage());

            _connection.Close();
        }

        private void ColorSchemeChanged(object sender, SelectionChangedEventArgs args)
        {
            string[] newColorScheme = (colorSchemeCmbBox.SelectedItem as ComboBoxItem)
                                                                                .Content.ToString()
                                                                                                .Split('/')
                                                                                                        .ToArray();

            chatListView.Background = (SolidColorBrush)new BrushConverter()
                                                                        .ConvertFromString(newColorScheme[0]);
            chatListView.Foreground = (SolidColorBrush)new BrushConverter()
                                                                        .ConvertFromString(newColorScheme[1]);
        }

        private void FontStyleChanged(object sender, SelectionChangedEventArgs args)
        {
            string newFont = (fontStyleCmbBox.SelectedItem as ComboBoxItem).Content.ToString();

            chatListView.FontFamily = new FontFamily(newFont);
        }

        private void BttnSendClick(object sender, RoutedEventArgs e)
        {
            string message = chatTxtBox.Text;
            chatTxtBox.Text = "";

            var item = new ListBoxItem();
            item.Content = $"{DateTime.Now} {_connectionData.Username}: {message}";
            item.HorizontalAlignment = HorizontalAlignment.Right;

            chatListView.Items.Add(item);

            SaveToFile($"{DateTime.Now} {_connectionData.Username}: {message}");

            SendMessage(message);
        }

        private void SaveToFile(string message)
        {
            StreamWriter writer = new StreamWriter(@"..\..\Dialogue.txt", true);

            writer.WriteLine(message);
            writer.Close();
        }

        private void SendMessage(string message)
        {
            UdpClient client = new UdpClient();

            byte[] userNameinBytes = Encoding.Default.GetBytes(_connectionData.Username);
            client.Send(userNameinBytes, userNameinBytes.Length, ConnectionData.IP, _connectionData.RemotePort);

            byte[] messageInBytes = Encoding.Default.GetBytes(message);
            client.Send(messageInBytes, messageInBytes.Length, ConnectionData.IP, _connectionData.RemotePort);

            client.Close();
        }

        private void ReceiveMessage()
        {
            while (true)
            {
                UdpClient client = new UdpClient(_connectionData.LocalPort);
                IPEndPoint remoteEP = null;

                byte[] userNameInBytes = client.Receive(ref remoteEP);
                string userName = Encoding.Default.GetString(userNameInBytes);

                byte[] messageInBytes = client.Receive(ref remoteEP);
                string message = Encoding.Default.GetString(messageInBytes);

                Dispatcher.Invoke(() => chatListView.Items.Add($"{DateTime.Now} {userName}: {message}"));

                client.Close();
            }
        }
    }
}
