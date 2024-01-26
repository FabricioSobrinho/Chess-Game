﻿using ChessGame.Board;
using ChessGame.Board.BoardExceptions;
using ChessGame.Chess;

namespace ChessGame
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ChessMatch chess = new ChessMatch();

                while (!chess.FinishedMatch)
                {
                    try
                    {
                        Console.Clear();
                        Screen.ShowScreen(chess.Board);
                        Console.WriteLine(" ");
                        Console.WriteLine($"Actual Player: {chess.ActualPlayer}");
                        Console.WriteLine($"Actual Round: {chess.Round}");
                        Console.WriteLine(" ");

                        Console.WriteLine("Insert origin position");
                        Position originPos = Screen.ReadChessPosition().ToPosition();
                        chess.ValidateOriginPosition(originPos);

                        bool[,] possiblePositions = chess.Board.Piece(originPos).PossibleMoves();
                        Screen.ShowScreen(chess.Board, possiblePositions);

                        Console.WriteLine("Insert final position");
                        Position finalPos = Screen.ReadChessPosition().ToPosition();
                        chess.ValidateFinalPosition(originPos, finalPos);

                        chess.PerformPlay(originPos, finalPos);
                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}