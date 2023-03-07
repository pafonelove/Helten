using ConsoleQuest;
using EnemySpace;
using GameScreen;
using GameSpace;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace PlayerSpace
{
    internal class Player
    {
        string name = "Unknown Hero";
        const int defaultHP = 50;
        int totalHP = defaultHP;
        int hp = defaultHP;
        int damage = 10;
        int money = 100;
        public string Name { get => name; set => name = value; }
        public int TotalHP { get => totalHP; set => totalHP = value; }
        public int HP { get => hp; set => hp = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Money { get => money; set => money = value; }
        void SetDamage(int newDamage) => Damage = newDamage;    // metod for set new damage after buing weapon
        void DecreaseHP(int enemyDamage) => HP -= enemyDamage;  // method for decrease hero hp after getting enemy damage
        public void DecreaseMoney(int price) => Money -= price; // method for decrease money after buying something in shop

        // Method for set new HP after buying armor
        void SetTotalHP(int newHP)
        {
            TotalHP += newHP;

            if ((TotalHP > HP) & (HP >= defaultHP))
                HP = TotalHP;     
        }

        // Restore HP after using item method.
        public void AddHP(int newHP, out bool hpFlag)
        {
            hpFlag = false;
            if ((HP == defaultHP) | (HP == TotalHP))
            {
                if (Game.languageFlag)
                    Console.WriteLine("    Your health is full - no healing required.");
                else
                    Console.WriteLine("    Ваше здоровье полное - лечение не требуется.");
                hpFlag = true;
                return;
            }
                
            HP += newHP;
            if (HP > TotalHP)
                HP = TotalHP;
            if (Game.languageFlag)
                Console.WriteLine($"    Вы залечили свои раны - HP увеличено на {newHP} единиц.");
            else
                Console.WriteLine($"    You have healed your wounds - HP increased by {newHP} points.");
        }

        public string[] Inventory = new string[10]; // inventory capacity sizing (size = string array capacity)
        //public string [] Inventory = {"Железный меч", "Железный топор", "Железный кинжал", "Железный шлем", "Железный нагрудник", "Железный щит", "Кувшин молока"};
        public int[] ItemsCount = new int[10];
        //public int[] ItemsCount = { 1, 1, 1, 1, 1, 1, 1 };

        Screen display = new Screen();
        Enemy enemy = new Enemy();

        // Display inventory method.
        public void ShowInventory(string[] plrInventory, int[] plrItemsCount)
        {
            display.DrawInventory();

            // Inventory capacity.
            int capacity = ItemsCount.Length;

            // Total amount of items in inventory.
            int sum = 0;
            for (int i = 0; i < ItemsCount.Length; i++)
            {
                sum += ItemsCount[i];
            }

            Console.WriteLine();

            display.DrawInventoryCapacity(capacity, sum);
            DrawHP();
            DrawMoney();

            if (Inventory[0] is null)
            {
                display.DrawInventoryIsEmpty();
                return;
            }

            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();

            for (int i = 0, j = 1; i < Inventory.Length; i++, j++)
            {
                if (Inventory[i] is null)
                    break;

                Console.WriteLine($"    {j}. {plrInventory[i]} - x{plrItemsCount[i]} шт.");
            }

            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine("\n");

            // If inventory is not empty player can use item from inventory.
            string input;
            while (true)
            {
                if (Game.languageFlag)
                    Console.Write("    Enter the number of the item you want to use (Q - abort item selection): ");
                else
                    Console.Write("    Введите номер предмета, который хотите использовать (Q - прервать выбор предметов): ");
                input = display.Input();
                if (input.ToUpper() == "Q")
                    return;

                try
                {
                    Convert.ToInt32(input);
                }
                catch
                {
                    display.MsgAlert();
                    continue;
                }
                
                if ((Convert.ToInt32(input) >= 1) & (Convert.ToInt32(input) <= 9))
                    break;
            }
            Console.WriteLine();
            //Console.Write("   Введите номер предмета, который хотите использовать: ");
            //int 
            UseItem(Convert.ToInt32(input));

            return;
        }

        public void DrawHP()
        {
            Console.Write("\tHP: ");

            // Player HP indicator color.
            if (HP > 25)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(HP);
                Console.ResetColor();
            }
            else if ((HP <= 25) && (HP > 10))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(HP);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(HP);
                Console.ResetColor();
            }
        }

        public void DrawMoney()
        {
            if (Game.languageFlag)
                Console.Write("    Money: ");
            else
                Console.Write("    Монеты: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Money);
            Console.ResetColor();

            Console.WriteLine();
        }

        // Battle mechanic between player and AI.
        public void Battle(out bool battleFlag)
        {
            battleFlag = false;
            Console.WriteLine();
            DrawBattleHud();
            if (enemy.HP <= 0)
            {
                Audio.PlaySFX("victory");
                if (Game.languageFlag)
                    Console.WriteLine("\n    You killed the enemy!");
                else
                    Console.WriteLine("\n    Вы убили врага!");
                battleFlag = true;
                enemy.Respawn();
                return;
            }

            if (hp <= 0)
            {
                Audio.PlaySFX("defeat");
                if (Game.languageFlag)
                    Console.WriteLine($"\n    You died - {enemy.Name} killed you.");
                else
                    Console.WriteLine($"\n    Вы погибли - {enemy.Name} убил Вас.");
                battleFlag = true;
                return;
            }

            // Combat is carried out by rolling a dice in random order.
            int dice = 0;
            Random rnd = new();
            dice = rnd.Next(0, 4);

            if (dice == 0)
            {
                if (Game.languageFlag)
                    Console.WriteLine("\n    The enemy dodged - you missed!");
                else
                    Console.WriteLine("\n    Враг увернулся - Вы промахнулись!");
                Console.WriteLine("    ===================================");
            }

            if (dice == 1)
            {
                if (Game.languageFlag)
                    Console.WriteLine($"\n    You dealt {damage} damage to an enemy!");
                else
                    Console.WriteLine($"\n    Вы нанесли врагу {damage} урона!");
                Console.WriteLine("    ===================================");
                enemy.DecreaseHP(Damage);
            }

            if (dice == 2)
            {
                if (Game.languageFlag)
                    Console.WriteLine("\n    Enemy missed - you dodged!");
                else
                    Console.WriteLine("\n    Враг промахнулся - Вы увернулись!");
                Console.WriteLine("    ===================================");
            }

            if (dice == 3)
            {
                if (Game.languageFlag)
                    Console.WriteLine($"\n    {enemy.Name} dealt you {enemy.Damage} damage!");
                else
                    Console.WriteLine($"\n    {enemy.Name} нанёс Вам {enemy.Damage} урона!");
                Console.WriteLine("    ===================================");
                DecreaseHP(enemy.Damage);
            }
        }

        // Display player and enemy HP method.
        public void DrawBattleHud()
        {
            if (Game.languageFlag)
                Console.Write("    Hero HP: ");
            else
                Console.Write("    Здоровье героя: ");

            // Player HP indicator color.
            if (HP > 25)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(hp);
                Console.ResetColor();
            }
            else if ((HP <= 25) && (HP > 10))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(HP);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(HP);
                Console.ResetColor();
            }

            Console.Write("\t\t");
            if (Game.languageFlag)
                Console.Write("Enemy HP: ");
            else
                Console.Write("Здоровье врага: ");
            // Enemy HP indicator color.
            if (enemy.HP > 15)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(enemy.HP);
                Console.ResetColor();
            }
            else if ((enemy.HP <= 15) && (enemy.HP > 5))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(enemy.HP);
                Console.ResetColor();
            }
            else
            {
                if (enemy.HP < 0)
                    enemy.HP = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(enemy.HP);
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        // Check bought item for decrease Player's money.
        ///// , "Кувшин молока", "Жареная говядина"
        //public void CheckItem(string item)
        //{
        //    switch (item)
        //    {
        //        case "Хлеб":
        //            DecreaseMoney(10);
        //            break;
        //    }
        //}

        bool swordFlag = false, axeFlag = false, daggerFlag = false;         // flags for usable weapon from inventory
        bool helmetFlag = false, chestPlateFlag = false, shieldFlag = false; // flags for usable armor from inventory

        // Use item from inventory method.
        void UseItem(int input)
        {
            bool hpFlag;    // flag for event, when hero HP is full

            

            int itemPos = --input;  // item position in array is -1, because items store in array from 0 element, not 1


            // Block for using food and potions for restore HP.
            // FOOD // "Bread", "Pitcher of Milk", "Roast Beef"
            if (Inventory[itemPos] == "Хлеб" | Inventory[itemPos] == "Bread")
            {
                AddHP(15, out hpFlag);
                if (hpFlag) // if hp is full, exit from method
                    return;
                display.MsgUsedItem();
                Console.WriteLine($"{Inventory[itemPos]}");
                ItemsCount[itemPos]--;
            }

            if (Inventory[itemPos] == "Кувшин молока" | Inventory[itemPos] == "Pitcher of Milk")
            {
                AddHP(10, out hpFlag);
                if (hpFlag)
                    return;
                display.MsgUsedItem();
                Console.WriteLine($"{Inventory[itemPos]}");
                ItemsCount[itemPos]--;
            }

            if (Inventory[itemPos] == "Жареная говядина" | Inventory[itemPos] == "Roast Beef")
            {
                AddHP(25, out hpFlag);
                if (hpFlag)
                    return;
                display.MsgUsedItem();
                Console.WriteLine($"{Inventory[itemPos]}");
                ItemsCount[itemPos]--;
            }

            // POTIONS // "Small Healing Potion", "Healing Potion", "Big Healing Potion"
            if (Inventory[itemPos] == "Малое зелье лечения" | Inventory[itemPos] == "Small Healing Potion")
            {
                AddHP(10, out hpFlag);
                if (hpFlag)
                    return;
                display.MsgUsedItem();
                Console.WriteLine($"{Inventory[itemPos]}");
                ItemsCount[itemPos]--;
            }

            if (Inventory[itemPos] == "Зелье лечения" | Inventory[itemPos] == "Healing Potion")
            {
                AddHP(30, out hpFlag);
                if (hpFlag)
                    return;
                display.MsgUsedItem();
                Console.WriteLine($"{Inventory[itemPos]}");
                ItemsCount[itemPos]--;
            }

            if (Inventory[itemPos] == "Большое зелье лечения" | Inventory[itemPos] == "Big Healing Potion")
            {
                AddHP(50, out hpFlag);
                if (hpFlag)
                    return;
                display.MsgUsedItem();
                Console.WriteLine($"{Inventory[itemPos]}");
                ItemsCount[itemPos]--;
            }

            // WEAPONS // "Iron Sword", "Iron Axe", "Iron Dagger"
            if (Inventory[itemPos] == "Железный меч" | Inventory[itemPos] == "Iron Sword")
            {
                if (swordFlag)  // if weapon flag is true, then msg + return from method
                {
                    display.MsgWeaponInHand(Inventory[itemPos]);
                    return;
                }

                display.MsgPickWeapon(Inventory[itemPos], 20);  // pick up the weapon
                swordFlag = true;   // weapon flag = true  
                axeFlag = daggerFlag = false; // other weapons flags = false
                SetDamage(20);
            }

            if (Inventory[itemPos] == "Железный топор" | Inventory[itemPos] == "Iron Axe")
            {
                if (axeFlag)
                {
                    display.MsgWeaponInHand(Inventory[itemPos]);
                    return;
                }

                display.MsgPickWeapon(Inventory[itemPos], 30); 
                axeFlag = true;
                swordFlag = daggerFlag = false;
                SetDamage(30);
            }
            
            if (Inventory[itemPos] == "Железный кинжал" | Inventory[itemPos] == "Iron Dagger")
            {
                if (daggerFlag)
                {
                    display.MsgWeaponInHand(Inventory[itemPos]);
                    return;
                }

                display.MsgPickWeapon(Inventory[itemPos], 15);
                Console.WriteLine();
                daggerFlag = true;
                swordFlag = axeFlag = false;
                SetDamage(15);
            }

            // ARMOR // "Iron Helmet", "Iron Chestplate", "Iron Shield"
            // "Железный шлем", "Железный нагрудник", "Железный щит"
            if (Inventory[itemPos] == "Железный шлем" | Inventory[itemPos] == "Iron Helmet")
            {
                if (helmetFlag) // if armor flag is true, then msg + return from method
                {
                    display.MsgArmorPutOn(Inventory[itemPos]);
                    return;
                }

                display.MsgPickArmor(Inventory[itemPos], 5);   // pick up the armor
                helmetFlag = true;  // current armor flag = true
                SetTotalHP(5);
            }

            if (Inventory[itemPos] == "Железный нагрудник" | Inventory[itemPos] == "Железный нагрудник")
            {
                if (chestPlateFlag) 
                {
                    display.MsgArmorPutOn(Inventory[itemPos]);
                    return;
                }

                display.MsgPickArmor(Inventory[itemPos], 20);
                chestPlateFlag = true;
                SetTotalHP(20);
            }

            if (Inventory[itemPos] == "Железный щит" | Inventory[itemPos] == "Железный щит")
            {
                if (shieldFlag)
                {
                    display.MsgArmorPutOn(Inventory[itemPos]);
                    return;
                }

                display.MsgPickArmor(Inventory[itemPos], 15);
                shieldFlag = true;
                SetTotalHP(15);
            }

            if (ItemsCount[itemPos] <= 0)
                DeleteFromPlayerInventory(Inventory, ItemsCount, itemPos);
        }

        static void DeleteFromPlayerInventory(string[] playerInventory, int[] playerItemsCount, int choice)
        {
            // If player inventory is full, then method running ends (count of items in trader inventory is not decreases).
            int j = choice;
            playerItemsCount[j]--;

            if (playerItemsCount[j] <= 0)                           // if product if out of stock
            {
                for (int i = choice; i < playerInventory.Length; i++)
                {
                    if (i >= playerInventory.Length - 1)            // if position was last in list, then
                    {
                        playerInventory[i] = null;                      // delete position,
                        playerItemsCount[i] = 0;                        // count resets to zero,
                        if (playerItemsCount[i] == 0)
                        {
                            break;                                      // exit from cycle
                        }
                    }

                    playerInventory[i] = playerInventory[++i];      // product name set to one position up
                    --i;


                    playerItemsCount[i] = playerItemsCount[++i];    // item count set to one position up
                    --i;

                    if (i >= playerItemsCount.Length - 1)
                    {
                        break;
                    }
                }
            }
        }
    }
}
