using NUnit.Framework;
using System.Linq;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using System.Threading.Tasks;

namespace UnitTests.Views
{
    [TestFixture]
    public class CellReadPageTests : CellReadPage
    {
        App app;
        CellReadPage page;

        public CellReadPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CellReadPage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CellReadPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CellReadPage_Update_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.CellEditButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_Delete_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Delete_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_GetItemToDisplay_Valid_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemToDisplay(ItemLocationEnum.Feet);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_ShowPopup_Valid_Should_Pass()
        {
            // Arrange

            // Act
            page.ShowPopup(new ItemModel());

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_AddItemsToDisplay_With_Data_Should_Remove_And_Pass()
        {
            // Arrange

            // Put some data into the box so it can be removed
            FlexLayout itemBox = (FlexLayout)page.Content.FindByName("ItemBox");

            itemBox.Children.Add(new Label());
            itemBox.Children.Add(new Label());

            // Act
            page.AddItemToDisplay();

            // Reset

            // Assert
            Assert.AreEqual(7, itemBox.Children.Count()); // Got to here, so it happened...
        }

        [Test]
        public async Task CellReadPage_GetItemToDisplay_With_Item_Should_Pass()
        {
            // Arrange
            ItemIndexViewModel.Instance.Dataset.Clear();
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Location = ItemLocationEnum.PrimaryHand });

            var character = new CharacterModel();
            character.PrimaryHand = ItemIndexViewModel.Instance.GetLocationItems(ItemLocationEnum.PrimaryHand).First().Id;
            page.ViewModel.Data = character;

            // Act
            var result = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

            // Reset
            ItemIndexViewModel.Instance.Dataset.Clear();
            await ItemIndexViewModel.Instance.LoadDefaultDataAsync();

            // Assert
            Assert.AreEqual(2, result.Children.Count()); // Got to here, so it happened...
        }

        [Test]
        public async Task CellReadPage_GetItemToDisplay_With_NoItem_Should_Pass()
        {
            // Arrange
            ItemIndexViewModel.Instance.Dataset.Clear();
            var item = new ItemModel { Location = ItemLocationEnum.PrimaryHand };
            await ItemIndexViewModel.Instance.CreateAsync(item);

            // Act
            var result = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

            // Reset
            ItemIndexViewModel.Instance.Dataset.Clear();
            await ItemIndexViewModel.Instance.LoadDefaultDataAsync();

            // Assert
            Assert.AreEqual(0, result.Children.Count()); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            page.ViewModel.Data.PrimaryHand = item.Id;
            var StackItem = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellReadPage_ShowDescriptionClicked_Valid_When_ImageFrame_IsVisible_Should_Pass()
        {
            // Arrange

            // Act 
            page.ShowDescriptionClicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellReadPage_ShowDescriptionClicked_Valid_When_DescriptionFrame_IsVisible_Should_Pass()
        {
            // Arrange
            var SetUpDescriptionFrame = page.FindByName<Label>("DescriptionFrame");
            SetUpDescriptionFrame.IsVisible = true;

            var SetUpImageFrame = page.FindByName<Frame>("ImageFrame");
            SetUpImageFrame.IsVisible = false;

            // Act 
            page.ShowDescriptionClicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellReadPage_ItemAttributeToggle_Valid_When_ItemFrame_IsVisible_Should_Pass()
        {
            // Arrange
            // No need to set anything because ItemFrame is set to visible at initial

            // Act
            page.ItemAttributeToggle(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellReadPage_ItemAttributeToggle_Valid_When_AttributeFrame_IsVisible_Should_Pass()
        {
            // Arrange
            var SetUpItemFrame = page.FindByName<ScrollView>("ItemsFrame");
            SetUpItemFrame.IsVisible = false;

            var SetUpAttriButeFrame = page.FindByName<Frame>("AttributeFrame");
            SetUpAttriButeFrame.IsVisible = true;

            // Act
            page.ItemAttributeToggle(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}