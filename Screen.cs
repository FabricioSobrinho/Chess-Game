using ChessGame.Board;
using ChessGame.Chess;

namespace ChessGame
{
    internal class Screen
    {
        public static void ShowScreen(GameBoard board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + "| ");
                for (int j = 0; j < board.Columns; j++)
                {
                    ShowPiece(board.ReturnPiece(i, j));
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   a b c d e f g h");
        }

        public static void ShowScreen(GameBoard board, bool[,] possiblePositions)
        {
            ConsoleColor originColor = Console.BackgroundColor;
            ConsoleColor changedColor = ConsoleColor.DarkBlue;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + "| ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedColor;
                    } 
                    else
                    {
                        Console.BackgroundColor = originColor; 
                    }

                    ShowPiece(board.Piece(i, j));
                    Console.BackgroundColor = originColor;
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   a b c d e f g h");
            Console.BackgroundColor = originColor;
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(piece + " ");

                    Console.ForegroundColor = aux;
                }
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string position = Console.ReadLine();
            char column = position[0];
            int row = int.Parse(position[1] + "");

            return new ChessPosition(column, row);
        }
    }
}
