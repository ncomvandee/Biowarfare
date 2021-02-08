using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// MonsterCell Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCellUpdatePage : ContentPage
    {
        // View Model for MonsterCell
        public readonly GenericViewModel<MonsterModel> ViewModel;

        // Empty Constructor for Tests
        public MonsterCellUpdatePage(bool UnitTest){ }

        /// <summary>
        /// Constructor that takes and existing data Monster
        /// </summary>
        public MonsterCellUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            MonsterTypePicker.SelectedItem = data.Data.MonsterType.ToString();
            MonsterImage.Source = data.Data.ImageURI.ToString();


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

        /// <summary>
        /// Cancel the update process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Changes the MonsterCell thumbnail related on seleccted Monstertype
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void ImageChanged(object sender, EventArgs e)
        //{
        //    // Selected CellType as String
        //    var TypeSelected = CellTypePicker.SelectedItem.ToString();

        //    switch (TypeSelected)
        //    {
        //        case "Basophil":
        //            CellImage.Source = "basophil_bg.png";
        //            ViewModel.Data.ImageURI = "basophil_bg.png";
        //            break;

        //        case "Eosinophil":
        //            CellImage.Source = "eosinophil_bg.png";
        //            ViewModel.Data.ImageURI = "eosinophil_bg.png";
        //            break;

        //        case "Macrophage":
        //            CellImage.Source = "macrophage_bg.png";
        //            ViewModel.Data.ImageURI = "macrophage_bg.png";
        //            break;

        //        case "BCell":
        //            CellImage.Source = "b_cell_bg.png";
        //            ViewModel.Data.ImageURI = "b_cell_bg.png";
        //            break;

        //        case "KillerTCell":
        //            CellImage.Source = "t_cell_bg.png";
        //            ViewModel.Data.ImageURI = "t_cell_bg.png";
        //            break; 

        //        case "NKCell":
        //            CellImage.Source = "";
        //            break;
        //    }
        //}

        /// <summary>
        /// Shows the value when Slider changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender == AttackSlider)
            {
                AttackStat.Text = String.Format("{0}", (int)e.NewValue);
            }
            else if (sender == DefenseSlider)
            {
                DefenseStat.Text = String.Format("{0}", (int)e.NewValue);
            }
            else if (sender == SpeedSlider)
            {
                SpeedStat.Text = String.Format("{0}", (int)e.NewValue);
            }

        }
    }
}