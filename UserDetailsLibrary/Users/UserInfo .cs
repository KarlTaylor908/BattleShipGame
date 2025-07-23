using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridLibrary;
using GridLibrary.Grid;

namespace UserDetailsLibrary.Users
{
    internal class UserInfo
    {
        public string UserName { get; set; }
        public List<GridSpotModel> ShipLocation {  get; set; }

        public List<GridSpotModel> ShotGrid { get; set; }
    }
}
