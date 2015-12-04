using HueLamps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace HueLamps
{

	public sealed partial class MainPage : Page
	{
		public static ApplicationDataContainer LOCAL_SETTINGS = ApplicationData.Current.LocalSettings;
        private APIfixer api = null;

		public MainPage()
		{
			this.InitializeComponent();
            api = new APIfixer(new Networkfixer());
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            api.Register();
            ObservableCollection<Lamp> allights = new ObservableCollection<Lamp>();
            var lamps = api.GetAllLights(allights);
            listBox.Items.Clear();
            
        }
    }
}
