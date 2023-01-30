namespace EnemySpace
{
    internal class Enemy
    {
        string name = "Orc";
        int hp = 30;
        int damage = 10;
        //public int damageTaken;
        public string Name { get => name; set => value = name; }
        public int HP { get => hp; set => hp -= value; }
        public int Damage { get => damage;}

        public int Respawn() => hp = 30;
    }
}
