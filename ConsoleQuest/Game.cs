using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using GameScreen;
using System.Data;

namespace GameSpace
{
    // Класс Game содержит логику игры.
    public class Game
    {
        static Screen display = new();              // экземпляр класса Screen, отвечающий за отображение текста и системных сообщений на экране
        static string? input;                       // переменная для ввода текста с клавиатуры
        static string? itemName;                    // переменная для взаимодействия с инвентарем
        static string[] inventory = new string[10]; // определение размера инвентаря (размер инвентаря = размер массива string)
        public static bool exit = false;            // глобальная переменная для выхода из бесконечного цикла while (завершение игры)
        static string savePoint;                    // переменная, сохраняющая в себе название текущей локации (имя текущего метода)
        
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
        public static void StartGame() => Cave();

        // Метод для выбора повторной игры или закрытия программы.
        public static void RestartGame()
        {
            while (true)
            {
                display.RestartScreen();                                                // вызов окна приглашения в повторную игру

                input = display.Input();
                if (input == "Д" || input == "д" || input == "Y" || input == "y")       // если пользователь согласился сыграть еще раз, то вызывается метод StartGame, отвечающий за запуск игры
                {
                    Array.Clear(inventory);                                             // обнуление инвентаря после перезапуска (очищение массива от элементов)
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
                Console.WriteLine("1. Cave.\n" +
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
                        Cave();
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

        // Описанме инструкций игры в локации "Пещера Петит-Равин".
        public static void Cave()
        {
            // display.Loading();      // вызов загрузочного экрана
            while (true)
            {
                Console.WriteLine("\nВы стоите перед входом в пещеру Петит-Равин. Слева и справа тупик, сзади – дорога в деревню Шик-Шагок, в которой есть местный лекарь, торговцы оружием и провизией и кузнец.\n" +
                                  "1. Отправиться в пещеру.\n" +
                                  "2. Развернуться и пойти в деревню.\n" +
                                  "3. Показать инвентарь.");

                Console.Write("> ");

                input = display.Input();    // механика выбора действий через вложенные конструкции if и вложенные циклы while
                if (input == "1")
                {
                    Console.WriteLine("\nВы отправились в пещеру . . .\n");
                    display.EndDemo();      // вызов экрана с текстом об окончании демоверсии
                    display.LoadScreen();
                    input = display.Input();
                    if (input == "Д" || input == "д" || input == "Y" || input == "y")
                        LoadGame();

                    exit = true;            // выход из бесконечного цикла
                    return;
                }

                else if (input == "2")
                {
                    Village();
                }

                else if (input == "3")
                {
                    ShowInventory();
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
                Console.WriteLine("\nВы вошли в деревню Кватре Котес. Поселение выглядит ухоженным, дома и постройки выполнены в классическом стиле, присущем средневековым колонистам-французам.\n" +
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
                    ShowInventory();
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
                Console.WriteLine("\nСледуя за незнакомой женщиной преклонного возраста, Вы вышли на сельскую площадь, на которой располагалась местная ярмарка.\n" +
                                  "1. Купить провизию.\n" +
                                  "2. Вернуться к воротам деревни.\n" +
                                  "3. Показать инвентарь.");
                Console.Write("> ");

                input = display.Input();
                if (input == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("\nЧто вы хотите купить из еды?");
                        ShowTraderInventory(traderFood);
                        Console.WriteLine("Q - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {
                            
                            if (input == "1")
                            {
                                Console.WriteLine($"\nВы купили {traderFood[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderFood[0]);
                                DeleteFromTraderInventory(traderFood, traderFood[0], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                Console.WriteLine($"\nВы купили {traderFood[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderFood[1]);
                                DeleteFromTraderInventory(traderFood, traderFood[1], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                Console.WriteLine($"\nВы купили {traderFood[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderFood[2]);
                                DeleteFromTraderInventory(traderFood, traderFood[2], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                return;
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
                    ShowInventory();
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
                Console.WriteLine("\nВы находитесь на дорожной развилке рядом с указателем: «Дом кузнеца <==== || ====> Дом лекаря».\n" +
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
                    ShowInventory();
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
                Console.WriteLine("\nВнутри Вас встретил местный кузнец по имени Леонард - взрослый, крепкий и высокий мужчина с длинной угольной бородой, - держащий в правой руке \n" +
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
                    while (true)
                    {
                        Console.WriteLine("\nЧто вы хотите купить из оружия?");
                        ShowTraderInventory(traderWeapons);
                        Console.WriteLine("Q - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine($"\nВы купили {traderWeapons[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderWeapons[0]);
                                DeleteFromTraderInventory(traderWeapons, traderWeapons[0], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                Console.WriteLine($"\nВы купили {traderWeapons[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderWeapons[1]);
                                DeleteFromTraderInventory(traderWeapons, traderWeapons[1], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                Console.WriteLine($"\nВы купили {traderWeapons[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderWeapons[2]);
                                DeleteFromTraderInventory(traderWeapons, traderWeapons[2], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                return;
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
                    while (true)
                    {
                        Console.WriteLine("\nЧто вы хотите купить из брони?");
                        ShowTraderInventory(traderArmor);
                        Console.WriteLine("Q - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine($"\nВы купили {traderArmor[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderArmor[0]);
                                DeleteFromTraderInventory(traderArmor, traderArmor[0], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                Console.WriteLine($"\nВы купили {traderArmor[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderArmor[1]);
                                DeleteFromTraderInventory(traderArmor, traderArmor[1], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                Console.WriteLine($"\nВы купили {traderArmor[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderArmor[2]);
                                DeleteFromTraderInventory(traderArmor, traderArmor[2], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                return;
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
                }

                else if (input == "4")
                {
                    return;
                }

                else if (input == "5")
                {
                    ShowInventory();
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
                Console.WriteLine("\nВ одноэтажном здании с выкрашенными травяными красителями зелеными стенами - что было сделано для создания атмосферы умиротворения и \n" +
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
                }

                else if (input == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("\nЧто вы хотите купить из травяных настоек?");
                        ShowTraderInventory(traderPotions);
                        Console.WriteLine("Q - выход из магазина.");

                        Console.Write("> ");

                        input = display.Input();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine($"\nВы купили {traderPotions[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderPotions[0]);
                                DeleteFromTraderInventory(traderPotions, traderPotions[0], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "2")
                            {
                                Console.WriteLine($"\nВы купили {traderPotions[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderPotions[1]);
                                DeleteFromTraderInventory(traderPotions, traderPotions[1], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if (input == "3")
                            {
                                Console.WriteLine($"\nВы купили {traderPotions[Convert.ToInt32(input) - 1]}.");
                                AddToInventory(inventory, traderPotions[2]);
                                DeleteFromTraderInventory(traderPotions, traderPotions[2], Convert.ToInt32(input) - 1);
                                break;
                            }

                            else if ((input == "Q") || (input == "q"))
                            {
                                return;
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
                }

                else if (input == "4")
                {
                    return;
                }

                else if (input == "5")
                {
                    ShowInventory();
                }

                else
                {
                    Console.WriteLine("\nВыберите корректное действие!\n");
                }
            }
        }

        static string[] traderWeapons = { "Железный меч", "Железный топор", "Железный кинжал" };
        static string[] traderArmor = { "Железный шлем", "Железный нагрудник", "Железный щит" };
        static string[] traderFood = { "Хлеб", "Кувшин молока", "Жареная говядина" };
        static string[] traderPotions = { "Малое зелье лечения", "Зелье лечения", "Большое зелье лечения" };

        // Метод отображения содержимого инвентаря торговца.
        static void ShowTraderInventory(string[] traderInventory)
        {
            Console.WriteLine();
            for (int i = 0, j = 1; i < traderInventory.Length; i++, j++)
            {
                if (traderInventory[0] is null)
                {
                    Console.WriteLine("У торговца закончились товары!");
                    return;
                }

                if (traderInventory[i] is null)
                    return;

                Console.WriteLine($"{j}. {traderInventory[i]}");
            }

            Console.WriteLine();
        }

        // Метод удаления купленного товара из инвентаря торговца.
        static void DeleteFromTraderInventory(string[] traderInventory, string? item, int choice)
        {
            for (int i = choice; i < traderInventory.Length; i++)
            {
                if (i >= traderInventory.Length - 1)
                {
                    traderInventory[i] = null;
                    break;
                }

                traderInventory[i] = traderInventory[++i];
                --i;
            }
        }

        // Метод отображения содержимого инвентаря.
        static void ShowInventory()
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            for (int i = 0, j = 1; i < inventory.Length; i++, j++)
            {
                if (inventory[0] is null)
                {
                    Console.WriteLine("Инвентарь пуст!");
                    break;
                }

                if (inventory[i] is null)
                    break;

                Console.WriteLine($"{j}. {inventory[i]}");
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            return;
        }

        // Метод добавления предмета в инвентарь.
        static void AddToInventory(string[] inventory, string itemName)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] is null)
                {
                    inventory[i] = itemName;
                    return;
                }

                if (inventory[inventory.Length - 1] != null)
                {
                    InventoryIsFull();
                    return;
                }
            }

            Console.WriteLine();
        }

        // Метод сообщения о заполненном инвентаре.
        static void InventoryIsFull()
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.WriteLine("Инвентарь заполнен!");

            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }

        // Метод сохранения игры (!!! ДОРАБОТАТЬ !!!).
        static void SaveGame([CallerMemberName] string? callerMemberName = null)
        {
            savePoint = callerMemberName;
        }

        // Метод загрузки игры (!!! ДОРАБОТАТЬ !!!).
        static void LoadGame()
        {
            Game currentLocation = new Game();
            MethodInfo m = currentLocation.GetType().GetMethod(savePoint);
            m.Invoke(currentLocation, null);
        }
    }
}
