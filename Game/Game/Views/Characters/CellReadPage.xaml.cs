using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellReadPage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<CharacterModel> ViewModel;

        // Empty Constructor for UTs
        public CellReadPage(bool UnitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public CellReadPage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = data.Data.Name + " Information";

            AddItemToDisplay();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CellDeletePage(ViewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
		/// Jump to the Cell Update Page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void CellEditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CellUpdatePage(ViewModel)));

            await Navigation.PopAsync(); 
        }

        /// <summary>
        /// Flip feature, to toggle between Cell thumbnail and description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowDescriptionClicked (object sender, EventArgs e)
        {

            if (ImageFrame.IsVisible)
            {
                ImageFrame.IsVisible = false;
                DescriptionFrame.IsVisible = true;
                
            } 
            else if (DescriptionFrame.IsVisible)
            {
                DescriptionFrame.IsVisible = false;
                ImageFrame.IsVisible = true;
            }
            
        }

        /// <summary>
        /// Flip feature, toggle betweem item list and attribute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ItemAttributeToggle(object sender, EventArgs e)
        {
            if (ItemsFrame.IsVisible)
            {
                ItemsFrame.IsVisible = false;
                AttributeFrame.IsVisible = true;
                ItemAttributeToggleButton.Text = "Attributes";
            }
            else if (AttributeFrame.IsVisible)
            {
                AttributeFrame.IsVisible = false;
                ItemsFrame.IsVisible = true;
                ItemAttributeToggleButton.Text = "Items";
            }
        }

        public void AddItemToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();

            foreach (var data in FlexList)
            {
                ItemBox.Children.Remove(data);
            }

            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));

        }

        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            var ClickableButton = true;

            var data = ViewModel.Data.GetItemByLocation(location);

            if (data == null)
            {

                return new StackLayout { };

            }

            var ItemButton = new ImageButton
            {
                Source = data.ImageURI,
                WidthRequest = 50,
                HeightRequest = 50,
            };

            if (ClickableButton)
            {

            }

            var ItemLabel = new Label
            {
                Text = data.Name,
                HorizontalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            var ItemStack = new StackLayout
            {
                Padding = 3,
                HorizontalOptions = LayoutOptions.Center,
                
                Children = { ItemButton, ItemLabel },
            };

            return ItemStack;
        }
    }
}