using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickItemsPage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        /// <summary>
        /// Empty constructor for test
        /// </summary>
        /// <param name="UnitTest"></param>
        public PickItemsPage(bool UnitTest){ }

        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            PopulateCellPicker();


        }

        /// <summary>
        /// Save progress and return to RoundOverPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SaveButton_Clicked(object sender, EventArgs e)
        {

            if (AssignItemToCell())
            {
                await Navigation.PopModalAsync();

            }
        }
        /// <summary>
        /// Cancel the progress and return to RoundOverPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CancelButton_Clicked(object sender, EventArgs e)
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

        /// <summary>
        /// Assign item to player
        /// </summary>
        /// <returns></returns>
        public bool AssignItemToCell()
        {
            // Selected cell
            var CellSelected = CellPicker.SelectedItem.ToString();

            // Look up matching selected cell in CharacterList
            foreach(var cell in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {

                if (CellSelected.Equals(cell.Name))
                {
                    // Item location
                    var ItemLocation = ViewModel.Data.Location;

                    // Item id
                    var id = ViewModel.Data.Id;

                    // Assign item to player
                    cell.AddItem(ItemLocation, id);

                    // Remove item from item pool
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Remove(ViewModel.Data);

                    // Add item to SelectedList
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Add(ViewModel.Data);

                    return true;
                }
            }

            return false;
        }
    }
}