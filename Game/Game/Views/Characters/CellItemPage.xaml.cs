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

            var selectedItem = ((ImageButton)sender).CommandParameter as ItemModel;
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
            
            var selectedItem = ((ImageButton)sender).CommandParameter ;
            var locationEnum = ItemLocationEnumHelper.GetLocationByPosition( Convert.ToInt32(selectedItem));
            ViewModel.Data.RemoveItem(locationEnum);
            UpdatePageBindingContext();
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if(data == null)
            {
                return;
            }

            ItemPopUpFrame.IsVisible = true;
            ItemPopUpImage.Source = data.ImageURI;
            ItemPopUpName.Text = data.Name;
            ItemPopUpDescription.Text = data.Description;
            ItemPopUpLocation.Text = data.Location.ToString();
            ItemPopUpAttribute.Text = data.Attribute.ToMessage();
            ItemPopUpValue.Text = "+ " + data.Value.ToString();


            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        /// <summary>
        /// Clost the popup frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ItemPopUpFrame.IsVisible = false;
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
            //Remove all children of the stacklayout
            CurrentHeadItem.Children.Clear();
            CurrentNecklessItem.Children.Clear();
            CurrentPrimaryHand.Children.Clear();
            CurrentOffHand.Children.Clear();
            CurrentRightFinger.Children.Clear();
            CurrentLeftFinger.Children.Clear();
            CurrentFeet.Children.Clear();

            //Add new children
            CurrentHeadItem.Children.Add(RenderItemInformation(ViewModel.Data.Head, ItemLocationEnum.Head));
            CurrentNecklessItem.Children.Add(RenderItemInformation(ViewModel.Data.Necklass, ItemLocationEnum.Necklass));
            CurrentPrimaryHand.Children.Add(RenderItemInformation(ViewModel.Data.PrimaryHand, ItemLocationEnum.PrimaryHand));
            CurrentOffHand.Children.Add(RenderItemInformation(ViewModel.Data.OffHand, ItemLocationEnum.OffHand));
            CurrentLeftFinger.Children.Add(RenderItemInformation(ViewModel.Data.LeftFinger, ItemLocationEnum.LeftFinger));
            CurrentRightFinger.Children.Add(RenderItemInformation(ViewModel.Data.RightFinger, ItemLocationEnum.RightFinger));
            CurrentFeet.Children.Add(RenderItemInformation(ViewModel.Data.Feet, ItemLocationEnum.Feet));

       
        }
        
        /// <summary>
        /// Display information of an item
        /// </summary>
        /// <param name="locationString"></param>
        /// <param name="locationEnum"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Grid RenderItemInformation(string locationString, ItemLocationEnum locationEnum)
        {

            var grid = new Grid { };


            if (locationString == null)
            {

                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(100)
                });

                //Create empty boxview
                var box = new BoxView
                {
                    BackgroundColor = Color.Transparent,
                    
                };

                grid.Children.Add(box);

                DeleteButtonVisual(locationEnum, false);

                return grid;
            }

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());


            //get item
            ItemModel item = ViewModel.Data.GetItem(locationString);


            //item label
            var itemNameLabel = new Label {
                Text = item.Name,
                Style = (Style)Application.Current.Resources["LabelBaseStyle"],
                HorizontalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = (LineBreakMode)4,
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
                Source = "damage_blue.png",
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
                Source = "range_blue.png",
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

            return grid;
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