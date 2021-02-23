using NUnit.Framework;

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
    public class MonsterCellCreatePageTests : MonsterCellCreatePage
    {
        App app;
        MonsterCellCreatePage page;

        public MonsterCellCreatePageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            //page = new MonsterCreatePage(new GenericViewModel<MonsterModel>(new MonsterModel()));
            page = new MonsterCellCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.CancelButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.SaveButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void MonsterCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCellCreatePage_OnSliderChanged_DefenseSlider__Should_Equal_To_1()
        {
            // Arrange
            page.ViewModel.Data = new MonsterModel()
            {
                Id = "test",
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
        public void MonsterCellUpdatePage_OnSliderChanged_SpeedSlider__Should_Equal_To_1()
        {
            // Arrange
            page.ViewModel.Data = new MonsterModel()
            {
                Id = "test",

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
        public void MonsterCellCreatePage_CheckValidInfo_Invalid_Unselected_Picker_Should_Not_Pass()
        {
            // Arrange
            var SetUpPicker = page.FindByName<Picker>("MonsterTypePicker");
            SetUpPicker.SelectedIndex = -1;

            var SetUpNameEntry = page.FindByName<Entry>("NameEntry");
            SetUpNameEntry.Text = "Ken";

            // Act
            var result = page.CheckValidInfo();

            // reset

            //Assert
            Assert.AreEqual(false, result);
        }

        //[Test]
        //public void MonsterCreatePage_Attack_OnStepperAttackChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new MonsterModel();
        //    var ViewModel = new GenericViewModel<MonsterModel>(data);

        //    page = new MonsterCreatePage(ViewModel);
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
        //public void MonsterCreatePage_Speed_OnStepperValueChanged_Default_Should_Pass()
        //{
        //    // ArSpeed
        //    var data = new MonsterModel();
        //    var ViewModel = new GenericViewModel<MonsterModel>(data);

        //    page = new MonsterCreatePage(ViewModel);
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
        //public void MonsterCreatePage_Defense_OnStepperDefenseChanged_Default_Should_Pass()
        //{
        //    // Arrange
        //    var data = new MonsterModel();
        //    var ViewModel = new GenericViewModel<MonsterModel>(data);

        //    page = new MonsterCreatePage(ViewModel);
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
        //public void MonsterCreatePage_RollDice_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.RollDice_Clicked(null,null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterCreatePage_ClosePopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterCreatePage_ClosePopup_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ClosePopup_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterCreatePage_ShowPopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.ShowPopup(new ItemModel());

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterCreatePage_OnPopupItemSelected_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    var data = new ItemModel();

        //    var selectedMonsterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);

        //    // Act
        //    page.OnPopupItemSelected(null, selectedMonsterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        ////}

        //[Test]
        //public void MonsterCreatePage_OnPopupItemSelected_Clicked_Null_Should_Fail()
        //{
        //    // Arrange

        //    var selectedMonsterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

        //    // Act
        //    page.OnPopupItemSelected(null, selectedMonsterChangedEventArgs);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterCreatePage_Item_ShowPopup_Default_Should_Pass()
        //{
        //    // Arrange

        //    var item = page.GetItemToDisplay();

        //    // Act
        //    var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterCreatePage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        //{
        //    // Arrange
        //    var StackItem = page.GetItemToDisplay();
        //    var dataImage = StackItem.Children[0];

        //    // Act
        //    ((ImageButton)dataImage).PropagateUpClicked();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void MonsterCreatePage_RandomButton_Clicked_Vaid_Should_Pass()
        //{
        //    // Arrange
        //    page.ViewModel.Data.ImageURI = null;

        //    // Act
        //    page.RandomButton_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}
    }
}