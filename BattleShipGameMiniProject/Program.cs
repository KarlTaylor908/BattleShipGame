using BattleShipLibrary;
using BattleShipLibrary.Models;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

Introduction();

static void Introduction()
{
    Console.WriteLine("Welcome to Battleship!");
    Console.WriteLine("Created by Karl Taylor");
    Console.WriteLine("Please enter the player names and ship locations (A-E, 1-5) below");
    Console.WriteLine();

    UserInfo activePlayer = CreatePlayer("Player 1");
    UserInfo opponent = CreatePlayer("Player 2");
    UserInfo winner = null;
    DisplayShotGrid(activePlayer);

    do
    {
       // int noTurns = 0;
       // Console.WriteLine();
       // Console.WriteLine($"This is turn {noTurns} ");
       
        RecordPlayerShot(activePlayer, opponent);

        bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

        if (doesGameContinue == true)
        {
            (activePlayer, opponent) = (opponent, activePlayer);
            DisplayShotGrid(activePlayer);
        }
        else
        {
            winner = activePlayer;
        }
       // noTurns =+ 1;
    } while (winner == null);

    IdentifyWinner(winner);
}

static void IdentifyWinner(UserInfo winner)
{
    Console.WriteLine($"Congratulations to {winner.UserName} for winning");
    Console.WriteLine($"{winner.UserName} took {GameLogic.GetShotCount(winner)} shots");
}

static void RecordPlayerShot(UserInfo activePlayer, UserInfo opponent)
{
    bool isValidShot = false;
    string row = "";
    int column = 0;

    do
    {
        string shot = AskForShot(activePlayer);
        (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
        isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
        if (isValidShot == false)
        {
            Console.Write("Invalid Shot Location, please try again.");
        }
    }
    while (isValidShot == false);

    bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

    GameLogic.MarkShotResult(activePlayer, row, column, isAHit);

    ShowHitOrMiss(isAHit, row, column);

}

static void ShowHitOrMiss(bool isAHit,string row, int column)
{
    if (isAHit)
    {
        Console.WriteLine($"Well done, {row}{column} hit a ship!");
    }
    else if (isAHit ==false)
    {
        Console.WriteLine($"Better luck next round, {row}{column} was a miss!");
    }
}

static string AskForShot(UserInfo activePlayer)
{

    Console.WriteLine();
    Console.Write($"{activePlayer.UserName}, please enter your shot selection: ");
    string output = Console.ReadLine();

    return output;
}

static void DisplayShotGrid2(UserInfo activePlayer)
{
    
}

static void DisplayShotGrid(UserInfo activePlayer)
{
    string currentRow = activePlayer.ShotGrid[0].SpotLetter;

    int currentColumn = activePlayer.ShotGrid[0].SpotNumber;

    HashSet<string> uniqueLetters = new HashSet<string>();
    HashSet<int> uniqueNumbers = new HashSet<int>();

    //Console.WriteLine(activePlayer.ShotGrid[1].SpotNumber);
    Console.WriteLine(  );
    Console.WriteLine($"This grid is {activePlayer.UserName} shots: ");

    Console.WriteLine("-----------------------");
    Console.Write("/ |");


    for (int i = 1; i <= 5; i++)
    {
        Console.Write($" {i} |");

    }

    Console.WriteLine();

    foreach (var letter in activePlayer.ShotGrid.Select(s => s.SpotLetter).Distinct())
    {
        Console.WriteLine("-----------------------");
        Console.Write($"{letter}");


        foreach (var column in activePlayer.ShotGrid.Where(spot => spot.SpotLetter == letter).OrderBy(s => s.SpotNumber))
             
            

            if (column.Status == GridSpotStatus.Empty)
            {
                Console.Write(" |");
                Console.Write("  ");
            }
            else if (column.Status == GridSpotStatus.Hit)
            {
                Console.Write(" |");
                Console.Write(" X");
            }
            else if (column.Status == GridSpotStatus.Miss)
            {
                Console.Write(" |");
                Console.Write(" O");
            }
            else
            {
                Console.Write(" |");
                Console.Write(" ?");
            }
        Console.Write(" |");
       
        Console.WriteLine();
       // Console.Write("-");





    }
    Console.WriteLine("-----------------------");
}
        













//if (gridSpot.SpotLetter == currentRow)
//{
//    Console.WriteLine();
//    currentRow = gridSpot.SpotLetter;
//    Console.WriteLine(currentRow);

//}





static UserInfo CreatePlayer(string playerTitle)
{
    Console.WriteLine($"Player Information for {playerTitle}");

    UserInfo output = new UserInfo();

    output.UserName = AskForUserName();

    GameLogic.InitaliseGrid(output);

    PlaceShips(output);

    Console.Clear();

    return output;
}

static string AskForUserName()
{
    Console.Write("What is your first name: ");
    string output = Console.ReadLine();

    return output;
}

static void PlaceShips(UserInfo model)
{
    do
    {
        Console.Write($"Where do you want your {model.ShipLocation.Count + 1} ship : ");
        string location = Console.ReadLine();

        bool isValidLocation = GameLogic.PlaceShip(model, location);

        if (isValidLocation == false)
        {
            Console.WriteLine("That was not a valid location, please try again: ");
        }

    } while (model.ShipLocation.Count < 5);

}
