using System;
using TheTimeSheet.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTimeSheet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MonthlyTimeSheet();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
