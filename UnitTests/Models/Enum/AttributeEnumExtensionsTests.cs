using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class AttributeEnumExtensionsTests
    {
        [Test]
        public void AttributeEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Unknown", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Attack_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Attack.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Attack", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_CurrentHealth_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.CurrentHealth.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Current Health", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Defense_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Defense.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Defense", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_MaxHealth_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.MaxHealth.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Max Health", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Speed_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Speed.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Speed", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToAbbrivation_Attack_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Attack.ToAbbrivation();

            // Reset

            // Assert
            Assert.AreEqual("ATK", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToAbbrivation_Defense_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Defense.ToAbbrivation();

            // Reset

            // Assert
            Assert.AreEqual("DEF", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToAbbrivation_Speed_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Speed.ToAbbrivation();

            // Reset

            // Assert
            Assert.AreEqual("SPD", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToAbbrivation_MaxHealth_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.MaxHealth.ToAbbrivation();

            // Reset

            // Assert
            Assert.AreEqual("HP", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToImage_Attack_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Attack.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("attack_icon.png", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToImage_Defense_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Defense.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("defense_icon.png", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToImage_Speed_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Speed.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("speed_icon.png", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToImage_MaxHealth_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.MaxHealth.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("heart_icon.png", result);
        }

        [Test]
        public void AttributeEnumExtensionTests_ToImage_CurrentHealth_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.CurrentHealth.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("heart_icon.png", result);
        }
    }
}
