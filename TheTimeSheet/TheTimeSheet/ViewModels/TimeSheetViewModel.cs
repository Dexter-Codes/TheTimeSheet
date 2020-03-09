using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TheTimeSheet.ViewModels
{
    public class TimeSheetViewModel:INotifyPropertyChanged
	{

		#region fields
		private List<Team> teams;
		private Team selectedItem;
		private bool isRefreshing;
		private bool _canEdit=true;
		private bool _canWrite=false;
		private string _currentMonth="November";
		#endregion

		#region Properties
		public List<Team> Teams
		{
			get { return teams; }
			set { teams = value; OnPropertyChanged(nameof(Teams)); }
		}

		public Team SelectedTeam
		{
			get { return selectedItem; }
			set
			{
				selectedItem = value;
				System.Diagnostics.Debug.WriteLine("Team Selected : " + value?.Name);
			}
		}

		public bool IsRefreshing
		{
			get { return isRefreshing; }
			set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
		}
		public string CurrentMonth
		{
			get { return _currentMonth; }
			set { _currentMonth = value; OnPropertyChanged(nameof(CurrentMonth)); }
		}
		public bool CanEdit
		{
			get { return _canEdit; }
			set { _canEdit = value; OnPropertyChanged(nameof(CanEdit)); }
		}		
		 public bool CanWrite
		{
			get { return _canWrite; }
			set { _canWrite = value; OnPropertyChanged(nameof(CanWrite)); }
		}
		public ICommand RefreshCommand { get; set; }
		public ICommand EditCommand { get; set; }
		#endregion

		public TimeSheetViewModel()
		{
			Teams = DummyDataProvider.GetTeams();
			RefreshCommand = new Command(CmdRefresh);
			EditCommand = new Command(OnEdit);
			AddDatesToCalendar(DateTime.Today);
		}

		public void AddDatesToCalendar(DateTime dateTime)
		{
			int iDay = (int)dateTime.Day;
			int iMonth = (int)dateTime.Month;
			int iYear = (int)dateTime.Year;
			//var x = dateTime.DayOfWeek;
			int iStartDayWeek = (int)new DateTime(iYear, iMonth, iDay).DayOfWeek;
			//DateTime FirstDate = new DateTime(dateTime.Year, dateTime.Month, 1);
			//int iDate = 1;
			int daysInMonth = 0;
			daysInMonth = DateTime.DaysInMonth(iYear, iMonth);

			if (dateTime.Month == 2)
			{
				if (DateTime.IsLeapYear(dateTime.Year))
				{
					daysInMonth = 29;
				}
			}

			int iDays = iDay;
			if (iDay > iStartDayWeek)
			{
				//Months = iMonth;
				iDays = iDay - iStartDayWeek;
			}
			else
			{
				if (iDays < 8)    //if Date bet 1 to 6 and the Last Months dates
				{
					iDays = 1;

				}
			}

			for(int colm=0;colm<=15;colm++)
			{
				for(int row=1;row<=7;row++)
				{
					var label = new Label
					{
						Text = row.ToString(),
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						HorizontalTextAlignment = TextAlignment.Center,
					};
					//TimeSheetGrid.Children.Add(label, dayOfWeek, row);
				}
			}



		}
		private void OnEdit(object obj)
		{
			CanEdit = false;
			CanWrite = true;
		}

		private async void CmdRefresh()
		{
			IsRefreshing = true;
			// wait 3 secs for demo
			await Task.Delay(3000);
			IsRefreshing = false;
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}

		#endregion
	}
	static class DummyDataProvider
	{
		public static List<Team> GetTeams()
		{
			var assembly = typeof(DummyDataProvider).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("TheTimeSheet.teams.json");
			string json = string.Empty;

			using (var reader = new StreamReader(stream))
			{
				json = reader.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<List<Team>>(json);
		}
	}
	public class Team
	{
		public string Name { get; set; }
		public int? Win { get; set; }
		public int? Loose { get; set; }
		public double? Percentage { get; set; }
		public string Conf { get; set; }
		public string Div { get; set; }
		public string Home { get; set; }
		public string Road { get; set; }
		public string Last10 { get; set; }
		public string Streak { get; set; }
		public string Logo { get; set; }


		public string Week1 { get; set; }
		public string Week1Date { get; set; }
		public string Week1Overtime { get; set; }
		public string Week2 { get; set; }
		public string Week2Date { get; set; }
		public string Week2Overtime { get; set; }

		public string Week3 { get; set; }
		public string Week3Date { get; set; }
		public string Week3Overtime { get; set; }

		public string Week4 { get; set; }
		public string Week4Date { get; set; }
		public string Week4Overtime { get; set; }

		public string Week5 { get; set; }
		public string Week5Date { get; set; }
		public string Week5Overtime { get; set; }
		public string TotalWeeklyHours { get; set; }

	}
}
