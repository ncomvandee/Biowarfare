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
    public class ItemCreatePageTests : ItemCreatePage
    {
        App app;
        ItemCreatePage page;

        public ItemCreatePageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void ItemCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_ValidateInfo_Invalid_Unselected_Attribute_Should_Not_Pass()
        {
            // Arrange
            page = new ItemCreatePage();

            page.FindByName<Picker>("AttributePicker").SelectedIndex = -1;
            page.FindByName<Entry>("NameEntry").Text = "Mjonir";

            // Act
            var result = page.ValidateInfo();

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ItemCreatePage_ValidateInfo_Invalid_Unselected_ItemCatagory_Should_Not_Pass()
        {
            // Arrange
            page = new ItemCreatePage();

            page.FindByName<Picker>("AttributePicker").SelectedIndex = 1;
            page.FindByName<Picker>("ItemCatagoryPicker").SelectedIndex = -1;
            page.FindByName<Entry>("NameEntry").Text = "Mjonir";

            // Act
            var result = page.ValidateInfo();

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ItemCreatePage_ValidateInfo_valid_Information_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();

            page.FindByName<Picker>("AttributePicker").SelectedIndex = 1;
            page.FindByName<Picker>("ItemCatagoryPicker").SelectedIndex = 1;
            page.FindByName<Entry>("NameEntry").Text = "Mjonir";

            // Act
            var result = page.ValidateInfo();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void ItemCreatePage_OnCatagoryChange_Valid_Null_Should_Pass()
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
        public void ItemCreatePage_OnSliderChange_Valid_Default_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();

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
        public void ItemCreatePage_SaveClicked_Valid_Information_Should_Pass()
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
        //public void ItemCreatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange

        //    page = new ItemCreatePage();
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
        //public void ItemCreatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange

        //    page = new ItemCreatePage();
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
        //public void ItemCreatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    page = new ItemCreatePage();
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