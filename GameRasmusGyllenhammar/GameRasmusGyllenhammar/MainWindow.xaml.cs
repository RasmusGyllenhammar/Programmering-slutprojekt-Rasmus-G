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

        bool goUp, goDown;
        int playerSpeed = 8;
        int playerTwoSpeed = 8;
        double ballSpeedX = 12;
        double ballSpeedY = 0;


        DispatcherTimer gameTimer = new DispatcherTimer(); //instans av timer
        Player firstPlayer = new Player();
        Player secondPlayer = new Player();
       
        Ball gameBall = new Ball { XPosition = 740, YPosition = 50 };
        Random randomize = new Random();
        public MainWindow()
        {
            InitializeComponent();
           
            myCanvas.Focus(); //fokusera på canvasen bara

            gameTimer.Tick += GameTimerEvent; //linking to an event
            //gameTimer.Tick += playermove;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);  //hur ofta vi vill att den ska ticka
            gameTimer.Start();

           
       
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
        {      


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
            UpdateBallPosition();
            NewPoint();
            CheckCollisionWithWalls();
            EndScreen();
          

            //reverese the speed när den når sin gräns från vänster till höger
            /*
               if (Canvas.GetLeft(ball) < 5 || Canvas.GetLeft(ball) + (ball.Width * 1.2) > Application.Current.MainWindow.Width)
            {
                speed = -speed;
            }
             */
            //om den är mindre än 5 l.e från vänster och är större än bredden av skärmen, 



        }

        private void PlayerMove()
        {

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
                ballSpeedX = -ballSpeedX;
                ResetBallPosition();
                firstPlayer.Score += 1;
                greenPlayerLabel.Content = "Green Score: " + firstPlayer.Score;

            }
            if (Canvas.GetLeft(ball) + (ball.Width * 1.3) > Application.Current.MainWindow.Width)
            {
                ballSpeedX = -ballSpeedX;
                ResetBallPosition();
                secondPlayer.Score += 1;
                redPlayerLabel.Content = "Red Score: " + secondPlayer.Score;
            }
        }

        private void ResetBallPosition()
        {
            Canvas.SetTop(ball, 200);
            Canvas.SetLeft(ball, 400);
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
                        
                        ballSpeedX = -ballSpeedX;
                       // ballSpeedX -= 1;
                        BallAngle(ballHitBox.Y  + 12.5, playerHitBox.Y + 65);
                        //ballSpeedY += 1;
                        Canvas.SetRight(ball, Canvas.GetRight(rectangles) - ball.Height);
                       
                    }
                    
                }
                
               
            }
        }

        private void BallAngle( double ballY, double playerY)
        {
            var playerHeight = 65;
            var maxAngle = (Math.PI / 3); //75 eller 60 degree, max vinklen
           
            //vinkeln att rotera hastigheten
            var nextAngle = (ballY - playerY) / playerHeight * maxAngle;
            //samma speed
            var speedMagnitude = Math.Sqrt(Math.Pow(ballSpeedX, 2) + Math.Pow(ballSpeedY, 2));

            //upptadera bollV i x och y
            ballSpeedX = speedMagnitude * Math.Cos(nextAngle) * Math.Sign(ballSpeedX); // kollar också ifall det är negativt eller positivt
            ballSpeedY = speedMagnitude * Math.Sin(nextAngle);

           
        }

        private void UpdateBallPosition() 
        {
          
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + ballSpeedX);
            Canvas.SetTop(ball, Canvas.GetTop(ball) + ballSpeedY);
          
        }
        private void CheckCollisionWithWalls()
        {
            if (Canvas.GetTop(ball) + (ball.Height + 35) > Application.Current.MainWindow.Height)
            {
                ballSpeedY = -ballSpeedY;
            }

            if (Canvas.GetTop(ball) < 10)
            {

                ballSpeedY = -ballSpeedY;
            }



        }


        /// <summary>
        /// stannar bollen och öppnar pop up fönstret med alternativ
        /// </summary>
        private void EndScreen()
        {
            if (firstPlayer.Score >= 1 || secondPlayer.Score >= 1)
            {
                ballSpeedX = 0;
                ballSpeedY = 0;
                endScreen.IsOpen = true;
                
            }

            

        }

        private void StartNewGame()
        {
            /*   UpdateBallPosition();
                greenPlayerLabel.Content = "Green Score: ";
                redPlayerLabel.Content = "Red Score: ";*/
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void button_restart(object sender, RoutedEventArgs e)
        {
            StartNewGame();
            
        }

        private void button_close(object sender, RoutedEventArgs e)
        {
            CloseGame();
        }

        private void CloseGame()
        {
            Application.Current.Shutdown();
        }
    }
}
