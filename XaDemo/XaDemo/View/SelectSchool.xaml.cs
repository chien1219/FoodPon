using System;
using System.Collections.Generic;
using XaDemo.Data;
using Xamarin.Forms;
using XaDemo.View;


namespace XaDemo
{
	public partial class SelectSchool : ContentPage
	{
		public SelectSchool()
		{
			InitializeComponent();
			confirmschool.Clicked += OnConfirmBtnClicked;
			school.Title = "NCTU";
			BackgroundImage = "background.jpq";
		}

		private async void OnConfirmBtnClicked(object sender, EventArgs e)
		{
			// Page appearance not animated
			await Navigation.PushAsync(new MainPage(), true);
		}

	}
}
