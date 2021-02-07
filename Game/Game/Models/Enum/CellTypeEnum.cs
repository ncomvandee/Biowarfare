using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum CellTypeEnum
    {
        // White blood cell. Special ability, provide damage imumunity to friendly ally
        BCell = 0,    

        // White blood cell. Special abilities, dealth highest damage and +5 attack
        KillerTCell = 10,

        // Lymphocyte. Special ability, +10 speed buff
        NKCell = 12,

        // White blood cell. Special ability, increase friendly ally +5 defense when alive
        Macrophage = 15,

        // White blood cell. Special ability, +10 hp
        Basophil = 19,

        // White blood cell. Specical ability, +10 attack if monster is parasite
        Eosinophil = 21,

    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CellTypeEnumExtension
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CellTypeEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case CellTypeEnum.KillerTCell:
                    Message = "Fighter";
                    break;

                case CellTypeEnum.NKCell:
                    Message = "Cleric";
                    break;

                case CellTypeEnum.BCell:
                default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Get the Cell type list
    /// </summary>
    public static class CharacterJobEnumHelper
    {
        public static List<string> GetCharacterJobList
        {
            get
            {   
                // List of all Cell type
                var myList = Enum.GetNames(typeof(CellTypeEnum)).ToList();

                return myList;
            }
        }
    }
}