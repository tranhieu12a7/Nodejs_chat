using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF_Nodejs.Models;
using XF_Nodejs.ViewModels;

namespace XF_Nodejs
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
       public static EventHandler GoScrollTo;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            GoScrollTo += GoOnAppearing;
        }

        private void GoOnAppearing(object sender, EventArgs e)
        {
            var item_end = LstViewChat.ItemsSource.Cast<Messenger_Model>().LastOrDefault();
            Device.BeginInvokeOnMainThread(() =>
            {
                LstViewChat.ScrollTo(item: item_end, position: ScrollToPosition.End, false);
            });
        }


           
        

    }
}
