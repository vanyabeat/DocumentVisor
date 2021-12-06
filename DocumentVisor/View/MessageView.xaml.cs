using System.Windows;
using System.Windows.Input;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для MessageView.xaml
    /// </summary>
    public partial class MessageView : Window
    {
        public MessageView(string text)
        {
            InitializeComponent();
            MessageText.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Close();
        }
    }
}