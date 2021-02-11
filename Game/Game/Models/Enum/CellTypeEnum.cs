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
            var Message = "Cell";

            switch (value)
            {
                case CellTypeEnum.KillerTCell:
                    Message = "Killer-T-Cell";
                    break;

                case CellTypeEnum.NKCell:
                    Message = "NK-Cell";
                    break;

                case CellTypeEnum.BCell:
                    Message = "B-Cell";
                    break;

                case CellTypeEnum.Macrophage:
                    Message = "Macrophage";
                    break;

                case CellTypeEnum.Eosinophil:
                    Message = "Eosinophil";
                    break;

                case CellTypeEnum.Basophil:
                    Message = "Basophil";
                    break;
            }

            return Message;
        }

        public static string ToImage(this CellTypeEnum value)
        {
            var image = "";

            switch (value)
            {
                case CellTypeEnum.KillerTCell:
                    image = "t_cell_bg.png";
                    break;

                case CellTypeEnum.NKCell:
                    image = "nkcell_bg.png";
                    break;

                case CellTypeEnum.BCell:
                    image = "b_Cell_bg.png";
                    break;

                case CellTypeEnum.Macrophage:
                    image = "macrophage_bg.png";
                    break;

                case CellTypeEnum.Eosinophil:
                    image = "eosinophil_bg.png";
                    break;

                case CellTypeEnum.Basophil:
                    image = "basophil_bg.png";
                    break;
            }

            return image;
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