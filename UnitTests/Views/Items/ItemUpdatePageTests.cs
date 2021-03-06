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
    public class ItemUpdatePageTests : ItemUpdatePage
    {
        App app;
        ItemUpdatePage page;

        public ItemUpdatePageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemUpdatePage(new GenericViewModel<ItemModel>(new ItemModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_OnCatagoryChange_Valid_Null_Should_Pass()
        {
            // Arrange
            page.FindByName<Picker>("ItemCatagoryPicker").SelectedItem = null;

            // Act
            page.OnCatagoryChange(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ItemUpdatePage_OnCatagoryChange_Valid_Information_Should_Pass()
        {
            // Arrange
            page.FindByName<Picker>("ItemCatagoryPicker").SelectedItem = "Face Mask";

            // Act
            page.OnCatagoryChange(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ItemUpdatePage_ValidateInfo_Empty_Name_Should_Not_Pass()
        {
            // Arrange
            page.FindByName<Entry>("NameEntry").Text = "";

            // Act
            var result = page.ValidateInfo();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ItemUpdatePage_ValidateInfo_Invalid_Unselected_ItemCatagory_Should_Not_Pass()
        {
            // Arrange
            page.FindByName<Picker>("AttributePicker").SelectedIndex = 1;
            page.FindByName<Picker>("ItemCatagoryPicker").SelectedIndex = -1;
            page.FindByName<Entry>("NameEntry").Text = "Mjonir";

            // Act
            var result = page.ValidateInfo();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ItemUdatePage_ValidateInfo_valid_Information_Should_Pass()
        {
            // Arrange
            page.FindByName<Picker>("AttributePicker").SelectedIndex = 1;
            page.FindByName<Picker>("ItemCatagoryPicker").SelectedIndex = 1;
            page.FindByName<Entry>("NameEntry").Text = "Mjonir";

            // Act
            var result = page.ValidateInfo();
            
            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void ItemUpdatePage_OnSliderChange_Valid_Default_Should_Pass()
        {
            // Arrange

            double OldVal = 2.0;
            double NewVal = 4.0;

            var arg = new ValueChangedEventArgs(OldVal, NewVal);

            // Act
            page.OnSliderChange(null, arg);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ItemUpdatePage_SaveClicked_Valid_Information_Should_Pass()
        {
            // Arrange
            page.FindByName<Picker>("AttributePicker").SelectedIndex = 1;
            page.FindByName<Picker>("ItemCatagoryPicker").SelectedIndex = 1;
            page.FindByName<Entry>("NameEntry").Text = "Mjonir";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        //[Test]
        //public void ItemUpdatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new ItemModel();
        //    var ViewModel = new GenericViewModel<ItemModel>(data);

        //    page = new ItemUpdatePage(ViewModel);
        //    double oldValue = 0.0;
        //    double newValue = 1.0;

        //    var args = new ValueChangedEventArgs(oldValue, newValue);

        //    // Act
        //    page.Value_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void ItemUpdatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new ItemModel();
        //    var ViewModel = new GenericViewModel<ItemModel>(data);

        //    page = new ItemUpdatePage(ViewModel);
        //    double oldRange = 0.0;
        //    double newRange = 1.0;

        //    var args = new ValueChangedEventArgs(oldRange, newRange);

        //    // Act
        //    page.Range_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void ItemUpdatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new ItemModel();
        //    var ViewModel = new GenericViewModel<ItemModel>(data);

        //    page = new ItemUpdatePage(ViewModel);
        //    double oldDamage = 0.0;
        //    double newDamage = 1.0;

        //    var args = new ValueChangedEventArgs(oldDamage, newDamage);

        //    // Act
        //    page.Damage_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}
    }
}