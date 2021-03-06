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
    public class CellCreatePageTests : CellCreatePage
    {
        App app;
        CellCreatePage page;

        public CellCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            //page = new CharacterCreatePage(new GenericViewModel<CharacterModel>(new CharacterModel()));
            page = new CellCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CellCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CellCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.CancelButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellCreatePage_SaveButtonClicked_With_Valid_Info_Should_Pass()
        {
            // Arrange
            page = new CellCreatePage();

            var SetUpPicker = page.FindByName<Picker>("CellTypePicker");
            SetUpPicker.SelectedIndex = 2;

            var SetUpNameEntry = page.FindByName<Entry>("NameEntry");
            SetUpNameEntry.Text = "Ken";

            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);


        }

        [Test]
        public void CellCreatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellCreatePage_AddItemClicked_Valid_Should_Pass()
        {
            // Arrange

            // Act
            page.AddItem_Clicked(null, null);

            // Reset

            //Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellCreatePage_CheckValidInfo_Invalid_Unselected_Picker_Should_Not_Pass()
        {
            // Arrange
            var SetUpPicker = page.FindByName<Picker>("CellTypePicker");
            SetUpPicker.SelectedIndex = -1;

            var SetUpNameEntry = page.FindByName<Entry>("NameEntry");
            SetUpNameEntry.Text = "Ken";

            // Act
            var result = page.CheckValidInfo();

            // reset

            //Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CellCreatePage_CheckValidInfo_Valid_Info_Default_Should_Pass()
        {
            // Arrange
            var SetUpPicker = page.FindByName<Picker>("CellTypePicker");
            SetUpPicker.SelectedIndex = 2;

            var SetUpNameEntry = page.FindByName<Entry>("NameEntry");
            SetUpNameEntry.Text = "Ken";

            // Act
            var result = page.CheckValidInfo();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CellCreatePage_LevelDownButtonClicked_Valid_Should_Pass()
        {
            // Arrange
            page = new CellCreatePage();
            page.ViewModel.Data.Level = 1;

            // Act
            page.LevelDownButtonClicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellCreatePage_LevelUpButtonClicked_Valid_Should_Pass()
        {
            // Arrange
            page = new CellCreatePage();
            page.ViewModel.Data.Level = 20;

            // Act
            page.LevelUpButtonClicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellCreatePage_OnSliderChanged_AttackSlider_Valid_Default_Should_Pass()
        {
            // Arrange
            page = new CellCreatePage();

            var AttackSlider = page.FindByName<Slider>("AttackSlider");

            double OldVal = 2.0;
            double NewVal = 5.0;

            var arg = new ValueChangedEventArgs(OldVal, NewVal);

            // Act
            page.OnSliderChanged(AttackSlider, arg);

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellCreatePage_OnSliderChanged_DefenseSlider_Valid_Default_Should_Pass()
        {
            // Arrange
            page = new CellCreatePage();

            var DefenseSlider = page.FindByName<Slider>("DefenseSlider");

            double OldVal = 2.0;
            double NewVal = 5.0;

            var arg = new ValueChangedEventArgs(OldVal, NewVal);

            // Act
            page.OnSliderChanged(DefenseSlider, arg);

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void CellCreatePage_OnSliderChanged_SpeedSlider_Valid_Default_Should_Pass()
        {
            // Arrange
            page = new CellCreatePage();

            var SpeedSlider = page.FindByName<Slider>("SpeedSlider");

            double OldVal = 2.0;
            double NewVal = 5.0;

            var arg = new ValueChangedEventArgs(OldVal, NewVal);

            // Act
            page.OnSliderChanged(SpeedSlider, arg);

            // Assert
            Assert.IsTrue(true);
        }



        //[Test]
        //public void CharacterCreatePage_Attack_OnStepperAttackChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterCreatePage(ViewModel);
        //    double oldAttack = 0.0;
        //    double newAttack = 1.0;

        //    var args = new ValueChangedEventArgs(oldAttack, newAttack);

        //    // Act
        //    page.Attack_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_Speed_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // ArSpeed
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterCreatePage(ViewModel);
        //    double oldSpeed = 0.0;
        //    double newSpeed = 1.0;

        //    var args = new ValueChangedEventArgs(oldSpeed, newSpeed);

        //    // Act
        //    page.Speed_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_Defense_OnStepperDefenseChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterCreatePage(ViewModel);
        //    double oldDefense = 0.0;
        //    double newDefense = 1.0;

        //    var args = new ValueChangedEventArgs(oldDefense, newDefense);

        //    // Act
        //    page.Defense_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_RollDice_Clicked_Default_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data = new CharacterModel();

        //    // Act
        //    page.RollDice_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_ClosePopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_ClosePopup_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_ShowPopup_Default_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data = new CharacterModel();

        //    // Act
        //    page.ShowPopup(ItemLocationEnum.PrimaryHand);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_OnPopupItemSelected_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    var data = new ItemModel();

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);

        //    // Act
        //    page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_OnPopupItemSelected_Clicked_Null_Should_Fail()
        //{
        //    // Arrange

        //    var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

        //    // Act
        //    page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCreatePage_Item_ShowPopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    var item = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

        //    // Act
        //    var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterCratePage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        //{
        //    // Arrange
        //    var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
        //    page.ViewModel.Data.Head = item.Id;
        //    var StackItem = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);
        //    var dataImage = StackItem.Children[0];

        //    // Act
        //    ((ImageButton)dataImage).PropagateUpClicked();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        /* [Test]
         public void CharacterCreatePage_RandomButton_Clicked_Vaid_Should_Pass()
         {
             // Arrange
             page.ViewModel.Data.ImageURI = null;

             // Act
             page.RandomButton_Clicked(null, null);

             // Reset

             // Assert
             Assert.IsTrue(true); // Got to here, so it happened...
         }*/

    }
}