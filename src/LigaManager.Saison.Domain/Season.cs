using System;
using System.Collections.Generic;
using System.Linq;

namespace LigaManager.Saison.Domain
{
    public class Season
    {
        private readonly List<GameDay> _gameDays = new List<GameDay>();
        private readonly List<Team> _teams = new List<Team>();

        public IReadOnlyList<GameDay> GameDays => _gameDays;

        private int CountGameDays()
        {
            var count = _teams.Count % 2 + _teams.Count - 1;
            return count * 2;
        }

        public void AddTeam(Team team)
        {
            _teams.Add(team);

        }

        private int CountGameDaysOnce()
        {
            return _teams.Count + _teams.Count % 2 - 1;
        }

        public IList<Game> Start(params int[] i)
        {
            if (i.Length != CountGameDays()) throw new InvalidOperationException("GameDaysMustMatch");
            if (i.Distinct().Count() != i.Length) throw new InvalidOperationException("Same GameDays are in the list");


            var result = GetAllCombinations().ToList();

            return result;
        }


        private IEnumerable<Game> GetAllCombinations()
        {
            return _teams.SelectMany(mainItem => _teams
                .Where(otherItem => _teams.IndexOf(otherItem) != _teams.IndexOf(mainItem))
                .Select(otherItem => new Game(mainItem, otherItem)));
        }

        public void Calculate()
        {
            _gameDays.Clear();

            GenerateGames();
        }

        private void GenerateGames()
        {
            var n = _teams.Count;
            var gameDays = CountGameDaysOnce(); //Domain
            var isEven = n % 2 == 0; //infrastructure
            int modifier;
            if (isEven) //infrastructure
                modifier = -1; //Domain
            else
            {
                modifier = 0;//Domain
            }
            
            for (var i = 0; i < gameDays; i++)
            {
                var games = new List<Game>();
                if (isEven) games.Add(new Game(_teams[i], _teams[n - 1]));

                for (var k = 1; k <= n / 2 + modifier; k++)
                {
                    var moduloTeam2 = (i - k) % (n - 1);
                    if (moduloTeam2 < 0) moduloTeam2 += n + modifier;

                    var moduloTeam1 = (i + k) % (n + modifier);
                    if (moduloTeam1 != moduloTeam2) games.Add(new Game(_teams[moduloTeam1], _teams[moduloTeam2]));
                }

                _gameDays.Add(new GameDay(games.ToArray()));
            }
        }
    }

    public class GameDay
    {
        public GameDay(params Game[] games)
        {
            Games = new List<Game>(games);
        }

        public IList<Game> Games { get; }
    }
}