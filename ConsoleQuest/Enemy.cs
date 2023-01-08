namespace EnemySpace
{
    internal class Enemy
    {
        public string name = "Orc";
        public string Name { get => name; set => name = value; }
        public int hp = 30;
        public int damage = 10;

        public int Respawn() => hp = 30;
    }
}
