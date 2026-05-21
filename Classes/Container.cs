using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHackthon.Classes
{
    public class Container
    {
        public int bay { get; set; }
        public int row { get; set; }
        public int tier { get; set; }
        public string container { get; set; } = string.Empty;
        public DateTime entrada { get; set; }
        public DateTime saida { get; set; }

        public Container Clone()
        {
            return (Container)this.MemberwiseClone();
        }
    }
}
