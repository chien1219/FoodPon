using System;
using System.Collections.Generic;
using System.Text;
using XaDemo.Data;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections;

namespace XaDemo.Services
{
    public class RestaurantManager
    {
        IMobileServiceTable<Restaurant> restaurants;
        MobileServiceClient client;

		public static object res1;
		public static object res2;
		public static object res3;
		public static object tofind;
		public int rep1;
		public int rep2;
		public static bool btn1;
		public static bool btn2;
		public static bool btn3;
		public static bool btn4;
		public static string selectedValue;


        public RestaurantManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            this.restaurants = client.GetTable<Restaurant>();
   
            
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }
     
        public async Task GenerateRandomData()
        {
            List<Restaurant> resList = new List<Restaurant> {
                new Restaurant("Burger King", "Tainan", "Fast food for burger", "Fast Food" ),
                new Restaurant("Mac Donald", "US", "Best Burger for American", "Fast Food"),
                new Restaurant("KFC", "Tainan", "The Best Fried Chicked", "Fast Food"),
                new Restaurant("Subway", "Tainan", "Good Salad", "Fast Food")
            };

            foreach (var res in resList)
            {
                await restaurants.InsertAsync(res);

            }            
        }

        //從資料庫抓資料
        public async Task<Object> GetRandomRestaurant(int count,bool flag)
        {
            var allRestaruant = await restaurants.Where(r => r.School == Constants.SchoolID).ToListAsync(); //抓符合學校ID的所有資料
            if (allRestaruant.Count == 0)
                return null;
            var rand = new Random();
			if (flag == false)
			{
				btn1 = true;
				btn2 = true;
				btn3 = true;
				btn4 = true;
				if (count == 1)
				{
					rep1 = rand.Next(allRestaruant.Count);
					res1 = allRestaruant[rep1];
					return res1;
				}
				else if (count == 2)
				{
					rep2 = rand.Next(allRestaruant.Count);
					while (rep2 == rep1)
					{
						rand = new Random();
						rep2 = rand.Next(allRestaruant.Count);
					}
					res2 = allRestaruant[rep2];
					return res2;
				}
				else if (count == 3)
				{
					int tmp = rand.Next(allRestaruant.Count);
					while (tmp == rep2 || tmp == rep1)
					{
						rand = new Random();
						tmp = rand.Next(allRestaruant.Count);
					}
					res3 = allRestaruant[tmp];
					return res3;
				}

			}  //回傳一筆隨機資料
			else if (count == 1)
			{
				return res1;
			}
			else if (count == 2)
			{
				return res2;
			}
			else if (count == 3)
			{
				return res3;
			}
			return null;
        }
		public void changestatus(int count)
		{
			if (count == 1)
				btn1 = false;
			else if (count == 2)
				btn2 = false;
			else if (count == 3)
				btn3 = false;
			else if (count == 4)
				btn4 = false;
		}
		public bool deter(int count)
		{
			if (count == 1)
			{
				if (btn1 == true) return true;
			}
				else if (count == 2)
				{
					if (btn2 == true) return true;
				}
					else if (count == 3)
					{
						if (btn3 == true) return true;
					}
			else if (count == 4)
			{
				if (btn4 == true) return true;
			}
			return false;
		}

		public async Task<Object> GetThisRestaurant()
		{
			var allRestaruant = await restaurants.Where(r => r.School == Constants.SchoolID).ToListAsync(); //抓符合學校ID的所有資料

			if (allRestaruant.Count == 0)
				return null;

		    tofind = allRestaruant.Find(a => a.Name == selectedValue); //抓符合name的所有資料

			return tofind;
		}

		public void assignchoose2(object a)
		{
			if(a!=null)
			res3 = a;
		}

		public void setbtn3true()
		{
			btn3 = true;
		}

    }
}
