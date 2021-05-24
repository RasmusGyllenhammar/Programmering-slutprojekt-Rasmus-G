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
    /// Interaction logic for EndMenu.xaml
    /// </summary>
    public partial class EndMenu : Window
    {
        public EndMenu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// metod som startar om programmet och stänger ner förra
        /// </summary>
        private void StartNewGame()
        {
           
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        /// <summary>
        /// knappen som kallar på startNewGame();  när man trycker på den
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_restart(object sender, RoutedEventArgs e)
        {
            StartNewGame();

        }
        /// <summary>
        /// button_close kallar på metod CloseGame(); 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close(object sender, RoutedEventArgs e)
        {
            CloseGame();
        }
        /// <summary>
        /// Metoden stänger ner programmet
        /// </summary>
        private void CloseGame()
        {
            Application.Current.Shutdown();
        }
    }
}
