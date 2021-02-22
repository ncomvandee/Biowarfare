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

        // Maximum Cell Level
        public int MaxLevel = 20;

        // Minimum Cell Level
        public int MinLevel = 1;

        //hold a copy of the original data for cancel to use
        public CharacterModel DataCopy;

        // Empty Constructor for Tests
        public CellUpdatePage(bool UnitTest){ }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CellUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            TitlePage.Text = "Update " + data.Data.Name;

            //Make a copy of the character for cancle to resotre
            DataCopy = new CharacterModel(data.Data);

            CellTypePicker.SelectedItem = data.Data.Job.ToString();
            CellImage.Source = data.Data.ImageURI.ToString();

            SetEnableLevelButton();


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
        /// Open Cell Item Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CellItemPage(ViewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Changes the Cell thumbnail related on seleccted Celltype
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ImageChanged(object sender, EventArgs e)
        {
            CellTypePicker.BackgroundColor = Color.FromHex("#3CAEA3");

            // Get the string CellType from picker
            var TypeSelected = CellTypePicker.SelectedItem.ToString();

            // Get image file name
            var image = ViewModel.Data.Job.ToImage();

            CellImage.Source = image;
            ViewModel.Data.ImageURI = image;

            GetDefaultDescription();
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
        /// Sets level Up and Level Down Buttons 
        /// </summary>
        public void SetEnableLevelButton()
        {
            LevelUpButton.IsEnabled = true;
            if (ViewModel.Data.Level == MaxLevel)
            {
                LevelUpButton.IsEnabled = false;
            }

            LevelDownButton.IsEnabled = true;
            if (ViewModel.Data.Level == MinLevel)
            {
                LevelDownButton.IsEnabled = false;
            }

        }

        /// <summary>
        /// Handling the Level Down button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LevelDownButtonClicked(object sender, EventArgs e)
        {
            ViewModel.Data.Level--;

            if (ViewModel.Data.Level <= MinLevel)
            {
                ViewModel.Data.Level = MinLevel;
            }

            LevelEntry.Text = ViewModel.Data.Level.ToString();

            // Call to set enable or disable the button
            SetEnableLevelButton();

        }

        /// <summary>
        /// Handling the Level Up button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LevelUpButtonClicked(object sender, EventArgs e)
        {

            ViewModel.Data.Level++;

            if (ViewModel.Data.Level >= MaxLevel)
            {
                ViewModel.Data.Level = MaxLevel;
            }

            LevelEntry.Text = ViewModel.Data.Level.ToString();

            // Call to set enable or disable the button
            SetEnableLevelButton();
        }

        /// <summary>
        /// Update the default description for each Cell to Database
        /// </summary>
        /// <param name="TypeSelected"></param>
        public void GetDefaultDescription()
        {
            ViewModel.Data.Description = ViewModel.Data.Job.ToDescription();
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
            if (CellTypePicker.SelectedIndex == -1)
            {
                CellTypePicker.BackgroundColor = Color.Red;
                return false;
            }

            return true;
        }

    }
}