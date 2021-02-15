using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemReadPage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        // Empty Constructor for UTs
        public ItemReadPage(bool UnitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public ItemReadPage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = data.Data.Name + " Information";

            //get the correct icon for attribute
            AttributeIcon.Source = ViewModel.Data.Attribute.ToItemImage();

            //get the name for location
            LocationName.Text = ViewModel.Data.Location.ToMessage();

            // Disable button if item is consumable or unique 
            if (ViewModel.Data.IsConsumable || ViewModel.Data.IsUnique)
            {
                AdjustButtonIfConsumable();
            } 
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemDeletePage(ViewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Jump to the Cell Update Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ItemEditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemUpdatePage(ViewModel)));

            await Navigation.PopAsync();
        }

        /// <summary>
        /// Flip feature, to toggle between Cell thumbnail and description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowDescriptionClicked(object sender, EventArgs e)
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
        /// Disable delete and edit button if item is consumable or unique
        /// </summary>
        public void AdjustButtonIfConsumable()
        {
            DeleteButton.IsEnabled = false;

            EditButton.IsVisible = false;
        }

    }
}