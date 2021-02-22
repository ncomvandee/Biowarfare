﻿using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using System.Linq;

namespace UnitTests.Views
{
    [TestFixture]
    public class CellItemPageTests : CellItemPage
    {
        App app;
        CellItemPage page;
        public  GenericViewModel<CharacterModel> Model;
        public CellItemPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CellItemPage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CellItemPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CellItemPage_UpdatePageBidingContext_Should_Return_True()
        {
            // Arrange

            // Act
            var result = page.UpdatePageBindingContext();

            // Reset

            // Assert
            Assert.IsTrue(result); // Got to here, so it happened...
        }

        [Test]
        public void CellItemPage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellItemPage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.CancelButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellItemPage_LocationPicker_SelectedItem_Null_Should_Skip()
        {
            // Arrange

            // Make a new Character to use for the Picker Tests
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = 10
            };

            var control = (Picker)page.FindByName("LocationPicker");
            control.SelectedItem = null;

            // Act
            page.LocationPicker_Changed(control, null);


            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellItemPage_LocationPicker_SelectedItem_Not_Null_Should_Pass()
        {
            // Arrange

            // Make a new Character to use for the Picker Tests
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = 10
            };

            //Make new item
            var dataTest = new ItemModel { Name = "test" , Location= ItemLocationEnum.PrimaryHand};

            var control = (Picker)page.FindByName("LocationPicker");
            control.SelectedItem = dataTest;

            // Act
            page.LocationPicker_Changed(control, null);


            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellItemPage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}