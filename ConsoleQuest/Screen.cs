﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (str == "2023")
                Console.WriteLine("Вы открыли пасхалку!");
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
    }
}
