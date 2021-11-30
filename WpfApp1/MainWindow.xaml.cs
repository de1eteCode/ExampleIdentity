using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
using WpfApp1.Network;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private WebApi _webApi;

        public MainWindow()
        {
            DataContext = this;
            _webApi = new WebApi();
            InitializeComponent();
        }

        public string Login { get; set; } = "testuser1";
        public string Password { get; set; } = "SKJdyu34_";
        private string _textResponse;

        public string TextResponse
        {
            get { return _textResponse; }
            set { _textResponse = value; OnPropertyChanged(); }
        }



        private async void LoginClick(object sender, RoutedEventArgs e)
        {
            var result = await _webApi.Authorize(Login, Password);
            TextResponse = $"Status: {result.StatusCode}\r\t Content: {result.Content}";

            _webApi.SetCookie(result.Cookies.First());
        }

        private async void Request(object sender, RoutedEventArgs e)
        {
            var result = await _webApi.GetInfo();
            TextResponse = $"Status: {result.StatusCode}\r\t Content: {result.Content}";
        }
    }
}
