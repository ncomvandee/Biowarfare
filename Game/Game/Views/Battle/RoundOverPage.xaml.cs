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
        bool isGameOver = false;
		/// <summary>
		/// Constructor
		/// </summary>
		public RoundOverPage()
        {
            InitializeComponent();

            // Update the Round Count
            TotalRound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString();

            PageTitle.Text = "Round " + BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString() + " Cleared";
            //Update the Found Number
            TotalFound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();

            // Update the Selected Number, this gets updated later when selected refresh happens
            TotalSelected.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Count().ToString();


            ItemFoundFrame.IsVisible = true;
            ItemListFoundFrame.IsVisible = true;

            ItemSelectedFrame.IsVisible = true;
            ItemListSelectedFrame.IsVisible = true;
            // Onlu button if there is drop item
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count() != 0)
            {
                AutoAssignButton.IsVisible = true;
            }
         
            DrawCharacterList();


            DrawItemLists();
        }

        /// <summary>
        /// Constructor for Game Over
        /// </summary>
        /// <param name="gameOver"></param>
        public RoundOverPage(bool gameOver)
        {
            InitializeComponent();
            isGameOver = gameOver;

            TotalRound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString();

            DrawCharacterList();
            BottomButton.Text = "Exit";
            ScoreButton.IsVisible = true;
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
        public void DrawItemLists()
        {
            DrawDroppedItems();
            DrawSelectedItems();

            // Only need to update the selected, the Dropped is set in the constructor
            TotalSelected.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Count().ToString();
        }

        /// <summary>
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

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList)
            {
                ItemListFoundFrame.Children.Add(GetItemToDisplay(data, true));
            }
        }

        /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
        public void DrawSelectedItems()
        {
            // Clear and Populate the Dropped Items
            var FlexList = ItemListSelectedFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemListSelectedFrame.Children.Remove(data);
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList)
            {
                ItemListSelectedFrame.Children.Add(GetItemToDisplay(data, false));
            }
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemModel item, bool Assignable)
        {
            if (item == null)
            {
                return new StackLayout();
            }

            if (string.IsNullOrEmpty(item.Id))
            {
                return new StackLayout();
            }

            // If item is already assigned, will be showed in ItemListSelecctedFrame and cannot click anymore.
            var ClickableButton = Assignable;

            var data = ItemIndexViewModel.Instance.GetItem(item.Id);
            if (data == null)
            {
                data = ConsumableItemIndexViewModel.Instance.GetItem(item.Id);

                if (data == null)
                {
                    // Show the Default Icon for the Location
                    data = new ItemModel { Name = "Unknown", ImageURI = "icon_cancel.png" };

                    // Turn off click action
                    ClickableButton = false;
                }
                
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            if (ClickableButton)
            {
                if (data.IsConsumable)
                {
                    // Add a event to the user can click the item and see more
                    ItemButton.Clicked += (sender, args) => ShowItemPopup(data);
                }

                if (data.IsConsumable == false)
                {
                    ItemButton.Clicked += (sender, args) => PickItem_Clicked(data);
                }
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
        }

        /// <summary>
        /// Show Item Pop Up
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool ShowItemPopup(ItemModel data)
        {
            PopupLoadingView.IsVisible = true;
            PopupItemImage.Source = data.ImageURI;

            PopupItemName.Text = data.Name;
            PopupItemDescription.Text = data.Description;
            PopupItemLocation.Text = data.Location.ToMessage();
            PopupItemAttribute.Text = data.Attribute.ToMessage();
            PopupItemValue.Text = " + " + data.Value.ToString();
            return true;
        }

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
/*            var ImageFrame = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["PrimaryMidgroundPurple"],
                CornerRadius = 10,
                BorderColor = Color.Transparent,
                WidthRequest = 100,
                HeightRequest = 100,
                Content = PlayerImage,
            };*/

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
                PlayerImage.Clicked += (sender, args) => ShowCharacterPopup(data);
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
                    PlayerImage,
                    cellName
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Displays items Cell equipped as image
        /// </summary>
        public void AddItemToDisplay(PlayerInfoModel CellData)
        {
            var FlexList = ItemBox.Children.ToList();

            foreach (var data in FlexList)
            {
                ItemBox.Children.Remove(data);
            }

            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head, CellData));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass, CellData));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand, CellData));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand, CellData));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger, CellData));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger, CellData));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet, CellData));

        }

        /// <summary>
        /// Look up items to display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location, PlayerInfoModel data)
        {
            var ItemData = data.GetItemByLocation(location);

            var ClickAbleButton = true;

            // If item is not equipped, will not render anything
            if (ItemData == null)
            {

                return new StackLayout { };

            }

            // Item image as ImageButton for pop-up detail view
            var ItemButton = new ImageButton
            {
                Source = ItemData.ImageURI,
                WidthRequest = 60,
                HeightRequest = 60,
                BackgroundColor = Color.White,
                CornerRadius = 50,
                Padding = 5,
            };

            if (ClickAbleButton)
            {
                ItemButton.Clicked += (sender, args) => UnequippedItem(data, location);
            }

            // Label for each item
            var ItemLabel = new Label
            {
                Text = ItemData.Attribute.ToString() + " + " + ItemData.Value.ToString(),
                HorizontalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.HeadTruncation

            };

            // Frame for label, so that the text will wrap if text is long
            var ItemFrame = new Frame
            {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent,
                WidthRequest = 100,

                Content = ItemLabel,
            };

            // Put ImageButton and label in to same stack
            var ItemStack = new StackLayout
            {
                Padding = 3,
                HorizontalOptions = LayoutOptions.Center,

                Children = { ItemButton, ItemFrame },
            };

            // Render particuler stack
            return ItemStack;
        }

        /// <summary>
        /// Show the Popup for the present character information
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowCharacterPopup(PlayerInfoModel data)
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

            AddItemToDisplay(data);
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
        /// Close item popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CloseItemPopup_Clicked(object sender, EventArgs e)
        {
            PopupLoadingView.IsVisible = false;
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
		public async void CloseButton_Clicked(object sender, EventArgs e)
		{

            if (isGameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;
                await Navigation.PushModalAsync(new MainPage());

                
                return;
            }
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
            DrawItemLists();
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

        /// <summary>
        /// Navigate to score page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ScoreButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScorePage()));
        }

        /// <summary>
        /// Navigate to pickitemPage
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void PickItem_Clicked(ItemModel data)
        {
            await Navigation.PushModalAsync(new PickItemsPage(new GenericViewModel < ItemModel >(data)));

        }

        /// <summary>
        /// Flip feature, toggle betweem item list and attribute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ItemAttributeToggle_Clicked(object sender, EventArgs e)
        {
            if (ItemsFrame.IsVisible)
            {
                ItemsFrame.IsVisible = false;
                AttributeFrame.IsVisible = true;
                ItemAttributeToggleButton.Text = "Attributes";
                return;
            }

            if (AttributeFrame.IsVisible)
            {
                AttributeFrame.IsVisible = false;
                ItemsFrame.IsVisible = true;
                ItemAttributeToggleButton.Text = "Items";
                return;
            }
        }

        public void UnequippedItem(PlayerInfoModel data, ItemLocationEnum location)
        {
            var item = data.GetItemByLocation(location);

            switch (location)
            {
                case ItemLocationEnum.Head:
                    data.Head = null;
                    break;

                case ItemLocationEnum.Necklass:
                    data.Necklass = null;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    data.PrimaryHand = null;
                    break;

                case ItemLocationEnum.OffHand:
                    data.OffHand = null;
                    break;

                case ItemLocationEnum.LeftFinger:
                    data.LeftFinger = null;
                    break;

                case ItemLocationEnum.RightFinger:
                    data.RightFinger = null;
                    break;

                case ItemLocationEnum.Feet:
                    data.Feet = null;
                    break;
            }


            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item);

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Remove(item);

            AddItemToDisplay(data);

            DrawItemLists();


        }

        /// <summary>
        /// Refresh page after come back from PickItemPage
        /// </summary>
        protected override void OnAppearing()
        {
            DrawItemLists();
        }
    }
}