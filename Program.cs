using ChessGame.Board;

namespace ChessGame
{
    class Program
    {
        public static void Main(string[] args)
        {
            GameBoard board = new GameBoard(8, 8);

            Screen.ShowScreen(board);
        }
    }
}