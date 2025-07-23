using BattleShipLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BattleShipLibrary
{
    public static class GameLogic
    {
        public static int GetShotCount(UserInfo winner)
        {
            int counter = 0;
            foreach (var shot in winner.ShotGrid)
            {
                if (shot.Status != GridSpotStatus.Empty)
                {
                    counter++;
                }
            }

            return counter;
        }

        public static bool IdentifyShotResult(UserInfo opponent, string row, int column)
        {
            GridSpotModel shipResult = new GridSpotModel
            {
                SpotLetter = row.ToUpper(),
                SpotNumber = column,
            };

            bool shotHit = false;
            bool shotProcessed = false;


            foreach (var ship in opponent.ShipLocation)
            {
                {

                    if (ship.SpotLetter == shipResult.SpotLetter && ship.SpotNumber == shipResult.SpotNumber)
                    {
                        ship.Status = GridSpotStatus.Sunk;
                        shotHit = true;
                        shotProcessed = true;
                        break;

                    }
                    else
                    {
                        shotHit = false;
                        shotProcessed = true;

                    }

                }

            }
            return shotHit;
        }

        public static void InitaliseGrid(UserInfo model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numb = new List<int>()
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (string letter in letters)
            {
                foreach (int num in numb)
                {
                    AddGridSpot(model, letter, num);

                }
            }
        }

        public static void MarkShotResult(UserInfo activePlayer, string row, int column, bool isAHit)
        {

            GridSpotModel shipResult = new GridSpotModel
            {
                SpotLetter = row.ToUpper(),
                SpotNumber = column,
            };

            foreach (var spot in activePlayer.ShotGrid)
            {
                if (spot.SpotLetter == shipResult.SpotLetter && spot.SpotNumber == shipResult.SpotNumber)
                {
                    if (isAHit)
                    {
                        spot.Status = GridSpotStatus.Hit;
                        break;
                    }
                    if (isAHit ==false)
                    {
                        spot.Status = GridSpotStatus.Miss;
                        break;
                    }
                }


            }
        }

        public static bool PlaceShip(UserInfo model, string location)
        {

            bool numeric = int.TryParse(location.Substring(1), out int number);


            GridSpotModel newShip = new GridSpotModel
            {
                SpotLetter = location.Substring(0, 1).ToUpper(),
                SpotNumber = number,
                Status = GridSpotStatus.Ship
            };

            bool isValid = false;


            var checkSpot = model.ShotGrid.FirstOrDefault(spot => spot.SpotLetter == newShip.SpotLetter && spot.SpotNumber == newShip.SpotNumber);
            var checkShip = model.ShipLocation.FirstOrDefault(ship => ship.SpotLetter == newShip.SpotLetter && ship.SpotNumber == newShip.SpotNumber);
            {
                if (checkShip == null || checkShip.Status != GridSpotStatus.Ship)
                {
                    if (checkSpot != null)
                    {
                        model.ShipLocation.Add(newShip);
                        isValid = true;

                    }
                }

            }




            return isValid;
        }

        public static bool PlayerStillActive(UserInfo opponent)
        {

            GridSpotModel gs = new GridSpotModel();

            bool isValid = false;

            if (opponent.ShipLocation.Any(spot => spot.Status == GridSpotStatus.Ship))
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }


            return isValid;

        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = string.Empty;
            string column = string.Empty;


            foreach (char c in shot)
            {
                if (char.IsLetter(c))
                    row += c;
                else if (char.IsDigit(c))
                    column += c;
            }

            int.TryParse(column, out int columnNumber);

            return (row, columnNumber);

        }

        public static bool ValidateShot(UserInfo activePlayer, string row, int column)
        {
            GridSpotModel newShot = new GridSpotModel
            {
                SpotLetter = row.ToUpper(),
                SpotNumber = column,
            };

            bool isValid = false;
            foreach (var spot in activePlayer.ShotGrid)
            {
                while (newShot.SpotLetter == spot.SpotLetter && newShot.SpotNumber == spot.SpotNumber && spot.Status == GridSpotStatus.Empty)
                {
                    isValid = true;
                    return (isValid);
                }

            }

            return (isValid);

        }

        private static void AddGridSpot(UserInfo model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };
            model.ShotGrid.Add(spot);
        }

    }
}


