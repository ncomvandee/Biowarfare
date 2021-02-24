using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public HomePage ()
		{
			InitializeComponent ();

			NavigationPage.SetHasNavigationBar(this, false);

			// Bool to set the background style based on timer 
			bool originalStyle = true;

				// Device timer will change the background color every 2 seconds from purple to red 
				Device.StartTimer(TimeSpan.FromSeconds(2), () =>
					{
						if (originalStyle)
						{
							// Change the background color to purple 
							Resources["baseStyle"] = Resources["PurpleHomePageContentStyle"];
							originalStyle = false;
						}
						else
						{
							// Change the background color to red 
							Resources["baseStyle"] = Resources["RedHomePageContentStyle"];
							originalStyle = true;
						}

						// Repeat again indefinately 
						return true; 
				});
	
		}

		/// <summary>
		/// Example of a Button Click (this one is Sync, if calling Async then needs to be Async)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        public async void GameButton_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new GamePage());
		}
    }
}