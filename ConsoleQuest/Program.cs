// 07.07.2022. Написана базовая логика квеста, начата проектировка механики перехода между уровнями.
// 11.07.2022. Добавлен экран отображения текста об окончании демоверсии игры.

/* Задачи:
 * 1. Сделать систему запуска и выхода из игры. (+++ DONE +++)
 * 2. Сделать механику загрузки уровней.        (+++ DONE +++)
 * 3. Написать сюжет для первых двух уровней.   (+++ DONE +++)
 * 4. Создать линейный сюжет с последующим переходом на нелинейность.
 * 5. Сделать систему сохранений и загрузки игры через взаимодействие с файлом или массивом.
 * 6. Настроить корректный размер окна и отображение текста в консоли.
 * 7. Сделать систему псевдоинвентаря через взаимодействие с файлом или массивом.
 * 8. Проставить бесконечный цикл while во всех методах с локациями (!!!) (+++ DONE +++)
 * 9. Добавить выбор повторного запуска игры или закрытия программы в конце. (+++ DONE +++)
 * 10. Сделать цветной текст для обозначения предметов, локаций и т.д.
 * 11. Сделать отображение одинаковых предметов в инвентаре количественно (x2, x3, x4, ...). (+++ DONE +++)
 * 12. Поработать над входом в пещеру после деревни (Cave => Village => Cave) - не срабатывает метод RestartGame. (!!!) (+++ DONE +++)
 * 13. Переписать корректный текст из Actions.txt в проект. (!!!) (+++ DONE +++)
 * 14. Сделать воспроизведение MP3-файлов (WAV) в консоли. (!!!) (+++ DONE +++)   
 * 15. Сделать так, чтобы игрок оставался в меню закупа, а не выходил из него автоматически. (+++ DONE +++)
 * 16. Сделать метод удаления купленного Игроком предмета из инвентаря продавца (купил Хлеб - он пропал из меню покупки). (+++ DONE +++)\
 * 17. Написать правильное отображение HUD'а + попробовать написать HUD внизу области экрана (HP, MP, dmg, etc)
 */

using ConsoleQuest;
using System.Windows;
using System.Drawing;
using GameSpace;
using System.Runtime.InteropServices;
using ConsoleHelper;

class Program
{
    // Method for set screen and buffer size.
    public static void SetScreen()
    {
        // Setting up screen resolution.
        //Console.SetWindowSize(Console.LargestWindowWidth - 20, Console.LargestWindowHeight - 15);

        // Setting up text buffer with scroll line.
        Console.SetBufferSize(Console.LargestWindowWidth - 20, Console.LargestWindowHeight - 20);

        // Setting up basic coordinates.
        //Console.WindowTop = 0;
        //Console.WindowLeft = 0;

        // Setting up window title.
        Console.Title = "My Game";

        // Setting up font size.
        //ConsoleHelper.SetCurrentFont("Consolas", 10);

        //Console.SetWindowPosition(150, 50);
        //Console.SetWindowSize(160, 45); // default console window size    // 240x63 - max size for 1920x1080
    }

    // Main method.
    public static void Main()
    {
        // Запуск игры (бесконечного цикла).
        while (!Game.exit)
        {
            //NewGame();
            //if (exit)
            //    return;
            // ChooseLocation();    // choose location for start program
            //Console.WindowHeight = Console.LargestWindowHeight;
            //Console.WindowWidth = Console.LargestWindowWidth;
            SetScreen();
            Game.StartGame();    // start program
            if (Game.exit)
                Game.RestartGame();
        }
    }
}