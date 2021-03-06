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
    public class CellUpdatePageTests : CellUpdatePage
    {
        App app;
        CellUpdatePage page;

        public CellUpdatePageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CellUpdatePage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CellUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CellUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.CancelButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellUpdatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void CellUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellUpdatePage_AddItem_Clicked_Valid_Should_Pass()
        {
            // Arrange

            // Act
            page.AddItem_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void CellUpdatePage_LevelDownButtonClicked_Level_1_Should_Equal_To_1()
        {
            // Arrange
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = 1
            };
            // Act
            page.LevelDownButtonClicked(null, null);
            var result = page.ViewModel.Data.Level;

            // Reset

            // Assert
            Assert.AreEqual(MinLevel, result); // 
        }

        [Test]
        public void CellUpdatePage_LevelUpButtonClicked_Character_MaxLevel_Should_Equal_To_MaxLevel()
        {
            // Arrange
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = MaxLevel
            };
            // Act
            page.LevelUpButtonClicked(null, null);
            var result = page.ViewModel.Data.Level;

            // Reset

            // Assert
            Assert.AreEqual(MaxLevel, result); // 
        }

        [Test]
        public void CellUpdatePage_OnSliderChanged_AttackSlider_Should_Equal_To_1()
        {
            // Arrange
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = MaxLevel
                
            };

            var statText = (Label)page.FindByName("AttackStat");
            var control = (Slider)page.FindByName("AttackSlider");
            ValueChangedEventArgs e = new ValueChangedEventArgs(page.ViewModel.Data.Attack,1);
            // Act
            page.OnSliderChanged(control,e);
            // Reset

            // Assert
            Assert.AreEqual("1", statText.Text); // 
        }

        [Test]
        public void CellUpdatePage_OnSliderChanged_DefenseSlider__Should_Equal_To_1()
        {
            // Arrange
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = MaxLevel
            };
            var statText = (Label)page.FindByName("DefenseStat");

            var control = (Slider)page.FindByName("DefenseSlider");
            ValueChangedEventArgs e = new ValueChangedEventArgs(page.ViewModel.Data.Attack, 1);
            // Act
            page.OnSliderChanged(control, e);
            // Reset

            // Assert
            Assert.AreEqual("1", statText.Text); // 
        }

        [Test]
        public void CellUpdatePage_OnSliderChanged_SpeedSlider__Should_Equal_To_1()
        {
            // Arrange
            page.ViewModel.Data = new CharacterModel()
            {
                Id = "test",
                Level = MaxLevel
            };
            var statText = (Label)page.FindByName("SpeedStat");

            var control = (Slider)page.FindByName("SpeedSlider");
            ValueChangedEventArgs e = new ValueChangedEventArgs(page.ViewModel.Data.Attack, 1);
            // Act
            page.OnSliderChanged(control, e);
            // Reset

            // Assert
            Assert.AreEqual("1", statText.Text); // 
        }

        [Test]
        public void CellUpdatePage_CheckValidInfo_Empty_String_Should_Return_False()
        {
            // Arrange

            var entry = (Entry)page.FindByName("NameEntry");
            entry.Text = "";
            // Act
            var result = page.CheckValidInfo();
            // Reset

            // Assert
            Assert.IsFalse(result); // 
        }

        [Test]
        public void CellUpdatePage_CheckValidInfo_InValid_Type_Picker_Should_Return_False()
        {
            // Arrange

            var picker = (Picker)page.FindByName("CellTypePicker");
            picker.SelectedIndex = -1;
            // Act
            var result = page.CheckValidInfo();
            // Reset

            // Assert
            Assert.IsFalse(result); // 
        }


        //[Test]
        //public void CharacterUpdatePage_Attack_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldValue = 0.0;
        //    double newValue = 1.0;

        //    var args = new ValueChangedEventArgs(oldValue, newValue);

        //    // Act
        //    page.Attack_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_Defense_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldRange = 0.0;
        //    double newRange = 1.0;

        //    var args = new ValueChangedEventArgs(oldRange, newRange);

        //    // Act
        //    page.Defense_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_Speed_OnStepperDamageChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldDamage = 0.0;
        //    double newDamage = 1.0;

        //    var args = new ValueChangedEventArgs(oldDamage, newDamage);

        //    // Act
        //    page.Speed_OnStepperValueChanged(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_Level_Changed_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new CharacterModel();
        //    var ViewModel = new GenericViewModel<CharacterModel>(data);

        //    page = new CharacterUpdatePage(ViewModel);
        //    double oldDamage = 0.0;
        //    double newDamage = 1.0;

        //    var args = new ValueChangedEventArgs(oldDamage, newDamage);

        //    // Act
        //    page.Level_Changed(null, args);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_RollDice_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.RollDice_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_ClosePopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_ClosePopup_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_OnPopupItemSelected_Clicked_Default_Should_Pass()
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
        //public void CharacterUpdatePage_OnPopupItemSelected_Clicked_Null_Should_Fail()
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
        //public void CharacterUpdatePage_Item_ShowPopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    var item = page.GetItemToDisplay(ItemLocationEnum.Head);

        //    // Act
        //    var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

        //    page.ShowPopup(ItemLocationEnum.Head);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        //{
        //    // Arrange
        //    var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.Head);
        //    page.ViewModel.Data.Head = item.Id;
        //    var StackItem = page.GetItemToDisplay(ItemLocationEnum.Head);
        //    var dataImage = StackItem.Children[0];

        //    // Act
        //    ((ImageButton)dataImage).PropagateUpClicked();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}


        //#region ButtonUpDown
        //[Test]
        //public void CharacterUpdatePage_AttackDownButton_Clicked_Valid_1_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Attack = 1;

        //    // Act
        //    page.AttackDownButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_AttackUpButton_Clicked_Valid_1_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Attack = 1;

        //    // Act
        //    page.AttackUpButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_DefenseDownButton_Clicked_Valid_1_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Defense = 1;

        //    // Act
        //    page.DefenseDownButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_DefenseUpButton_Clicked_Valid_1_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Defense = 1;

        //    // Act
        //    page.DefenseUpButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_SpeedDownButton_Clicked_Valid_1_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Speed = 1;

        //    // Act
        //    page.SpeedDownButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_SpeedUpButton_Clicked_Valid_1_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Speed = 1;

        //    // Act
        //    page.SpeedUpButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_AttackDownButton_Clicked_Invalid_0_Should_Fail()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Attack = 0;

        //    // Act
        //    page.AttackDownButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_AttackUpButton_Clicked_Invalid_10_Should_Fail()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Attack = 10;

        //    // Act
        //    page.AttackUpButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_DefenseDownButton_Clicked_Invalid_0_Should_Fail()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Defense = 0;

        //    // Act
        //    page.DefenseDownButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_DefenseUpButton_Clicked_Invalid_10_Should_Fail()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Defense = 10;

        //    // Act
        //    page.DefenseUpButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_SpeedDownButton_Clicked_Invalid_0_Should_Fail()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Speed = 0;

        //    // Act
        //    page.SpeedDownButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void CharacterUpdatePage_SpeedUpButton_Clicked_Invalid_10_Should_Fail()
        //{
        //    // Arrange
        //    page.ViewModel.Data.Speed = 10;

        //    // Act
        //    page.SpeedUpButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}
        //#endregion ButtonUpDown

        //#region UpdateHealth
        //[Test]
        //public void CharacterUpdatePage_UpdateHealthValue_Valid_Default_Should_Pass()
        //{
        //    // Arrange
        //    // Act
        //    page.UpdateHealthValue();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //#endregion UpdateHealth

        //#region RandomButton_Clicked
        //[Test]
        //public void CharacterUpdatePage_RandomButton_Clicked_Valid_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.RandomButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}
        //#endregion RandomButton_Clicked

        //#region LevelPicker_Changed
        //[Test]
        //public void CharacterUpdatePage_LevelPicker_SelectedIndex_Neg1_Should_Return_Level()
        //{
        //    // Arrange

        //    // Make a new Character to use for the Picker Tests
        //    page.ViewModel.Data = new CharacterModel()
        //    {
        //        Id = "test",
        //        Level = 10
        //    };

        //    var control = (Picker)page.FindByName("LevelPicker");
        //    control.SelectedIndex = -1;

        //    // Act
        //    page.LevelPicker_Changed(null, null);
        //    var result = control.SelectedIndex;

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(10, result+1); 
        //}

        //[Test]
        //public void CharacterUpdatePage_LevelPicker_SelectedIndex_No_Change_Should_Skip()
        //{
        //    // Arrange

        //    // Make a new Character to use for the Picker Tests
        //    page.ViewModel.Data = new CharacterModel()
        //    {
        //        Id = "test",
        //        Level = 10
        //    };

        //    var control = (Picker)page.FindByName("LevelPicker");
        //    control.SelectedIndex = 10-1;

        //    // Act
        //    page.LevelPicker_Changed(null, null);
        //    var result = control.SelectedIndex;

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(10, result + 1);
        //}


        //[Test]
        //public void CharacterUpdatePage_LevelPicker_SelectedIndex_Change_Should_Update_Picker_To_Level()
        //{
        //    // Arrange

        //    // Make a new Character to use for the Picker Tests
        //    page.ViewModel.Data = new CharacterModel()
        //    {
        //        Id = "test",
        //        Level = 1
        //    };

        //    var control = (Picker)page.FindByName("LevelPicker");
        //    control.SelectedIndex = 15;

        //    // Act
        //    page.LevelPicker_Changed(null, null);
        //    var result = control.SelectedIndex;

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(16, result + 1);
        //}
        //#endregion LevelPicker_Changed
    }
}