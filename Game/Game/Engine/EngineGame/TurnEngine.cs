using System.Collections.Generic;

using Game.Models;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Engine.EngineBase;
using System.Diagnostics;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;

namespace Game.Engine.EngineGame
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     * 
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : TurnEngineBase, ITurnEngineInterface
    {
        #region Algrorithm
        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool TakeTurn(PlayerInfoModel Attacker)
        {
           //return base.TakeTurn(Attacker);

            // Choose Action.Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            bool result = false;


            //check for posion character
            foreach (var data in EngineSettings.CharacterList)
            {
                var poison = data.CheckPoison();
                if (poison)
                {
                    EngineSettings.BattleMessagesModel.TurnMessage = GetPronounce(data) + data.Name + "\"" + "Get Poison Damge, Remaining Health " + data.CurrentHealth;
                    Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);
                }


                RemoveIfDead(data);

                if(data.CurrentHealth==0 && data == Attacker)
                {
                    return result;
                }
            }
            // If the action is not set, then try to set it or use Attact
            if (EngineSettings.CurrentAction == ActionEnum.Unknown)
            {
                // Set the action if one is not set
                EngineSettings.CurrentAction = DetermineActionChoice(Attacker);

                // When in doubt, attack...
                if (EngineSettings.CurrentAction == ActionEnum.Unknown)
                {
                    EngineSettings.CurrentAction = ActionEnum.Attack;
                }
            }

            switch (EngineSettings.CurrentAction)
            {
                //case ActionEnum.Unknown:
                //    // Action already happened
                //    break;

                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                case ActionEnum.Ability:
                    result = UseAbility(Attacker);
                    break;

                case ActionEnum.Move:
                    result = MoveAsTurn(Attacker);
                    break;

                case ActionEnum.UseConsumableItem:
                    result = UseConsumableItem(Attacker);
                    break;
            }

            EngineSettings.BattleScore.TurnCount++;

            // Save the Previous Action off
            EngineSettings.PreviousAction = EngineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            EngineSettings.CurrentAction = ActionEnum.Unknown;

            return result;
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            // If the action is not set, then try to set it or use Attact

            // Based on the current action...

            // Increment Turn Count so you know what turn number

            // Save the Previous Action off

            // Reset the Action to unknown for next time

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Determine what Actions to do
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override ActionEnum DetermineActionChoice(PlayerInfoModel Attacker)
        {
            return base.DetermineActionChoice(Attacker);
            // If it is the characters turn, and NOT auto battle, use what was sent into the engine

            /*
             * The following is Used for Monsters, and Auto Battle Characters
             * 
             * Order of Priority
             * If can attack Then Attack
             * Next use Ability or Move
             */

            // Assume Move if nothing else happens

            // Check to see if ability is avaiable

            // See if Desired Target is within Range, and if so attack away

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        public override bool MoveAsTurn(PlayerInfoModel Attacker)
        {

            /*
             * TODO: TEAMS Work out your own move logic if you are implementing move
             * 
             * Mike's Logic
             * The monster or charcter will move to a different square if one is open
             * Find the Desired Target
             * Jump to the closest space near the target that is open
             * 
             * If no open spaces, return false
             * 
             */

            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                // For Attack, Choose Who
                EngineSettings.CurrentDefender = AttackChoice(Attacker);

                if (EngineSettings.CurrentDefender == null)
                {
                    return false;
                }

                // Get X, Y for Defender
                var locationDefender = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentDefender);
                if (locationDefender == null)
                {
                    return false;
                }

                var locationAttacker = EngineSettings.MapModel.GetLocationForPlayer(Attacker);
                if (locationAttacker == null)
                {
                    return false;
                }

                // Find Location Nearest to Defender that is Open.

                // Get the Open Locations                Was: ReturnEmptyLocation(locationDefender); 
                var openSquare = EngineSettings.MapModel.ReturnClosestEmptyLocationSpeed(locationDefender, locationAttacker, Attacker);

                Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, openSquare.Column, openSquare.Row));
                string line = string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, openSquare.Column, openSquare.Row); 

                EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + " (Speed is: " + Attacker.Speed + ") moves " + line + " closer to " + EngineSettings.CurrentDefender.Name;

                return EngineSettings.MapModel.MovePlayerOnMap(locationAttacker, openSquare);
            }

            return true;
        }

        /// <summary>
        /// Decide to use an Ability or not
        /// 
        /// Set the Ability
        /// </summary>
        public override bool ChooseToUseAbility(PlayerInfoModel Attacker)
        {
            return base.ChooseToUseAbility(Attacker);
            // See if healing is needed.

            // If not needed, then role dice to see if other ability should be used
            // Choose the % chance
            // Select the ability

            // Don't try

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use the Ability
        /// </summary>
        public override bool UseAbility(PlayerInfoModel Attacker)
        {
            return base.UseAbility(Attacker);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use the Ability
        /// </summary>
        public bool UseConsumableItem(PlayerInfoModel Attacker)
        {
          
          throw new System.NotImplementedException();
        }


        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        public override bool Attack(PlayerInfoModel Attacker)
        {
            return base.Attack(Attacker);
            // INFO: Teams, AttackChoice will auto pick the target, good for auto battle

            // Manage autobattle

            // Do Attack

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        public override PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            return base.AttackChoice(data);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        public override PlayerInfoModel SelectCharacterToAttack()
        {

            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Select first in the list

            // Order by who got the biggest Attack damge 
            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                .OrderByDescending(m => m.GetAttackTotal).FirstOrDefault();

            return Defender;
            //return base.SelectCharacterToAttack();

        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        public override PlayerInfoModel SelectMonsterToAttack()
        {
            //return base.SelectMonsterToAttack();

            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 

            // Order by who got the biggest Attack damge 
            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderByDescending(m => m.GetAttackTotal).FirstOrDefault();

            return Defender;

        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        public override bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            EngineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            EngineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            switch (EngineSettings.BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.CriticalMiss:
                    // It's a Critical Miss, so Bad things may happen
                    DetermineCriticalMissProblem(Attacker);

                    break;

                case HitStatusEnum.CriticalHit:
                case HitStatusEnum.Hit:
                    // It's a Hit

                    //Calculate Damage
                    EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    // If critical Hit, double the damage
                    if (EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
                    {
                        EngineSettings.BattleMessagesModel.DamageAmount *= 2;
                    }

                    //Cancer Cell has a cahnce to instant kill 
                    if (Attacker.MonsterType == MonsterTypeEnum.Cancer)
                    {
                        int chance = DiceHelper.RollDice(1, 20);
                        if (chance == 1)
                        {
                            EngineSettings.BattleMessagesModel.DamageAmount = Target.CurrentHealth;
                        }
                    };

                    BeforeApplyDamge(Attacker, Target);

                    // Apply the Damage
                    ApplyDamage(Target);

                    // Check if Dead and Remove
                    RemoveIfDead(Target);

                    AfterApplyDamge(Attacker, Target);

                    EngineSettings.BattleMessagesModel.TurnMessageSpecial = EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();


                    // If it is a character apply the experience earned
                    CalculateExperience(Attacker, Target);

                    break;
            }

            EngineSettings.BattleMessagesModel.TurnMessage = GetPronounce(Attacker) + Attacker.Name + "\"" + EngineSettings.BattleMessagesModel.AttackStatus + GetPronounce(Target) + Target.Name + "\"" + EngineSettings.BattleMessagesModel.TurnMessageSpecial + EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;


            // Set Messages to empty

            // Do the Attack

            // Hackathon
            // ?? Hackathon Scenario ?? 

            // See if the Battle Settings Overrides the Roll

            // Based on the Hit Status, what to do...
            // It's a Miss

            // It's a Hit

            //Calculate Damage

            // Apply the Damage

            // Check if Dead and Remove

            // If it is a character apply the experience earned

            // Battle Message 

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Special Effect after apply damge
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public bool AfterApplyDamge(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if(Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                //Spore applies poison to the target
                if (Attacker.MonsterType == MonsterTypeEnum.Spore)
                {
                    int chance = DiceHelper.RollDice(1, 3);

                    if (chance <= 3)
                    {
                        Target.CausePoison();
                    }
                }


                //Parasite heal 25% of its attack damge 
                if (Attacker.MonsterType == MonsterTypeEnum.Parasite)
                {
                    var chance = DiceHelper.RollDice(1, 2);
                    if (chance == 1)
                    {
                        Attacker.CurrentHealth += EngineSettings.BattleMessagesModel.DamageAmount * 25 / 100;
                        EngineSettings.BattleMessagesModel.TurnMessage = GetPronounce(Attacker) + Attacker.Name + "\"" + " health itself, current health " + Attacker.CurrentHealth;
                        Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);
                    }

                }

            }

            return true;
        }

        /// <summary>
        /// Special damge calculator before deal damage 
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public bool BeforeApplyDamge(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {

            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                //Bacteria has threee random skill: 25% to double damge, 50% deal regular damge, 25% heal opponent
                //Cancer Cell has a cahnce to instant kill 
                if (Attacker.MonsterType == MonsterTypeEnum.Bacteria)
                {
                    int chance = DiceHelper.RollDice(1, 12);

                    //Double the damge
                    if (chance == 1 || chance == 3 || chance == 5)
                    {
                        EngineSettings.BattleMessagesModel.DamageAmount *= 2;
                    }

                    //Health The Target
                    if (chance == 2 || chance == 4 || chance == 6)
                    {
                        //EngineSettings.BattleMessagesModel.DamageAmount /= 2;
                        //Health Target first
                        Target.CurrentHealth += EngineSettings.BattleMessagesModel.DamageAmount / 2;
                    }
                }

            }
            if(Attacker.PlayerType == PlayerTypeEnum.Character) 
            {
                //10% damge against Parasite
                if (Attacker.Job == CellTypeEnum.Eosinophil)
                {
                    if (Target.MonsterType == MonsterTypeEnum.Parasite)
                    {
                        EngineSettings.BattleMessagesModel.DamageAmount += EngineSettings.BattleMessagesModel.DamageAmount * 10 / 100;
                    }
                }
                
            }
           

            return true;
        }

        /// <summary>
        /// Get Cell/Enemy for debug message
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public string GetPronounce(PlayerInfoModel player)
        {
            if (player.PlayerType == PlayerTypeEnum.Character)
            {
                return "Cell \"";
            }

            return "Enemy \"";
        }
        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            return base.BattleSettingsOverride(Attacker);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            return base.BattleSettingsOverrideHitStatusEnum(myEnum);
            // Based on the Hit Status, establish a message

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        public override void ApplyDamage(PlayerInfoModel Target)
        {
            base.ApplyDamage(Target);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        public override HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateAttackStatus(Attacker, Target);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        public override bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateExperience(Attacker, Target);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        public override bool RemoveIfDead(PlayerInfoModel Target)
        {

            return base.RemoveIfDead(Target);
            //if (Target.CurrentHealth == 0)
            //{
            //    Target.Alive = false;
            //}
            //return base.RemoveIfDead(Target);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        public override bool TargetDied(PlayerInfoModel Target)
        {

            return base.TargetDied(Target);
            // Mark Status in output

            // Removing the 

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            // Add the Character to the killed list

            // Add one to the monsters killed count...

            // Add the MonsterModel to the killed list

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        public override int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            var myItemList = new List<ItemModel>();

            if (Target.PlayerType == PlayerTypeEnum.Character)
            {
                // Drop Items to ItemModel Pool
                myItemList = Target.DropAllItems();

            }

            // 50 50 percent chance that the item drop will be equipable or consumable
            int DropChance = DiceHelper.RollDice(1, 2);
            ItemModel data = new ItemModel();

            // Drop equipable
            if (DropChance == 1)
            {
                int index = DiceHelper.RollDice(1, ItemIndexViewModel.Instance.Dataset.Count() - 1);

                // Get the random item from the dataset
                data = ItemIndexViewModel.Instance.Dataset[index];

            }

            // Drop consumable
            if (DropChance == 2)
            {
                int index = DiceHelper.RollDice(1, ConsumableItemIndexViewModel.Instance.Dataset.Count() - 1);

                // Get the random item from dataset
                data = ConsumableItemIndexViewModel.Instance.Dataset[index];

                // If item is consumable, will be picked to emergency kit automatically
                EngineSettings.EmergencyKit.Add(data);
                
            }

            myItemList.Add(data);




            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                EngineSettings.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            EngineSettings.ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            EngineSettings.BattleMessagesModel.DroppedMessage = DroppedMessage;

            EngineSettings.BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        public override HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            return base.RollToHitTarget(AttackScore, DefenseScore);
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        public override List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            return base.GetRandomMonsterItemDrops(round);
            // TODO: Teams, You need to implement your own modification to the Logic cannot use mine as is.

            // You decide how to drop monster items, level, etc.

            // The Number drop can be Up to the Round Count, but may be less.  
            // Negative results in nothing dropped

            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Critical Miss Problem
        /// </summary>
        public override bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return base.DetermineCriticalMissProblem(attacker);
            // throw new System.NotImplementedException();
        }
    }
}