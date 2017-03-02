using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimersTimer = System.Timers.Timer;
using Xamarin.Forms;

namespace XaDemo.View
{

	public partial class MainPage : ContentPage
	{
		public static bool flag;
		public int flag1;
		//TimersTimer timer_timer = new TimersTimer(1000);
		public MainPage ()
		{
			flag = false;
			InitializeComponent();
			//CoverImage.Source = ImageSource.FromUri(new Uri("http://i.imgur.com/CSWaeYX.png"));
			CoverImage.Image = "main002.png";
			flag1= 1;
			FindBtn.Clicked += OnFindBtnClicked;
			comment.Clicked += OncmtBtnClicked;
			CoverImage.Clicked += OncoverBtnClicked;

         //   CoverImage.Aspect = Aspect.AspectFit;

        }

        private async void OnFindBtnClicked(object sender, EventArgs e)
        {
			// Page appearance not animated
			await Navigation.PushAsync(new RestaurantLayout(flag), true);
			flag = true;
        }
		private async void OncmtBtnClicked(object sender, EventArgs e)
		{
			// Page appearance not animated
			await Navigation.PushAsync(new comment(false,null), true);	
		}
		private async void OncoverBtnClicked(object sender, EventArgs e)
		{
			// Page appearance not animated
			if (flag1 == 1)
			{
				CoverImage.Image = "main002.png";
				flag1 = 2;
			}
			else if(flag1 == 2)
			{
				CoverImage.Image = "main003.png";
				flag1 = 3;
			}
			else if (flag1 == 3)
			{
				CoverImage.Image = "main004.png";
				flag1 = 4;
			}
			else if (flag1 == 4)
			{
				CoverImage.Image = "main004.png";
				flag1 = 5;
			}
			else if (flag1 == 5)
			{
				CoverImage.Image = "main005.png";
				flag1 = 6;
			}
			else if (flag1 == 6)
			{
				CoverImage.Image = "main001.png";
				flag1 = 1;
			}
			//await Navigation.PushAsync(new comment(), true);
		}


	}
}
