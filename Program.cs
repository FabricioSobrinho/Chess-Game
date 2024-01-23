using ChessGame.Board;
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
                GameBoard board = new GameBoard(8, 8);

                board.PutPiece(new King(Color.Black, board), new Position(1, 0));
                board.PutPiece(new Tower(Color.Black, board), new Position(10, 0));

                Screen.ShowScreen(board);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}