using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.GameRules;
using Game.Models;
using Game.ViewModels;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// AutoBattle Engine
    /// 
    /// Runs the engine simulation with no user interaction
    /// 
    /// </summary>
    public class AutoBattleEngine : AutoBattleEngineBase, IAutoBattleInterface
    {
        #region Algrorithm
        // Prepare for Battle
        // Pick 6 Characters
        // Initialize the Battle
        // Start a Round

        // Fight Loop
        // Loop in the round each turn
        // If Round is over, Start New Round
        // Check end state of round/game

        // Wrap up
        // Get Score
        // Save Score
        // Output Score
        #endregion Algrorithm

        ///// <summary>
        ///// Create new autobattle 
        ///// </summary>
        //public new IBattleEngineInterface Battle
        //{
        //    get
        //    {
        //        if (base.Battle == null)
        //        {
        //            base.Battle = new BattleEngine();
        //        }
        //        return base.Battle;
        //    }
        //    set { base.Battle = Battle; }
        //}

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AutoBattleEngine()
        {
            Battle = new BattleEngine();
        }

        /// <summary>
        /// Override for creating character party in autobattle
        /// </summary>
        /// <returns></returns>
        public override bool CreateCharacterParty()
        {
            //Reset the list

            Battle.EngineSettings.CharacterList =  new List<PlayerInfoModel>();
            // Picks 6 Characters

            // To use your own characters, populate the List before calling RunAutoBattle

            //// Will first pull from existing characters
            foreach (var data in CellIndexViewModel.Instance.Dataset)
            {
                if (Battle.EngineSettings.CharacterList.Count() >= Battle.EngineSettings.MaxNumberPartyCharacters)
                {
                    break;
                }

                // Start off with max health if adding a character in
                data.CurrentHealth = data.GetMaxHealthTotal;
                Battle.PopulateCharacterList(data);
            }

            //If there are not enough will add random ones
            for (int i = Battle.EngineSettings.CharacterList.Count(); i < Battle.EngineSettings.MaxNumberPartyCharacters; i++)
            {
                Battle.PopulateCharacterList(RandomPlayerHelper.GetRandomCharacter(1));
            }

            return true;
            //return base.CreateCharacterParty();
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Override for detecting infinite loops in the autobattle 
        /// </summary>
        /// <returns></returns>
        public override bool DetectInfinateLoop()
        {
            return base.DetectInfinateLoop();
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Override for running the autobattle 
        /// </summary>
        /// <returns></returns>
        public async override Task<bool> RunAutoBattle()
        {
            RoundEnum RoundCondition;

            Debug.WriteLine("Auto Battle Starting");

            // Auto Battle, does all the steps that a human would do.

            // Prepare for Battle

            CreateCharacterParty();

            // Start Battle in AutoBattle mode
            Battle.StartBattle(true);

            // Fight Loop. Continue until Game is Over...
            do
            {
                // Check for excessive duration.
                if (DetectInfinateLoop())
                {
                    Debug.WriteLine("Aborting, More than Max Rounds");
                    Battle.EndBattle();
                    return false;
                }

                Debug.WriteLine("Next Turn");

                // Do the turn...
                // If the round is over start a new one...
                RoundCondition = Battle.Round.RoundNextTurn();

                if (RoundCondition == RoundEnum.NewRound)
                {
                    Battle.Round.NewRound();
                    Debug.WriteLine("New Round");
                }

            } while (RoundCondition != RoundEnum.GameOver);

            Debug.WriteLine("Game Over");

            // Wrap up
            Battle.EndBattle();

            return true;
            // throw new System.NotImplementedException();
        }
    }
}