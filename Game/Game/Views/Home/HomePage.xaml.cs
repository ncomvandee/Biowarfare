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
			bool nextStyle = false; 

				// Device timer will change the background color every 1 second from purple to red 
				Device.StartTimer(TimeSpan.FromSeconds(1), () =>
					{
						if (originalStyle && !nextStyle)
						{
							// Change the background color to purple 
							Resources["baseStyle"] = Resources["PurpleHomePageContentStyle"];
							originalStyle = false;
							nextStyle = true; 
						}
						else if (!originalStyle && nextStyle)
						{
							// Change the background color to transition color 
							Resources["baseStyle"] = Resources["RedPurpleHomePageContentStyle"];
							nextStyle = false; 
						}
						else if (!originalStyle && !nextStyle)
						{
							// Change the background color to red 
							Resources["baseStyle"] = Resources["RedHomePageContentStyle"];
							originalStyle = true;
							nextStyle = true; 
						}
						else
						{
							// Change the background color to transition color 
							Resources["baseStyle"] = Resources["RedPurpleHomePageContentStyle"];
							nextStyle = false;
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