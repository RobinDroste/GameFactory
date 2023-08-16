﻿using System;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    class Program
    {
        static GameBoard gameBoard = new GameBoard();
        static string player1, player2;
        static int scorePlayer1, scorePlayer2;
        static void Main(string[] args)
        { 

            Console.WriteLine("Input name of first Challenger");
            player1 = Console.ReadLine();
            Console.WriteLine("Input name of second Challenger");
            player2 = Console.ReadLine();

            do
            {
                playGame(); 
                gameBoard.ResetBoard();

                Console.WriteLine("Score:  " + player1 + " : " + scorePlayer1 + " !     " + player2 + " : " + scorePlayer2 + "!");
                Console.WriteLine("Want a rematch Noob ? Y / N ?");


            } while (Console.ReadLine().ToLower() == "y") ;
        }

        private class  GameBoard
        {
            private int[] board = new int[9];
            public GameBoard()
            {
                ResetBoard();
            }

            public void ResetBoard()
            {
                for (int i = 0; i < 9; i++)
                {
                    board[i] = 0;
                }
            }
            public int GetCell(int index)
            {
                return board[index];
            }

            public void SetCell(int index, int value)
            {
                if (index >= 0 && index < 9)
                {
                    board[index] = value;
                }
            }
        }
        private static int playGame()
        {

            int player1turn = -1;
            int player2turn = -1;

            Random rand = new Random();

            int startingPlayer = rand.Next(1, 3);

            Console.WriteLine(startingPlayer == 1 ? player1 + " starts!" : player2 + " starts!");



            while (checkForWinner() == 0)
            {
                if ((startingPlayer == 1 && player1turn == -1) || (startingPlayer == 2 && player2turn == -1))
                {
                    Console.WriteLine(startingPlayer == 1 ? player1 + ", input a number from 0 to 8" : player2 + ", input a number from 0 to 8");
                }


                int currentPlayer = startingPlayer == 1 ? 1 : 2;

                int chosenCell = int.Parse(Console.ReadLine());

                if (gameBoard.GetCell(chosenCell) == 0)
                {
                    gameBoard.SetCell(chosenCell, currentPlayer);
                    startingPlayer = startingPlayer == 1 ? 2 : 1;
                }
                else
                {
                    Console.WriteLine("Invalid move, idiot. Try again.");
                    continue;
                }

                printBoard();


            }
            int winnerNumber = checkForWinner();

            if (winnerNumber == 1)
            {
                Console.WriteLine(player1 + " won the game! " + player2 + " should feel ashamed!");
                scorePlayer1 += 1;
            }
            else if (winnerNumber == 2)
            {
                Console.WriteLine(player2 + " won the game! " + player1 + " is a utterly bad player!");
                scorePlayer2 += 1;
            }
            else if (winnerNumber == -1)
            {
                Console.WriteLine("It's a draw, Idiots!");
            }
            return winnerNumber;


        }
        private static int checkForWinner()
        {
            // row 1-3
            if (gameBoard.GetCell(0) == gameBoard.GetCell(1) && gameBoard.GetCell(0) == gameBoard.GetCell(2))
            {
                return gameBoard.GetCell(0);
            }
            if (gameBoard.GetCell(3) == gameBoard.GetCell(4) && gameBoard.GetCell(3) == gameBoard.GetCell(5))
            {
                return gameBoard.GetCell(3);
            }
            if (gameBoard.GetCell(6) == gameBoard.GetCell(7) && gameBoard.GetCell(6) == gameBoard.GetCell(8))
            {
                return gameBoard.GetCell(6);
            }
            // column 1-3
            if (gameBoard.GetCell(0) == gameBoard.GetCell(3) && gameBoard.GetCell(0) == gameBoard.GetCell(6))
            {
                return gameBoard.GetCell(0);
            }
            if (gameBoard.GetCell(1) == gameBoard.GetCell(4) && gameBoard.GetCell(1) == gameBoard.GetCell(7))
            {
                return gameBoard.GetCell(1);
            }
            if (gameBoard.GetCell(2) == gameBoard.GetCell(5) && gameBoard.GetCell(2) == gameBoard.GetCell(8))
            {
                return gameBoard.GetCell(2);
            }
            // diagonal
            if (gameBoard.GetCell(0) == gameBoard.GetCell(4) && gameBoard.GetCell(0) == gameBoard.GetCell(8))
            {
                return gameBoard.GetCell(0);
            }
            if (gameBoard.GetCell(2) == gameBoard.GetCell(4) && gameBoard.GetCell(2) == gameBoard.GetCell(6))
            {
                return gameBoard.GetCell(2);
            }

            // Check for draw (Uncomment if needed)
            //for (int i = 0; i < 9; i++)
            //{
            //    if (gameBoard.GetCell(i) == 0)
            //    {
            //        return -1;
            //    }
            //}

            return 0;
        }

        private static void printBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                int cellValue = gameBoard.GetCell(i);
                // print x or o
                // 0 nothing, 1 is x, 2 is o
                switch (cellValue)
                {
                    case 0:
                        Console.Write(".");
                        break;
                    case 1:
                        Console.Write("X");
                        break;
                    case 2:
                        Console.Write("O");
                        break;
                }

                // lines
                if (i == 2 || i == 5 || i == 8)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}