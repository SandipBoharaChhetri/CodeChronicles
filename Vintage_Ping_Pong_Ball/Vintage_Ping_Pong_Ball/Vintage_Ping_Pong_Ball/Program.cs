using System;
using System.Threading;
using System.Drawing;
using GDIDrawer;

namespace Vintage_Ping_Pong_Ball
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Make GDI Drawer
            CDrawer canvas = new CDrawer();

            //Continuous update off
            canvas.ContinuousUpdate = false;

            Point playQuit;
            Point leftClick;
            Point mousePos;
            canvas.Clear();
            canvas.Scale = 5; //Scale the GDI window
            bool quit = false;


            do
            {
                int x, y; //position of the ball
                int moveBallX = 1; // move the ball
                int moveBallY = 1; // move the ball
                int ballSpeed = 20; //spped of the ball
                int score = 0; //initial Score

                //GDI background edges
                int top = 0;
                int right = 0;
                int bottom = 0;
                canvas.Clear();

                //Top edge
                while (top < canvas.ScaledWidth)
                {
                    canvas.SetBBScaledPixel(top, 0, Color.Aqua);
                    top++;
                }

                //Right edge
                while (right < canvas.ScaledHeight)
                {
                    canvas.SetBBScaledPixel(159, right, Color.Aqua);
                    right++;
                }

                //buttom
                while (bottom < canvas.ScaledWidth)
                {
                    canvas.SetBBScaledPixel(bottom, 119, Color.Aqua);
                    bottom++;
                }

                //Game start when left mouse click
                int startGame = 0;

                canvas.AddText("Click to Play game!!", 20, Color.Yellow);
                //canvas.AddText(clickToPlay, 20, (canvas.ScaledWidth / 2) - (clickToPlay.Length / 2), canvas.ScaledHeight / 2, canvas.ScaledWidth / 2, 20, Color.Yellow);
                canvas.Render();

                do
                {
                    bool check = canvas.GetLastMouseLeftClickScaled(out leftClick);
                    Thread.Sleep(100); //Time taken to start
                    if (check)
                    { startGame = 1; }
                } while (startGame == 0);

                //Creating Random number for ball position
                Random random = new Random();
                x = random.Next(12, 18);
                y = random.Next(0, 120);


                // Conditions for left wall
                while (x > 0)
                {
                    //Creating ball as rectangle
                    canvas.AddRectangle(x, y, 2, 2, Color.Green);

                    //setting mouse cursor position for paddle
                    canvas.GetLastMousePositionScaled(out mousePos);

                    //Display the score and converting int to string
                    canvas.AddText(score.ToString(), 25, 45, 0, canvas.ScaledWidth / 2, 10, Color.White);

                    //limiting the value of Y
                    mousePos.Y = (mousePos.Y < 12) ? 12 : mousePos.Y;
                    mousePos.Y = (mousePos.Y > (canvas.ScaledHeight - 12)) ? (canvas.ScaledHeight - 12) : mousePos.Y;

                    //Add Paddle
                    canvas.AddLine(1, mousePos.Y - 10, 1, mousePos.Y + 10, Color.Red, 10);

                    //if ball hits paddle
                    if ((x == 2) && (y >= (mousePos.Y - 10)) && (y <= (mousePos.Y + 10)))

                    {
                        moveBallX = -moveBallX;
                        ballSpeed -= 1; //decreasing delay to increase speed

                        //increase score every time ball hits paddle
                        score++;
                        //Console.Beep();
                    }
                    canvas.Render();

                    //Adding time delay
                    Thread.Sleep(ballSpeed);

                    canvas.Clear();

                    //Change the position of the ball
                    x += moveBallX;
                    y += moveBallY;

                    //limit the ball within the wall
                    if (x >= (canvas.ScaledWidth - 3))
                    {
                        moveBallX = -moveBallX;
                    }

                    if (y >= (canvas.ScaledHeight - 3) || y < 2)
                    {
                        moveBallY = -moveBallY;
                    }
                }

                canvas.Clear();

                //Output the final score in center after game over
                canvas.AddText($"Final Score: {score}", 40, Color.White);

                //Display Play again inside a box
                canvas.AddRectangle(90, 102, 25, 13, Color.Black, 2, Color.Green);
                canvas.AddText("Play Again", 15, 90, 95, 25, 25, Color.Green);

                //Display Quit inside a box
                canvas.AddRectangle(120, 102, 25, 13, Color.Black, 2, Color.Red);
                canvas.AddText("Quit", 15, 120, 95, 25, 25, Color.Red);

                canvas.Render();

                //Limit the user to click inside quit box to quit

                do
                {
                    canvas.GetLastMouseLeftClickScaled(out playQuit);
                    if ((playQuit.X >= 120 && playQuit.X <= 145 && playQuit.Y >= 102 && playQuit.Y <= 115)) // Condition If player clicks in the QUIT box
                    {
                        quit = false;
                        /* Environment.Exit(0);*/

                    }
                    //Play again
                    else if ((playQuit.X >= 90 && playQuit.X <= 115 && playQuit.Y >= 102 && playQuit.Y <= 115))
                    {
                        quit = true;
                    }
                } while (!((playQuit.X >= 90 && playQuit.X <= 115 && playQuit.Y >= 102 && playQuit.Y <= 115) || (playQuit.X >= 120 && playQuit.X <= 145 && playQuit.Y >= 102 && playQuit.Y <= 115)));

            } while (quit == true);


        
         }
    }
}
