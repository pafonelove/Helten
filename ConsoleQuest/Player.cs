using EnemySpace;
using GameScreen;

namespace PlayerSpace
{
    internal class Player
    {
        public string name = "Unknown Hero";
        public string Name { get => name; set => value = name; }
        int hp = 50;
        int damage = 15;

        public string[] Inventory = new string[10]; // определение размера инвентаря (размер инвентаря = размер массива string)
        public int[] ItemsCount = new int[10];

        Screen display = new();
        Enemy enemy = new Enemy();


        // Метод отображения содержимого инвентаря.
        public void ShowInventory(string[] plrInventory, int[] plrItemsCount)
        {
            display.DrawInventory();

            // Вместимость инвентаря.
            int capacity = ItemsCount.Length;

            // Общее количество вещей, находящихся в инвентаре игрока.
            int sum = 0;
            for (int i = 0; i < ItemsCount.Length; i++)
            {
                sum += ItemsCount[i];
            }

            Console.WriteLine();

            display.DrawInventoryCapacity(capacity, sum);

            if (Inventory[0] is null)
            {
                display.DrawInventoryIsEmpty();
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            for (int i = 0, j = 1; i < Inventory.Length; i++, j++)
            {
                if (Inventory[i] is null)
                    break;

                Console.WriteLine($"{j}. {plrInventory[i]} - x{plrItemsCount[i]} шт.");
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            return;
        }

        // Механика боя между игроком и AI.
        public void Battle(out bool battleFlag)
        {
            battleFlag = false;

            if (enemy.hp <= 0)
            {
                Console.WriteLine("\nВы победили врага!");
                battleFlag = true;
                enemy.Respawn();
                return;
            }

            if (hp <= 0)
            {
                Console.WriteLine($"\nВы проиграли - {enemy.Name} убил Вас.");
                battleFlag = true;
                return;
            }

            // Бой осуществляется посредством бросания кубика в случайном порядке.
            int dice = 0;
            Random rnd = new();
            dice = rnd.Next(0, 4);

            if (dice == 0)
            {
                Console.WriteLine("\nВраг увернулся - Вы промахнулись!");
            }

            if (dice == 1)
            {
                Console.WriteLine($"\nВы нанесли врагу {damage} урона!");
                enemy.hp -= damage;
            }

            if (dice == 2)
            {
                Console.WriteLine("\nВраг промахнулся - Вы увернулись!");
            }

            if (dice == 3)
            {
                Console.WriteLine($"\n{enemy.Name} нанёс Вам {enemy.damage} урона!");
                hp -= enemy.damage;
            }
        }
    }
}
