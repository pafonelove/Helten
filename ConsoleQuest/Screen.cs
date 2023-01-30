using EnemySpace;
using PlayerSpace;
using System.Diagnostics;

namespace GameScreen
{
    // Класс Screen отвечает за ввод/вывод симоволов с клавиатуры на экран.
    public class Screen
    {
        // Метод ввода с клавиатуры.
        //public string? Input() => Console.ReadLine();

        // Доработанный метод с досрочным закрытием программы через нажатие на клавишу "q".
        public string? Input()
        {
            string? str = Console.ReadLine();
            if ((str == "R") || (str == "r"))
                Process.GetCurrentProcess().Kill();
            // Пасхальное яйцо.
            if (str == "2023") 
            {
                Console.WriteLine("Вы открыли пасхалку!");
                Console.WriteLine("Нажмите любую кнопку для выхода из инвентаря . . .");
                Console.ReadKey();
            }
                
            return str;
        }

        // Метод отображения начального экрана.
        public void GreetingScreen()
        {
            Console.WriteLine("Добро пожаловать в игру \"ConsoleQuest v 0.1!\"");  // окно приглашения в игру
            Console.Write("Желаете начать игру? [Д/Н]: ");
        }

        // Метод отображения сюжета видеоигры.
        public void Story()
        {
            Console.Clear();
            Console.WriteLine("Вы – Дариус, ученик чародея, выходец из крестьянской рабочей семьи, который захотел познать тайны вселенной, недоступные простолюдинам.\n\n" +
                              "Ваш наставник – местный колдун Рубик, когда - то бывший светлым магом, который участвовал в великой Битве за Кеонур.\n\n" +
                              "Будучи ребенком, Дариус был отдан отцом Рубику в роли подмастерья.Отец посчитал, что так будет лучше для маленького сына, потому что родители Дариуса сводили концы с концами, " +
                              "работая на земельных участках по 20 часов в день, – мальчик родился в самое жуткое для всего королевства Райёме Гранд Леон Ле время." +
                              "Летописцы прозвали этот промежуток истории «Веком тьмы».\n\n" +
                              "Вы должны найти последний осколок «Камня Десяти». Именно Дариус был избран владыкой «Земли крылатого льва», королём Люсьен VI, чтобы восстановить равновесие между " +
                              "королевствами Цардонии.\n\n");
            Console.WriteLine("Нажмите Enter для продолжения . . .");
            Input();
        }

        // Метод отображения экрана перезапуска игры.
        public void RestartScreen() => Console.Write("Желаете начать игру заново? [Д/Н]: ");

        // Метод отображения экрана выхода.
        public void ExitScreen() => Console.WriteLine("\nВыход из игры . . .\n");

        // Метод имитации загрузочного экрана.
        public void Loading()
        {
            Console.Clear();
            Console.Write("L O A D I N G ");
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(150);
                Console.Write(". ");
            }
            Console.Clear();
        }

        // ДОРАБОТАТЬ!!! // Метод для отрисовки рамки на стартовом экране (в будущем требуется сделать произвольный размер рамки по горизонтали и вертикали).
        public void DrawBorder(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.Write("#");

                for (int j = 0; j < cols; j++)
                {
                    if ((i == 0) || (i == rows - 1))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                }

                Console.Write("#");

                Console.WriteLine();
            }
        }

        

        // Метод отрисовки названия локации.
        public void LocationName(string locationName)
        {
            switch (locationName)
            {
                case "Dungeon":
                    Console.WriteLine();
                    Console.WriteLine("========================================================================================================================");
                    Console.WriteLine("============================================= Л О К А Ц И Я \"П Е Щ Е Р А\" ==============================================");
                    Console.WriteLine("========================================================================================================================");
                    break;
                case "Village":
                    Console.WriteLine();
                    Console.WriteLine("========================================================================================================================");
                    Console.WriteLine("============================================= Л О К А Ц И Я \"Д Е Р Е В Н Я\" ============================================");
                    Console.WriteLine("========================================================================================================================");
                    break;

                case "Market":
                    Console.WriteLine();
                    Console.WriteLine("=======================================================================================================================");
                    Console.WriteLine("============================================== Л О К А Ц И Я \"Р Ы Н О К\" ==============================================");
                    Console.WriteLine("=======================================================================================================================");
                    break;

                case "Road":
                    Console.WriteLine();
                    Console.WriteLine("========================================================================================================================");
                    Console.WriteLine("============================================= Л О К А Ц И Я \"Д О Р О Г А\" ==============================================");
                    Console.WriteLine("========================================================================================================================");
                    break;

                case "BlacksmithHouse":
                    Console.WriteLine();
                    Console.WriteLine("========================================================================================================================");
                    Console.WriteLine("======================================== Л О К А Ц И Я \"Д О М    К У З Н Е Ц А\" ========================================");
                    Console.WriteLine("========================================================================================================================");
                    break;

                case "DoctorHouse":
                    Console.WriteLine();
                    Console.WriteLine("========================================================================================================================");
                    Console.WriteLine("======================================== Л О К А Ц И Я \"Д О М    Л Е К А Р Я\" ==========================================");
                    Console.WriteLine("========================================================================================================================");
                    break;

                case "BattleZone":
                    Console.WriteLine();
                    Console.WriteLine("========================================================================================================================");
                    Console.WriteLine("================================================= А Р Е Н А   Б И Т В Ы ================================================");
                    Console.WriteLine("========================================================================================================================");
                    break;

                default:
                    break;
            }
            
        }

        // Метод отображения экрана перехода на новый уровень.
        public void NextLevel()
        {
            Console.WriteLine("Вы перешли на следующий уровень. Нажмите любую клавишу для продолжения . . .\n");
            Console.ReadKey();
        }

        // Метод отображения предложения сохранить игру.
        public void SaveScreen() => Console.Write("Вы хотите сохранить игру? [Д/Н]: ");

        // Метод отображения предложения загрузить игру.
        public void LoadScreen() => Console.Write("Вы хотите загрузить игру? [Д/Н]: ");

        // Метод вывода текста завершения демо игры.
        public void EndDemo()
        {
            Console.WriteLine("Конец демонстрационной версии игры. Нажмите любую клавишу для продолжения . . .\n");
            Console.ReadKey();
        }

        // Метод отрисовки "шапки" инвентаря.
        public void DrawInventory()
        {
            Console.WriteLine();
            Console.WriteLine("[========] [========] [========] [========] [========]");
            Console.Write("[========] [====]  ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("И Н В Е Н Т А Р Ь");
            Console.ResetColor();
            Console.WriteLine(" [====] [========]");
            Console.WriteLine("[========] [========] [========] [========] [========]");
        }

        // Метод отображения занятого пространства в инвентаре героя.
        public void DrawInventoryCapacity(int invCapacity, int takenSpace)
        {
            if (takenSpace == 0)                                    // если инвентарь пустой
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Слотов заполнено: {takenSpace}/{invCapacity}");
                Console.ResetColor();
            }

            if ((takenSpace > 0) && (takenSpace < invCapacity))     // если инвентарь заполнен, но не полностью
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Слотов заполнено: {takenSpace}/{invCapacity}");
                Console.ResetColor();
                Console.WriteLine();
            }

            if (takenSpace == invCapacity)                          // если инвентарь заполнен
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Слотов заполнено: {takenSpace}/{invCapacity}");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Метод - сообщение о том, что инвентарь пуст.
        public void DrawInventoryIsEmpty()
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Инвентарь пуст!");
            Console.ResetColor();

            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }

        // Метод - сообщение о том, что инвентарь заполнен.
        public void DrawInventoryIsFull()
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Инвентарь заполнен!");
            Console.ResetColor();

            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
        }

        // Метод - сообщение о том, что товар в магазине отсутствует.
        public void DrawItemIsOut()
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("X");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Товар отсутствует!");
            Console.ResetColor();

            for (int i = 0; i < 10; i++)
            {
                Console.Write("X");
            }
            Console.WriteLine();
        }
    }
}
