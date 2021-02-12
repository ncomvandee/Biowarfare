using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        //hold a copy of the original data for cancel to use
        public ItemModel DataCopy;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest){ }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            ItemCatagoryPicker.SelectedItem = ViewModel.Data.Location.ToCatagories();
            AttributePicker.SelectedItem = ViewModel.Data.Attribute.ToString();
            StatIcon.Text = ViewModel.Data.Attribute.ToAbbrivation();
            DescriptionBox.Text = ViewModel.Data.Description;

            //Make a copy of the character for cancle to resotre
            DataCopy = new ItemModel(data.Data);
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            if (ValidateInfo())
            {
                MessagingCenter.Send(this, "Update", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            //put the copy back
            ViewModel.Data.Update(DataCopy);

            await Navigation.PopModalAsync();
        }

        public void OnCatagoryChange(object sender, EventArgs e)
        {
            ItemCatagoryPickerFrame.BackgroundColor = Color.FromHex("#BBC300");

            if (ItemCatagoryPicker.SelectedItem == null)
            {
                return;
            }

            // Get the ItemCatagory as EnumLocation
            var locationEnum = ItemLocationEnumHelper.ConvertCatagoryToEnum(ItemCatagoryPicker.SelectedItem.ToString());

            // Set the location as location selected
            ViewModel.Data.Location = locationEnum;

            // Get the image from what ItemCatagory has been chosen
            var ImageSource = ViewModel.Data.Location.ToImage();

            // Show the Image in UI
            ItemImage.Source = ImageSource;

            // Set the ImageURI as image
            ViewModel.Data.ImageURI = ImageSource;
        }

        public void OnPickerChange(object sender, EventArgs e)
        {
            AttributePickerFrame.BackgroundColor = Color.FromHex("#BBC300");

            // Set the text to show what attribute user choose
            StatIcon.Text = ViewModel.Data.Attribute.ToAbbrivation();
        }
    

        public void OnSliderChange(object sender, ValueChangedEventArgs e)
        {
            StatValue.Text = String.Format("{0}", (int)e.NewValue);
        }

        /// <summary>
        /// Validate if the information required is empty on not
        /// </summary>
        /// <returns>True if all required infomation is not empty</returns>
        public bool ValidateInfo()
        {
            // Check the name entry
            if (String.IsNullOrEmpty(NameEntry.Text))
            {
                NameEntry.PlaceholderColor = Color.Red;
                return false;
            }

            // Check the Attribute picker
            if (AttributePicker.SelectedIndex < 0)
            {
                AttributePickerFrame.BackgroundColor = Color.Red;
                return false;
            }

            // Chech the ItemCatagory picker
            if (ItemCatagoryPicker.SelectedIndex < 0)
            {
                ItemCatagoryPickerFrame.BackgroundColor = Color.Red;
                return false;
            }

            return true;
        }
    }
}