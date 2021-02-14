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

            // Displays item Cell equipped
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

        /// <summary>
        /// Displays items Cell equipped as image
        /// </summary>
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

        /// <summary>
        /// Look up items to display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Default clickable button
            var ClickableButton = true;

            // Data as ItemModel per specific item
            var data = ViewModel.Data.GetItemByLocation(location);

            // If item is not equipped, will not render anything
            if (data == null)
            {

                return new StackLayout { };

            }

            // Item image as ImageButton for pop-up detail view
            var ItemButton = new ImageButton
            {
                Source = data.ImageURI,
                WidthRequest = 50,
                HeightRequest = 50,
            };

            // If valid clickable ImageButton, show to pop-up detail view
            if (ClickableButton)
            {
                ItemButton.Clicked += (sender, args) => ShowPopup(data);
            }

            // Label for each item
            var ItemLabel = new Label
            {
                Text = data.Name,
                HorizontalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            // Put ImageButton and label in to same stack
            var ItemStack = new StackLayout
            {
                Padding = 3,
                HorizontalOptions = LayoutOptions.Center,
                
                Children = { ItemButton, ItemLabel },
            };

            // Render particuler stack
            return ItemStack;
        }

        /// <summary>
        /// Show pop-up detail view
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemModel data)
        {
            ItemPopUpFrame.IsVisible = true;
            ItemPopUpImage.Source = data.ImageURI;
            ItemPopUpName.Text = data.Name;
            ItemPopUpDescription.Text = data.Description;
            ItemPopUpLocation.Text = data.Location.ToString();
            ItemPopUpAttribute.Text = data.Attribute.ToAbbrivation();
            ItemPopUpValue.Text = "+ " + data.Value.ToString();

            return true;
        }

        /// <summary>
        /// Close pop-up detail view handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ItemPopUpFrame.IsVisible = false;
        }
    }
}