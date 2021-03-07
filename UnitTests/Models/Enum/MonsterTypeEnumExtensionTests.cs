using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class MonsterTypeEnumExtensionTests
    {
        [Test]
        public void MonsterTypeEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Monster", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToMessage_Spore_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Spore.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Spore", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToMessage_Bacteria_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Bacteria.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Bacteria", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToMessage_Parasite_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Parasite.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Parasite", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToMessage_Virus_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Virus.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Virus", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToMessage_Cancer_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Cancer.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("BIG BOSS CANCER", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToImage_Bacteria_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Bacteria.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("bacteria_no_bg.png", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToImage_Parasite_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Parasite.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("parasite_no_bg.png", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToImage_Virus_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Virus.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("virus_no_bg.png", result);
        }

        [Test]
        public void MonsterTypeEnumExtensionTests_ToImage_Cancer_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterTypeEnum.Cancer.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("cancer_no_bg.png", result);
        }

    }
}
