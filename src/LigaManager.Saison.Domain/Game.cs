namespace LigaManager.Saison.Domain
{
    public sealed class Game
    {
        public Team Home { get; }
        public Team Guest { get; }

        public Game(Team home, Team guest)
        {
            Home = home;
            Guest = guest;
        }

        public override string ToString()
        {
            return $"{Home} vs {Guest}";
        }
    }
}