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

		/// <summary>
		/// Constructor
		/// </summary>
		public NewRoundPage ()
		{
			InitializeComponent ();

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
                Padding = 0,
                Spacing = 0,
                Children = {
                    ImageFrame,
                },
            };

            return PlayerStack;
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
	}
}