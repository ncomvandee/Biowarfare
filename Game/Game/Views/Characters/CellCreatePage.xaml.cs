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
    }
}