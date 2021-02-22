using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views.Items
{
    /// <summary>
    /// Tabbed page for easy item index navigation between item types. 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemIndexNavigation : TabbedPage
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ItemIndexNavigation()
        {
            InitializeComponent();
        }
    }
}