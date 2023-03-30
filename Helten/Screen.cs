using EnemySpace;
using PlayerSpace;
using System.Diagnostics;
using System.IO;
using Helten;
using System.Numerics;
using System.Runtime.InteropServices;
using System;
using GameSpace;
namespace GameScreen
{
    // Class Screen for IO symbols from keyboard on monitor.
    public class Screen
    {
        static Player player = new Player();    // Player class object for interact with Player
        static Text text = new Text();          // Text class object, which display game text on screen
        // Keyboard input method.
        //public string? Input() => Console.ReadLine();

        // Basic messages for game.
        public void MsgContinue()
        {
            if (Game.languageFlag)
                Console.WriteLine("    Press Enter to continue!");
            else 
                Console.WriteLine("    Для продолжения нажмите клавишу Enter!");
        }

        public void MsgAlert()
        {
            if (Game.languageFlag)
                Console.WriteLine("\n    Choose the correct action!\n");
            else
                Console.WriteLine("\n    Выберите корректное действие!\n");
        }

        //public void MsgEnter()
        //{
        //    if (Game.languageFlag)

        //    elseConsole.WriteLine("    Для продолжения нажмите клавишу Enter");
        //}

        // BATTLE //
        public void MsgBattleEscape()
        {
            if (Game.languageFlag)
                Console.WriteLine("\n    You fled the battlefield and survived.");
            else
                Console.WriteLine("\n    Вы сбежали с поля боя и остались живы.");
        }
            
        public void MsgInvExit()
        {
            if (Game.languageFlag)
                Console.WriteLine("\n    Press any button to exit inventory . . .");
            else
                Console.WriteLine("\n    Нажмите любую кнопку для выхода из инвентаря . . .");
        }
        
        public void MsgCaveEnter()
        {
            if (Game.languageFlag)
                Console.WriteLine("\n    You went to the cave . . .\n");
            else
                Console.WriteLine("\n    Вы отправились в пещеру . . .\n");
        }

        public void Cursor() => Console.Write("    > ");

        public void MsgBuyMenu()
        {
            if (Game.languageFlag)
                Console.WriteLine("\n    What do you want to buy?");
            else
                Console.WriteLine("\n    Что вы хотите купить?");
        }

        public void MsgShopExit()
        {
            if (Game.languageFlag)
                Console.WriteLine("\n    Q - exit the store.");
            else
                Console.WriteLine("\n    Q - выход из магазина.");
        }
        
        public void MsgUsedItem()
        {
            if (Game.languageFlag)
                Console.Write("    You used ");
            else
                Console.Write("    Вы использовали ");
        }

        public void MsgTraderItemsIsOut()
        {
            if (Game.languageFlag)
                Console.WriteLine("    Merchants are out of stock!");
            else
                Console.WriteLine("    У торговца закончились товары!");
        }

        public void MsgWeaponInHand(string weaponName)
        {
            if (Game.languageFlag)
                Console.WriteLine($"    You are already holding {weaponName}.");
            else
                Console.WriteLine($"    Вы уже держите в руках {weaponName}.");
        }
        
        public void MsgPickWeapon(string weaponName, int damage)
        {
            if (Game.languageFlag)
                Console.WriteLine($"    You picked up {weaponName}. Damage - {damage}.");
            else
                Console.WriteLine($"    Вы взяли в руки {weaponName}. Урон - {damage}."); 
        }

        public void MsgArmorPutOn(string armorName)
        {
            if (Game.languageFlag)
                Console.WriteLine($"    You already put on {armorName}.");
            else
                Console.WriteLine($"    Вы уже надели {armorName}.");
        }

        public void MsgPickArmor(string armorName, int armor)
        {
            if (Game.languageFlag)
                Console.WriteLine($"    You have equipped {armorName}. Health increased by {armor} points.");
            else
                Console.WriteLine($"    Вы надели {armorName}. Здоровье увеличено на {armor} единиц.");
        }

        // Input method with termination through "R" key.
        public string? Input()
        {
            string? str = Console.ReadLine();
            if ((str == "R") || (str == "r"))
                Process.GetCurrentProcess().Kill();
            // Easter egg.
            if (str == "2023") 
            {
                if (Game.languageFlag)
                {
                    Console.WriteLine("    You opened the Easter egg!");
                    Console.WriteLine("    Press any button to exit . . .");
                }
                else
                {
                    Console.WriteLine("    Вы открыли пасхалку!");
                    Console.WriteLine("    Нажмите любую кнопку для выхода . . .");
                }
                Console.ReadKey();
            }
                
            return str;
        }

        // Display main screen method.
        public void GreetingScreen()
        {
            //Console.WriteLine("Добро пожаловать в игру \"ConsoleQuest v 0.1!\"");  // окно приглашения в игру
            //Console.Write("Желаете начать игру? [Д/Н]: ");

            string path = @"txt/logo.txt";  // path to file with main screen
            string[] lines = File.ReadAllLines(path);   // read file symbol by symbol
            Console.WriteLine(String.Join(Environment.NewLine, lines)); // display file on screen
            Console.ReadKey();
        }
        
        // Allow to choose language in game.
        public void Localization()
        {
            Console.Write("    Choose language (выберите язык): English - 1 / Русский - 2: ");
        }

        // Display game control method.
        public void Control()
        {
            Console.WriteLine("\n    Управление осуществляется вводом определенной цифры и нажатием клавиши Enter.\n" +
                              "    Если никакая цифра не указана на экране, то для продолжения нужно нажать клавишу Enter.\n\n" +
                              "    Перед начало игры откройте ее в режиме полного экрана (Alt + Enter).\n" +
                              "    Приятной игры!\n");
            Console.WriteLine("    Нажмите клавишу Enter для продолжения.");

            ConsoleKeyInfo keypress;
            do
            {
                keypress = Console.ReadKey();
                if (keypress.KeyChar != 13)
                    Console.WriteLine("    Нажмите клавишу Enter!");
            } while (keypress.KeyChar != 13);

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine();
                Thread.Sleep(50);
            }
        }

        public void Control_En()
        {
            Console.WriteLine("\n    Control is carried out by entering a specific number and pressing the Enter key.\n" +
                              "    If no number is shown on the screen, you must press the Enter key to continue.\n\n" +
                              "    Before starting the game, open it in full screen mode (Alt + Enter).\n" +
                              "    Enjoy the game!\n");
            Console.WriteLine("    Press the Enter key to continue.");

            ConsoleKeyInfo keypress;
            do
            {
                keypress = Console.ReadKey();
                if (keypress.KeyChar != 13)
                    Console.WriteLine("    Press the Enter key!");
            } while (keypress.KeyChar != 13);

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine();
                Thread.Sleep(50);
            }
        }

        // Display game story method.
        public void Lore()
        {
            Console.Clear();
            //if (Game.languageFlag)
            //    Console.Write("    Skip lore? [Y/N]: ");
            //else
            //    Console.Write("    Пропустить историю? [Y/N]: ");
            //string? input = Input();
            //if (input?.ToUpper() == "Д" || input?.ToUpper() == "Y")
            //{
            //    return;
            //}

            for (int i = 1; i < 13; i++)
                text.DrawLore(i);

            MsgContinue();
            Console.ReadKey();
        }

        //public void Lore_En()
        //{
        //    Console.Clear();
        //    Console.Write("    Skip lore? [Y/N]: ");
        //    string input = Input();
        //    if (input.ToUpper() == "Y")
        //    {
        //        return;
        //    }

        //    for (int i = 1; i < 13; i++)
        //        text.DrawLore_En(i);
        //}

        // Display restart screen method.
        public void RestartScreen()
        {
            if (Game.languageFlag)
                Console.Write("\n    Would you like to restart the game? [Y/N]: ");
            else
                Console.Write("\n    Желаете начать игру заново? [Y/N] (Д/Н): ");
        }

        // Display exit screen method.
        public void ExitScreen()
        {
            if (Game.languageFlag)
                Console.WriteLine("\n    Exit game . . .\n");
            else
                Console.WriteLine("\n    Выход из игры . . .\n");
        }

        // Loading screen imitation method.
        public void Loading()
        {
            Console.Clear();
            Console.Write("    L O A D I N G ");
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(75);
                Console.Write(". ");
            }
            Console.Clear();
        }


        // Display location name method.
        public void LocationName(string locationName)
        {
            string path = @"txt/locations.txt";  // path to file, from which read location name symbol by symbol
            IEnumerable<string> strings;    // IEnumerable collection for string reading
            switch (locationName)
            {
                case "Cave":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(0).Take(11);    // read file (skip - from which string start, take - how many string read)
                    foreach (string s in strings)   // display location name on screen symbol by symbol
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    break;
                case "Village":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(14).Take(13);
                    foreach (string s in strings)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    break;

                case "Market":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(28).Take(11);
                    foreach (string s in strings)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    break;

                case "Crossroad":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(40).Take(11);
                    foreach (string s in strings)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    break;

                case "Blacksmith":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(52).Take(11);
                    foreach (string s in strings)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    break;

                case "Doctor":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(64).Take(11);
                    foreach (string s in strings)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    break;

                case "BattleZone":
                    Console.WriteLine();
                    strings = File.ReadLines(path).Skip(76).Take(11);
                    foreach (string s in strings)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine();
                    break;

                default:
                    break;
            }
            
        }

        // Display next level screen method.
        public void NextLevel()
        {
            Console.WriteLine("Вы перешли на следующий уровень. Нажмите любую клавишу для продолжения . . .\n");
            Console.ReadKey();
        }

        // Display save screen method.
        public void SaveScreen() => Console.Write("   Вы хотите сохранить игру? [Д/Н]: ");

        // Display loadig screen method.
        public void LoadScreen() => Console.Write("   Вы хотите загрузить игру? [Д/Н]: ");

        // Display end of demo method.
        public void EndDemo()
        {
            if (Game.languageFlag)
                Console.WriteLine("    End of game demo. Press any key to continue . . .");
            else
                Console.WriteLine("    Конец демонстрационной версии игры. Нажмите любую клавишу для продолжения . . .");
        }

        // Dislay inventory "cape" method.
        public void DrawInventory()
        {
            Console.WriteLine();
            Console.WriteLine("    [========] [========] [========] [========] [========]");
            Console.Write("    [========] [====]  ");
            Console.ForegroundColor = ConsoleColor.Blue;
            if (Game.languageFlag)
                Console.Write("I N V E N T O R Y");
            else
                Console.Write("И Н В Е Н Т А Р Ь");
            Console.ResetColor();
            Console.WriteLine(" [====] [========]");
            Console.WriteLine("    [========] [========] [========] [========] [========]");
            
            string path = @"txt/avatar.txt";    // path to file, from which read avatar symbol by symbol
            IEnumerable<string> strings;        // IEnumerable collection for string reading            
            Console.WriteLine();
            strings = File.ReadLines(path).Skip(1).Take(24);    // read file (skip - from which string start, take - how many string read)
            foreach (string s in strings)   // display location name on screen symbol by symbol
            {
                Console.WriteLine(s);
            }
        }

        // Display occupied space in inventory method.
        public void DrawInventoryCapacity(int invCapacity, int takenSpace)
        {
            if (Game.languageFlag)
                Console.Write("    Slots full: ");
            else
                Console.Write("    Слотов заполнено: ");
            if (takenSpace == 0)                                    // if inventory is empty
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{takenSpace}/{invCapacity}");
                Console.ResetColor();
            }

            if ((takenSpace > 0) && (takenSpace < invCapacity))     // if inventory is empty, but not full
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{takenSpace}/{invCapacity}");
                Console.ResetColor();
            }

            if (takenSpace == invCapacity)                          // if inventory is full
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{takenSpace}/{invCapacity}");
                Console.ResetColor();
            }
        }

        // Display empty inventory message.
        public void DrawInventoryIsEmpty()
        {
            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            if (Game.languageFlag)
                Console.WriteLine("    Inventory empty");
            else
                Console.WriteLine("    Инвентарь пуст!");
            Console.ResetColor();

            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }

        // Display full inventory message.
        public void DrawInventoryIsFull()
        {
            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            if (Game.languageFlag)
                Console.WriteLine("    Inventory is full!");
            else
                Console.WriteLine("    Инвентарь заполнен!");
            Console.ResetColor();

            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }

        // Display money of Player is out of stock method.
        public void DrawMoneyIsOut()
        {
            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            if (Game.languageFlag)
                Console.WriteLine("    You've run out of money. You can't buy anything else!");
            else
                Console.WriteLine("    У Вас закончились деньги. Вы не можете больше ничего купить!");
            Console.ResetColor();

            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }

        // Display money of Player is not enough for purchase.
        public void DrawMoneyIsNotEnough()
        {
            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            if (Game.languageFlag)
                Console.WriteLine("    You cannot buy this product - not enough money.");
            else
                Console.WriteLine("    Вы не можете купить этот товар - недостаточно денег.");
            Console.ResetColor();

            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }

        // Display product is out of stock in shop method.
        public void DrawItemIsOut()
        {
            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("X");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (Game.languageFlag)
                Console.WriteLine("    The item is out of stock!");
            else
                Console.WriteLine("    Товар отсутствует!");
            Console.ResetColor();

            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("X");
            }
            Console.WriteLine();
        }
    }
}
