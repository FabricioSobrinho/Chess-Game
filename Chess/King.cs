using ChessGame.Board;

namespace ChessGame.Chess
{
    internal class King : Piece
    {
        public King( Color color, GameBoard board)
            : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
