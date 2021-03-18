using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Game.ViewModels;
using Game.Models;

namespace UnitTests.Views
{
    [TestFixture]
    public class PickItemsPageTests : PickItemsPage
    {
        App app;
        PickItemsPage page;

        public PickItemsPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            // For now, set the engine to the Koenig Engine, change when ready 
            //BattleEngineViewModel.Instance.SetBattleEngineToKoenig();

            BattleEngineViewModel.Instance.SetBattleEngineToGame();

            page = new PickItemsPage(new GenericViewModel<ItemModel>(new ItemModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void PickItemsPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PickItemsPage_CloseButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.CancelButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellPicker_SelectedIndexChanged_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.CellPicker_SelectedIndexChanged(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_SaveButton_Clicked_Null_Should_Pass()
        {
            // Arrange
            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_Valid_Assigned_Should_Pass()
        {
            // Arrange
            var characterKen = new PlayerInfoModel(new CharacterModel { Name = "Ken" });
            var FaceMask = new ItemModel { Name = "Face Mask of Mine" };

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(characterKen);

            BattleEngineViewModel.Instance.Engine.EngineSettings.ItemPool.Add(FaceMask);

            var SetUpPicker = page.FindByName<Picker>("CellPicker");

            SetUpPicker.SelectedItem = "Ken";

            // Act
            var result = page.AssignItemToCell();

            // Reset

            // Assert

            Assert.IsTrue(result);


        }

        [Test]
        public void PickItemsPage_InValid_Assigned_Should_Fail()
        {
            // Arrange
            var characterKen = new PlayerInfoModel(new CharacterModel { Name = "Ken" });
            var FaceMask = new ItemModel { Name = "Face Mask of Mine" };

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(characterKen);

            BattleEngineViewModel.Instance.Engine.EngineSettings.ItemPool.Add(FaceMask);

            var SetUpPicker = page.FindByName<Picker>("CellPicker");

            SetUpPicker.SelectedItem = "Cloud";

            // Act
            var result = page.AssignItemToCell();

            // Reset

            // Assert

            Assert.IsFalse(result);

        }

        [Test]
        public void PickItemsPage_SaveButton_Clicked_Should_Pass()
        {

            // Arrange
            var characterKen = new PlayerInfoModel(new CharacterModel { Name = "Ken" });
            var FaceMask = new ItemModel { Name = "Face Mask of Mine" };

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(characterKen);

            BattleEngineViewModel.Instance.Engine.EngineSettings.ItemPool.Add(FaceMask);

            var SetUpPicker = page.FindByName<Picker>("CellPicker");

            SetUpPicker.SelectedItem = "Ken";

            // Act

            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}