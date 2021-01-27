using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views.Characters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellIndexPage : ContentPage
    {
        public CellIndexPage()
        {
            InitializeComponent();
        }

        public async void CreateCell_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CellCreatePage()));
        }
    }
}