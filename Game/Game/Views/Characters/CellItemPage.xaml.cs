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
        readonly ItemIndexViewModel ItemViewModel = ItemIndexViewModel.Instance;
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

        public void Picker_Clicked(object sender, EventArgs e)
        {
            //Game.Models.ItemLocationEnum location = LocationPicker.SelectedItem.ToString();
            //var item = ItemViewModel.GetLocationItems(location);
        }
    }
}