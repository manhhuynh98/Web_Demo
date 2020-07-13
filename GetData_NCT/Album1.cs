using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetData_NCT
{
    public class Album1
    {
        public string img { get; set; }
        public string name { get; set; }
        public Album1()
        {
        }

        public Album1(string img, string name)
        {
            this.img = img;
            this.name = name;
        }

    }
}
