using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Game.Models;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Game.ViewModels;

namespace UnitTests.Views
{
    [TestFixture]
    public class RoundOverPageTests
    {
        App app;
        RoundOverPage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            // For now, set the engine to the Koenig Engine, change when ready 
            BattleEngineViewModel.Instance.SetBattleEngineToGame();
            //.SetBattleToEngineKoenig()
            page = new RoundOverPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void RoundOverPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RoundOverPage_NextButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.CloseButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_AutoAssignButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AutoAssignButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_ShowPopup_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.ShowItemPopup(new ItemModel());

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_CreatePlayerDisplayBox_Null_Should_Pass()
        {
            // Arrange
            // Act
            page.CreatePlayerDisplayBox(null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_GetItemToDisplay_Null_Should_Pass()
        {
            // Arrange
            // Act
            page.GetItemToDisplay(null, false);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_GetItemToDisplay_InValid_Id_Should_Pass()
        {
            // Arrange
            // Act
            page.GetItemToDisplay(new ItemModel { Id = "" }, false);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public async Task RoundOverPage_GetItemToDisplay_Valid_Should_Pass()
        {
            // Arrange
            var data = new ItemModel { Name = "Mike" };
            await ItemIndexViewModel.Instance.CreateAsync(data);

            // Act
            page.GetItemToDisplay(data, false);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawCharacterList_Valid_Should_Pass()
        {
            // Arrange

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.CharacterModelDeathList.Add(new PlayerInfoModel(new CharacterModel()));

            // Draw the Monsters
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Add(new PlayerInfoModel(new CharacterModel()));

            // Do it two times
            page.DrawCharacterList();

            // Act
            page.DrawCharacterList();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawDroppedItems_Valid_Should_Pass()
        {
            // Arrange

            // Draw the Items
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());

            // Draw two times
            page.DrawDroppedItems();

            // Act
            page.DrawDroppedItems();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawItemLists_Valid_Should_Pass()
        {
            // Arrange

            // Draw the Items
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Add(new ItemModel());

            // Draw two times
            page.DrawItemLists();

            // Act  BattleEngineViewModel.Instance.Engine.EngineSettings.
            page.DrawItemLists();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawSelectedItems_Valid_Should_Pass()
        {
            // Arrange

            // Draw the Items
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Add(new ItemModel());

            // Draw two times
            page.DrawSelectedItems();

            // Act
            page.DrawSelectedItems();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            var StackItem = page.GetItemToDisplay(item, true);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_CreatePlayerDisplayBox_Clicked_Should_Pass()
        {
            // Arrange
            var result = page.CreatePlayerDisplayBox(new PlayerInfoModel(new CharacterModel { Name = "test" }));

            var button = result.Children[0];


            // Act

            ((ImageButton)button).PropagateUpClicked();
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_CloseButton_Clicked_Should_Pass()
        {
            // Arrange
            var res = new RoundOverPage(true);



            // Act
            res.CloseButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_ScoreButton_Clicked_Clicked_Should_Pass()
        {
            // Arrange




            // Act
            page.ScoreButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void RoundOverPage_PickItem_Clicked_Clicked_Should_Pass()
        //{
        //    // Arrange




        //    // Act
        //    page.PickItem_Clicked(null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}


        [Test]
        public void RoundOverPage_CloseItemPopup__Clicked_Should_Pass()
        {
            // Arrange


            // Act
            page.CloseItemPopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_ItemAttributeToggle__Clicked_Should_Pass()
        {
            // Arrange


            // Act
            page.ItemAttributeToggle_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_ItemAttributeToggle__Clicked_ItemsFrame_Visible_Should_Pass()
        {
            // Arrange

            // Act
            page.ItemAttributeToggle_Clicked(null, null);
            page.ItemAttributeToggle_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_OnAppearing__Clicked_Should_Pass()
        {
            // Arrange


            // Act
            page.CallOnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void RoundOverPage_UnequippedItem__PrimaryHand_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "test", Location = ItemLocationEnum.PrimaryHand };
            data.AddItem(dataTest.Location, dataTest.Guid);
            //var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            // Act

            page.UnequippedItem(data, dataTest.Location);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_UnequippedItem__Head_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "test", Location = ItemLocationEnum.Head };
            data.AddItem(dataTest.Location, dataTest.Guid);
            //var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            // Act

            page.UnequippedItem(data, dataTest.Location);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_UnequippedItem__Necklass_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "test", Location = ItemLocationEnum.Necklass };
            data.AddItem(dataTest.Location, dataTest.Guid);
            //var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            // Act

            page.UnequippedItem(data, dataTest.Location);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_UnequippedItem__OffHand_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "test", Location = ItemLocationEnum.OffHand };
            data.AddItem(dataTest.Location, dataTest.Guid);
            //var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            // Act

            page.UnequippedItem(data, dataTest.Location);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_UnequippedItem__LeftFinger_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "test", Location = ItemLocationEnum.LeftFinger };
            data.AddItem(dataTest.Location, dataTest.Guid);
            //var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            // Act

            page.UnequippedItem(data, dataTest.Location);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_UnequippedItem__RightFinger_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "test", Location = ItemLocationEnum.RightFinger };
            data.AddItem(dataTest.Location, dataTest.Guid);
            //var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            // Act

            page.UnequippedItem(data, dataTest.Location);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_UnequippedItem__Feet_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "test", Location = ItemLocationEnum.Feet };
            data.AddItem(dataTest.Location, dataTest.Guid);
            //var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            // Act

            page.UnequippedItem(data, dataTest.Location);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}