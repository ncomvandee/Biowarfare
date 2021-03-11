using System.Collections.Generic;
using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Game.GameRules;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Manages the Rounds
    /// </summary>
    public class RoundEngine : RoundEngineBase, IRoundEngineInterface
    {
        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        //// The Turn Engine
        //public new ITurnEngineInterface Turn
        //{
        //    get
        //    {
        //        if (base.Turn == null)
        //        {
        //            base.Turn = new TurnEngine();
        //        }
        //        return base.Turn;
        //    }
        //    set { base.Turn = Turn; }
        //}

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RoundEngine()
        {
            Turn = new TurnEngine();
        }

        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        public override bool ClearLists()
        {
            EngineSettings.ItemPool.Clear();
            EngineSettings.MonsterList.Clear();
            return true;
        }

        /// <summary>
        /// Call to make a new set of monsters..
        /// </summary>
        public override bool NewRound()
        {
            // End the existing round
            EndRound();

            // Remove Character Buffs
            RemoveCharacterBuffs();

            // Populate New Monsters..
            AddMonstersToRound();

            // Make the BaseEngine.PlayerList
            MakePlayerList();

            // Set Order for the Round
            OrderPlayerListByTurnOrder();

            // Populate BaseEngine.MapModel with Characters and Monsters
            EngineSettings.MapModel.PopulateMapModel(EngineSettings.PlayerList);

            // Update Score for the RoundCount
            EngineSettings.BattleScore.RoundCount++;

            return true;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// 
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///   
        /*
            * Hint: 
            * I don't have crudi monsters yet so will add 6 new ones..
            * If you have crudi monsters, then pick from the list

            * Consdier how you will scale the monsters up to be appropriate for the characters to fight
            * 
            */
        /// </summary>
        /// <returns></returns>
        public override int AddMonstersToRound()
        {

            // Random object
            Random rand = new Random();

            // List of monster except Boss cancer
            var MonsterList = MonsterIndexViewModel.Instance.GetMonstersListExceptBoss();

            // Number of monster in MonsterList
            int count = MonsterList.Count();

            // Average Cell level use for calculating monster level
            //int AverageCellLevel = Convert.ToInt32(EngineSettings.CharacterList.Average(m => m.Level));
            int AverageCellLevel = 3;
            // Boss cancer will appear every 5 rounds
            if (EngineSettings.BattleScore.RoundCount > 0 && EngineSettings.BattleScore.RoundCount % 5 == 0)
            {
                // Data for cancer
                var boss = MonsterIndexViewModel.Instance.GetSpecificMonster(MonsterTypeEnum.Cancer);

                EngineSettings.MonsterList.Add(new PlayerInfoModel(boss));


                //hackathon only spaw Boss Cancer cell
                return EngineSettings.MonsterList.Count;
            }

            // Keep adding monster until full
            while (EngineSettings.MaxNumberPartyMonsters > EngineSettings.MonsterList.Count)
            {
                // Index for selecting monster in to list
                int index = rand.Next(0, count);

                // Data for particular monster
                var data = MonsterList[index];

                // Set the level for monster bases on average level of Character party
                int LevelForMonster = rand.Next(AverageCellLevel - 3, AverageCellLevel + 3);

                // If level formonster is lesser than 0 or more than 20, set the level to Cell average level
                if (LevelForMonster <= 1 || LevelForMonster >= 20)
                {
                    LevelForMonster = AverageCellLevel;
                }

                // Set level to monster
                data.Level = LevelForMonster;

                // Modify attribute
                data = RandomPlayerHelper.ModifyMonsterAttributeBaseOnLevel(data);

                // Add to list
                EngineSettings.MonsterList.Add(new PlayerInfoModel(data));

            }

            return EngineSettings.MonsterList.Count;
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        public override bool EndRound()
        {
            // In Auto Battle this happens and the characters get their items, In manual mode need to do it manualy
            if (EngineSettings.BattleScore.AutoBattle)
            {
                PickupItemsForAllCharacters();
            }

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        public override void PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        public override RoundEnum RoundNextTurn()
        {
            //return base.RoundNextTurn();
            // No characters, game is over...
            if (EngineSettings.CharacterList.Count < 1)
            {
                // Game Over
                EngineSettings.RoundStateEnum = RoundEnum.GameOver;
                return EngineSettings.RoundStateEnum;
            }

            // Check if round is over
            if (EngineSettings.MonsterList.Count < 1)
            {
                // If over, New Round
                EngineSettings.RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            if (EngineSettings.BattleScore.AutoBattle)
            {
                // Decide Who gets next turn
                // Remember who just went...
                EngineSettings.CurrentAttacker = GetNextPlayerTurn();

                // Only Attack for now
                EngineSettings.CurrentAction = ActionEnum.Attack;
            }

            // Do the turn....
            Turn.TakeTurn(EngineSettings.CurrentAttacker);

            EngineSettings.RoundStateEnum = RoundEnum.NextTurn;

            return EngineSettings.RoundStateEnum;
            // No characters, game is over..

            // Check if round is over

            // If in Auto Battle pick the next attacker

            // Do the turn..

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        public override PlayerInfoModel GetNextPlayerTurn()
        {
            return base.GetNextPlayerTurn();
            // Remove the Dead

            // Get Next Player

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        public override List<PlayerInfoModel> RemoveDeadPlayersFromList()
        {
            return base.RemoveDeadPlayersFromList();
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public override List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            return base.OrderPlayerListByTurnOrder();
            // TODO Teams: Implement the order

            // throw new System.NotImplementedException();
        }

        public List<PlayerInfoModel> SlowIsTheNewFast()
        {
            EngineSettings.PlayerList = EngineSettings.PlayerList.OrderBy(a => a.GetSpeed())
               .ThenByDescending(a => a.Level)
               .ThenByDescending(a => a.ExperienceTotal)
               .ThenByDescending(a => a.PlayerType)
               .ThenBy(a => a.Name)
               .ThenBy(a => a.ListOrder)
               .ToList();

            return EngineSettings.PlayerList;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public override List<PlayerInfoModel> MakePlayerList()
        {
            return base.MakePlayerList();
            // Start from a clean list of players

            // Remember the Insert order, used for Sorting

            // Add the Characters

            // Add the Monsters

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        public override PlayerInfoModel GetNextPlayerInList()
        {
            return base.GetNextPlayerInList();
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null

            // No current player, so set the first one

            // Find current player in the list

            // If at the end of the list, return the first element

            // Return the next element

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        public override bool PickupItemsFromPool(PlayerInfoModel character)
        {
            return base.PickupItemsFromPool(character);
            // TODO: Teams, You need to implement your own Logic if not using auto apply

            // I use the same logic for Auto Battle as I do for Manual Battle

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        public override bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            return base.GetItemFromPoolIfBetter(character, setLocation);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// </summary>
        public override ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem)
        {
            return base.SwapCharacterItem(character, setLocation, PoolItem);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// For all characters in player list, remove their buffs
        /// </summary>
        public override bool RemoveCharacterBuffs()
        {
            return base.RemoveCharacterBuffs();
            // throw new System.NotImplementedException();
        }
    }
}