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
        private ObservableCollection<Lamp> totallamps;

		public MainPage()
		{
			this.InitializeComponent();
            api = new APIfixer(new Networkfixer());
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            api.Register();
            totallamps = new ObservableCollection<Lamp>();
            ObservableCollection<Lamp> lamps = await api.GetAllLights(totallamps);
            listBox.Items.Clear();
            foreach (Lamp lamp in lamps)
            {
                listBox.Items.Add("Lamp " + lamp.id);
                lamp.on = true;
                api.SetLightState(lamp);
                lamp.hue = 46920; //hue 0 - 65280
                lamp.bri = 254; //brightness 0 - 254
                lamp.sat = 254; //saturation 0 - 254
                api.SetLightValues(lamp);
            }
        }
    }
}
