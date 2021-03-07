using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of Monster
    /// Used in Monster Crudi, and in Battles.
    /// </summary>
    public enum MonsterTypeEnum
    {
        Unknown = 1,

        // 25% chance to poison Cell, reduce 1hp every turn, last for 5 turns
        Spore = 2,

        // 25 % to deal double damage, 50% to deal regular damage and 25% to heal opponent
        Bacteria = 12,

        // Heals itself for 25% from damage dealth 
        Parasite = 23,

        // Deal pure attack damage to opponent 
        Virus = 34,

        // BOSS. +10 speed and 5% to cause opponent instant dead
        Cancer = 45,

    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class MonsterTypeEnumExtension
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this MonsterTypeEnum value)
        {
            // Default String
            var Message = "Monster";

            switch (value)
            {
                case MonsterTypeEnum.Spore:
                    Message = "Spore";
                    break;

                case MonsterTypeEnum.Bacteria:
                    Message = "Bacteria";
                    break;

                case MonsterTypeEnum.Parasite:
                    Message = "Parasite";
                    break;

                case MonsterTypeEnum.Virus:
                    Message = "Virus";
                    break;

                case MonsterTypeEnum.Cancer:
                    Message = "BIG BOSS CANCER";
                    break;
            }

            return Message;
        }

        /// <summary>
        /// Get MonsterCell's image per specific MonsterCellType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImage(this MonsterTypeEnum value)
        {
            var image = "";
            switch (value)
            {
                case MonsterTypeEnum.Spore:
                    image = "spore_no_bg.png";
                    break;

                case MonsterTypeEnum.Bacteria:
                    image = "bacteria_no_bg.png";
                    break;

                case MonsterTypeEnum.Parasite:
                    image = "parasite_no_bg.png";
                    break;

                case MonsterTypeEnum.Virus:
                    image = "virus_no_bg.png";
                    break;

                case MonsterTypeEnum.Cancer:
                    image = "cancer_no_bg.png";
                    break;
            }

            return image;
        }
    }

    /// <summary>
    /// Get the Monster type list
    /// </summary>
    public static class MonsterTypeEnumHelper
    {
        /// <summary>
        /// Returns a list of Monster types 
        /// </summary>
        public static List<string> GetMonsterTypeList
        {
            get
            {   
                // List of all Cell type
                var myList = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();

                return myList;
            }
        }
    }
}