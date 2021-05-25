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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace GameRasmusGyllenhammar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
       
        public MainWindow()
        {
            InitializeComponent();
           
           
            


            // dd.keyIsUp(); fixa koden i PLayerOne
        }
        /// <summary>
        /// skapar instans av pongfönstret och showar det och stänger ner start menyn
        /// </summary>
        private void StartGame()
        {
            GameWindow Pong = new GameWindow();
            Pong.Show();
            this.Close();
            //Application.Current.Shutdown();
        }

        /// <summary>
        /// När man trycker på knappen så kallar man på metoden StartGame()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_start(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        /// <summary>
        /// När man trycker på knappen så kommer man stänga ner applikationen 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
