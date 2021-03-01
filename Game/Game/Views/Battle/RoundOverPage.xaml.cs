using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoundOverPage: ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RoundOverPage()
        {
            InitializeComponent();

            // Update the Round Count
            TotalRound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString();

            PageTitle.Text = "Round " + BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString() + " Cleared";
            // Update the Found Number
            //TotalFound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();

            // Update the Selected Number, this gets updated later when selected refresh happens
            //TotalSelected.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Count().ToString();

            DrawCharacterList();


           // DrawItemLists();
        }

        /// <summary>
        /// Clear and Add the Characters that survived
        /// </summary>
        public void DrawCharacterList()
        {
            // Clear and Populate the Characters Remaining
            var FlexList = CharacterListFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                CharacterListFrame.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {
                CharacterListFrame.Children.Add(CreatePlayerDisplayBox(data));
            }
        }

        /// <summary>
        /// Draw the List of Items
        /// 
        /// The Ones Dropped
        /// 
        /// The Ones Selected
        /// 
        /// </summary>
/*        public void DrawItemLists()
        {
            DrawDroppedItems();
            DrawSelectedItems();

            // Only need to update the selected, the Dropped is set in the constructor
            TotalSelected.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Count().ToString();
        }*/

 /*       /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
        public void DrawDroppedItems()
        {
            // Clear and Populate the Dropped Items
            var FlexList = ItemListFoundFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemListFoundFrame.Children.Remove(data);
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Distinct())
            {
                ItemListFoundFrame.Children.Add(GetItemToDisplay(data));
            }
        }*/

        /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
/*        public void DrawSelectedItems()
        {
            // Clear and Populate the Dropped Items
            var FlexList = ItemListSelectedFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemListSelectedFrame.Children.Remove(data);
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList)
            {
                ItemListSelectedFrame.Children.Add(GetItemToDisplay(data));
            }
        }*/

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
       /* public StackLayout GetItemToDisplay(ItemModel item)
        {
            if (item == null)
            {
                return new StackLayout();
            }

            if (string.IsNullOrEmpty(item.Id))
            {
                return new StackLayout();
            }

            // Defualt Image is the Plus
            var ClickableButton = true;

            var data = ItemIndexViewModel.Instance.GetItem(item.Id);
            if (data == null)
            {
                // Show the Default Icon for the Location
                data = new ItemModel { Name="Unknown", ImageURI = "icon_cancel.png" };

                // Turn off click action
                ClickableButton = false;
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            if (ClickableButton)
            {
                // Add a event to the user can click the item and see more
                ItemButton.Clicked += (sender, args) => ShowPopup(data);
            }

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                },
            };

            return ItemStack;
        }*/

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Default clickable item
            var ClickAble = true;

            // Hookup the image
            var PlayerImage = new ImageButton
            {
                //Style = (Style)Application.Current.Resources["ImageBattleLargeStyle"],
                BackgroundColor = Color.White,
                CornerRadius = 100,
                HeightRequest = 90,
                WidthRequest = 90,
                Source = data.ImageURI
            };

            // Frame containing Character image button
            var ImageFrame = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["PrimaryMidgroundPurple"],
                CornerRadius = 10,
                BorderColor = Color.Transparent,
                WidthRequest = 100,
                HeightRequest = 100,
                Content = PlayerImage,
            };

            //name of the cell
            var cellName = new Label
            {
                Style = (Style)Application.Current.Resources["LabelBaseStyle"],
                Text = data.Name,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            // If image is clickable show the popup page
            if (ClickAble)
            {
                PlayerImage.Clicked += (sender, args) => ShowPopup(data);
            }


            // Put the Image Button inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 10,
                Spacing = 10,
                Margin = new Thickness(10, 0, 10, 0),
                Children = {
                    ImageFrame,
                    cellName
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Show the Popup for the present character information
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowPopup(PlayerInfoModel data)
        {
            CharacterPopUpFrame.IsVisible = true;
            CharacterPopupCellType.Text = data.Job.ToString();
            CharacterPopUpImage.Source = data.ImageURI;
            CharacterPopUpName.Text = data.Name;
            CharacterPopupLevel.Text = data.Level.ToString();
            Attack.Text = data.GetAttackTotal.ToString();
            Defense.Text = data.GetDefenseTotal.ToString();
            Speed.Text = data.GetSpeedTotal.ToString();
            Health.Text = data.GetCurrentHealthTotal.ToString();
            Experience.Text = data.ExperienceTotal.ToString();
            return true;
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
		{
            CharacterPopUpFrame.IsVisible = false;
        }
		
		/// <summary>
		/// Closes the Round Over Popup
        /// 
        /// Launches the Next Round Popup
        /// 
        /// Resets the Game Round
        /// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void CloseButton_Clicked(object sender, EventArgs e)
		{
            // Reset to a new Round
            BattleEngineViewModel.Instance.Engine.Round.NewRound();

            // Show the New Round Screen
            ShowModalNewRoundPage();
		}

		/// <summary>
		/// Start next Round, returning to the battle screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void AutoAssignButton_Clicked(object sender, EventArgs e)
		{
			// Distribute the Items
			BattleEngineViewModel.Instance.Engine.Round.PickupItemsForAllCharacters();

            // Show what was picked up
            //DrawItemLists();
        }

        /// <summary>
        /// Show the Page for New Round
        /// 
        /// Upcomming Monsters
        /// 
        /// </summary>
        public async void ShowModalNewRoundPage()
        {
            await Navigation.PopModalAsync();
        }

    }
}