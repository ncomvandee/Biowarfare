﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ItemReadPageTests : ItemReadPage
    {
        App app;
        ItemReadPage page;

        public ItemReadPageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemReadPage(new GenericViewModel<ItemModel>(new ItemModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemReadPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemReadPage_Update_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ItemEditButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemReadPage_Delete_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Delete_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemReadPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemReadPage_Constructor_New_Item_Is_Consumable_Should_Pass()
        {
            // Arrange


            
            // Act

            var result = new ItemReadPage(new GenericViewModel<ItemModel>(new ItemModel()
            {
                IsConsumable = true
            }));
            // Reset

            // Assert
            Assert.IsNotNull(result); // Got to here, so it happened...
        }


        [Test]
        public void ItemReadPage_Constructor_New_Item_Is_Unique_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemReadPage(new GenericViewModel<ItemModel>(new ItemModel()
            {
                IsUnique = true
            })); 

            // Reset

            // Assert
            Assert.IsNotNull(result); // Got to here, so it happened...
        }

        [Test]
        public void ItemReadPage_ShowDescriptionClicked_ImageFrame_IsVisible_True_Should_False()
        {
            // Arrange
            var imageFrame = (Frame)page.FindByName("ImageFrame");
            imageFrame.IsVisible = true;
            var descriptionFrame = (Label)page.FindByName("DescriptionFrame");
            descriptionFrame.IsVisible = false;
            // Act
            page.ShowDescriptionClicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(descriptionFrame.IsVisible);
            Assert.IsFalse(imageFrame.IsVisible);
        }

        [Test]
        public void ItemReadPage_ShowDescriptionClicked_DescriptionFrame_IsVisible_True_Should_False()
        {
            // Arrange
            var imageFrame = (Frame)page.FindByName("ImageFrame");
            imageFrame.IsVisible = false;
            var descriptionFrame = (Label)page.FindByName("DescriptionFrame");
            descriptionFrame.IsVisible = true;
            // Act
            page.ShowDescriptionClicked(null, null);

            // Reset

            // Assert
            Assert.IsFalse(descriptionFrame.IsVisible);
            Assert.IsTrue(imageFrame.IsVisible);
        }
    }
}