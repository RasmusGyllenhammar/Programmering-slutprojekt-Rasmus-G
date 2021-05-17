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
        int ballSpeed = 12;
        

        DispatcherTimer gameTimer = new DispatcherTimer(); //instans av timer
        Player firstPlayer = new Player();
        Player secondPlayer = new Player();
        Ball gameBall = new Ball { XPosition = 740, YPosition = 50 };
       
        public MainWindow()
        {
            InitializeComponent();

            myCanvas.Focus(); //fokusera på canvasen bara

            gameTimer.Tick += GameTimerEvent; //linking to an event
            //gameTimer.Tick += playermove;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);  //hur ofta vi vill att den ska ticka
            gameTimer.Start();

            //objekt av klassen player, till spelarna sen
           
            




            // dd.keyIsUp(); fixa koden i PLayerOne
        }
        
        public double BallXPosition
        {
            get { return gameBall.XPosition; }
            set
            {
                gameBall.XPosition = value;
            }
        }
        public double BallYPosition
        {
            get { return gameBall.YPosition; }
            set
            {
                gameBall.YPosition = value;
            }
        }


        private void GameTimerEvent(object sender, EventArgs e)
        {       //kolla upp om spelaren


                        //testa while go == up, båda har goUp därför går de samtdigt
                        if (goUp == true && Canvas.GetTop(player)  > 5)
                        {
                            Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);

                        }else if (goDown == true && Canvas.GetTop(player) + (player.Height + 45) < Application.Current.MainWindow.Height)
                        {
                            Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
                        }
                        //player two
                        if (goUp == true && Canvas.GetTop(playerTwo) > 5)
                        {
                            Canvas.SetTop(playerTwo, Canvas.GetTop(playerTwo) - playerTwoSpeed);

                        }else if (goDown == true && Canvas.GetTop(playerTwo) + (playerTwo.Height + 45) < Application.Current.MainWindow.Height)
                        {
                            Canvas.SetTop(playerTwo, Canvas.GetTop(playerTwo) + playerTwoSpeed);
                        }

            CheckCollisionWithPlayers();
          
            //rör mot vänster sida
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) - ballSpeed);

                        //reverese the speed när den når sin gräns från vänster till höger
                        //om den är mindre än 5 l.e från vänster och är större än bredden av skärmen, 
                      NewPoint();

           
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
            //kolla ifall den överstiger skärmen, sen reset (x,y) 
            if (Canvas.GetLeft(ball) < 1 )
            {
                ballSpeed = -ballSpeed;
                ResetBallPosition();

            }
            if (Canvas.GetLeft(ball) + (ball.Width * 1.3) > Application.Current.MainWindow.Width)
            {
                ballSpeed = -ballSpeed;
                ResetBallPosition();
            }
        }

        private void ResetBallPosition()
        {
            Canvas.SetTop(ball, 300);
            Canvas.SetLeft(ball, 350);
        }
        private void CheckCollisionWithPlayers()
        {

            foreach (var rectangles in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)rectangles.Tag == "paddle")
                {
                    rectangles.Stroke = Brushes.Black;

                    Rect ballHitBox = new Rect(Canvas.GetLeft(ball), Canvas.GetTop(ball), ball.Width, ball.Height);
                    Rect playerHitBox = new Rect(Canvas.GetLeft(rectangles), Canvas.GetTop(rectangles), rectangles.Width, rectangles.Height);
                    if (ballHitBox.IntersectsWith(playerHitBox))
                    {
                        ballSpeed = -ballSpeed;
                        Canvas.SetRight(ball, Canvas.GetRight(rectangles) - ball.Height);
                       
                    }
                    
                }
                
               
            }
        }
        private void CheckCollisionWithWall()
        {
            
        }
        private void changeDirection()
        {
            
        }
    }
}
