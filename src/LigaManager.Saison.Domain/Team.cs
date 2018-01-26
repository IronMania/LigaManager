using System;

namespace LigaManager.Saison.Domain
{
    public class Team
    {
        public string Name { get; }

        public Team(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
