using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTimeSheet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthlyTimeSheet : ContentPage
    {
		public List<GridBox> Blocks { get; set; }
        public MonthlyTimeSheet()
        {
            InitializeComponent();
			Blocks = new List<GridBox>();
			AddDatesToCalendar(DateTime.Today);
        }

		public void AddDatesToCalendar(DateTime dateTime)
		{
			int iD = 0;
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
			iDay = 1;
			iMonth = 2;
			daysInMonth = 28;
			iStartDayWeek = 6;
			for (int colm = 0; colm <= 15; colm=colm+3)
			{
				for (int row = 1; row <= 6; row++)
				{
					if (iStartDayWeek == row)
					{						
						if (iDay < daysInMonth)
						{

							var label = new Entry
							{
								Text = $"{iDay}/{iMonth}/{iYear}",
								HorizontalOptions = LayoutOptions.End,
								VerticalOptions = LayoutOptions.End,
								HorizontalTextAlignment = TextAlignment.End,
							};
							iD++;
							Blocks.Add(new GridBox 
							{
								DateEntered=label.Text,
								HorizontalBlocks=row,
								VerticalBlocks=colm,
								Id=iD
							});
							TimeSheetGrid.Children.Add(label, colm, row);

							iDay++;
							iStartDayWeek = (int)new DateTime(iYear, iMonth, iDay).DayOfWeek;
							if (iStartDayWeek == 0)
							{
								iStartDayWeek++;
								if(iDay < daysInMonth )
								iDay++;
							}
							if(iStartDayWeek == 6)
							{
								iStartDayWeek=1;
								if (iDay < daysInMonth)
									iDay = iDay + 2;
							}
						}
						else
						{
							break;
						}
					}
				}
			}

			for (int colm = 1; colm <= 15; colm = colm +3)
			{
				for (int row = 1; row <= 6; row++)
				{
					var label = new Label
					{
						Text = "8",
						HorizontalOptions = LayoutOptions.End,
						VerticalOptions = LayoutOptions.End,
						HorizontalTextAlignment = TextAlignment.End,
					};
					TimeSheetGrid.Children.Add(label, colm, row);
				}
			}
			for (int colm = 2; colm <= 15; colm = colm + 3)
			{
				for (int row = 1; row <= 6; row++)
				{
					var label = new Label
					{
						Text = "NA",
						HorizontalOptions = LayoutOptions.End,
						VerticalOptions = LayoutOptions.End,
						HorizontalTextAlignment = TextAlignment.End,
					};
					TimeSheetGrid.Children.Add(label, colm, row);
				}
			}
			var x = Blocks;

		}
		public class GridBox
		{
			public int HorizontalBlocks { get; set; }
			public int VerticalBlocks { get; set; }
			public string DateEntered { get; set; }
			public int Id { get; set; }
		}

	}
}