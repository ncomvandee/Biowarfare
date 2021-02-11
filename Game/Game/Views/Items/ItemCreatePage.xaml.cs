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

            //Need to make the SelectedItem a string, so it can select the correct item.
            //LocationPicker.SelectedItem = ViewModel.Data.Location.ToString();
            //AttributePicker.SelectedItem = ViewModel.Data.Attribute.ToString();
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

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
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

        public void OnSliderChange(object sender, ValueChangedEventArgs e)
        {
            StatValue.Text = String.Format("{0}", (int)e.NewValue);
        }

        public void OnPickerChange(object sender, EventArgs e)
        {
            StatIcon.Text = ViewModel.Data.Attribute.ToAbbrivation();
        }

        public void OnCatagoryChange(object sender, EventArgs e)
        {
            if (ItemCatagoryPicker.SelectedItem == null)
            {
                return;
            }

            //convert message to Enum
            var locationEnum = ItemLocationEnumHelper.ConvertCatagoryToEnum(ItemCatagoryPicker.SelectedItem.ToString());

            ViewModel.Data.Location = locationEnum;

            ItemImage.Source = ViewModel.Data.Location.ToImage(); 


            
        }
    }
}