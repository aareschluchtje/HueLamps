using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamps
{
    public class Light
    {
        public int sat;
        public int hue;
        public bool on;
        public int bri;
        public int id;
        public APIfixer api;
        public string name;
        public string type;
    }
}
