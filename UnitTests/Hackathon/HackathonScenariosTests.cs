using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;
using Game.Views;
using Xamarin.Forms.Mocks;
using Game;
using Xamarin.Forms;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
            // Choose which engine to run
            EngineViewModel.SetBattleEngineToGame();

            // Put seed data into the system for all tests
            EngineViewModel.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            EngineViewModel.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;


        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

//        #region Scenario1
//<<<<<<< HEAD
//        [Test]
//        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
//=======
//        [Test]
//        /*public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
//>>>>>>> be63601fa33a163d35ee87499df9ffbb379cfa10
//        {
//            *//* 
//            *Scenario Number:
//            *1
//            *
//            *Description: 
//            *Make a Character called Mike, who dies in the first round
//      *
//      * Changes Required(Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
//            *No Code changes requied
//      *
//      * Test Algrorithm:
//            *Create Character named Mike
//      * Set speed to -1 so he is really slow
// * Set Max health to 1 so he is weak
//* Set Current Health to 1 so he is weak
//*
//*Startup Battle
//* Run Auto Battle
//            *
//            *Test Conditions:
//            *Default condition is sufficient
//      *
//      *Validation:
//            *Verify Battle Returned True
//      * Verify Mike is not in the Player List
// * Verify Round Count is 1
//*
//*//*

//Arrange

//             Set Character Conditions

//            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

//            var CharacterPlayerMike = new PlayerInfoModel(
//                            new CharacterModel
//                            {
//                                Speed = -1, // Will go last...
//                                Level = 1,
//                                CurrentHealth = 1,
//                                ExperienceTotal = 1,
//                                ExperienceRemaining = 1,
//                                Name = "Mike",
//                            });

//            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

//            Set Monster Conditions

//            Auto Battle will add the monsters

//            Monsters always hit
//             EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

//            Act
//            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

//            Reset
//            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

//            Assert
//            Assert.AreEqual(true, result);
//            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
//            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
//        }
//        #endregion Scenario1

        #region Scenario3
        [Test]
        public void HakathonScenario_Boss_Monster_Respawned_At_Round_five_Should_Return_True()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      Boss monster shows up at round 5
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Change Round number to round 5
            *      Clear the monster list
            *      Call AddMonstersToRound();
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            *      Check number of monsterlist
            *      check member of monsterlist
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *      Monsterlist count to one 
            *      Monsterlist data is Monstertype.Cancer
            *  
            */

            // Arrange
            EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount = 5;
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            // Act
            var result = EngineViewModel.Engine.Round.AddMonstersToRound();
            var data = EngineViewModel.Engine.EngineSettings.MonsterList[0];

            // Reset

            // Assert
            Assert.AreEqual(1, result);
            Assert.AreEqual(MonsterTypeEnum.Cancer, data.MonsterType);
        }
        #endregion Scenario3

        #region Scenario35
        [Test]
        public void HakathonScenario_Monsters_Movement_Based_On_Speed_Should_Pass()
        {
            /* 
           * Scenario Number:  
           *      #35
           *      
           * Description: 
           *      Monster movement should be based on speed, monsters pick 
           *      the most optimal square closest to the target that is within
           *      the speed range of the monster and move there. 
           *      
           * 
           * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
           *     MapModel.cs:
           *       Added ReturnClosestEmptyLocationSpeed(MapModelLocation Target, MapModelLocation Attacker, PlayerInfoModel PlayerAttacker)
           *       Added more parameters so that I could:
           *           1) Determine the free spaced on the map that are in range of the Attacker to move into based on speed
           *           2) Then Calculate the distance between the in range empty location and the target location
           *           3) Return the location that is the closest to the Target that is also in speed range
           *     
           * 
           *     TurnEngine.cs:
           *       line 213, added more parameters to the ReturnClosestEmptyLocationSpeed call
           *     
           * Test Algrorithm:
           *      <Step by step how to validate this change>
           *      
           * Test Conditions:
           *      <List the different test conditions to make>
           *      NumberDistance should be <= to attacking monster speed because they can only move at max their speed 
           * 
           * Validation:
           *      <List how to validate this change>
           *      Create 1 monster, 1 character
           *      Make monster speed higher 
           *      Set monster and character spaces on the map
           *      Calculate closest empty space from monster to character based on monster speed
           *      Result should be a number < = monster speed 
           *  
           */

            // Arrange
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            // Add a slow character so that monsters can go first 
            var CharacterPlayerMike = new PlayerInfoModel(
                        new CharacterModel
                        {
                            Speed = -1, // Will go last...
                            Level = 1,
                            CurrentHealth = 1,
                            ExperienceTotal = 1,
                            ExperienceRemaining = 1,
                            Name = "Mike",
                        });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Add quicker monsters so that monsters can go first & we know their speed 
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyMonsters = 1;


            var MonsterPlayerMelissa = new PlayerInfoModel(
                        new MonsterModel
                        {
                            Speed = 3, // Will go second 
                            Level = 1,
                            CurrentHealth = 10,
                            ExperienceTotal = 1,
                            ExperienceRemaining = 1,
                            Name = "Melissa",
                        });


            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayerMelissa);
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;


            // Act
            EngineViewModel.Engine.EngineSettings.CurrentAttacker = MonsterPlayerMelissa;
            EngineViewModel.Engine.EngineSettings.CurrentDefender = CharacterPlayerMike;

            var MelissaLocation = new MapModelLocation();
            MelissaLocation.Row = 4;
            MelissaLocation.Column = 3;
            MelissaLocation.IsSelectedTarget = true;

            var MikeLocation = new MapModelLocation();
            MikeLocation.Row = 1;
            MikeLocation.Column = 1;
            var distance = EngineViewModel.Engine.EngineSettings.MapModel.ReturnClosestEmptyLocationSpeed(MikeLocation, MelissaLocation, MonsterPlayerMelissa);
            var numberdistance = EngineViewModel.Engine.EngineSettings.MapModel.CalculateDistance(MelissaLocation, distance);


            // Reset

            // Assert
            Assert.IsTrue(numberdistance <= MonsterPlayerMelissa.Speed);


        }
        #endregion Scenario4

        #region Scenario16
        [Test]
        public void HackathonScenario16_Valid_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #16
            *      
            * Description: 
            *      When toggle the switch to Slow is the new fast, the order of player will be changed. The slowest go first
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Set up the switch in BattleSettingPage
            *      Add new property to keep track of the event in BattleSettingModel
            *      Add method interface in IRoundEngineInterfacr
            *      Add new method to return the list of slowest first in TurnEngine
            *      Also add method in RoundEngineBase to inherit from IRoundEngineInterface
            *      
            *      
            *      
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            *      Set the maximum party of character and monster to be 1
            *      Create the new player with slowest speed as possible
            *      Create another player with fastest speed
            *      Toggle the event switch in BattleSettingPage
            *      Check if the slowest is at index 0 in playerList
            *      
            *      
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *      Check if the slowest is at index 0 in playerList
            *      
            *  
            */

            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go first in this case
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            var MonsterPlayerAnakin = new PlayerInfoModel(new MonsterModel
            {
                Speed = 20, // Will go last in this case
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Anakin",
            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayerAnakin);

            // Act
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.SlowIsTheNewFast = true;
            BattleEngineViewModel.Instance.Engine.Round.NewRound();

            var result = EngineViewModel.Engine.EngineSettings.PlayerList[0];

            // Reset

            // Assert

            Assert.AreEqual(CharacterPlayerMike.Name, result.Name);
        }

        #endregion Scenario16
    }
}