using System;
using System.Linq;
using FluentAssertions;
using LigaManager.Saison.Domain;
using NUnit.Framework;

namespace LibaManager.Saison.Domain.Test
{
    public class SeasonTest
    {
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 6)]
        [TestCase(5, 10)]
        [TestCase(6, 10)]
        public void Add_Two_Teams_Adds_GameDays(int teams, int gameDays)
        {
            var league = new Season();

            for (var i = 0; i < teams; i++) league.AddTeam(new Team($"Team {i}"));

            //league.GameDays.Should().Be(gameDays);
        }

        [Test]
        public void Create_GamePlan_Requires_Correct_Amount_Of_Play_Weeks()
        {
            var league = new Season();
            league.AddTeam(new Team("Home"));
            league.AddTeam(new Team($"Team 2"));

            var act = new Action(() => league.Start(1));

            act.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void Create_GamePlan_Create_Two_Games()
        {
            var league = new Season();
            var homeTeam = new Team("Team 1");
            league.AddTeam(homeTeam);
            league.AddTeam(new Team("Team 2"));

            var result = league.Start(1,2);
            result.Count.Should().Be(2);
            result.First().Home.Should().Be(homeTeam);
            result.Last().Guest.Should().Be(homeTeam);
        }
        [Test]
        public void Create_GamePlan_OnSameGameDaysDoesNotWork()
        {
            var league = new Season();
            league.AddTeam(new Team("Team 1"));
            league.AddTeam(new Team("Team 2"));

            var act = new Action(() => league.Start(1,1));

            act.ShouldThrow<InvalidOperationException>();
        }
    }
}