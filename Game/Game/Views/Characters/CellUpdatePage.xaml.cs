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

            CellTypePicker.SelectedItem = data.Data.Job.ToString();
            CellImage.Source = data.Data.ImageURI.ToString();

        }

        /// <summary>
        /// Send Create message to ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SaveButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Update", ViewModel.Data);

            await Navigation.PopModalAsync();
        }

        public async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Open Cell Item Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CellItemPage(ViewModel)));
            await Navigation.PopAsync();
        }

        public void ImageChanged(object sender, EventArgs e)
        {
            var TypeSelected = CellTypePicker.SelectedItem.ToString();

            switch (TypeSelected)
            {
                case "Basophil":
                    CellImage.Source = "basophil_bg.png";
                    ViewModel.Data.ImageURI = "basophil_bg.png";
                    break;

                case "Eosinophil":
                    CellImage.Source = "eosinophil_bg.png";
                    ViewModel.Data.ImageURI = "eosinophil_bg.png";
                    break;

                case "Macrophage":
                    CellImage.Source = "macrophage_bg.png";
                    ViewModel.Data.ImageURI = "macrophage_bg.png";
                    break;

                case "BCell":
                    CellImage.Source = "b_cell_bg.png";
                    ViewModel.Data.ImageURI = "b_cell_bg.png";
                    break;

                case "KillerTCell":
                    CellImage.Source = "t_cell_bg.png";
                    ViewModel.Data.ImageURI = "t_cell_bg.png";
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