using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HueLamps
{
	public class APIfixer
	{

		Networkfixer nwf;
		public APIfixer(Networkfixer nwf)
		{
			this.nwf = nwf;
			
		}

		public async Task Register()
		{
			try
			{
				var json = await nwf.RegisterName("Hue", "Remco");
				json = json.Replace("[", "").Replace("]", "");
				JObject o = JObject.Parse(json);
				string id = o["success"]["username"].ToString();
				MainPage.LOCAL_SETTINGS.Values["id"] = id;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Could not register.");
			}
		}

		public async void SetLightState(Lamp l)
		{
			var json = await nwf.SetLightInfo(l.ID, $"{{\"on\": {((l.On) ? "true" : "false")}}}");
			Debug.WriteLine(json);
		}

		public async void SetLightValues(Lamp l)
		{
			if (l.On)
			{
				Debug.WriteLine(l.Hue);
				var json = await nwf.SetLightInfo(l.ID, $"{{\"bri\": {l.Bri},\"hue\": {(l.Hue)},\"sat\": {l.Sat}}}");
				Debug.WriteLine(json);
			}

		}

		public async Task<ObservableCollection<Lamp>> GetAllLights(ObservableCollection<Lamp> alllights)
		{
			try
			{
				var json = await nwf.AllLights();
				JObject o = JObject.Parse(json);
				foreach (var i in o)
				{
					var light = o[i.Key];
					var state = light["state"];
					alllights.Add(new Lamp(this,
											Int32.Parse(i.Key),
											(int)state["bri"],
											((string)state["on"]).ToLower() == "true" ? true : false,
											(int)state["hue"],
											(int)state["sat"],
											(string)light["name"],
											(string)light["type"]));

					Debug.WriteLine("Added light number " + i + " " + state["on"]);
				}

				//lightlist = lightlist.OrderBy(q => q.Name).ToList();

				//lightlist.Sort(
				//    delegate (Light p1, Light p2)
				//   {
				//       return p1.Name.CompareTo(p2.Name);
				//   }
				// );

				//lightlist.ForEach(q => alllights.Add(q));
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.StackTrace);
				Debug.WriteLine("Could not get all lights.");
			}
			return alllights;
		}
	}
}
