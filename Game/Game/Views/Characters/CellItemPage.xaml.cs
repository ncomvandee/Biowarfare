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

        //hold a copy of the original data for cancel to use
        public CharacterModel DataCopy;
 
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

            //Make a copy of the character for cancle to resotre
            DataCopy = new CharacterModel(data.Data);

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
            //put the copy back
            ViewModel.Data.Update(DataCopy);

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

           
            CurrentHeadItem.Children.Add(RenderItemInformation(ViewModel.Data.Head, ItemLocationEnum.Head,1));
            CurrentNecklessItem.Children.Add(RenderItemInformation(ViewModel.Data.Necklass, ItemLocationEnum.Necklass, 1));
            CurrentPrimaryHand.Children.Add(RenderItemInformation(ViewModel.Data.PrimaryHand, ItemLocationEnum.PrimaryHand, 1));
            CurrentOffHand.Children.Add(RenderItemInformation(ViewModel.Data.OffHand, ItemLocationEnum.OffHand, 1));
            CurrentLeftFinger.Children.Add(RenderItemInformation(ViewModel.Data.LeftFinger, ItemLocationEnum.LeftFinger, 1));
            CurrentRightFinger.Children.Add(RenderItemInformation(ViewModel.Data.RightFinger, ItemLocationEnum.RightFinger, 1));
            CurrentFeet.Children.Add(RenderItemInformation(ViewModel.Data.Feet, ItemLocationEnum.Feet, 1));

       
        }
        
        /// <summary>
        /// Display information of an item
        /// </summary>
        /// <param name="locationString"></param>
        /// <param name="locationEnum"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Grid RenderItemInformation(string locationString, ItemLocationEnum locationEnum, int col)
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition(),

                    },
                ColumnDefinitions =
                    {
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition()
                    }
            };



            if (locationString == null)
            {

                DeleteButtonVisual(locationEnum, false);
                grid.SetValue(Grid.ColumnProperty, col);
                return grid;
            }

            //get item
            ItemModel item = ViewModel.Data.GetItem(locationString);


            //item label
            var itemNameLabel = new Label {
                Text = item.Name,
                Style = (Style)Application.Current.Resources["LabelBaseStyle"],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Grid.SetRow(itemNameLabel, 0);
            Grid.SetColumnSpan(itemNameLabel, 3);

            grid.Children.Add(itemNameLabel);

            //Attribute image
            grid.Children.Add(new Image
            {
                WidthRequest = 20,
                Source = item.Attribute.ToImage(),

            }, 0, 1);

            //Attribute value
            grid.Children.Add(new Label 
            {
                
                Text = item.Value.ToString(),
                Style = (Style)Application.Current.Resources["ReadStatsLabelStyle"],
                FontSize = 20,

            }, 0, 2);

            //Damge Image
            grid.Children.Add(new Image
            {
                Source = "item_damage.png",
                WidthRequest = 20,

            }, 1, 1);

            //Damge value
            grid.Children.Add(new Label
            {
                Text = item.Damage.ToString(),
                Style = (Style)Application.Current.Resources["ReadStatsLabelStyle"],
                FontSize = 20,

            }, 1, 2);

            //item Range
            grid.Children.Add(new Image
            {
                Source = "item_range.png",
                WidthRequest = 20,

            }, 2, 1);

            //Damge value
            grid.Children.Add(new Label
            {
                Text = item.Range.ToString(),
                Style = (Style)Application.Current.Resources["ReadStatsLabelStyle"],
                FontSize = 20,

            }, 2, 2);

            DeleteButtonVisual(locationEnum, true);

            grid.SetValue(Grid.ColumnProperty, col);
            return grid;
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