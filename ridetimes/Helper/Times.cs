using ridetimes.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ridetimes.Helper
{
    public static class Times
    {
        public static async void Timer()
        {
            /*var timer = new System.Threading.Timer((e) =>
            {
                Init();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));*/
            Init();
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(5)).ContinueWith((antecedent) => { Console.WriteLine("Executing Init()"); });
                Init();
            }
        }
        public static async Task Init()
        {
            CurrentRides.Update();
            var stamp = DateTime.Now;
            //need to move to another thread

            //TEMP DO NOT DELETE!!!!!!!
            //Task t = Task.Factory.StartNew(() =>
            //{
            //    Database.Insert(CurrentRides.Disneyland, (int)Parks.Disneyland, stamp);
            //    Database.Insert(CurrentRides.CaliforniaAdventure, (int)Parks.CaliforniaAdventure, stamp);
            //});
        }

        public static async void countdown(Auth_Token token)
        {
            while (token.expires_in > 0)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
                token.expires_in -= 1;
                Debug.WriteLine("expires in: " + token.expires_in);
            }
        }
    }
}
