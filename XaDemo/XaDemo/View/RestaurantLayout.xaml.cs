using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XaDemo.Data;
using Xamarin.Forms;
using XaDemo.Services;
using System.Collections;

namespace XaDemo.View
{
	public partial class RestaurantLayout : ContentPage
	{

		RestaurantManager restaurant;
		public static Object choose;
		public static Object choose1;
		public static Object choose2;
		public static Object result1;

		public RestaurantLayout(bool a)
		{

			InitializeComponent();

			BackgroundImage = "backforcoupon.jpg";

			coupon1.IsEnabled = false;
			coupon2.IsEnabled = false;
			coupon3.IsEnabled = false;
			comments.IsEnabled = false;
			refresh.IsEnabled = false;

			ResultData.BindingContext = new Restaurant();   //初始化欄位"loading..."
			ResultData1.BindingContext = new Restaurant();   //初始化欄位"loading..."
			ResultData2.BindingContext = new Restaurant();   //初始化欄位"loading..."
			restaurant = new RestaurantManager();   //初始化

			showRestaurant(a,false);   //讀取並顯示餐廳資料

			coupon1.Clicked += OnAlertYesNoClicked1;
			coupon2.Clicked += OnAlertYesNoClicked2;
			coupon3.Clicked += OnAlertYesNoClicked3;
			comments.Clicked += OncommentsClicked;
			refresh.Clicked += OnrefreshClicked;

		}
		private async void OnAlertYesNoClicked1(object sender, EventArgs e)
		{
			var answer = await DisplayAlert(" ", "確定兌換嗎？", "Yes", "No");
			if (answer == true)
			{
				coupon1.IsEnabled = false;
				coupon1.Text = "已兌換";
				coupon1.BackgroundColor = Color.Silver;
				if(choose != null)
				await Navigation.PushAsync(new Coupon(choose), true);
				restaurant.changestatus(1);
			}

		}
		private async void OnAlertYesNoClicked2(object sender, EventArgs e)
		{
			var answer = await DisplayAlert(" ", "確定兌換嗎？", "Yes", "No");
			if (answer == true)
			{
				coupon2.IsEnabled = false;
				coupon2.Text = "已兌換";
				coupon2.BackgroundColor = Color.Silver;
				if (choose != null)
					await Navigation.PushAsync(new Coupon(choose1), true);
				restaurant.changestatus(2);
			}
		}
			private async void OnAlertYesNoClicked3(object sender, EventArgs e)
		{
			var answer = await DisplayAlert(" ", "確定兌換嗎？", "Yes", "No");
			if (answer == true)
			{
				coupon3.IsEnabled = false;
				coupon3.Text = "已兌換";
				coupon3.BackgroundColor = Color.Silver;
				if (choose != null)
					await Navigation.PushAsync(new Coupon(choose2), true);
				restaurant.changestatus(3);
			}
		}

		private async void OncommentsClicked(object sender, EventArgs e)
		{
			var answer = await DisplayAlert("確定填寫評論以獲取額外一張兌換券？", "將覆蓋掉已有的優惠券3！", "Yes", "No");
			if (answer == true)
			{
				await Navigation.PushAsync(new comment(true,this), true);
			}
		}

		private async void OnrefreshClicked(object sender, EventArgs e)
		{
			var answer = await DisplayAlert(" ", "確定花費十點刷新優惠券？", "Yes", "No");
			if (answer == true)
			{
				ResultData.BindingContext = new Restaurant();   //初始化欄位"loading..."
				ResultData1.BindingContext = new Restaurant();   //初始化欄位"loading..."
				ResultData2.BindingContext = new Restaurant();   //初始化欄位"loading..."
				restaurant = new RestaurantManager();   //初始化

				showRestaurant(false,false);   //讀取並顯示餐廳資料
			}
		}

		public void changebtn4status()
		{
			restaurant.changestatus(4);
			comments.IsEnabled = false;
		}

		public async void showRestaurant(bool flag,bool commentflag)
		{
			using (var scope = new ActivityIndicatorScope(syncIndicator, true))     //跑loading圈圈
			{
				try
				{
					//Restaurant_img.Source = ImageSource.FromFile("hourglass.png");  //顯示出loading圖片(沙漏)

					choose = await restaurant.GetRandomRestaurant(1,flag);     //從資料庫隨機抓取一筆資料

					if (choose == null)   //防呆
					{
						await DisplayAlert("找不到餐廳", "找不到餐廳", "確定");
						Navigation.RemovePage(Navigation.NavigationStack[1]);
					}
					ResultData.BindingContext = (Restaurant)choose;             //傳入資料

					choose1 = await restaurant.GetRandomRestaurant(2,flag);     //從資料庫隨機抓取一筆資料

					if (choose1 == null)   //防呆
					{
						await DisplayAlert("找不到餐廳", "找不到餐廳", "確定");
						Navigation.RemovePage(Navigation.NavigationStack[1]);
					}
					ResultData1.BindingContext = (Restaurant)choose1;             //傳入資料

					 choose2 = await restaurant.GetRandomRestaurant(3,flag);     //從資料庫隨機抓取一筆資料


					if (choose2 == null)   //防呆
					{
						await DisplayAlert("找不到餐廳", "找不到餐廳", "確定");
						Navigation.RemovePage(Navigation.NavigationStack[1]);
					}

					if (commentflag == false)
						ResultData2.BindingContext = (Restaurant)choose2;             //傳入資料/// <summary>
																					  /// //////////////////////////////////////////////
					/// 
					else if (commentflag == true)
					{
						result1 = await restaurant.GetThisRestaurant();
						ResultData2.BindingContext = (Restaurant)result1;
						restaurant.setbtn3true();
						restaurant.assignchoose2(result1);
					}

					if (restaurant.deter(1) == true)
					{
						coupon1.IsEnabled = true;
						coupon1.BackgroundColor = Color.Aqua;
						coupon1.Text = "兌換優惠1";
					}
					else {
						coupon1.Text = "已兌換";
						coupon1.BackgroundColor = Color.Silver;
					}
					if (restaurant.deter(2) == true)
					{
						coupon2.IsEnabled = true;
						coupon2.BackgroundColor = Color.Aqua;
						coupon2.Text = "兌換優惠2";
					}
					else {
						coupon2.Text = "已兌換";
						coupon2.BackgroundColor = Color.Silver;
					}
					if (restaurant.deter(3) == true)
					{
						coupon3.IsEnabled = true;
						coupon3.BackgroundColor = Color.Aqua;
						coupon3.Text = "兌換優惠3";
					}
					else {
						coupon3.Text = "已兌換";
						coupon3.BackgroundColor = Color.Silver;
					}
					if (restaurant.deter(4) == true)
					{
						comments.IsEnabled = true;
						comments.BackgroundColor = Color.White;
						comments.Text= "沒抽到喜歡的？";
						comments.TextColor = Color.Red;
					}
					else {
						comments.Text = "已使用";
						comments.TextColor = Color.Silver;
						comments.BackgroundColor = Color.Silver;
					}
					refresh.IsEnabled = true;

				}
				//例外處理
				catch
				{
					await DisplayAlert("網路連線", "請連線網路後繼續", "確定");
					Navigation.RemovePage(Navigation.NavigationStack[1]);
				}

			}

		}

		/*  public async void generateData(bool showActivityIndicator)
		  {
			  using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
			  {
				  await restaurant.GenerateRandomData();
			  }
		  }
		*/

		//控制loading圈圈
		private class ActivityIndicatorScope : IDisposable
		{
			private bool showIndicator;
			private ActivityIndicator indicator;
			private Task indicatorDelay;

			public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
			{
				this.indicator = indicator;
				this.showIndicator = showIndicator;

				if (showIndicator)
				{
					indicatorDelay = Task.Delay(2000);
					SetIndicatorActivity(true);
				}
				else
				{
					indicatorDelay = Task.FromResult(0);
				}
			}

			private void SetIndicatorActivity(bool isActive)
			{
				this.indicator.IsVisible = isActive;
				this.indicator.IsRunning = isActive;

			}

			public void Dispose()
			{
				if (showIndicator)
				{
					indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());

				}
			}
		}
	}
}
