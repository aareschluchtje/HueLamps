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
		private APIfixer api;
		private ObservableCollection<Lamp> totallamps;
		private bool _justSelected = false;

		private int SelectedID
		{
			get { return Int32.Parse(LightListBox.SelectedItem.ToString().Substring(5)); }
		}

		public MainPage()
		{
			this.InitializeComponent();
			api = new APIfixer(new Networkfixer());
		}

		private async void button_Click(object sender, RoutedEventArgs e)
		{
			api.Register();
			totallamps = new ObservableCollection<Lamp>();
			ObservableCollection<Lamp> lamps = await api.GetAllLights(totallamps);
			LightListBox.Items.Clear();
			foreach (Lamp lamp in lamps)
			{
				LightListBox.Items.Add("Lamp " + lamp.ID);
				lamp.On = false;
				api.SetLightState(lamp);
				lamp.Hue = 0; //hue 0 - 65280
				lamp.Bri = 254; //brightness 0 - 254
				lamp.Sat = 254; //saturation 0 - 254
				api.SetLightValues(lamp);
			}
		}

		private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			if (_justSelected == false)
			{
				foreach (Lamp lamp in totallamps)
				{
					if (lamp.ID == SelectedID)
					{
						lamp.Toggle();
					}
				}
			}
		}

		private void onListSelectionChanged(object sender, RoutedEventArgs e)
		{
			_justSelected = true;
			if (totallamps != null)
			{
				foreach (Lamp lamp in totallamps)
				{
					if (lamp.ID == SelectedID)
					{
						OnOffButton.IsOn = lamp.On;
						HueSlider.Value = lamp.Hue;
						SatSlider.Value = lamp.Sat;
						BriSlider.Value = lamp.Bri;
					}
				}
			}
			_justSelected = false;

		}

		private void Slider_Released(object sender, PointerRoutedEventArgs e)
		{
			if(_justSelected == false)
			{
				if (totallamps != null)
				{
					foreach (Lamp lamp in totallamps)
					{
						if (lamp.ID == SelectedID)
						{
							if (sender == HueSlider)
							{
								lamp.Hue = (int)HueSlider.Value;
							}
							else if (sender == SatSlider)
							{
								lamp.Sat = (int)SatSlider.Value;
							}
							else if (sender == BriSlider)
							{
								lamp.Bri = (int)BriSlider.Value;
							}
						}
					}
				}			
			}
		}
	}
}
