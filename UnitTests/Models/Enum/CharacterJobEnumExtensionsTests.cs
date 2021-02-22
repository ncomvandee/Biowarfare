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
