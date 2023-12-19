using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private TcpClient client;
        private string _message;
        private string _serverResponse;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string ServerResponse
        {
            get { return _serverResponse; }
            set
            {
                _serverResponse = value;
                OnPropertyChanged(nameof(ServerResponse));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            client = new TcpClient("127.0.0.1", 50532);
            DataContext = this;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.ASCII.GetBytes(Message);
                stream.Write(data, 0, data.Length);

                data = new byte[256];
                int bytesRead = stream.Read(data, 0, data.Length);
                string responseData = Encoding.ASCII.GetString(data, 0, bytesRead);
                ServerResponse = $"Réponse du serveur : {responseData}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
