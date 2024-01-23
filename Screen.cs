using ChessGame.Board;

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
                    if (board.ReturnPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ShowPiece(board.ReturnPiece(i, j));
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   a b c d e f g h");
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece.Color  == Color.White)
            {
                Console.Write(piece + " ");
            } else
            {
                ConsoleColor aux = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(piece + " ");

                Console.ForegroundColor = aux;
            }
        }
    }
}
