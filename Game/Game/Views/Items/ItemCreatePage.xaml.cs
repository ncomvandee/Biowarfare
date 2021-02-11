using Game.Models;
using Game.ViewModels;

using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)] 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<ItemModel> ViewModel = new GenericViewModel<ItemModel>();

        // Empty Constructor for UTs
        public ItemCreatePage(bool UnitTest){}

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ItemCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = new ItemModel();

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Create";

            // Set the name entry to be empty
            NameEntry.Text = "";

        }

        /// <summary>
        /// Save by calling for Create
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

            // Checks if the required info is not empty eg. name, location, attribute
            if (ValidateInfo())
            {
                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Slider handler, chages the stat value as slider changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSliderChange(object sender, ValueChangedEventArgs e)
        {
            StatValue.Text = String.Format("{0}", (int)e.NewValue);
        }

        /// <summary>
        /// AttributePicker handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPickerChange(object sender, EventArgs e)
        {
            AttributePickerFrame.BackgroundColor = Color.FromHex("#BBC300");

            // Set the text to show what attribute user choose
            StatIcon.Text = ViewModel.Data.Attribute.ToAbbrivation();
        }

        /// <summary>
        /// ItemCatagory picker handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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