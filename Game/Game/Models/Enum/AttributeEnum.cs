using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of Attributes
    /// Used by Item to specify what it modifies.
    /// </summary>
    public enum AttributeEnum
    {
        // Not specified
        Unknown = 0,    

        // The speed of the character, impacts movement, and initiative
        Speed = 10,

        // The defense score, to be used for defending against attacks
        Defense = 12,

        // The Attack score to be used when attacking
        Attack = 14,

        // Current Health which is always at or below MaxHealth
        CurrentHealth = 16,

        // The highest value health can go
        MaxHealth = 18,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class AttributeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this AttributeEnum value)
        {
            // Default String
            var Message = "Unknown";

            switch (value)
            {
                case AttributeEnum.Attack:
                    Message = "Attack";
                    break;

                case AttributeEnum.CurrentHealth:
                    Message = "Current Health";
                    break;

                case AttributeEnum.Defense:
                    Message = "Defense";
                    break;

                case AttributeEnum.MaxHealth:
                    Message = "Max Health";
                    break;

                case AttributeEnum.Speed:
                    Message = "Speed";
                    break;

                case AttributeEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }

        /// <summary>
        /// Get the abbrivation per specific attribute
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToAbbrivation(this AttributeEnum value)
        {
            var msg = "";

            switch (value)
            {
                case AttributeEnum.Attack:
                    msg = "ATK";
                    break;

                case AttributeEnum.Defense:
                    msg = "DEF";
                    break;

                case AttributeEnum.Speed:
                    msg = "SPD";
                    break;

                case AttributeEnum.MaxHealth:
                    msg = "HP";
                    break;
            }

            return msg;
        }

        /// <summary>
        /// Get Character Color Icon image for attribute
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImage(this AttributeEnum value)
        {
            var msg = "";

            switch (value)
            {
                case AttributeEnum.Attack:
                    msg = "attack_icon.png";
                    break;

                case AttributeEnum.Defense:
                    msg = "defense_icon.png";
                    break;

                case AttributeEnum.Speed:
                    msg = "speed_icon.png";
                    break;

                case AttributeEnum.CurrentHealth:
                    msg = "heart_icon.png";
                    break;

                case AttributeEnum.MaxHealth:
                    msg = "heart_icon.png";
                    break;
            }

            return msg;
        }


        /// <summary>
        /// Get Item color Icon image for attribute
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToItemImage(this AttributeEnum value)
        {
            var msg = "";

            switch (value)
            {
                case AttributeEnum.Attack:
                    msg = "item_attack.png";
                    break;

                case AttributeEnum.Defense:
                    msg = "defense_green.png";
                    break;

                case AttributeEnum.Speed:
                    msg = "speed_green.png";
                    break;

                case AttributeEnum.CurrentHealth:
                    msg = "hp_green.png";
                    break;

                case AttributeEnum.MaxHealth:
                    msg = "hp_green.png";
                    break;

                case AttributeEnum.Unknown:
                    msg = "item_value.png";
                    break;
            }

            return msg;
        }

    }

/// <summary>
/// Helper for the Attribute Enum Class
/// </summary>
public static class AttributeEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Attribute
        /// Removes the attributes that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(AttributeEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Attribute
        /// Removes the unknown
        /// </summary>
        public static List<string> GetListAttributes
        {
            get
            {
                var myList = Enum.GetNames(typeof(AttributeEnum)).ToList().Where(m => m.ToString().Equals("Unknown") == false && m.ToString().Equals("CurrentHealth") == false).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AttributeEnum ConvertStringToEnum(string value)
        {
            return (AttributeEnum)Enum.Parse(typeof(AttributeEnum), value);
        }
    }
}