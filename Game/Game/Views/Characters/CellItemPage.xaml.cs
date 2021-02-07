using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Cell Item Page
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)] 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellItemPage : ContentPage
    {
        // View Model for cell
        public readonly GenericViewModel<CharacterModel> ViewModel;

        //View Model of Item
        //readonly ItemIndexViewModel ItemViewModel = ItemIndexViewModel.Instance;
        // Empty Constructor for UTs
        public CellItemPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Cell Item Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public CellItemPage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = data.Data.Name + " Equipped Item" ;


            //Get Current character item
            GetCharacterItem();

        }

        /// <summary>
        /// Send Save message to ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SaveButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Update", ViewModel.Data);

            await Navigation.PopModalAsync();
        }
        /// <summary>
        /// Cancel the modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Show List of Location's item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LocationPicker_Changed(object sender, EventArgs e)
        {
            if(LocationPicker.SelectedItem == null)
            {
                return;
            }

            //convert String to Enum
            var locationEnum = ItemLocationEnumHelper.ConvertStringToEnum(LocationPicker.SelectedItem.ToString());

            ItemsListView.ItemsSource = ItemIndexViewModel.Instance.GetLocationItems(locationEnum);

        }

        /// <summary>
        /// Asign item to character's location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddButton_Clicked(object sender, EventArgs e)
        {
 
            var selectedItem = ((Button)sender).CommandParameter as ItemModel;
            var item = ViewModel.Data.AddItem(selectedItem.Location, selectedItem.Id) ;
            UpdatePageBindingContext();

        }

        /// <summary>
        /// Rebiding BidingContext to refresh the page
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var data = this.ViewModel.Data;

            //Store location picker
            var locationPickerData = LocationPicker.SelectedItem;

            // Clear the Binding and reset it
            BindingContext = null;
            this.ViewModel.Data = data;
            this.ViewModel.Title = ViewModel.Data.Name + " Equipped Item";

            BindingContext = this.ViewModel;

            //Keep the display for the picker
            LocationPicker.SelectedItem = locationPickerData;

            //Get Current character item
            GetCharacterItem();

            return true;
        }


        #region LocationItem
        /// <summary>
        /// Display the current item of character base on location
        /// </summary>
        private void GetCharacterItem()
        {
            GetCharacterItemHelper(CurrentHeadItem, ViewModel.Data.Head);
            GetCharacterItemHelper(CurrentNecklessItem, ViewModel.Data.Necklass);
            GetCharacterItemHelper(CurrentPrimaryHand, ViewModel.Data.PrimaryHand);
            GetCharacterItemHelper(CurrentOffHand, ViewModel.Data.OffHand);
            GetCharacterItemHelper(CurrentLeftFinger, ViewModel.Data.LeftFinger);
            GetCharacterItemHelper(CurrentRightFinger, ViewModel.Data.RightFinger);
            GetCharacterItemHelper(CurrentFeet, ViewModel.Data.Feet);
       
        }

        /// <summary>
        /// Print the item of a location if not null. Otherwise print empty string
        /// </summary>
        /// <param name="locationLabel"> The location of the label</param>
        /// <param name="locationString">The string of the location</param>
        public void GetCharacterItemHelper(Label locationLabel, string locationString )
        {
            if(locationString == null)
            {
                locationLabel.Text = "";
            }
            else
            {
                locationLabel.Text = ViewModel.Data.GetItem(locationString).FormatOutput();
            }
        }
        #endregion LocationItem
    }
}