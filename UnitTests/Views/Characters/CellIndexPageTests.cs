using NUnit.Framework;

using Game;
using Game.Views;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Game.ViewModels;
using System.Threading.Tasks;

namespace UnitTests.Views
{
    [TestFixture]
    public class CellIndexPageTests : CellIndexPage
    {
        App app;
        CellIndexPage page;

        public CellIndexPageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CellIndexPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CellIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void CharacterIndexPage_AddCharacter_Clicked_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    page.Add_Clicked(null, null);

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        [Test]
        public void CellIndexPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellIndexPage_OnCharacterSelected_Clicked_Default_Should_Pass()
        {
            // Arrange

            var selectedCharacter = new CharacterModel();

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(selectedCharacter, 0);

            // Act
            page.OnItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellIndexPage_OnCharacterSelected_Clicked_Invalid_Null_Should_Fail()
        {
            // Arrange

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

            // Act
            page.OnItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellIndexPage_OnAppearing_Valid_Should_Pass()
        {
            // Arrange
            CellIndexViewModel ViewModel = CellIndexViewModel.Instance;

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellIndexPage_OnAppearing_Valid_Empty_Should_Pass()
        {
            // Arrange

            CellIndexViewModel ViewModel = CellIndexViewModel.Instance;
            ViewModel.Dataset.Clear();

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CellIndexPage_CreateCell_Clicked_Valid_Should_Pass()
        {
            // Arrange
            // Act
            page.CreateCell_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        
    }
}