using System;
using System.Threading.Tasks;
using HardwareFunctionality;
using IO;

namespace TheCoffeMaker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Grinder gr = new Grinder();
            MilkFoamer mf = new MilkFoamer();
            SugarDispenser sd = new SugarDispenser();
            WaterHeater wh = new WaterHeater();
            Coffe coffe = new Coffe(CupSize.large, true, true);
            ProcessEvents(gr, mf, sd, wh);
            ErrorEvents(gr, mf, sd, wh);
            while(true)
            {
                await Task.Run(() => MakeCoffe(UserChoiseMenu(), gr, mf, sd, wh));
                await Task.Delay(1500);
                Console.Clear();
                Console.WriteLine("Din kaffe är klar!");
                await Task.Delay(1500);
            }
            
        }
        static Coffe UserChoiseMenu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            bool milk = false;
            bool sugar = false;
            int size = 0;
            while (true)
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 1;
                Console.Write("Hur vill du ha ditt kaffe?");
                Console.CursorLeft = 4;
                Console.CursorTop = 3;
                Console.Write("[M]jölk?");
                Console.CursorLeft = 4;
                Console.CursorTop = 4;
                Console.Write("[S]ocker?");
                Console.CursorLeft = 0;
                Console.CursorTop = 5;
                Console.Write("X");
                Console.CursorLeft = 4;
                Console.CursorTop = 5;
                Console.Write("[K]oppstorlek?");
                Console.CursorLeft = 4;
                Console.CursorTop = 6;
                Console.Write("[R]edo?");
                ConsoleKey choise = Console.ReadKey(true).Key;
                
                switch (choise)
                {
                    case ConsoleKey.M:
                    if (milk == true)
                    {
                        Console.CursorLeft = 1;
                        Console.CursorTop = 3;
                        Console.Write(" ");
                        milk = false;
                    }
                    else if (milk == false)
                    {
                        Console.CursorLeft = 1;
                        Console.CursorTop = 3;
                        Console.Write("X");
                        milk = true;
                    }
                    break;

                    case ConsoleKey.S:
                    if (sugar == true)
                    {
                        Console.CursorLeft = 1;
                        Console.CursorTop = 4;
                        Console.Write(" ");
                        sugar = false;
                    }
                    else if (sugar == false)
                    {
                        Console.CursorLeft = 1;
                        Console.CursorTop = 4;
                        Console.Write("X");
                        sugar = true;
                    }
                    break;

                    case ConsoleKey.K:
                    if (size == 0)
                    {
                        Console.CursorLeft = 1;
                        Console.CursorTop = 5;
                        Console.Write("X");
                        size = 1;
                    }
                    else if (size == 1)
                    {
                        Console.CursorLeft = 2;
                        Console.CursorTop = 5;
                        Console.Write("X");
                        size = 2;
                    }
                    else if (size == 2)
                    {
                        Console.CursorLeft = 1;
                        Console.CursorTop = 5;
                        Console.Write("  ");
                        size = 0;
                    }
                    break;

                    case ConsoleKey.R:
                    Coffe coffe = new Coffe((CupSize)size, milk, sugar);
                    return coffe;


                }
            }
        }
        static async Task<bool> MakeCoffe(Coffe coffe, Grinder grinder, MilkFoamer milkFoamer, SugarDispenser sugarDispenser, WaterHeater waterHeater)
        {
            float timeDelay = 0;
            Task sugar = null;
            Task milk = null;
            switch(coffe.Cupsize)
            {
                case CupSize.small:
                timeDelay = 1.0f;
                break;

                case CupSize.medium:
                timeDelay = 1.2f;
                break;
                
                case CupSize.large:
                timeDelay = 1.5f;
                break;
            }
            if (coffe.SugarChoise == true)
            {
                sugar = sugarDispenser.Run(timeDelay);
            }
            if (coffe.MilkChoise == true)
            {
                milk = milkFoamer.Run(timeDelay);
            }
            Task grind = grinder.Run(timeDelay);
            Task heat = waterHeater.Run(timeDelay);
            if (sugar != null)
            {
                await sugar;
            }
            if (milk != null)
            {
                await milk;
            }
            await grind;
            await heat;
            return true;
        }
        static void ErrorEvents(Grinder gr, MilkFoamer mf, SugarDispenser sd, WaterHeater wh)
        {
            gr.cc.OnContainerEmpty += (async (o, s) => 
            {
                Console.Clear();
                Console.WriteLine("Kaffet är slut. Tryck enter när du fyllt på."); 
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    await gr.cc.Refill();
                    UserChoiseMenu();
                }
            });
            mf.mc.OnContainerEmpty += (async (o, s) => 
            {
                Console.Clear();
                Console.WriteLine("Mjölken är slut. Tryck enter när du fyllt på."); 
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    await mf.mc.Refill();
                    UserChoiseMenu();
                }
            });
            sd.sc.OnContainerEmpty += (async (o, s) => 
            {
                Console.Clear();
                Console.WriteLine("Sockret är slut. Tryck enter när du fyllt på."); 
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    await sd.sc.Refill();
                    UserChoiseMenu();
                }
            });
            wh.wc.OnContainerEmpty += ((o, s) => 
            {
                Console.Clear();
                Console.WriteLine("Fyller på vatten.");
            });
            wh.wc.OnRefillWater += (async (o, s) => 
            {
                Console.WriteLine("Vatten påfyllt.");
                await Task.Delay(1500);
                UserChoiseMenu();
            });
        }
        static void ProcessEvents (Grinder gr, MilkFoamer mf, SugarDispenser sd, WaterHeater wh)
        {
            gr.OnComplete += ((o, s) => 
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.Write(new string(' ', Console.WindowWidth));
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.Write("Kaffet malt");
            });
            mf.OnComplete += ((o, s) => 
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.Write(new string(' ', Console.WindowWidth));
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.WriteLine("Mjölken skummad");
            });
            sd.OnComplete += ((o, s) => 
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.Write(new string(' ', Console.WindowWidth));
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.WriteLine("Socker tillsatt");
            });
            wh.OnComplete += ((o, s) => 
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.Write(new string(' ', Console.WindowWidth));
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.WriteLine("Vattnet varmt");
            });
        }
    }
}
