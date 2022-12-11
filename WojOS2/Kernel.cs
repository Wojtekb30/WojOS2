using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
//using Cosmos.System.FileSystem;
//using Cosmos.System.FileSystem.VFS;
using System.IO;
//using System.Drawing;
using Cosmos.System.Graphics.Fonts;
using Cosmos.System.Network.IPv4;


namespace WojOS2
{

    public class Kernel : Sys.Kernel
    {
        Canvas canvas;
        

        protected override void BeforeRun()
        {

            Console.WriteLine("Welcome to WojOS 2!");
            Console.WriteLine("Launching desktop...");

            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1280, 720, ColorDepth.ColorDepth32));
            canvas.Clear(System.Drawing.Color.LightCyan);


    }
        
        
        protected override void Run()
        {
            int myszx;
            int myszy;


            //while (1 == 1)
            //{
                Cosmos.Core.Memory.Heap.Collect();
                try
                {
                    Pen pen = new Pen(System.Drawing.Color.Green);
                    
                    //Power button
                    pen.Color = System.Drawing.Color.Red;
                    canvas.DrawFilledRectangle(pen, 20, 20, 50, 50);
                    pen.Color = System.Drawing.Color.White;
                    canvas.DrawFilledCircle(pen, 45, 45, 20);
                    pen.Color = System.Drawing.Color.Black;
                    canvas.DrawFilledRectangle(pen, 45, 30, 3, 13);
                    canvas.DrawCircle(pen, 45, 45, 15);

                    //reset button
                    pen.Color = System.Drawing.Color.LightGreen;
                    canvas.DrawFilledRectangle(pen, 80, 20, 50, 50);
                    pen.Color = System.Drawing.Color.White;
                    //char litera = 'R';
                    //canvas.DrawChar(litera, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, pen, 80, 25);
                    
                    
                    
                    
                    
                    //terminal button
                    pen.Color = System.Drawing.Color.Black;
                    canvas.DrawFilledRectangle(pen, 20, 20 + 60, 50, 50);
                    pen.Color = System.Drawing.Color.White;
                    canvas.DrawFilledRectangle(pen, 25, 20 + 70, 20, 3);
                    canvas.DrawFilledRectangle(pen, 25, 20 + 75, 40, 3);
                    canvas.DrawFilledRectangle(pen, 25, 20 + 80, 30, 3);
                    canvas.DrawFilledRectangle(pen, 25, 20 + 85, 10, 3);
                    canvas.DrawFilledRectangle(pen, 25, 20 + 90, 40, 3);
                    canvas.DrawFilledRectangle(pen, 25, 20 + 95, 30, 3);

                    Sys.MouseManager.ScreenWidth = (uint)canvas.Mode.Columns;
                    Sys.MouseManager.ScreenHeight = (uint)canvas.Mode.Rows;
                    uint myszxr = Sys.MouseManager.X;
                    uint myszyr = Sys.MouseManager.Y;

        
                    myszx = Convert.ToInt32(myszxr); //mouse
                    myszy = Convert.ToInt32(myszyr);
                    if (myszx < 0)
                    {
                        myszx = 10;
                    }
                    if (myszx > 1280)
                    {
                        myszx = 10;
                    }
                    if (myszy < 0)
                    {
                        myszy = 10;
                    }
                    if (myszy > 720)
                    {
                        myszy = 10;

                    }
                    pen.Color = System.Drawing.Color.Purple;
                    canvas.DrawFilledCircle(pen, myszx, myszy, 3);


                    canvas.Display();
                    canvas.Clear(System.Drawing.Color.LightCyan);

                    if (Sys.MouseManager.MouseState == Sys.MouseState.Left)
                    {
                        if ((myszx > 20) & (myszx < 70) & (myszy > 20) & (myszy < 70)) //power off click
                        {
                            Sys.Power.Shutdown();
                        }
                        if ((myszx > 80) & (myszx < 80 + 50) & (myszy > 20) & (myszy < 70)) //reset click
                        {
                            Sys.Power.Reboot();
                        }


                        //terminal from here
                        if ((myszx > 20) & (myszx < 70) & (myszy > 20 + 60) & (myszy < 70 + 60)) //terminal click
                        {
                            bool terminalstatus = true;
                            var terminaltekst = "";
                            canvas.Disable();
                            FullScreenCanvas.Disable();
                            Stop();
                            Console.Clear();
                            Console.WriteLine("Desktop turned off. Type 'desktop' to launch again.");
                            Console.WriteLine("Type 'help' to see terminal commands.");
                            while (terminalstatus == true)
                            {
                                terminaltekst = "";
                                Console.WriteLine("");
                                terminaltekst = Console.ReadLine();
                                terminaltekst = terminaltekst.ToLower();
                                if (terminaltekst == "help") { Console.WriteLine("Commands: help, desktop, time, clear, shutdown, reboot, info, "); }
                                if (terminaltekst == "info") { Console.WriteLine("WojOS 2, made by Wojtekb30, 08.2022"); }
                                if (terminaltekst == "clear") 
                                { 
                                    Console.Clear();
                                    Console.WriteLine("Type 'help' to see terminal commands");
                                }
                                if (terminaltekst == "shutdown") 
                                { 
                                    Sys.Power.Shutdown();
                                    Console.WriteLine("Shutting down. Goodbye!");
                                }
                                if (terminaltekst == "reboot") 
                                { 
                                    Sys.Power.Reboot();
                                    Console.WriteLine("Restarting...");
                                }
                                if (terminaltekst == "desktop")
                                {
                                    //Console.WriteLine("Launching desktop...");
                                    terminalstatus = false;
                                    terminaltekst = "";
                                    myszx = 0;
                                    myszy = 0;
                                    canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1280, 720, ColorDepth.ColorDepth32));
                                    canvas.Display();
                                    Run();
                                    //break;
                                }
                                if (terminaltekst == "time") 
                                {
                                    Console.WriteLine("Number of day of the week: " + Cosmos.HAL.RTC.DayOfTheWeek.ToString());
                                    Console.WriteLine("Date (D.M.Y): " + Cosmos.HAL.RTC.DayOfTheMonth.ToString() +"."+ Cosmos.HAL.RTC.Month.ToString() + "." + Cosmos.HAL.RTC.Year.ToString());
                                    Console.WriteLine("Time (H:M:S): " + Cosmos.HAL.RTC.Hour.ToString() + ":" + Cosmos.HAL.RTC.Minute.ToString() + ":" + Cosmos.HAL.RTC.Second.ToString());
                                }
                            }

                        }
                    }

                }
                catch (Exception e)
                {
                    mDebugger.Send("Exception occurred: " + e.Message);
                    Sys.Power.Shutdown();
                }
            }
            }
        }
    //}
