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

        bool  goUp, goDown;
        int playerSpeed = 8;
        int playerTwoSpeed = 8;
        int speed = 12;

        DispatcherTimer gameTimer = new DispatcherTimer(); //instans av timer

        public MainWindow()
        {
            InitializeComponent();

            myCanvas.Focus(); //fokusera på canvasen bara

            gameTimer.Tick += PlayerMove; //linking to an event
            //gameTimer.Tick += playermove;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);  //hur ofta vi vill att den ska ticka
            gameTimer.Start();
            //objekt av klassen player, till spelarna sen
            player playerOne = new player();
            player playerTwo = new player();
            playerOne.Score = 0;
            
           // dd.keyIsUp(); fixa koden i PLayerOne
        }
        
        private void PlayerMove(object sender, EventArgs e)
        {       //kolla upp om spelaren


            if (goUp == true && Canvas.GetTop(player)  > 5)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
               
            }

            if (goDown == true && Canvas.GetTop(player) + (player.Height + 45) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            }
            //player two
            if (goUp == true && Canvas.GetTop(playerTwo) > 5)
            {
                Canvas.SetTop(playerTwo, Canvas.GetTop(playerTwo) - playerTwoSpeed);
                
            }

            if (goDown == true && Canvas.GetTop(playerTwo) + (playerTwo.Height + 45) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(playerTwo, Canvas.GetTop(playerTwo) + playerTwoSpeed);
            }

            Canvas.SetLeft(ball, Canvas.GetLeft(ball) - speed);

            //reverese the speed när den når sin gräns från vänster till höger
            //om den är mindre än 5 l.e från vänster och är större än bredden av skärmen, lägga i en annan metod
           if (Canvas.GetLeft(ball) < 5 || Canvas.GetLeft(ball) + (ball.Width * 1.2) > Application.Current.MainWindow.Width)
            {
                speed = -speed;
            }
        }
        /*
         ifall man inte trycker på någon knapp så ska det var true
        känner av ifall en av pilarna är trycka
         */
        private void KeyIsDown(object sender, KeyEventArgs e)
        { 

            switch (e.Key)
            {
                case Key.Up: //ha player.move boolean?
                    goUp = true;
                    goDown = false;
                    playerSpeed = 0;
                    break;
                case Key.W:
                    goUp = true;
                    goDown = false;
                    playerTwoSpeed = 0;
                    
                    break;
                case Key.Down:
                    goUp = false;
                    goDown = true;
                    playerSpeed = 0;
                    break;
                case Key.S:
                    goUp = false;
                    goDown = true;
                    playerTwoSpeed = 0;
                    break;
            }
          

        }
        /*
         ifall man inte trycker på någon knapp så ska det var false
         */
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    goUp = false;
                    playerTwoSpeed = 8;
                    break;
                case Key.Up:
                    goUp = false;
                    playerSpeed = 8;
                    break;
                case Key.Down:
                    goDown = false;
                    playerSpeed = 8;
                    break;

                case Key.S:
                    goDown = false;
                    playerTwoSpeed = 8;
                    break;
                default:
                    break;
            }

           
        }
        
        private void NewPoint()
        {
            
        }
        private void CheckCollisionWithPlayers()
        {

        }
        private void ChechCollisionWithWall()
        {

        }
    }
}
