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
        // Unknown Cell type for the sake of Unit Test
        Unknown = 0,

        // Fighters hit hard and have fight abilities
        Fighter = 10,

        // Clerics defend well and have buff abilities
        Cleric = 12,

        // White blood cell. Special ability, provide damage imumunity to friendly ally
        BCell = 2,    

        // White blood cell. Special abilities, dealth highest damage and +5 attack
        KillerTCell = 30,

        // Lymphocyte. Special ability, +10 speed buff
        NKCell = 42,

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

                case CellTypeEnum.Unknown:
                    Message = "Player";
                    break;
            }

            return Message;
        }

        /// <summary>
        /// Get default description for each Cell
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this CellTypeEnum value)
        {
            // Default String
            var Message = "Immune Cell";

            switch (value)
            {
                case CellTypeEnum.KillerTCell:
                    Message = "Killer T Cells are a type of white blood cell that kill infected," +
                              " damaged, or cancerous cells. Every time the Killer T Cell attacks," +
                              " they will always roll the highest weapon damage. Has +5% attack.";
                    break;

                case CellTypeEnum.NKCell:
                    Message = "NK cells are a type of lymphocyte that provides a rapid response to viruses in the body." +
                              " Has a +10% speed buff.";
                    break;

                case CellTypeEnum.BCell:
                    Message = "B Cells are a specialized white blood cell that secrete antibodies." +
                              " Once per round, B Cells can give any living team member an Immunity token. " +
                              "This token will protect that team member from damage on one turn," +
                              " if are to be dealt damage that turn. Instead," +
                              " the Immunity token is spent and no damage is taken for that team member.";
                    break;

                case CellTypeEnum.Macrophage:
                    Message = "Macrophages are a type of white blood cell that seek out and dispose of foreign invaders and non-healthy cells in their path." +
                              " Macrophages are unique because they recruit other immune cells to fight alongside them." +
                              " Having an active Macrophage in your immune system will increase all friendly characters by +5% defense power." +
                              " Has a 5% personal defense buff.  ";
                    break;

                case CellTypeEnum.Eosinophil:
                    Message = "Eosinophil are a type of white blood cell that specialize in attacking parasites." +
                              " Eosinophil have a 10% attack buff when fighting against invaders of the parasite type.";
                    break;

                case CellTypeEnum.Basophil:
                    Message = "Basophils are a type of white blood cell that are responsible for causing inflammatory reactions and producing histamine." +
                              " Has a +10% hp buff.";
                    break;
            }

            return Message;
        }

        /// <summary>
        /// Get Cell's image per specific CellType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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