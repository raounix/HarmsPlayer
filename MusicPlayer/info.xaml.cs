using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for info.xaml
    /// </summary>
    public partial class info : Window
    {
        public info()
        {
            InitializeComponent();
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Telegram_Click(object sender, RoutedEventArgs e)
        {
            string targetURL = @"http://www.telegram.me/capitanpick";
            System.Diagnostics.Process.Start(targetURL);
        }

        private void Instagram_Click(object sender, RoutedEventArgs e)
        {
            string targetURL = @"https://www.instagram.com/capitanpick/";
            System.Diagnostics.Process.Start(targetURL);
        }

        private void Github_Click(object sender, RoutedEventArgs e)
        {
            string targetURL = @"https://github.com/captainpick";
            System.Diagnostics.Process.Start(targetURL);
        }
    }
}
