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
                    Name = "Vicious Scalpel of Swedish Hospital operation room",
                    Description = "The super sharp operating knife useually use in surgery. The blade is very sharp, it can cut though everything, except cut you out of my mind",
                    ImageURI = "vicious_scalpel_white_bg.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                },
                new ItemModel {
                    Name = "Face Mask of Protection",
                    Description = "A medical grade mask that can protect you from Corona Virus, but cannot protect you from her cuteness.",
                    ImageURI = "face_mask_no_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense,
                },
                new ItemModel {
                    Name = "Stethoscope",
                    Description = "A metal item that monitors the patient’s heartbeat. Boosts a cell’s Defense by default.  ",
                    ImageURI = "stethoscope_white_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense,
                },
                new ItemModel {
                    Name = "Thermometer",
                    Description = "A expensive yet solid item that can be used to check the enemy’s temperature. Boosts a cell’s attack by default.  ",
                    ImageURI = "thermometer_white_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack,
                },
                new ItemModel {
                    Name = "Finger Glove",
                    Description = "A flexible glove to keep the germs away. Adds to a cell’s defense by default.  ",
                    ImageURI = "finger_glove_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.RightFinger,
                    Attribute = AttributeEnum.Defense,
                },
                new ItemModel {
                    Name = "Medical Boots",
                    Description = "Sturdy boots with comfortable insoles add a boost to a cell’s speed by default. ",
                    ImageURI = "boots_no_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed,
                },
                 new ItemModel {
                    Name = "Disinfectant",
                    Description = "A full canister to ward away enemies, boosts a cell’s attack by default. ",
                    ImageURI = "disinfectant_spray_white_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.LeftFinger,
                    Attribute = AttributeEnum.Attack,
                 },

            };

            return datalist;
        }

        public static List<ItemModel> LoadConsumableData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
               
                 new ItemModel {
                    Name = "Antidote",
                    Description = "Cure you from poison ",
                    ImageURI = "antidote_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.Unknown,
                    Attribute = AttributeEnum.Unknown,
                    IsConsumable = true,
                 },

                 new ItemModel {
                    Name = "Adrenaline Syringe",
                    Description = "A syringe that drastically increase a Cell's speed for a turn",
                    ImageURI = "adrenaline_syringe_white_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.Unknown,
                    Attribute = AttributeEnum.Unknown,
                    IsConsumable = true,
                 },

                 new ItemModel {
                    Name = "Band aid",
                    Description = "Heals a Cell a small amount of HP when used",
                    ImageURI = "band_aid_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.Unknown,
                    Attribute = AttributeEnum.Unknown,
                    IsConsumable = true,
                 },

                 new ItemModel {
                    Name = "Gummy Multi-Vitamin",
                    Description = "A delicious vitamin that boosts a Cell's defense",
                    ImageURI = "gummy_multivitamin_white_bg.png",
                    Value = 9,
                    Location = ItemLocationEnum.Unknown,
                    Attribute = AttributeEnum.Unknown,
                    IsConsumable = true,
                 },

            };

            //for (int i = 0; i < 20; i++)
            //{
            //    var item = new ItemModel
            //    {
            //        ImageURI = "item.png",
            //        Range = 2,
            //        Damage = 10,
            //        Value = 9,
            //        Location = ItemLocationEnum.PrimaryHand,
            //        Attribute = AttributeEnum.Attack
            //    };
            //    item.Name = "I" + (datalist.Count+1).ToString();
            //    item.Description = item.Name;

            //    datalist.Add(item);
            ////}

            return datalist;
        }



        public static List<ItemModel> LoadUniqueData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {

                 new ItemModel {
                    Name = "Chemo Port Of Pain",
                    Description = "Damage dealt to equipped character is halved, excludes cancer cell.",
                    ImageURI = "chemo_port_of_pain_white_bg.png",
                    Value = 15,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense,
                    IsUnique = true,
                 },


                     new ItemModel {
                        Name = "X Ray Gun",
                        Description = "Pew Pew Pew …",
                        ImageURI = "x_ray_gun_white_bg.png",
                        Value = 16,
                        Range = 6,
                        Location = ItemLocationEnum.PrimaryHand,
                        Attribute = AttributeEnum.Attack,
                        IsUnique = true,
                     },

                     new ItemModel {
                        Name = "MRI Headband",
                        Description = "Shows the character the next round of monsters",
                        ImageURI = "mri_headband_no_bg.png",
                        Value = 14,
                        Location = ItemLocationEnum.Head,
                        Attribute = AttributeEnum.Defense,
                        IsUnique = true,
                     },

                     new ItemModel {
                        Name = "Blood Stone",
                        Description = "Indcrease max health based on the number of enemies defeated",
                        ImageURI = "bloodstone_white_bg.png",
                        Value = 16,
                        Location = ItemLocationEnum.OffHand,
                        Attribute = AttributeEnum.MaxHealth,
                        IsUnique = true,
                     },

                };

            //for (int i = 0; i < 20; i++)
            //{
            //    var item = new ItemModel
            //    {
            //        ImageURI = "item.png",
            //        Range = 2,
            //        Damage = 10,
            //        Value = 9,
            //        Location = ItemLocationEnum.PrimaryHand,
            //        Attribute = AttributeEnum.Attack
            //    };
            //    item.Name = "I" + (datalist.Count+1).ToString();
            //    item.Description = item.Name;

            //    datalist.Add(item);
            ////}


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
        var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.RightFinger);
        var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.LeftFinger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Connie",
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
                    Name = "Missy",
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
                    Name = "Georgie",
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
                    Name = "George",
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
                    Name = "Mary",
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
                    Name = "Sheldon",
                    Job = CellTypeEnum.NKCell,
                    Description = "NK cells are a type of lymphocyte that provides a rapid response to viruses in the body." +
                                  " Has a +10% speed buff.",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "nkcell_bg.png",
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
                    Name = "Pony",
                    Description = "A Spore is a small unicellular entity that typically can invade the body through the respiratory system." +
                                  " Spores have a 25% chance of causing a character to be poisoned if a hit succeeds." +
                                  " When a character is poisoned, it loses 1hp at the beginning of its turn for 5 turns." +
                                  " At the end of 5 turns, the character is cured. Poison does not stack damage and extend duration.",
                    ImageURI = "spore_bg.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Bacteria,
                    Name = "Rusty",
                    Description = "Bacteria are unicellular organisms that can be both beneficial or detrimental to the body." +
                                  " Bacteria has a 25% chance on attack to deal double damage," +
                                  " 50% chance to deal regular damage, or 25% chance to heal its opponent for 50% of attack power.",
                    ImageURI = "bacteria_bg.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Parasite,
                    Name = "Tison",
                    Description = "Parasites are organisms that feed off of a host organism." +
                                  " Parasites heal themselves when they deal damage." +
                                  " For every damage point a Parasite deals to a character," +
                                  " it heals itself for 25% of the total damage dealt.",
                    ImageURI = "parasite_bg.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Virus,
                    Name = "Vector",
                    Description = "A microscopic pathogen that can wreak havoc on the body through rapid replication." +
                                  " A Virus’s attack ignores defense points and deals pure attack damage to a character.",
                    ImageURI = "virus_bg.png",
                },

                new MonsterModel {
                    MonsterType = MonsterTypeEnum.Cancer,
                    Name = "Simon",
                    Description = "Boss enemy: Cancer is a malformed body cell that can rapidly divide and spread quickly." +
                                  " Cancer has a 5% chance to deal lethal damage instantly to one Cell." +
                                  " Cancer has +10% to movement speed.  ",
                    ImageURI = "cancer_bg.png",
                },
            };

        return datalist;
    }
}
}