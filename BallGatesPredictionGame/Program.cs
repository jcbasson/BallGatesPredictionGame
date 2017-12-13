using BallGatesPredictionGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BallGatesPredictionGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string continueDecision = "Y";
            do
            {
                Play();
                Console.WriteLine("Would you like to try again? Y/N");
                continueDecision = Console.ReadLine().ToUpper();
            } while (continueDecision.ToUpper() == "Y");
        }

        public static void Play()
        {
            var game = new Game();
            game.Start();
            string gateWithoutBallName = game.GetNameOfGateWithoutBall();
            string userGuess = string.Empty;

            do
            {
                Console.WriteLine("Please guess which gate does not have a ball [A-P]:");
                userGuess = Console.ReadLine().ToUpper();
            } while (String.IsNullOrEmpty(userGuess) || userGuess.Length > 1 || !(Regex.IsMatch(userGuess, @"[a-zA-Z]")));

            Console.WriteLine(userGuess == gateWithoutBallName ?
                    $"Congratulations your guess of {userGuess} is correct! Gate {gateWithoutBallName} does not have a ball!" :
                    $"Sorry your guess of {userGuess} is wrong! Gate {gateWithoutBallName} does not have a ball!");
            
        }
    }
}
