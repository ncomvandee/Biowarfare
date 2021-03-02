using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickItemsPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage()
        {
            InitializeComponent();

            PopulateCellPicker();


        }
        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Populate picker to be Character party list
        /// </summary>
        public void PopulateCellPicker()
        {
            List<String> Character = new List<String>();

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {
                // Cell name
                var name = data.Name;

                Character.Add(name);            
            }

            CellPicker.ItemsSource = Character;
        }
    }
}