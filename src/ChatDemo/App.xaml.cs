using System;
using ChatDemo.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo
{
    public partial class App : Application
    {
        public static IKeyboardService KeyboardService { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
