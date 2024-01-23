using ChessGame.Board;
using ChessGame.Chess;

namespace ChessGame
{
    class Program
    {
        public static void Main(string[] args)
        {
            GameBoard board = new GameBoard(8, 8);

            board.PutPiece(new King(Color.Black, board), new Position(1, 0));

            Screen.ShowScreen(board);
        }
    }
}