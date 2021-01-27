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
        // Not specified
        BCell = 0,    

        // Fighters hit hard and have fight abilities
        KillerTCell = 10,

        // Clerics defend well and have buff abilities
        NKCell = 12,

        Macrophage = 15,

        Basophil = 19,

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