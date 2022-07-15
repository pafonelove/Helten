using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleQuest
{
    class Screen
    {
        // Метод ввода с клавиатуры.
        public string? Input() => Console.ReadLine();

        // Метод отображения начального экрана.
        public void Greeting()
        {
            Console.WriteLine("Добро пожаловать в игру \"ConsoleQuest v 0.1!\"");  // окно приглашения в игру
            Console.Write("Желаете запустить игру? [Д/Н]: ");
        }

        // Метод отображения экрана выхода.
        public void Exit() => Console.WriteLine("Выход из игры . . .\n");

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

        // Метод отображения экрана перехода на новый уровень.
        public void NextLevel()
        {
            Console.WriteLine("Вы перешли на следующий уровень. Нажмите любую клавишу для продолжения . . .\n");
            Console.ReadKey();
        }

        // Метод вывода текста завершения демо игры.
        public void EndDemo()
        {
            Console.WriteLine("Конец демонстрационной версии игры. Нажмите любую клавишу для продолжения . . .\n");
            Console.ReadKey();
        }
    }
}
