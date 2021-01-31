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

        public async void CancelButton_Clicked (object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public void ImageChanged (object sender, EventArgs e)
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