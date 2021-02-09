using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCellReadPage : ContentPage
    {
        // View Model for Monster
        public readonly GenericViewModel<MonsterModel> ViewModel;

        // Empty Constructor for UTs
        public MonsterCellReadPage(bool UnitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public MonsterCellReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = data.Data.Name + " Information";
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterCellDeletePage(ViewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
		/// Jump to the MonsterCell Update Page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void MonsterCellEditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterCellUpdatePage(ViewModel)));

            await Navigation.PopAsync(); 
        }

        /// <summary>
        /// Flip feature, to toggle between MonsterCell thumbnail and description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowDescriptionClicked (object sender, EventArgs e)
        {
            // Hide image, show description
            if (ImageFrame.IsVisible)
            {
                ImageFrame.IsVisible = false;
                DescriptionFrame.IsVisible = true;
                
            } 
            // Hide description, show image
            else if (DescriptionFrame.IsVisible)
            {
                DescriptionFrame.IsVisible = false;
                ImageFrame.IsVisible = true;
            }
         
        }

    }
}