﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Engine.EngineKoenig;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;
using System.Threading.Tasks;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		// Hold the Engine, so it can be swapped out for unit testing
		public IAutoBattleInterface AutoBattle = BattleEngineViewModel.Instance.AutoBattleEngine;

		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Defines what happens when the AutoBattleButton is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void AutobattleButton_Clicked(object sender, EventArgs e)
		{

			//loading picture
			AutobattleImage.Source = "cell_gif.gif";
			//AutobattleImage.IsAnimationPlaying = true;

			// Call into Auto Battle from here to do the Battle...

			// To See Level UP happening, a character needs to be close to the next level
			var Character = new CharacterModel
			{
				ExperienceTotal = 300,    // Enough for next level
				Name = "Mike",
				Speed = 100,    // Go first
			};

			var CharacterPlayer = new PlayerInfoModel(Character);

			// Turn on the Koenig version for now...
			//BattleEngineViewModel.Instance.SetBattleEngineToKoenig();

			BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
			//await Task.Run(()=> BattleEngineViewModel.Instance.AutoBattleEngine.RunAutoBattle());
			await BattleEngineViewModel.Instance.AutoBattleEngine.RunAutoBattle();

			var BattleMessage = string.Format("Done {0} Rounds", AutoBattle.Battle.EngineSettings.BattleScore.RoundCount);

			BattleMessageValue.Text = BattleMessage;

			AutobattleImage.Source = "splashscreen.png";
		}
	}
}