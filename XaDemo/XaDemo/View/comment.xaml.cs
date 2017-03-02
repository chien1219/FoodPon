using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XaDemo.Services;
using XaDemo.View;
using XaDemo.Data;

namespace XaDemo
{
	public partial class comment : ContentPage
	{
		public string Name;
		public string comments;
		public bool a;
		public RestaurantLayout tmp;
		public static object temp;


		public comment(bool flag,RestaurantLayout k)
		{
			InitializeComponent();
			confirm.Clicked += OnConfirmBtnClicked;
			a = flag;
			if(a==true)
			tmp = k;
			name.Completed += EditorCompleted;
			comm.Completed += EditorComplete;
		}

		private async void OnConfirmBtnClicked(object sender, EventArgs e)
		{
			if (res.SelectedIndex == -1)
				await DisplayAlert("Warning", "請選擇餐廳", "確定");
			else if (meal.SelectedIndex == -1)
				await DisplayAlert("Warning", "請選擇餐點", "確定");
			else if (Name == null)
				await DisplayAlert("Warning", "請輸入名字", "確定");
			else if (comments == null)
				await DisplayAlert("Warning", "請輸入意見", "確定");
		

			else {
				if (a == true)
				{
					RestaurantManager.selectedValue = res.Items[res.SelectedIndex];

					tmp.changebtn4status();

					tmp.showRestaurant(true, true);

				}
				                   
				await DisplayAlert("","評論已新增" , "確定");
				await Navigation.PopAsync(true);
			}
		}

		public void EditorCompleted(object sender, EventArgs e)
		{
			Name = ((Editor)sender).Text; // sender is cast to an Editor to enable reading the `Text` property of the view.
		}
		public void EditorComplete(object sender, EventArgs e)
		{
			comments = ((Editor)sender).Text; // sender is cast to an Editor to enable reading the `Text` property of the view.
		}
	}
}
