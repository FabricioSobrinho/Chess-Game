using ChessGame.Board;

namespace ChessGame
{
    internal class Screen
    {
        public static void ShowScreen(GameBoard board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.ReturnPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.ReturnPiece(i, j) + " ");
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
