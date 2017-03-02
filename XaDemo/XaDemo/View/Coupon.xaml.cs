using System;
using XaDemo.Data;
using Xamarin.Forms;
using TimersTimer = System.Timers.Timer;
using System.Threading.Tasks;
using Android.App;

namespace XaDemo
{
	public partial class Coupon : ContentPage
	{
		public int sec = 300;
		TimersTimer timer_timer = new TimersTimer(1000);
		TimeSpan duration = new TimeSpan(0, 0, 300);

		public Coupon(object a)
		{
			InitializeComponent();
			BackgroundImage = "backgroundforcoupon.jpg";
			var tmp = (Restaurant)a;
			cou1_name.Text = tmp.Name;
			cou1_description.Text = tmp.Description;
			Restaurant_img.Source = ImageSource.FromUri(new Uri(tmp.Image));   //從網路載入圖片
			Restaurant_img.Aspect = Aspect.AspectFit;
			confirm.Clicked += OnConfirmBtnClicked;
			timer.Clicked += OntimerBtnClicked;
			countdown();

		}
		public void countdown()
		{
			timer_timer.Enabled = true;
			timer_timer.Start();
			timer_timer.Elapsed += timer_Tick;
		}
		private void timer_Tick(object sender, System.Timers.ElapsedEventArgs e)
		{
			sec -= 1;

			if (sec == 0)
			{
				DisplayAlert("Time's up!", "時間到囉!", "確認");
				timer_timer.Stop();
				timer_timer.Enabled = false;
			}
				//await Navigation.PopAsync(true);

		}
		private async void OntimerBtnClicked(object sender, EventArgs e)
		{
			duration = new TimeSpan(0, 0, sec);

			string update = "剩餘時間: " + duration.ToString(@"mm\:ss");

			timer.Text = update;

			if (sec == 0)
			{
				await DisplayAlert("Time's up!", "時間到囉!", "確認");
			}
		}

		private async void OnConfirmBtnClicked(object sender, EventArgs e)
		{
			//Navigation.RemovePage(this);

			await Navigation.PopAsync(true);
		}

	}
}
