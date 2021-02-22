using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class CharacterJobEnumExtensionsTests
    {
        [Test]
        public void CharacterJobEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Player", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToMessage_Killer_T_Cell_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.KillerTCell.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Killer-T-Cell", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToMessage_NK_Cell_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.NKCell.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("NK-Cell", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToMessage_Eosinophil_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Eosinophil.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Eosinophil", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToMessage_Basophil_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Basophil.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Basophil", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToDescription_Killer_T_Cell_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.KillerTCell.ToDescription();

            // Reset

            // Assert
            Assert.AreEqual("Killer T Cells are a type of white blood cell that kill infected, " +
                              "damaged, or cancerous cells. Every time the Killer T Cell attacks," +
                              " they will always roll the highest weapon damage. Has +5% attack.", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToDescription_NK_Cell_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.NKCell.ToDescription();

            // Reset

            // Assert
            Assert.AreEqual("NK cells are a type of lymphocyte that provides a rapid response to viruses in the body." +
                              " Has a +10% speed buff.", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToDescription_Macrophage_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Macrophage.ToDescription();

            // Reset

            // Assert
            Assert.AreEqual("Macrophages are a type of white blood cell that seek out and dispose of foreign invaders and non-healthy cells in their path." +
                              " Macrophages are unique because they recruit other immune cells to fight alongside them." +
                              " Having an active Macrophage in your immune system will increase all friendly characters by +5% defense power." +
                              " Has a 5% personal defense buff.  ", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToDescription_Eosinophil_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Eosinophil.ToDescription();

            // Reset

            // Assert
            Assert.AreEqual("Eosinophil are a type of white blood cell that specialize in attacking parasites." +
                              " Eosinophil have a 10% attack buff when fighting against invaders of the parasite type.", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToDescription_Basophil_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Basophil.ToDescription();

            // Reset

            // Assert
            Assert.AreEqual("Basophils are a type of white blood cell that are responsible for causing inflammatory reactions and producing histamine." +
                              " Has a +10% hp buff.", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToImage_Killer_T_Cell_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.KillerTCell.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("t_cell_bg.png", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToImage_NK_Cell_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.NKCell.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("nkcell_bg.png", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToImage_Macrophage_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Macrophage.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("macrophage_bg.png", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToImage_Eosinophil_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Eosinophil.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("eosinophil_bg.png", result);
        }

        [Test]
        public void CellTypeEnumExtensionTests_ToImage_Basophil_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CellTypeEnum.Basophil.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("basophil_bg.png", result);
        }

        //[Test]
        //public void CharacterJobEnumExtensionsTests_Fighter_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    var result = CharacterJobEnum.Fighter.ToMessage();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual("Fighter", result);
        //}

        //[Test]
        //public void CharacterJobEnumExtensionsTests_Cleric_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    var result = CharacterJobEnum.Cleric.ToMessage();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual("Cleric", result);
        //}
    }
}
