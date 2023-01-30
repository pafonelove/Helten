using GameScreen;
using PlayerSpace;
using EnemySpace;

namespace GameSpace
{
    // Класс Game содержит логику игры.
    public class Game
    {
        static Screen display = new();              // экземпляр класса Screen, отвечающий за отображение текста и системных сообщений на экране
        static Player player = new();
        static Enemy enemy = new();

        static string? input;                       // поле класса для ввода текста с клавиатуры
        // static string? itemName;                    // поле класса для взаимодействия с инвентарем

        public static bool exit = false;            // глобальное поле класса для выхода из бесконечного цикла while (завершение игры)
        // static string savePoint;                    // поле класса, сохраняющее в себе название текущей локации (имя текущего метода)

        // Метод отображения стартового экрана.
        public static void NewGame()
        {
            while (true)
            {
                display.GreetingScreen();    // вызов окна приглашения в игру

                input = display.Input();
                if (input == "Д" || input == "д" || input == "Y" || input == "y")
                {
                    display.Story();         // вызов метода отображения предыстории (сюжета)
                    break;
                }
                else if (input == "Н" || input == "н" || input == "N" || input == "n")
                {
                    display.ExitScreen();    // вызов экрана выхода из игры
                    exit = true;
                    return;                  // выход из метода Main - конец программы
                }
                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n"); // конструкция будет срабатывать бесконечно, пока игрок не введет корректный символ
                }
            }
        }


        // Метод запуска игры.
        //public static void StartGame() => Dungeon();

        public static void StartGame() => Dungeon();

        // Метод для выбора повторной игры или закрытия программы.
        public static void RestartGame()
        {
            while (true)
            {
                display.RestartScreen();                                                // вызов окна приглашения в повторную игру

                input = display.Input();
                if (input == "Д" || input == "д" || input == "Y" || input == "y")       // если пользователь согласился сыграть еще раз, то вызывается метод StartGame, отвечающий за запуск игры
                {
                    Array.Clear(player.Inventory);                                       // обнуление инвентаря после перезапуска (очищение массива от элементов)
                    StartGame();
                }

                else if (input == "Н" || input == "н" || input == "N" || input == "n")  // иначе игра завершается
                {
                    display.ExitScreen();
                    exit = true;
                    return;
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Метод выбора стартовой локации (подобие чита или инструмент для облегчения отладки программы).
        public static void ChooseLocation()
        {
            while (true)
            {
                Console.WriteLine("1. Dungeon.\n" +
                                  "2. Village.\n" +
                                  "3. Market.\n" +
                                  "4. Road.\n" +
                                  "5. BlacksmithHouse.\n" +
                                  "6. DoctorHouse.");

                Console.Write("> ");

                input = display.Input();
                switch (input)
                {
                    case "1":
                        Dungeon();
                        break;
                    case "2":
                        Village();
                        break;
                    case "3":
                        Market();
                        break;
                    case "4":
                        Road();
                        break;
                    case "5":
                        BlacksmithHouse();
                        break;
                    case "6":
                        DoctorHouse();
                        break;
                    default:
                        Console.WriteLine("\nВыберите корректное действие!\n");
                        break;
                }
            }
        }

        //Тестовая локация.
        public static void TestLoc()
        {
            Console.SetCursorPosition(0, Console.BufferHeight-1);
            Console.WriteLine(Console.BufferWidth + " " + Console.BufferHeight);
            
            Console.ReadKey();
            Console.Clear();
        }

        // Арена битвы.
        static void BattleZone()
        {
            while (true)
            {
                Console.Clear();
                display.LocationName("BattleZone");
                bool battleFlag = false;
                Console.WriteLine($"Перед вами враг - {enemy.Name}");
                Console.WriteLine("1. Начать бой.\n" +
                                  "2. Сбежать.\n" +
                                  "3. Показать инвентарь.");

                Console.Write("> ");

                input = display.Input();
                if (input == "1")
                {
                    while (true)
                    {
                        player.Battle(out battleFlag);
                        Console.ReadKey();
                        if (battleFlag)
                        { 
                            Console.Clear();
                            return;
                        }
                    }
                }

                else if (input == "2")
                {
                    Console.WriteLine("\nВы сбежали с поля боя и остались живы.");
                    Console.ReadKey();
                    Console.Clear();
                    exit = true;
                    return;
                }

                else if (input == "3")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Описанме инструкций игры в локации "Пещера Петит-Равин".
        public static void Dungeon()
        {

            // display.Loading();      // вызов загрузочного экрана
            while (true)
            {
                Console.Clear();
                if (SpawnEnemy())
                    BattleZone();
                display.LocationName("Dungeon");
                Console.WriteLine("Вы стоите перед входом в пещеру Петит-Равин. Слева и справа тупик, сзади – дорога в деревню Шик-Шагок, в которой есть местный лекарь, торговцы оружием и провизией и кузнец.\n" +
                                  "1. Отправиться в пещеру.\n" +
                                  "2. Развернуться и пойти в деревню.\n" +
                                  "3. Показать инвентарь.");

                Console.Write("> ");

                input = display.Input();    // механика выбора действий через вложенные конструкции if и вложенные циклы while
                if (input == "1")
                {
                    Console.WriteLine("\nВы отправились в пещеру . . .\n");
                    display.EndDemo();      // вызов экрана с текстом об окончании демоверсии
                    //display.LoadScreen();
                    //input = display.Input();
                    //if (input == "Д" || input == "д" || input == "Y" || input == "y")
                    //    // LoadGame();

                    exit = true;            // выход из бесконечного цикла
                    return;
                }

                else if (input == "2")
                {
                    Village();
                }

                else if (input == "3")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Описанме инструкций игры в локации "Деревня Кватре Котес".
        public static void Village()
        {
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("Village");
                Console.WriteLine("Вы вошли в деревню Кватре Котес. Поселение выглядит ухоженным, дома и постройки выполнены в классическом стиле, присущем средневековым колонистам-французам.\n" +
                                  "Прямо по дороге видно кузню и дом лекаря. Слева от ворот в деревню находится рынок.\n" +
                                  "1. Пойти прямо по дороге.\n" +
                                  "2. Свернуть налево в сторону рынка.\n" +
                                  "3. Выйти за ворота деревни и отправиться к пещере.\n" +
                                  "4. Показать инвентарь.");
                Console.Write("> ");

                input = display.Input();
                if (input == "1")
                {
                    Road();
                }

                else if (input == "2")
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
                    Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Описанме инструкций игры в локации "Рынок".
        public static void Market()
        {
            // display.Loading();
            while (true)
            {
                // display.SaveScreen();
                //input = display.Input();
                //if (input == "Д" || input == "д" || input == "Y" || input == "y")
                //{
                //    SaveGame();
                //    Console.WriteLine(savePoint);
                //}
                Console.Clear();
                if (SpawnEnemy())
                    BattleZone();
                display.LocationName("Market");
                Console.WriteLine("Следуя за незнакомой женщиной преклонного возраста, Вы вышли на сельскую площадь, на которой располагалась местная ярмарка.\n" +
                                  "1. Купить провизию.\n" +
                                  "2. Вернуться к воротам деревни.\n" +
                                  "3. Показать инвентарь.");
                Console.Write("> ");

                input = display.Input();
                if (input == "1")
                {
                    bool flag = true;   // переменная-флаг для произвольного выхода из магазина (при нажатии на "Q"
                                        // прекращается выполнение бесконечного цикла)
                    while (flag)
                    {
                        Console.WriteLine("\nЧто вы хотите купить из еды?");
                        ShowTraderInventory(traderFood, traderFoodItemsCount);
                        Console.WriteLine("\nQ - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {

                            if (input == "1")
                            {
                                //Console.WriteLine($"\nВы купили {traderFood[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(player.Inventory, traderFood, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderFood, traderFoodItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderFood, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderFood, traderFoodItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderFood, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderFood, traderFoodItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                flag = false;   // выход из магазина (прерывание бесконечного цикла)
                                break;
                            }

                            else
                            {
                                Console.WriteLine("\nВыберите корректное действие!\n");
                                break;
                            }
                        }
                    }
                }

                // Данный блок кода служит для возвращения героя на предыдущий шаг игры (в данном случае - на развилку на улице). Таким образом реализована механика нелинейного прохождения,
                // чтобы игрок мог входить в одно место и выходить из него, возвращаясь в предыдущее место.

                else if (input == "2")
                {
                    return;
                }

                else if (input == "3")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Описанме инструкций игры в локации "Дорожная развилка".
        public static void Road()
        {
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("Road");
                Console.WriteLine("Вы находитесь на дорожной развилке рядом с указателем: «Дом кузнеца <==== || ====> Дом лекаря».\n" +
                                  "1. Войти в дом кузнеца.\n" +
                                  "2. Войти в дом лекаря.\n" +
                                  "3. Вернуться к воротам деревни.\n" +
                                  "4. Показать инвентарь.");

                Console.Write("> ");

                input = display.Input();
                if (input == "1")
                {
                    BlacksmithHouse();
                }

                else if (input == "2")
                {
                    DoctorHouse();
                }

                else if (input == "3")
                {
                    return;
                }

                else if (input == "4")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Описанме инструкций игры в локации "Кузнец Леонард".
        public static void BlacksmithHouse()
        {
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("BlacksmithHouse");
                Console.WriteLine("Внутри Вас встретил местный кузнец по имени Леонард - взрослый, крепкий и высокий мужчина с длинной угольной бородой, - держащий в правой руке \n" +
                                    "молот и в левой - лезвие для заготовки клинка.\n" +
                                    "- Добро пожаловать в мои скромные владения, хе-хе, - проговорил басовитым голосом кузнец. - Чего желаете?\n" +
                                  "1. Купить оружие.\n" +
                                  "2. Купить броню.\n" +
                                  "3. Получить информацию о пещере Петит-Равин.\n" +
                                  "4. Выйти на улицу.\n" +
                                  "5. Показать инвентарь.");

                Console.Write("> ");

                input = display.Input();
                if (input == "1")
                {
                    bool flag = true;
                    while (flag)
                    {
                        Console.WriteLine("\nЧто вы хотите купить из оружия?");
                        ShowTraderInventory(traderWeapons, traderWeaponsItemsCount);
                        Console.WriteLine("\nQ - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                AddToInventory(player.Inventory, traderWeapons, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderWeapons, traderWeaponsItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderWeapons, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderWeapons, traderWeaponsItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderWeapons, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderWeapons, traderWeaponsItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                flag = false;
                                break;
                            }

                            else
                            {
                                Console.WriteLine("\nВыберите корректное действие!\n");
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
                        Console.WriteLine("\nЧто вы хотите купить из брони?");
                        ShowTraderInventory(traderArmor, traderArmorItemsCount);
                        Console.WriteLine("\nQ - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                AddToInventory(player.Inventory, traderArmor, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderArmor, traderArmorItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderArmor, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderArmor, traderArmorItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderArmor, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderArmor, traderArmorItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                flag = false;
                                break;
                            }

                            else
                            {
                                Console.WriteLine("\nВыберите корректное действие!\n");
                                break;
                            }
                        }
                    }
                }

                else if (input == "3")
                {
                    Console.WriteLine("\nЛеонард рассказал Вам слухи, которые окутали пещеру.");
                    Console.WriteLine("Нажмите любую кнопку для продолжения . . .");
                    Console.ReadKey();
                }

                else if (input == "4")
                {
                    return;
                }

                else if (input == "5")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Описанме инструкций игры в локации "Дом лекаря".
        public static void DoctorHouse()
        {
            // display.Loading();
            while (true)
            {
                Console.Clear();
                display.LocationName("DoctorHouse");
                Console.WriteLine("В одноэтажном здании с выкрашенными травяными красителями зелеными стенами - что было сделано для создания атмосферы умиротворения и \n" +
                                "полного спокойствия - Вы увидели угловатую деревянную стойку с разными сосудами, за которой на кресле-качалке располагался старец \n" +
                                "преклонного возраста - судя по всему, местный лекарь.\n" +
                                "- Слушаю тебя, путник-незнакомец, - отозвался старый врач. - Хочешь подлечиться или купить лекарства?\n" +
                              "1. Залечить раны.\n" +
                              "2. Купить травяные настойки.\n" +
                              "3. Получить информацию о пещере Петит-Равин.\n" +
                              "4. Выйти на улицу.\n" +
                              "5. Показать инвентарь.");

                Console.Write("> ");

                input = display.Input();
                if (input == "1")
                {
                    Console.WriteLine("\nВы излечили все ранения.");
                    Console.WriteLine("Нажмите любую кнопку для продолжения . . .");
                    Console.ReadKey();
                }

                else if (input == "2")
                {
                    bool flag = true;
                    while (flag)
                    {
                        Console.WriteLine("\nЧто вы хотите купить из травяных настоек?");
                        ShowTraderInventory(traderPotions, traderPotionsItemsCount);
                        Console.WriteLine("\nQ - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                AddToInventory(player.Inventory, traderPotions, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderPotions, traderPotionsItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                AddToInventory(player.Inventory, traderPotions, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderPotions, traderPotionsItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                AddToInventory(player.Inventory, traderPotions, player.ItemsCount, Convert.ToInt32(input) - 1);
                                DeleteFromTraderInventory(traderPotions, traderPotionsItemsCount, Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                flag = false;
                                break;
                            }

                            else
                            {
                                Console.WriteLine("\nВыберите корректное действие!\n");
                                break;
                            }
                        }
                    }
                }

                else if (input == "3")
                {
                    Console.WriteLine("\nЛекарь рассказал Вам всё, что знает о пещере.");
                    Console.WriteLine("Нажмите любую кнопку для продолжения . . .");
                    Console.ReadKey();
                }

                else if (input == "4")
                {
                    return;
                }

                else if (input == "5")
                {
                    player.ShowInventory(player.Inventory, player.ItemsCount);
                    Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        // Массивы - списки товаров у каждого из продавцов.
        static string[] traderFood = { "Хлеб", "Кувшин молока", "Жареная говядина" };
        static int[] traderFoodItemsCount = { 3, 4, 5 };

        static string[] traderWeapons = { "Железный меч", "Железный топор", "Железный кинжал" };
        static int[] traderWeaponsItemsCount = { 4, 6, 7 };

        static string[] traderArmor = { "Железный шлем", "Железный нагрудник", "Железный щит" };
        static int[] traderArmorItemsCount = { 8, 9, 10 };

        //static string[] formatTraderFoodInv = FormattingTraderInventory(traderFood);

        static string[] traderPotions = { "Малое зелье лечения", "Зелье лечения", "Большое зелье лечения" };
        static int[] traderPotionsItemsCount = { 11, 12, 13 };

        // Метод отображения содержимого инвентаря торговца.
        static void ShowTraderInventory(string[] traderInventory, int[] traderItemsCount)
        {
            Console.WriteLine();
            for (int i = 0, j = 1; i < traderInventory.Length; i++, j++)
            {
                if (traderItemsCount[i] <= 0) {
                    if (traderInventory[0] is null)
                    {
                        Console.WriteLine("У торговца закончились товары!");
                        return;
                    }

                    if (traderInventory[i] is null)
                        return;
                }

                Console.WriteLine($"{j}. {traderInventory[i]} - x{traderItemsCount[i]} шт.");
            }
        }

        // Метод удаления купленного товара из инвентаря торговца.
        static void DeleteFromTraderInventory(string[] traderInventory, int[] traderItemsCount, int choice)
        {
            // Если инвентарь игрока переполнен, то выполнение метода прерывается (количество предметов в инвентаре продавца не уменьшается).
            if (fullFlag)
                return;

            int j = choice;
            traderItemsCount[j]--;

            if (traderItemsCount[j] <= 0)                           // если кончился товар, то
            {
                for (int i = choice; i < traderInventory.Length; i++)
                {
                    if (i >= traderInventory.Length - 1)            // если позиция была последней в списке, то
                    {
                        traderInventory[i] = null;                      // позиция удаляется,
                        traderItemsCount[i] = 0;                        // количество обнуляется,
                        if (traderItemsCount[i] == 0) {
                            break;                                      // выход из цикла
                        }
                    }

                    traderInventory[i] = traderInventory[++i];      // название товара смещается на позицию выше 
                    --i;


                    traderItemsCount[i] = traderItemsCount[++i];    // количество товара смещается на позицию выше
                    --i;

                    if (i >= traderItemsCount.Length - 1)
                    {
                        break;
                    }
                }
            }
        }

        // Поле класса - флаг для обозначения переполнения инвентаря игрока.
        static bool fullFlag = false;

        // Метод добавления предмета в инвентарь.
        static void AddToInventory(string[] plrInventory, string[] traderInventory, int[] plrItemsCount, int choice)
        {
            // Вместимость инвентаря.
            int capacity = player.ItemsCount.Length;

            // Общее количество вещей, находящихся в инвентаре игрока.
            int sum = 0;
            for (int i = 0; i < player.ItemsCount.Length; i++)
            {
                sum += player.ItemsCount[i];
            }

            int j = choice;

            if (traderInventory[j] is null)               // если товар в магазине отсутствует,
                                                            // то выводится сообщение и прерывается выполнение метода
            {
                display.DrawItemIsOut();
                return;
            }

            if (sum >= capacity)                         // если сумка переполнена, то
            {
                display.DrawInventoryIsFull();              // сообщение о переполнении
                fullFlag = true;
                return;
            }

            if (traderInventory[j] is not null)            // если товар в магазине в наличии, то происходит покупка
            {
                Console.WriteLine($"\nВы купили {traderInventory[j]}.");
            } 

            for (int i = 0; i < plrInventory.Length; i++)
            {
                if (plrInventory[i] is null)                // если место в сумке свободно, то
                {
                    plrInventory[i] = traderInventory[j];       // присваивается название предмета,
                    plrItemsCount[i]++;                         // увеличивается количестве на 1

                    return;
                }

                if (plrInventory[i] == traderInventory[j])  // если такой предмет уже есть в сумке, то
                {
                    plrItemsCount[i]++;                         // увеличивается только количество на 1
                    return;
                }

                
            }

            Console.WriteLine();
        }

        // Метод случайной генерации врагов.
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

        //// Метод сохранения игры (!!! ДОРАБОТАТЬ !!!).
        //static void SaveGame([CallerMemberName] string? callerMemberName = null)
        //{
        //    savePoint = callerMemberName;
        //}

        //// Метод загрузки игры (!!! ДОРАБОТАТЬ !!!).
        //static void LoadGame()
        //{
        //    Game currentLocation = new Game();
        //    MethodInfo m = currentLocation.GetType().GetMethod(savePoint);
        //    m.Invoke(currentLocation, null);
        //}
    }
}
