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

        // Empty Constructor for Tests
        public CellUpdatePage(bool UnitTest){ }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CellUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            TitlePage .Text = "Update " + data.Data.Name;

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
            // Selected CellType as String
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

            GetDefaultDescription(TypeSelected);
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
        public void GetDefaultDescription(String TypeSelected)
        {
            switch (TypeSelected)
            {
                case "Basophil":
                    ViewModel.Data.Description = "Basophils are a type of white blood cell that are responsible for causing inflammatory reactions and producing histamine." +
                                                 " Has a +10% hp buff.";
                    break;

                case "Eosinophil":
                    ViewModel.Data.Description = "Eosinophil are a type of white blood cell that specialize in attacking parasites." +
                                                 " Eosinophil have a 10% attack buff when fighting against invaders of the parasite type.";
                    break;

                case "Macrophage":
                    ViewModel.Data.Description = "A Spore is a small unicellular entity that typically can invade the body through the respiratory system." +
                                                 " Spores have a 25% chance of causing a character to be poisoned if a hit succeeds. When a character is poisoned," +
                                                 " it loses 1hp at the beginning of its turn for 5 turns." +
                                                 " At the end of 5 turns, the character is cured." +
                                                 " Poison does not stack damage and extend duration.";
                    break;

                case "BCell":
                    ViewModel.Data.Description = "B Cells are a specialized white blood cell that secrete antibodies." +
                                                 " Once per round, B Cells can give any living team member an Immunity token. " +
                                                 "This token will protect that team member from damage on one turn," +
                                                 " if are to be dealt damage that turn. Instead," +
                                                 " the Immunity token is spent and no damage is taken for that team member.";
                    break;

                case "KillerTCell":
                    ViewModel.Data.Description = "Killer T Cells are a type of white blood cell that kill infected," +
                                                 " damaged, or cancerous cells. Every time the Killer T Cell attacks," +
                                                 " they will always roll the highest weapon damage. Has +5% attack.";
                    break;

                case "NKCell":
                    ViewModel.Data.Description = "NK cells are a type of lymphocyte that provides a rapid response to viruses in the body." +
                                                 " Has a +10% speed buff.";
                    break;
            }
        }

    }
}