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

namespace GameRasmusGyllenhammar
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }
        
        private void StartGame()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

            Application.Current.Shutdown();
        }
        private void button_start(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void button_settings(object sender, RoutedEventArgs e)
        {

        }
    }
}
