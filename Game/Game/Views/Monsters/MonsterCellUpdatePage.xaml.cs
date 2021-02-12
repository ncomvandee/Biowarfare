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

        //hold a copy of the original data for cancel to use
        public MonsterModel DataCopy;

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

            //Make a copy of the character for cancle to resotre
            DataCopy = new MonsterModel(data.Data);

        }

        /// <summary>
        /// Send Create message to ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SaveButton_Clicked(object sender, EventArgs e)
        {

            if (CheckValidInfo())
            {
                MessagingCenter.Send(this, "Update", ViewModel.Data);

                await Navigation.PopModalAsync();

            }
        }

        /// <summary>
        /// Cancel the update process
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
        /// Changes the MonsterCell thumbnail related on seleccted Monstertype
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ImageChanged(object sender, EventArgs e)
        {
            MonsterTypePicker.BackgroundColor = Color.FromHex("#D8BFFF");

            var ImageSource = ViewModel.Data.MonsterType.ToImage();

            MonsterImage.Source = ImageSource;
            ViewModel.Data.ImageURI = ImageSource;
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

        /// <summary>
        /// Check if the input data are complete or not
        /// </summary>
        /// <returns></returns>
        public bool CheckValidInfo()
        {
            // If name is blank, change the placeholder color to be red and return false
            if (NameEntry.Text.Equals(""))
            {
                NameEntry.PlaceholderColor = Color.Red;
                return false;
            }

            // If CellType is not selected, change picker color to red and return false;
            if (MonsterTypePicker.SelectedIndex == -1)
            {
                MonsterTypePicker.BackgroundColor = Color.Red;
                return false;
            }

            return true;
        }
    }
}