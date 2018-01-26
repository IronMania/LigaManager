using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace LigaManager.Saison.Domain.Spec
{
    [Binding]
    public class LigaManagementSteps
    {
        private Season _season;

        [Given(@"I created a Saison")]
        public void GivenICreatedASaison()
        {
            _season = new Season();
        }

        [When(@"I add (.*) Teams")]
        public void WhenIAddTeams(int teams)
        {
            for (var i = 0; i < teams; i++) _season.AddTeam(new Team($"Team {i}"));
        }

        [Then(@"I should have (.*) GameDays")]
        public void ThenIShouldHaveGameDays(int gameDays)
        {
            Assert.That(_season.GameDays.Count, Is.EqualTo(gameDays));
        }

        [Then(@"No Team plays twice a day")]
        public void ThenNoTeamPlaysTwiceADay()
        {
            foreach (var seasonGameDay in _season.GameDays)
            {
                var tmp = seasonGameDay.Games.Select(game => game.Home).Concat(seasonGameDay.Games.Select(game => game.Guest)).ToList();
                if (tmp.Distinct().Count() != tmp.Count)
                {
                    Assert.Fail("A team is playing twice a day");
                }
            }
        }

        [Then(@"Each gameday has only one game")]
        public void ThenEachGamedayHasOnlyOneGame()
        {
            foreach (var seasonGameDay in _season.GameDays) Assert.That(seasonGameDay.Games.Count, Is.EqualTo(1));
        }

        [Then(@"Each Team has (.*) Games")]
        public void ThenEachTeamHasGames(int p0)
        {
            var homeGames = _season.GameDays.SelectMany(day => day.Games).GroupBy(test => test.Home, game => game);
            var awayGames = _season.GameDays.SelectMany(day => day.Games).GroupBy(test => test.Guest, game => game);
            var tmp = homeGames.Concat(awayGames);
            var allgames = tmp.GroupBy(games => games.Key, games => games, (team, enumerable) =>
            {
                var result = new List<Game>();
                foreach (var enumerator in enumerable) result.AddRange(enumerator);
                return result;
            });

            foreach (var allgame in allgames) Assert.That(allgame.Count, Is.EqualTo(p0));
        }
        [Given(@"I add (.*) Teams")]
        public void GivenIAddTeams(int teams)
        {
            for (var i = 0; i < teams; i++) _season.AddTeam(new Team($"Team {i}"));
        }
        
        [When(@"I start the Season")]
        public void WhenIStartTheSeason()
        {
            _season.Calculate();
        }

    }
}