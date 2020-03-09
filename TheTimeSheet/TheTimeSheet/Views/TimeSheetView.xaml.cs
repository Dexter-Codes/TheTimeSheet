using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTimeSheet.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTimeSheet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeSheetView : ContentPage
    {
        TimeSheetViewModel timeSheetViewModel;
        public TimeSheetView()
        {
            InitializeComponent();
            timeSheetViewModel = new TimeSheetViewModel();
            this.BindingContext = timeSheetViewModel;
        }
    }
}