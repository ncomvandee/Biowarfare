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
    }
}