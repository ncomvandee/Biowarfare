using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class ItemLocationEnumExtensionsTests
    {
        [Test]
        public void ItemLocationEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Consumable", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_Head_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.PrimaryHand.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Primary Hand", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_Necklass_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Necklass.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Necklace", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_PrimaryHand_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.PrimaryHand.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Primary Hand", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_OffHand_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.OffHand.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Off Hand", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_RightFinger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.RightFinger.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Right Finger", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_LeftFinger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.LeftFinger.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Left Finger", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_Finger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Finger.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Any Finger", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_Feet_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Feet.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Feet", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToCatagories_Finger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Finger.ToCatagories();

            // Reset

            // Assert
            Assert.AreEqual("Any Finger", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_Head_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Head.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("face_mask_no_bg.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_Necklass_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Necklass.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("stethoscope_white_bg.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_PrimaryHand_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.PrimaryHand.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("vicious_scalpel_white_bg.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_OffHand_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.OffHand.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("thermometer_white_bg.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_RightFinger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.RightFinger.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("finger_glove_bg.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_LeftFinger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.LeftFinger.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("disinfectant_spray_white_bg.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_Feet_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Feet.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("boots_no_bg.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToImage_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Unknown.ToImage();

            // Reset

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToIcon_Head_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Head.ToIcon();

            // Reset

            // Assert
            Assert.AreEqual("head_location.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToIcon_Necklace_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Necklass.ToIcon();

            // Reset

            // Assert
            Assert.AreEqual("necklace_location.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToIcon_OffHand_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.OffHand.ToIcon();

            // Reset

            // Assert
            Assert.AreEqual("off_hand_location.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToIcon_RightFinger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.RightFinger.ToIcon();

            // Reset

            // Assert
            Assert.AreEqual("right_finger_location.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToIcon_LeftFinger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.LeftFinger.ToIcon();

            // Reset

            // Assert
            Assert.AreEqual("left_finger_location.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToIcon_Feet_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Feet.ToIcon();

            // Reset

            // Assert
            Assert.AreEqual("feet_location.png", result);
        }

        [Test]
        public void ItemLocationEnumExtensionsTests_ToIcon_Finger_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnum.Finger.ToIcon();

            // Reset

            // Assert
            Assert.AreEqual("any_finger_location.png", result);
        }
    }
}
