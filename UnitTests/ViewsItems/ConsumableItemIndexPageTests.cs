using NUnit.Framework;

using Game;
using Game.Views;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Game.ViewModels;
using System.Threading.Tasks;
using Game.Views.Items;

namespace UnitTests.ViewsItems
{
    [TestFixture]
    public class ConsumableItemIndexPageTests : ConsumableItemIndexPage
    {
        App app;
        ConsumableItemIndexPage page;

        public ConsumableItemIndexPageTests() : base(true) { }
        
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ConsumableItemIndexPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ConsumableItemIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ConsumableItemIndexPage_OnItemSelected_Clicked_Default_Should_Pass()
        {
            // Arrange

            var selectedItem = new ItemModel();

            var selectedItemChangedEventArgs = new SelectedItemChangedEventArgs(selectedItem, 0);

            // Act
            page.OnItemSelected(null, selectedItemChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


    }
}