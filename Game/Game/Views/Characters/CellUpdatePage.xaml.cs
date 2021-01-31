using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Cell Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<CharacterModel> ViewModel;

        // Empty Constructor for Tests
        public CellUpdatePage(bool UnitTest){ }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CellUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

        }

        /// <summary>
        /// Constructor w/o parameters for Update Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public CellUpdatePage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
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

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Send Create message to ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SaveButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", ViewModel.Data);

            await Navigation.PopModalAsync();
        }

        public async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public void ImageChanged(object sender, EventArgs e)
        {
            var TypeSelected = CellTypePicker.SelectedItem.ToString();

            switch (TypeSelected)
            {
                case "Basophil":
                    CellImage.Source = "basophil_bg.png";
                    break;

                case "Eosinophil":
                    CellImage.Source = "eosinophil_bg.png";
                    break;

                case "Macrophage":
                    CellImage.Source = "macrophage_bg.png";
                    break;

                case "BCell":
                    CellImage.Source = "";
                    break;

                case "KillerTCell":
                    CellImage.Source = "";
                    break;

                case "NKCell":
                    CellImage.Source = "";
                    break;
            }
        }












        //    /// <summary>
        //    /// Catch the change to the Stepper for Range
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        //    {
        //        RangeValue.Text = String.Format("{0}", e.NewValue);
        //    }

        //    /// <summary>
        //    /// Catch the change to the stepper for Value
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        //    {
        //        ValueValue.Text = String.Format("{0}", e.NewValue);
        //    }

        //    /// <summary>
        //    /// Catch the change to the stepper for Damage
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        //    {
        //        DamageValue.Text = String.Format("{0}", e.NewValue);
        //    }
    }
}