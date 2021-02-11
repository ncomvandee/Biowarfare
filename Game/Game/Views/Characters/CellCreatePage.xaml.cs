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
            // Boolean for checking input completion
            bool isValidInfo = CheckValidInfo();

            if (isValidInfo)
            {
                MessagingCenter.Send(this, "Create", ViewModel.Data);

                await Navigation.PopModalAsync();
            }
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

            CellTypePicker.BackgroundColor = Color.FromHex("#D8BFFF");

            // Get the string CellType from picker
            var TypeSelected = CellTypePicker.SelectedItem.ToString();
            var image = ViewModel.Data.Job.ToImage();

            CellImage.Source = image;
            ViewModel.Data.ImageURI = image;

            GetDefaultDescription(TypeSelected);
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

        /// <summary>
        /// Send the default description for each Cell to Database
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
                    ViewModel.Data.Description = "Macrophages are a type of white blood cell that seek out and dispose of foreign invaders and non-healthy cells in their path." +
                                                 " Macrophages are unique because they recruit other immune cells to fight alongside them." +
                                                 " Having an active Macrophage in your immune system will increase all friendly characters by +5% defense power." +
                                                 " Has a 5% personal defense buff.  ";
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