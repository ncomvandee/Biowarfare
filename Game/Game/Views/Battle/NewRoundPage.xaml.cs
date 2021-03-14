using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewRoundPage: ContentPage
	{
		// This uses the Instance so it can be shared with other Battle Pages as needed
		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
        
        // View Model for Item
        public readonly GenericViewModel<CharacterModel> ViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        public NewRoundPage ()
		{
			InitializeComponent ();

            PageTitle.Text = "Begin Round " + BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString();

            // Draw the Characters
            foreach (var data in EngineViewModel.Engine.EngineSettings.CharacterList)
			{
                PartyListFrame.Children.Add(CreatePlayerDisplayBox(data));
			}

			// Draw the Monsters
			foreach (var data in EngineViewModel.Engine.EngineSettings.MonsterList)
			{
				MonsterListFrame.Children.Add(CreatePlayerDisplayBox(data));
			}

		}

		/// <summary>
		/// Start next Round, returning to the battle screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void BeginButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Return a stack layout with the Player information inside
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public Frame CreatePlayerDisplayBox(PlayerInfoModel data)
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

          
            // If image is clickable show the popup page
            if (ClickAble)
            {
                PlayerImage.Clicked += (sender, args) => ShowPopup(data);
            }

            return ImageFrame;
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
        /// Shows the popup frame that present character information
        /// </summary>
        /// <param name="data"></param>
        public void ShowPopup (PlayerInfoModel data)
        {
            CharacterPopUpFrame.IsVisible = true;

            if (data.PlayerType == PlayerTypeEnum.Character)
            {
                CharacterPopupCellType.Text = data.Job.ToString();
            }

            if (data.PlayerType == PlayerTypeEnum.Monster)
            {
                CharacterPopupCellType.Text = data.MonsterType.ToString();
            }

            CharacterPopUpImage.Source = data.ImageURI;
            CharacterPopUpName.Text = data.Name;
            CharacterPopupLevel.Text = data.Level.ToString();
            Attack.Text = data.GetAttackTotal.ToString();
            Defense.Text = data.GetDefenseTotal.ToString();
            Speed.Text = data.GetSpeedTotal.ToString();
            Health.Text = data.GetCurrentHealthTotal.ToString();
            Experience.Text = data.ExperienceTotal.ToString();

            AddItemToDisplay(data);

        }

        /// <summary>
        /// Close popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            CharacterPopUpFrame.IsVisible = false;
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


    }
}