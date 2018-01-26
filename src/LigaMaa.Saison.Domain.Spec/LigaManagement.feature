Feature: LigaManagement
	Creating a gameplan should be easy
	As a novice game Creator
	I want to make a working gameplan

Background: 
	Given I created a Saison

Scenario Outline: Adding Teams creates GameDays
	Given I add <Teams> Teams
	When I start the Season
	Then I should have <GameDays> GameDays
Examples: 
	| Teams | GameDays |
	| 2     | 1        |
	| 3     | 3        |
	| 4     | 3        |
	| 5     | 5        |
	| 6     | 5        |


Scenario: A team can only play once on a gameday
	Given I add 3 Teams
	When I start the Season
	Then No Team plays twice a day

Scenario Outline: A team has enough games
	Given I add <Teams> Teams
	When I start the Season
	Then Each Team has <Games> Games

Examples: 
	| Teams | Games |
	| 2     | 1        |
	| 3     | 2        |
	| 4     | 3        |
	| 5     | 4        |
	| 6     | 5        |
