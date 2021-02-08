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

        #region Button
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
        /// Asign item to character's location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddButton_Clicked(object sender, EventArgs e)
        {

            var selectedItem = ((Button)sender).CommandParameter as ItemModel;
            var item = ViewModel.Data.AddItem(ItemLocationEnumHelper.ConvertMessageToEnum(LocationPicker.SelectedItem.ToString()), selectedItem.Id);
            UpdatePageBindingContext();

        }

        /// <summary>
        /// Delete item from location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteButton_Clicked(object sender, EventArgs e)
        {
            
            var selectedItem = ((Button)sender).CommandParameter ;
            var locationEnum = ItemLocationEnumHelper.GetLocationByPosition( Convert.ToInt32(selectedItem));
            ViewModel.Data.RemoveItem(locationEnum);
            UpdatePageBindingContext();
        }

        #endregion Button

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

            //convert message to Enum
            var locationEnum = ItemLocationEnumHelper.ConvertMessageToEnum(LocationPicker.SelectedItem.ToString());
            

            ItemsListView.ItemsSource = ItemIndexViewModel.Instance.GetLocationItems(locationEnum);
            
        }



        /// <summary>
        /// Rebiding BidingContext to refresh the page
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {

            //Get Current character item
            GetCharacterItem();

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
            GetCharacterItemHelper(CurrentHeadItem, ViewModel.Data.Head, ItemLocationEnum.Head);
            GetCharacterItemHelper(CurrentNecklessItem, ViewModel.Data.Necklass, ItemLocationEnum.Necklass);
            GetCharacterItemHelper(CurrentPrimaryHand, ViewModel.Data.PrimaryHand, ItemLocationEnum.PrimaryHand);
            GetCharacterItemHelper(CurrentOffHand, ViewModel.Data.OffHand, ItemLocationEnum.OffHand);
            GetCharacterItemHelper(CurrentLeftFinger, ViewModel.Data.LeftFinger, ItemLocationEnum.LeftFinger);
            GetCharacterItemHelper(CurrentRightFinger, ViewModel.Data.RightFinger, ItemLocationEnum.RightFinger);
            GetCharacterItemHelper(CurrentFeet, ViewModel.Data.Feet, ItemLocationEnum.Feet);
       
        }

        /// <summary>
        /// Print the item of a location if not null. Otherwise print empty string
        /// </summary>
        /// <param name="locationLabel"> The location of the label</param>
        /// <param name="locationString">The string of the location</param>
        public void GetCharacterItemHelper(Label locationLabel, string locationString, ItemLocationEnum itemLocationEnum)
        {
            if(locationString == null)
            {
                locationLabel.Text = "";
                DeleteButtonVisual(itemLocationEnum, false);
            }
            else
            {
                locationLabel.Text = ViewModel.Data.GetItem(locationString).FormatOutput();
                DeleteButtonVisual(itemLocationEnum, true);
            }
        }

        /// <summary>
        /// Make Delete Button invisible if no item equip in that location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="v">true if visible</param>
        private void DeleteButtonVisual(ItemLocationEnum location, bool v)
        {
            switch (location)
            {
                
                case ItemLocationEnum.Head:
                    HeadDeleteButton.IsVisible = v;
                    break;

                case ItemLocationEnum.Necklass:
                    NeckLessDeleteButton.IsVisible = v;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    PrimaryHandDeleteButton.IsVisible = v;
                    break;

                case ItemLocationEnum.OffHand:
                    OffHandDeleteButton.IsVisible = v;
                    break;

                case ItemLocationEnum.LeftFinger:
                    LeftFingerDeleteButton.IsVisible = v;
                    break;

                case ItemLocationEnum.RightFinger:
                    RightFingerDeleteButton.IsVisible = v;
                    break;

                case ItemLocationEnum.Feet:
                    FeetDeleteButton.IsVisible = v;
                    break;

                default:
                    break;

            }
        }
        #endregion LocationItem
    }
}