using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipLibrary.Models
{
    public enum GridSpotStatus
    {
        // 0= empty, 1= Ship, 2= miss, 3=hit, 4 =Sunk
        Empty,
        Ship,
        Miss,
        Hit,
        Sunk

    }
}
