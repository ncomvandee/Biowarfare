﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;

using Game;
using Game.Views;
using Game.Models;
using Game.ViewModels;

namespace UnitTests.Views
{
    [TestFixture]
    public class NewRoundPageTests
    {
        App app;
        NewRoundPage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            // For now, set the engine to the Koenig Engine, change when ready 
            BattleEngineViewModel.Instance.SetBattleEngineToKoenig();

            page = new NewRoundPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void NewRoundPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void NewRoundPage_BeginButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.BeginButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void NewRoundPage_CreatePlayerDisplayBox_Valid_Should_Pass()
        {
            // Arrange
            // Act
            var result = page.CreatePlayerDisplayBox(new PlayerInfoModel(new CharacterModel { Name="test"}));

            // Reset

            // Assert
            Assert.IsNotNull(result); // Got to here, so it happened...
        }

        [Test]
        public void NewRoundPage_CreatePlayerDisplayBox_Null_Should_Pass()
        {
            // Arrange
            // Act
            var result = page.CreatePlayerDisplayBox(null);

            // Reset

            // Assert
            Assert.IsNotNull(result); // Got to here, so it happened...
        }

        [Test]
        public void NewRoundPage_NewRoundPage_CharacterList_MonsterList_Should_Pass()
        {
            // Arrange
            // Act

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList = new List<PlayerInfoModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList = new List<PlayerInfoModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

            var result = new NewRoundPage();

            // Reset

            // Assert
            Assert.IsNotNull(result); // Got to here, so it happened...
        }

        [Test]
        public void NewRoundPage_ShowPopUp_PlayerType_Character_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel();
            data.PlayerType = PlayerTypeEnum.Character;
            // Act

            page.ShowPopup(data);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void NewRoundPage_ShowPopUp_PlayerType_Monster_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel();
            data.PlayerType = PlayerTypeEnum.Monster;
            // Act

            page.ShowPopup(data);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void NewRoundPage_ClosePopup_Clicked_Should_Pass()
        {
            // Arrange
            // Act

            page.ClosePopup_Clicked(null,null);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void NewRoundPage_CreatePlayerDisplayBox_Clicked_Should_Pass()
        {
            // Arrange
            var result = page.CreatePlayerDisplayBox(new PlayerInfoModel(new CharacterModel { Name = "test" }));
            
            var button = result.Content;
     
            
            // Act

            ((ImageButton)button).PropagateUpClicked();
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void NewRoundPage_ItemAttributeToggle_Clicked_Should_Pass()
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
        public void NewRoundPage_AddItemToDisplay_Should_Pass()
        {
            // Arrange
            var cell = new PlayerInfoModel(new CharacterModel());
            var itemBox = (FlexLayout)page.FindByName("ItemBox");
            itemBox.Children.Add(new StackLayout());
            // Act
            page.AddItemToDisplay(cell);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void NewRoundPage_GetItemToDisplay_True_Valid_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel { Name = "test" });
            var dataTest = new ItemModel { Name = "AttackBoots", Location = ItemLocationEnum.PrimaryHand, Attribute = AttributeEnum.Attack, Value = 10, IsConsumable = false, Damage = 10 };
            ItemIndexViewModel.Instance.Dataset.Add(dataTest);
            dataTest.Guid = "test";
            data.AddItem(ItemLocationEnum.PrimaryHand, dataTest.Id);



            // Act
            var result = page.GetItemToDisplay(dataTest.Location, data);
            var ItemData = data.GetItemByLocation(ItemLocationEnum.PrimaryHand);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}