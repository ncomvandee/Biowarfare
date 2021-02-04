using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellCreatePage : ContentPage      
    {

        // Maximum Cell Level
        public int MaxLevel = 20;

        // Minimum Cell Level
        public int MinLevel = 1;

        public GenericViewModel<CharacterModel> ViewModel = new GenericViewModel<CharacterModel>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CellCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = new CharacterModel();

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Create";
        }

        /// <summary>
        /// Send Create message to ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SaveButton_Clicked (object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", ViewModel.Data);

            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the create process and go back to index page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CancelButton_Clicked (object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Open Cell Item Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddItem_Clicked (object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CellItemPage(ViewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Changes Cell's thumbnail depends on selected CellType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ImageChanged (object sender, EventArgs e)
        {
            // Get the string CellType from picker
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

        /// <summary>
        /// Set the value to change on Slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSliderChanged (object sender, ValueChangedEventArgs e)
        {
            if (sender == AttackSlider)
            {
                AttackStat.Text = String.Format("{0}", (int)e.NewValue);
            }

            if (sender == DefenseSlider)
            {
                DefenseStat.Text = String.Format("{0}", (int)e.NewValue);
            }

            if (sender == SpeedSlider)
            {
                SpeedStat.Text = String.Format("{0}", (int)e.NewValue);
            }
        }

        /// <summary>
        /// Set the enable or disable for Leveling button
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
        public void LevelDownButtonClicked (object sender, EventArgs e)
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
        public void LevelUpButtonClicked (object sender, EventArgs e)
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

    }
}