using System;
using System.Collections.Generic;
using System.Linq;

using Windows.UI.Xaml.Controls;

namespace jcRTSPV
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            hamburgerMenuControl.ItemsSource = MenuItem.GetMainItems();
            hamburgerMenuControl.OptionsItemsSource = MenuItem.GetOptionsItems();

            selectMenuItem(MenuItem.GetMainItems().FirstOrDefault());
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;

            selectMenuItem(menuItem);
        }

        private void selectMenuItem(MenuItem menuItem)
        {
            contentFrame.Navigate(menuItem.PageType);
        }
    }

    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public string Name { get; set; }
        public Type PageType { get; set; }

        public static List<MenuItem> GetMainItems()
        {
            var items = new List<MenuItem>
            {
                new MenuItem() {Icon = Symbol.Camera, Name = "Live Feed", PageType = typeof(Views.MainPage)}
            };

            return items;
        }

        public static List<MenuItem> GetOptionsItems()
        {
            var items = new List<MenuItem>
            {
                new MenuItem() {Icon = Symbol.Setting, Name = "Settings", PageType = typeof(Views.SettingsPage)}
            };

            return items;
        }
    }
}