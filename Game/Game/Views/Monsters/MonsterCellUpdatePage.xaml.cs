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

            TitlePage.Text = "Update " + data.Data.Name;

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
        public void ImageChanged(object sender, EventArgs e)
        {
            MonsterTypePicker.BackgroundColor = Color.FromHex("#D8BFFF");

            // Get the string CellType from picker
            var TypeSelected = MonsterTypePicker.SelectedItem.ToString();

            switch (TypeSelected)
            {
                case "Spore":
                    MonsterImage.Source = "spore_bg.png";
                    ViewModel.Data.ImageURI = "spore_bg.png";
                    break;

                case "Bacteria":
                    MonsterImage.Source = "bacteria_bg.png";
                    ViewModel.Data.ImageURI = "bacteria_bg.png";
                    break;

                case "Parasite":
                    MonsterImage.Source = "parasite_bg.png";
                    ViewModel.Data.ImageURI = "parasite_bg.png";
                    break;

                case "Virus":
                    MonsterImage.Source = "virus_bg.png";
                    ViewModel.Data.ImageURI = "virus_bg.png";
                    break;

                case "Cancer":
                    MonsterImage.Source = "cancer_bg.png";
                    ViewModel.Data.ImageURI = "cancer_bg.png";
                    break;
            }
        }

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