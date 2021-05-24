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
        double ballSpeedY = 2;
        bool openStartMenu = true;
        bool openPopup = true;

        DispatcherTimer gameTimer = new DispatcherTimer(); //instans av timer
        Player firstPlayer = new Player();
        Player secondPlayer = new Player();
       
        Ball gameBall = new Ball { XPosition = 740, YPosition = 50 };
       
        public MainWindow()
        {
            InitializeComponent();
           
            myCanvas.Focus(); //fokusera på canvasen bara

            gameTimer.Tick += GameTimerEvent; //Länkar till ett event
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);  //hur ofta vi vill att den ska ticka
            gameTimer.Start();
            


            // dd.keyIsUp(); fixa koden i PLayerOne
        }



        /// <summary>
        /// Metoden länkad till tick så denna uppdateras varje 20 millisekund.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTimerEvent(object sender, EventArgs e)
        {
           

            PlayersMovement();        

            CheckCollisionWithPlayers();

            UpdateBallPosition();

            NewPoint();

            CheckCollisionWithWalls();

            EndScreen();

        }

        /// <summary>
        /// Kollar ifall boolean är sann och spelaren är innanför skärmen så ska den 
        /// kunna röra på sig, 
        /// </summary>
        private void PlayersMovement()
        {
            firstPlayer.Move(goUp);
            secondPlayer.Move(goUp);


            if (goUp == true && Canvas.GetTop(player) > 5)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);

            }
            else if (goDown == true && Canvas.GetTop(player) + (player.Height + 45) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            }
            //player two
            if (goUp == true && Canvas.GetTop(playerTwo) > 5)
            {
                Canvas.SetTop(playerTwo, Canvas.GetTop(playerTwo) - playerTwoSpeed);

            }
            else if (goDown == true && Canvas.GetTop(playerTwo) + (playerTwo.Height + 45) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(playerTwo, Canvas.GetTop(playerTwo) + playerTwoSpeed);
            }
        }


        /// <summary>
        ///  ifall man inte trycker på någon knapp så ska det var true
        ///  känner av ifall en av pilarna är trycka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        ///  ifall man inte trycker på någon knapp så ska det var false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Metoden kollar ifall bollen åker ut ur sidorna och då ska
        /// bollen byta riktning och man kallar på ResetBallPosition()
        /// beroende vilket håll den åker ut så ökar spelarnas score med 1
        /// och uppdaterar labeln
        /// </summary>
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

        /// <summary>
        /// Metoden återställer bollen grund position dvs i mitten
        /// </summary>
        private void ResetBallPosition()
        {
            Canvas.SetTop(ball, 200);
            Canvas.SetLeft(ball, 400);
        }

        /// <summary>
        /// Denna metod kollar ifall bollen interagerar med spelarna. Skapar två rect med egenskaperna 
        /// från bollen och racken och ifall de interagerar så ska X hastigheten byta riktning samt att 
        /// man kallar på BallAngle för att skapa vinkeln åt bollen när den nuddar racken. 
        /// </summary>
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

        /// <summary>
        /// Metoden räknar ut vinkeln när den nuddar racken, man utgår ifrån mitten av racken.
        /// man utgår ifrån spelarnas halva höjd. Sedan tar vi skillnaden mellan y-pos av bollen och spelarn och delar det på 
        /// spelarnas höjd gånger maxvinkeln vi vill den ska studsa. Sedan räknar vi ut speed och har den till samma hastighet. 
        /// I slutet uppdaterar man X och Y hastighet med den hastigheten gånger vinkeln. 
        /// </summary>
        /// <param name="ballY">Y-pos för bollen</param>
        /// <param name="playerY">Y-pos för spelaren för att kunna se vart bollen och spelaren kolliderar med varandra</param>
        private void BallAngle( double ballY, double playerY)
        {
            //halva spelarnas höjd
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
        /// <summary>
        /// uppdaterar bollen och sätter igång att den åker åt ett visst håll; 
        /// </summary>
        private void UpdateBallPosition() 
        {
            // var ballDirection = randomize.Next(-12, 12);
           // ballSpeedX = randomize.Next(-11, 21);
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + ballSpeedX);
            Canvas.SetTop(ball, Canvas.GetTop(ball) + ballSpeedY);
          
        }

        /// <summary>
        /// Kollar ifall bollen kolliderar med väggarna isåfall ska Y-värdet ändra riktning
        /// </summary>
        private void CheckCollisionWithWalls()
        {
            if (Canvas.GetTop(ball) + (ball.Height + 37) > Application.Current.MainWindow.Height)
            {
                ballSpeedY = -ballSpeedY;
            }

            if (Canvas.GetTop(ball) < 8)
            {

                ballSpeedY = -ballSpeedY;
            }



        }

        /* private void StartScreen()
          {
              if (openStartMenu)
              {


                  StartMenu StartWindow = new StartMenu();

                  StartWindow.Show();
                  openStartMenu = false;
              }
          }*/

        /// <summary>
        /// stannar bollen och öppnar pop up fönstret med alternativ att stänga ner eller att starta nytt spel
        /// </summary>
        private void EndScreen()
        {
            if (firstPlayer.Score >= 5 || secondPlayer.Score >= 5)
            {
                ballSpeedX = 0;
                ballSpeedY = 0;

                if (openPopup)
                {
                  //  this.Hide();
                    EndMenu MenuWindow = new EndMenu();
                    MenuWindow.Show();
                    openPopup = false;
                }
                
            }

        }
      
      
    }
}
