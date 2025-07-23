using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GridLibrary.Grid
{
    public class GridSpotModel
    {
        public string SpotLetter {  get; set; }
        public string SpotName { get; set;}

        public int MyProperty { get; set; } // 0= empty, 1= sunk, 2= miss, 3=hit
    }
}
