using LakerScouting.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LakerScouting
{
    public partial class TeamDirectory : ContentPage
    {
        TeamManager manager;

        public TeamDirectory()
        {
            InitializeComponent();

            manager = TeamManager.DefaultManager;

            // OnPlatform<T> doesn't currently support the "Windows" target platform, so we have this check here.
            if (manager.IsOfflineEnabled &&
                (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone))
            {
                var syncButton = new Button
                {
                    Text = "Sync items",
                    HeightRequest = 30
                };
                syncButton.Clicked += OnSyncItems;

                buttonsPanel.Children.Add(syncButton);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            await RefreshItems(true, syncItems: false);
        }

        // Data methods
        async Task AddTeam(Team team)
        {
            await manager.SaveTeamAsync(team);
            teamDirectory.ItemsSource = await manager.GetTeamsAsync();
        }
        public async void OnAdd(object sender, EventArgs e)
        {
            var newTeam = new Team ();
            await AddTeam(newTeam);

            newItemName.Text = string.Empty;
            newItemName.Unfocus();
        }
        // Event handlers
        public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var team = e.SelectedItem as Team;
            if (Device.OS != TargetPlatform.iOS && team != null)
            {
                //// Not iOS - the swipe-to-delete is discoverable there
                //if (Device.OS == TargetPlatform.Android)
                //{
                //    await DisplayAlert(todo.Name, "Press-and-hold to complete task " + todo.Name, "Got it!");
                //}
                //else
                //{
                //    // Windows, not all platforms support the Context Actions yet
                //    if (await DisplayAlert("Mark completed?", "Do you wish to complete " + todo.Name + "?", "Complete", "Cancel"))
                //    {
                //        await CompleteItem(todo);
                //    }
                //}
            }

            // prevents background getting highlighted
            teamDirectory.SelectedItem = null;
        }

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#context
        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#pulltorefresh
        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }

        public async void OnSyncItems(object sender, EventArgs e)
        {
            await RefreshItems(true, true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                teamDirectory.ItemsSource = await manager.GetTeamsAsync(syncItems);
            }
        }

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
