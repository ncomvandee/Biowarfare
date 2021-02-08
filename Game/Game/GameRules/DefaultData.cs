using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "I1",
                    Description = "I1",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I2",
                    Description = "I2",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I3",
                    Description = "I3",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I4",
                    Description = "I4",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I5",
                    Description = "I5",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I6",
                    Description = "I6",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack
                },
            };

            for (int i = 0; i < 20; i++)
            {
                var item = new ItemModel
                {
                    ImageURI = "item.png",
                    Range = 2,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack
                };
                item.Name = "I" + (datalist.Count+1).ToString();
                item.Description = item.Name;

                datalist.Add(item);
            }

            return datalist;
        }

    /// <summary>
    /// Load Example Scores
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    public static List<ScoreModel> LoadData(ScoreModel temp)
    {
        var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

        return datalist;
    }

    /// <summary>
    /// Load Characters
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    public static List<CharacterModel> LoadData(CharacterModel temp)
    {
        var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
        var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
        var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
        var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
        var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
        var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
        var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "C1",
                    Job = CellTypeEnum.Basophil,
                    Description = "Basophils are a type of white blood cell that are responsible for causing inflammatory reactions and producing histamine." +
                                  " Has a +10% hp buff.",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "basophil_bg.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "C2",
                    Job = CellTypeEnum.BCell,
                    Description = "B Cells are a specialized white blood cell that secrete antibodies." +
                                  " Once per round, B Cells can give any living team member an Immunity token. " +
                                  "This token will protect that team member from damage on one turn," +
                                  " if are to be dealt damage that turn. Instead," +
                                  " the Immunity token is spent and no damage is taken for that team member.",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "b_cell_bg.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "C3",
                    Job = CellTypeEnum.Eosinophil,
                    Description = "Eosinophil are a type of white blood cell that specialize in attacking parasites." +
                                  " Eosinophil have a 10% attack buff when fighting against invaders of the parasite type.",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "eosinophil_bg.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "C4",
                    Job = CellTypeEnum.KillerTCell,
                    Description = "Killer T Cells are a type of white blood cell that kill infected," +
                                  " damaged, or cancerous cells. Every time the Killer T Cell attacks," +
                                  " they will always roll the highest weapon damage. Has +5% attack.",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "t_cell_bg.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "C5",
                    Job = CellTypeEnum.Macrophage,
                    Description = "Macrophages are a type of white blood cell that seek out and dispose of foreign invaders and non-healthy cells in their path." +
                                  " Macrophages are unique because they recruit other immune cells to fight alongside them." +
                                  " Having an active Macrophage in your immune system will increase all friendly characters by +5% defense power" +
                                  ". Has a 5% personal defense buff.  ",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "macrophage_bg.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "C6",
                    Job = CellTypeEnum.NKCell,
                    Description = "NK cells are a type of lymphocyte that provides a rapid response to viruses in the body." +
                                  " Has a +10% speed buff.",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

            };

        return datalist;
    }

    /// <summary>
    /// Load Characters
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    public static List<MonsterModel> LoadData(MonsterModel temp)
    {
        var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Spore,
                    Name = "M1",
                    Description = "M1",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Bacteria,
                    Name = "M2",
                    Description = "M2",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Parasite,
                    Name = "M3",
                    Description = "M3",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Virus,
                    Name = "M4",
                    Description = "M4",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Cancer,
                    Name = "M5",
                    Description = "M5",
                    ImageURI = "item.png",
                },
            };

        return datalist;
    }
}
}