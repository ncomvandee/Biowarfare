﻿using System;
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
    public partial class MonsterCellCreatePage : ContentPage      
    {

        // Maximum Monster Level
        public int MaxLevel = 20;

        // Minimum Monster Level
        public int MinLevel = 1;

        public GenericViewModel<MonsterModel> ViewModel = new GenericViewModel<MonsterModel>();

        /// <summary>
        /// Constructor
        /// </summary>
        public MonsterCellCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = new MonsterModel();

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
        /// Changes Monster's thumbnail depends on selected MonsterType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void ImageChanged (object sender, EventArgs e)
        //{

        //    CellTypePicker.BackgroundColor = Color.FromHex("#D8BFFF");

        //    // Get the string CellType from picker
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