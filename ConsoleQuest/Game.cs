using GameScreen;
using PlayerSpace;
using EnemySpace;
using ConsoleQuest;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace GameSpace
{
    // Class Game for game logic.
    public class Game
    {
        static Screen display = new Screen();   // Screen class object, which show text and system messages on screen
        static Player player = new Player();    // Player class object, which interact with Player class object
        static Enemy enemy = new Enemy();       // Enemy class object, which interact with Enemy class object
        static Text text = new Text();          // Text class object, which display game text on screen

        static string? input;                       // class field for input text from keyboard
        // static string? itemName;                    // class field for interact with inventory

        public static bool exit = false;            // global class field for exit from infinite while cycle (game over)
        static string savePoint;                    // class field for contain current location name (current method name)
        public static bool languageFlag = false;    // language flag for choosing english language in game
        // Method for show start screen.
        //public static void NewGame()
        //{
        //    Console.Clear();
        //    while (true)
        //    {
        //        display.GreetingScreen();    // call greeting window

        //        input = display.Input();
        //        if (input.ToUpper() == "Д" || input.ToUpper() == "Y")
        //        {
        //            display.Lore();         // call method, which show game story
        //            break;
        //        }
        //        else if (input.ToUpper() == "Н" || input.ToUpper() == "N")
        //        {
        //            display.ExitScreen();    // call game exit screen
        //            exit = true;
        //            return;                  // exit from Main method - end of program
        //        }
        //        else
        //        {
        //            Console.WriteLine("\n   Выберите корректное действие!\n"); // instruction will be work infinite, while player will not enter correct symbol
        //        }
        //    }
        //}

        // Game start method.
        //public static void StartGame() => Cave();

        public static void StartGame()
        {
            //Console.Clear();
            display.GreetingScreen();   // display main screen and logo
            display.Localization();
            string input = display.Input();
            if (input == "1")
            {
                languageFlag = true;
                display.Control_En();
            }
            else
                display.Control();          // display game control

            display.Loading();          // display loading process

            ShowLore();                 // display game lore

            Audio.PlayMusic();          // play music
            if (languageFlag)
            {
                traderFood = traderFood_En;
                traderWeapons = traderWeapons_En;
                traderArmor = traderArmor_En;
                traderPotions = traderPotions_En;
            }
            Cave();                     // start from first location
        }
        
        public static void ShowLore()
        {
            Audio.PlayMusic();
            display.Lore();
        }
        // Game restart method + exit from game method.
        public static void RestartGame()
        {
            while (true)
            {
                display.RestartScreen();                                                // call restart game sreen

                input = display.Input();
                if (input.ToUpper() == "Д" || input.ToUpper() == "Y")      // if player want to start game again, then call StartGame method
                {
                    Array.Clear(player.Inventory);                                       // clear inventory after restart (array cleaning)
                    //StartGame();

                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);  // start new system process (program restart)
                    Environment.Exit(0);                                                            // kill current work process
                }

                else if (input.ToUpper() == "Н" || input.ToUpper() == "N")  // else game is end
                {
                    display.ExitScreen();
                    exit = true;
                    Environment.Exit(0);
                }

                else
                {
                    display.MsgAlert();
                }
            }
        }

        // Starter location choice (for debug).
        public static void ChooseLocation()
        {
            while (true)
            {
                Console.WriteLine("1. Cave.\n" +
                                  "2. Village.\n" +
                                  "3. Market.\n" +
                                  "4. Crossroad.\n" +
                                  "5. Blacksmith.\n" +
                                  "6. Doctor.");

                Console.Write("    > ");

                input = display.Input();
                switch (input)
                {
                    case "1":
                        Cave();
                        break;
                    case "2":
                        Village();
                        break;
                    case "3":
                        Market();
                        break;
                    case "4":
                        Crossroad();
                        break;
                    case "5":
                        Blacksmith();
                        break;
                    case "6":
                        Doctor();
                        break;
                    default:
                        Console.WriteLine("\n   Выберите корректное действие!\n");
                        break;
                }
            }
        }

        // Test location.
        public static void TestLoc()
        {
            Console.SetCursorPosition(0, Console.BufferHeight-1);
            Console.WriteLine(Console.BufferWidth + " " + Console.BufferHeight);
            
            Console.ReadKey();
            Console.Clear();
        }

        // Battle area.
        static void BattleZone([CallerMemberName] string? callerMemberName = null)  // callerMemberName is using for saving method name, which called BattleZone method
        {
            string prevLoc = callerMemberName;  // save location name, which started battle
            Audio.PlayMusic(); // play music
            while (true)
            {
                Console.Clear();
                display.LocationName("BattleZone");
                bool battleFlag = false;    // flag for end of method after end of battle
                if (languageFlag)
                {
                    Console.WriteLine($"    In front of you is an enemy - {enemy.Name}\n");
                    Console.WriteLine("    1. Start a fight.\n" +
                                      "    2. Escape.\n" +
                                      "    3. Show inventory.");
                }
                else
                {
                    Console.WriteLine($"    Перед вами враг - {enemy.Name}\n");
                    Console.WriteLine("    1. Начать бой.\n" +
                                      "    2. Сбежать.\n" +
                                      "    3. Показать инвентарь.");
                }
                display.Cursor();

                input = display.Input();
                if (input == "1")
                {
                    while (true)
                    {
                        player.Battle(out battleFlag);

                        ConsoleKeyInfo keypress;
                        do
                        {
                            keypress = Console.ReadKey();
                            if (keypress.KeyChar != 13)
                                display.MsgContinue();
                        } while (keypress.KeyChar != 13);

                        if (battleFlag)
                        { 
                            if (player.HP == 0)
                            {
                                Audio.Stop(prevLoc);   // stop battle music and start music from last location
                                RestartGame();
                            }
                            Audio.Stop(prevLoc);
                            Console.Clear();
                            return;
                        }
                    }
                }
                
                if (input == "2")
                {
                    display.MsgBattleEscape();
                    Console.ReadKey();
                    Audio.Stop(prevLoc);
                    Console.Clear();
                    exit = true;
                    return;
                }

                if (input == "3")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    display.MsgInvExit();
                    Console.ReadKey();
                }

                //else
                //{
                //    Console.WriteLine("\n    Выберите корректное действие!\n");
                //    Console.ReadKey();
                //}
            }
        }

        // Game instructions in location "Пещера Петит-Равин".
        public static void Cave()
        {

            // display.Loading();      // call loading screen
            while (true)
            {
                Console.Clear();
                //if (SpawnEnemy())
                //    BattleZone();
                display.LocationName("Cave");
                text.DrawText("Cave");

                display.Cursor();

                input = display.Input();    // action choice through if conditions and while cycles
                if (input == "1")
                {
                    display.MsgCaveEnter();
                    display.EndDemo();      // call screen with end of demo text
                    Console.ReadKey();
                    //display.LoadScreen();
                    //input = display.Input();
                    //if (input.ToUpper() == "Д" || input.ToUpper() == "Y")
                    //    LoadGame();

                    exit = true;            // exit from infinite cycle
                    return;
                }

                else if (input == "2")
                {
                    Village();
                }

                else if (input == "3")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    display.MsgInvExit();
                    Console.ReadKey();
                }

                //else
                //{
                //    Console.WriteLine("\n    Выберите корректное действие!\n");
                //    Console.ReadKey();
                //}
            }
        }

        // Game instructions in location "Деревня Кватре Котес".
        public static void Village()
        {
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("Village");
                text.DrawText("Village");

                Console.Write("    > ");

                input = display.Input();
                if (input == "1")
                {
                    Crossroad();
                }

                else if(input == "2")
                {
                    Market();
                }

                else if (input == "3")
                {
                    return;
                }

                else if (input == "4")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    display.MsgInvExit();
                    Console.ReadKey();
                }

                //else
                //{
                //    Console.WriteLine("\n    Выберите корректное действие!\n");
                //}
            }
        }

        // Game instructions in location "Рынок".
        public static void Market()
        {
            Audio.PlayMusic();
            // display.Loading();
            while (true)
            {
                // display.SaveScreen();
                //input = display.Input();
                //if (input.ToUpper() == "Д" || input.ToUpper() == "Y")
                //{
                //    SaveGame();
                //    Console.WriteLine(savePoint);
                //}
                Console.Clear();
                if (SpawnEnemy())   // if enemy spawns on location,
                    BattleZone();   // then start battle
                display.LocationName("Market");
                text.DrawText("Market");

                display.Cursor();

                input = display.Input();
                if (input == "1")
                {
                    bool flag = true;   // flag for exit from shop (when pressed "Q" key, end of infinite cycle)

                    while (flag)
                    {
                        display.MsgBuyMenu();
                        ShowTraderInventory(traderFood, traderFoodItemsCount, traderFoodPrices);
                        display.MsgShopExit();

                        display.Cursor();

                        input = display.Input();
                        while (true)
                        {

                            if (input == "1")
                            {
                                //Console.WriteLine($"\nВы купили {traderFood[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(player.Inventory, traderFood, player.ItemsCount, traderFoodPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderFood, traderFoodItemsCount, traderFoodPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderFood, player.ItemsCount, traderFoodPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderFood, traderFoodItemsCount, traderFoodPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderFood, player.ItemsCount, traderFoodPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderFood, traderFoodItemsCount, traderFoodPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input.ToUpper() == "Q")
                            {
                                flag = false;   // exit from shop (end of infinite cycle)
                                break;
                            }

                            else
                            {
                                //Console.WriteLine("\n    Выберите корректное действие!\n");
                                break;
                            }
                        }
                    }
                }

                // This block of code is used to return the hero to the previous step of the game (in this case, to the crossroad in the street). Thus, the mechanics of non-linear passage is implemented,
                // so that the player can enter and exit one place, returning to the previous place.

                else if (input == "2")
                {
                    return;
                }

                else if (input == "3")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    display.MsgInvExit();
                    Console.ReadKey();
                }

                //else
                //{
                //    Console.WriteLine("\n    Выберите корректное действие!\n");
                //}
            }
        }

        // Game instructions in location "Дорожная развилка".
        public static void Crossroad()
        {
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("Crossroad");
                text.DrawText("Crossroad");

                display.Cursor();

                input = display.Input();
                if (input == "1")
                {
                    Blacksmith();
                }

                else if (input == "2")
                {
                    Doctor();
                }

                else if (input == "3")
                {
                    return;
                }

                else if (input == "4")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    display.MsgInvExit();
                    Console.ReadKey();
                }

                //else
                //{
                //    //Console.WriteLine("\n    Выберите корректное действие!\n");
                //}
            }
        }

        // Game instructions in location "Кузнец Леонард".
        public static void Blacksmith()
        {
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("Blacksmith");
                text.DrawText("Blacksmith");

                display.Cursor();

                input = display.Input();
                if (input == "1")
                {
                    bool flag = true;
                    while (flag)
                    {
                        display.MsgBuyMenu();
                        ShowTraderInventory(traderWeapons, traderWeaponsItemsCount, traderWeaponsPrices);
                        display.MsgShopExit();

                        display.Cursor();

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                AddToInventory(player.Inventory, traderWeapons, player.ItemsCount, traderWeaponsPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderWeapons, traderWeaponsItemsCount, traderWeaponsPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderWeapons, player.ItemsCount, traderWeaponsPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderWeapons, traderWeaponsItemsCount, traderWeaponsPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderWeapons, player.ItemsCount, traderWeaponsPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderWeapons, traderWeaponsItemsCount, traderWeaponsPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input.ToUpper() == "Q")
                            {
                                flag = false;
                                break;
                            }

                            else
                            {
                                //Console.WriteLine("\n    Выберите корректное действие!\n");
                                break;
                            }
                        }
                    }
                }

                else if (input == "2")
                {
                    bool flag = true;
                    while (flag)
                    {
                        display.MsgBuyMenu();
                        ShowTraderInventory(traderArmor, traderArmorItemsCount, traderArmorPrices);
                        display.MsgShopExit();

                        display.Cursor();

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                AddToInventory(player.Inventory, traderArmor, player.ItemsCount, traderArmorPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderArmor, traderArmorItemsCount, traderArmorPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderArmor, player.ItemsCount, traderArmorPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderArmor, traderArmorItemsCount, traderArmorPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderArmor, player.ItemsCount, traderArmorPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderArmor, traderArmorItemsCount, traderArmorPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input.ToUpper() == "Q")
                            {
                                flag = false;
                                break;
                            }

                            else
                            {
                                //Console.WriteLine("\n    Выберите корректное действие!\n");
                                break;
                            }
                        }
                    }
                }

                else if (input == "3")
                {
                    if (languageFlag)
                    {
                        Console.WriteLine("\n    Leonard told you the rumors that have enveloped the cave.");
                        Console.WriteLine("    Press any button to continue . . .");
                    }
                    else
                    {
                        Console.WriteLine("\n    Леонард рассказал Вам слухи, которые окутали пещеру.");
                        Console.WriteLine("    Нажмите любую кнопку для продолжения . . .");
                    }
                    Console.ReadKey();
                }

                else if (input == "4")
                {
                    return;
                }

                else if (input == "5")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    display.MsgInvExit();
                    Console.ReadKey();
                }

                //else
                //{
                //    Console.WriteLine("\n    Выберите корректное действие!\n");
                //}
            }
        }

        // Game instructions in location "Дом лекаря".
        public static void Doctor()
        {
            Audio.PlayMusic();
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("Doctor");
                text.DrawText("Doctor");

                display.Cursor();

                input = display.Input();
                if (input == "1")
                {
                    if (languageFlag)
                    {
                        Console.WriteLine("\n    You have healed all wounds.");
                        Console.WriteLine("    Press any button to continue . . .");
                    }
                    else
                    {
                        Console.WriteLine("\n    Вы излечили все ранения.");
                        Console.WriteLine("    Нажмите любую кнопку для продолжения . . .");
                    }
                    
                    Console.ReadKey();
                }

                else if (input == "2")
                {
                    bool flag = true;
                    while (flag)
                    {
                        display.MsgBuyMenu();
                        ShowTraderInventory(traderPotions, traderPotionsItemsCount, traderPotionsPrices);
                        display.MsgInvExit();

                        display.Cursor();

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                AddToInventory(player.Inventory, traderPotions, player.ItemsCount, traderPotionsPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderPotions, traderPotionsItemsCount, traderPotionsPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderPotions, player.ItemsCount, traderPotionsPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderPotions, traderPotionsItemsCount, traderPotionsPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderPotions, player.ItemsCount, traderPotionsPrices, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderPotions, traderPotionsItemsCount, traderPotionsPrices, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input.ToUpper() == "Q")
                            {
                                flag = false;
                                break;
                            }

                            else
                            {
                                //Console.WriteLine("\n   Выберите корректное действие!\n");
                                break;
                            }
                        }
                    }
                }

                else if (input == "3")
                {
                    if (languageFlag)
                    {
                        Console.WriteLine("\n    The healer has told you everything he knows about the cave.");
                        Console.WriteLine("    Press any button to continue . . .");
                    }
                    else
                    {
                        Console.WriteLine("\n    Лекарь рассказал Вам всё, что знает о пещере.");
                        Console.WriteLine("    Нажмите любую кнопку для продолжения . . .");
                    }
                    Console.ReadKey();
                }

                else if (input == "4")
                {
                    return;
                }

                else if (input == "5")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    display.MsgInvExit();
                    Console.ReadKey();
                }

                //else
                //{
                //    Console.WriteLine("\n   Выберите корректное действие!\n");
                //}
            }
        }

        // Arrays - lists of products each trader.
        static string[] traderFood = { "Хлеб", "Кувшин молока", "Жареная говядина" };
        static string[] traderFood_En = { "Bread", "Pitcher of Milk", "Roast Beef" };
        static int[] traderFoodItemsCount = { 3, 4, 5 };
        static int[] traderFoodPrices = { 10, 5, 20 };

        static string[] traderWeapons = { "Железный меч", "Железный топор", "Железный кинжал" };
        static string[] traderWeapons_En = { "Iron Sword", "Iron Axe", "Iron Dagger" };
        static int[] traderWeaponsItemsCount = { 4, 6, 7 };
        static int[] traderWeaponsPrices = { 30, 40, 20 };

        static string[] traderArmor = { "Железный шлем", "Железный нагрудник", "Железный щит" };
        static string[] traderArmor_En = { "Iron Helmet", "Iron Chestplate", "Iron Shield" };
        static int[] traderArmorItemsCount = { 8, 9, 10 };
        static int[] traderArmorPrices = { 20, 40, 15 };

        //static string[] formatTraderFoodInv = FormattingTraderInventory(traderFood);

        static string[] traderPotions = { "Малое зелье лечения", "Зелье лечения", "Большое зелье лечения" };
        static string[] traderPotions_En = { "Small Healing Potion", "Healing Potion", "Big Healing Potion" };
        static int[] traderPotionsItemsCount = { 11, 12, 13 };
        static int[] traderPotionsPrices = { 10, 20, 30 };

        // Display trader inventory method.
        static void ShowTraderInventory(string[] traderInventory, int[] traderItemsCount, int[] traderItemsPrices)
        {
            Console.WriteLine();
            player.DrawMoney();
            for (int i = 0, j = 1; i < traderInventory.Length; i++, j++)
            {
                if (traderItemsCount[i] <= 0) {
                    if (traderInventory[0] is null)
                    {
                        display.MsgTraderItemsIsOut();
                        return;
                    }

                    if (traderInventory[i] is null)
                        return;
                }

                Console.Write($"    {j}. {traderInventory[i]} - x{traderItemsCount[i]} ");
                if (languageFlag)
                    Console.Write("pieces. Price: ");
                else
                    Console.Write("шт. Цена: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(traderItemsPrices[i]);
                Console.ResetColor();
                if (languageFlag)
                    Console.WriteLine(" coins.");
                else
                    Console.WriteLine(" монет.");
            }
        }

        // Delete purchased product from trader inventory method.
        static void DeleteFromTraderInventory(string[] traderInventory, int[] traderItemsCount, int[] traderItemsPrices, int choice)
        {
            // If player inventory is full, then method running ends (count of items in trader inventory is not decreases).
            if (fullFlag)
                return;

            int j = choice;
            if (player.Money < traderItemsPrices[j])
                return;
            player.DecreaseMoney(traderItemsPrices[j]); // decrease money from price
            traderItemsCount[j]--;
            

            if (traderItemsCount[j] <= 0)                           // if product if out of stock
            {
                for (int i = choice; i < traderInventory.Length; i++)
                {
                    if (i >= traderInventory.Length - 1)            // if position was last in list, then
                    {
                        
                        traderInventory[i] = null;                      // delete position,
                        traderItemsCount[i] = 0;                        // count resets to zero,
                        traderItemsPrices[i] = 0;                       // delete position price
                        if (traderItemsCount[i] == 0) {
                            break;                                      // exit from cycle
                        }
                    }

                    traderInventory[i] = traderInventory[++i];      // product name set to one position up
                    --i;


                    traderItemsCount[i] = traderItemsCount[++i];    // item count set to one position up
                    --i;

                    traderItemsPrices[i] = traderItemsPrices[++i];
                    --i;

                    if (i >= traderItemsCount.Length - 1)
                    {
                        break;
                    }
                }
            }
        }

        // flag for marking overflow player inventory.
        static bool fullFlag = false;

        // Adding item in inventory method.
        static void AddToInventory(string[] plrInventory, string[] traderInventory, int[] plrItemsCount, int[] traderItemsPrices, int choice)
        {
            // Inventory capacity.
            int capacity = player.ItemsCount.Length;

            // Total items in player inventory.
            int sum = 0;
            for (int i = 0; i < player.ItemsCount.Length; i++)
            {
                sum += player.ItemsCount[i];
            }

            int j = choice;

            if (traderInventory[j] is null)               // if product is out of stock in shop,
                                                          // then message shows and method breaks
            {
                display.DrawItemIsOut();
                return;
            }

            if (sum >= capacity)                         // if inventory is full,
            {
                display.DrawInventoryIsFull();              // then show overflow message
                fullFlag = true;
                return;
            }

            if (traderInventory[j] is not null)            // if product in stock, then 
            {
                if ((player.Money > 0) & (player.Money >= traderItemsPrices[j]))  // if player money > 0, then purchase product
                {
                    if (languageFlag)
                        Console.Write("\n    You bought ");
                    else
                        Console.Write("\n    Вы купили ");
                    Console.WriteLine($"{traderInventory[j]}.");
                    Console.WriteLine("    ===============================================");
                }
                else if ((player.Money > 0) & (player.Money < traderItemsPrices[j]))    // else if player money > 0 but < price, then exit from purchase
                {
                    display.DrawMoneyIsNotEnough();
                    return;
                }
                else // else exit from purchase
                {
                    display.DrawMoneyIsOut();
                    return;
                }
            } 

            for (int i = 0; i < plrInventory.Length; i++)
            {
                if (plrInventory[i] is null)                // if inventory place is free,
                {
                    plrInventory[i] = traderInventory[j];       // then assigned item name,
                    plrItemsCount[i]++;                         // count increases by 1

                    return;
                }

                if (plrInventory[i] == traderInventory[j])  // if such an item is already in inventory,
                {
                    plrItemsCount[i]++;                         // only item count increases by 1
                    return;
                }
            }

            Console.WriteLine();
        }

        // Random spawn enemy method.
        static public bool SpawnEnemy()
        {
            int dice = 0;
            Random rnd = new();
            dice = rnd.Next(0, 3);
            if (dice == 1)
            {
                return true;
            }

            return false;
        }

        //// Game saving method (!!! EDIT !!!).
        static void SaveGame([CallerMemberName] string? callerMemberName = null)
        {
            savePoint = callerMemberName;
        }

        // Game loading method (!!! EDIT !!!).
        static void LoadGame()
        {
            Game currentLocation = new Game();
            MethodInfo m = currentLocation.GetType().GetMethod(savePoint);
            m.Invoke(currentLocation, null);
        }
    }
}
