using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamps
{
	public class Lamp
	{
		public Lamp(APIfixer inApi,int inID, int inBri, bool inOn, int inHue, int inSat, string inName, string inType)
		{
			_api = inApi;
			ID = inID;
			_bri = inBri;
			_on = inOn;
			_hue = inHue;
			_sat = inSat;
			Name = inName;
			Type = inType;
		}

		public int ID { get; private set; }

		public int Sat
		{
			get { return _sat; } 
			set { _sat = value; Update(); }
		}
		
		public int Hue
		{
			get { return _hue; }
			set { _hue = value; Update(); }
		}

		public bool On
		{
			get { return _on; }
			set { _on = value;  Update();}
		}

		public void Toggle()
		{
			_on = !_on;
			Update();
		}

		public int Bri
		{
			get { return _bri; }
			set { _bri = value;  Update();}
		}

		public string Name { get; private set; }

		public string Type { get; private set; }

		private void Update()
		{
			_api.SetLightValues(this);
			_api.SetLightState(this);
		}

		private APIfixer _api;
		private int _sat;
		private int _hue;
		private bool _on;
		private int _bri;


	}
}
