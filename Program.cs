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
                ChessMatch chess = new ChessMatch();

                while (!chess.FinishedMatch)
                {
                    Console.Clear();
                    Screen.ShowScreen(chess.Board);

                    Console.WriteLine("Insert origin position");
                    Position originPos = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = chess.Board.Piece(originPos).PossibleMoves(); 
                    Screen.ShowScreen(chess.Board, possiblePositions);

                    Console.WriteLine("Insert final position");
                    Position finalPos = Screen.ReadChessPosition().ToPosition();

                    chess.ExecuteMove(originPos, finalPos);
                }
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}