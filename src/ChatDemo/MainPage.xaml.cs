using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatDemo.Data;
using ChatDemo.Models;
using Xamarin.Forms;

namespace ChatDemo
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Task _scrollTask;

        public ICommand TapCommand { get; private set; }
        public ObservableCollection<ChatMessage> Messages { get; private set; } = new ObservableCollection<ChatMessage>(SampleMessages.GenerateMessages(10));

        public MainPage()
        {
            InitializeComponent();
            TapCommand = new Command((arg) => AddMessage());
            BindingContext = this;

            App.KeyboardService.RegisterKeyboardFocusLockInputSource(MessageEntry);
            App.KeyboardService.RegisterKeyboardFocusLockInputSource(SendButton);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChatMessagesLayout.ChildAdded += ChatMessagesScrollView_ChildAdded;
            ChatMessagesLayout.SizeChanged += ChatMessagesLayout_SizeChanged;
            ChatMessagesScrollView.SizeChanged += ChatMessagesLayout_SizeChanged;

            ScrollToBottomElementAsync(animated: false).ContinueWith((task) => { if (task.IsFaulted) throw task.Exception; });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ChatMessagesLayout.ChildAdded -= ChatMessagesScrollView_ChildAdded;
            ChatMessagesLayout.SizeChanged -= ChatMessagesLayout_SizeChanged;
            ChatMessagesScrollView.SizeChanged -= ChatMessagesLayout_SizeChanged;
        }

        void ChatMessagesScrollView_ChildAdded(object sender, ElementEventArgs e)
            => ScrollToBottomElementAsync().ContinueWith((task) => { if (task.IsFaulted) throw task.Exception; });

        void ChatMessagesLayout_SizeChanged(object sender, EventArgs e)
            => ScrollToBottomElementAsync().ContinueWith((task) => { if (task.IsFaulted) throw task.Exception; });

        void AddMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageEntry.Text))
                return;

            var messageText = MessageEntry.Text;
            MessageEntry.Text = string.Empty;

            Messages.Add(new ChatMessage
            {
                Text = messageText,
                Timestamp = DateTime.Now
            });

            // Add sample reply
            Messages.Add(new ChatMessage
            {
                IsInbound = true,
                Timestamp = DateTime.Now,
                Text = SampleMessages.GenerateMessageText(),
            });
        }

        Task ScrollToBottomElementAsync(bool animated = true)
        {
            if (_scrollTask == null || _scrollTask.IsCompleted)
                _scrollTask = ScrollToBottomElementTask(animated);

            return _scrollTask;
        }

        async Task ScrollToBottomElementTask(bool animated = true)
        {
            int heightDelta;
            double currentScroll;
            double targetScroll;

            do
            {
                // This is a workaround specific to Android
                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(200);

                Point point = ChatMessagesScrollView.GetScrollPositionForElement(ChatMessagesLayout, ScrollToPosition.End);

                targetScroll = point.Y;
                currentScroll = ChatMessagesScrollView.ScrollY;

                heightDelta = (int)(targetScroll - currentScroll);

                if (heightDelta <= 0)
                    break;

                // Animation speed for Android is hardcoded to 1000ms which is very slow
                // As a workaround, this is set and adjusted without animation
                await ChatMessagesScrollView.ScrollToAsync(0, targetScroll, Device.RuntimePlatform == Device.iOS && animated); 
            }
            while (heightDelta > 0);
        }
    }
}