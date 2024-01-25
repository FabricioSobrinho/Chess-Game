using ChessGame.Board;

namespace ChessGame.Chess
{
    internal class King : Piece
    {
        public King(Color color, GameBoard board)
            : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }
        private bool CanMove(Position position)
        {
            Piece piece = Board!.Piece(position);

            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            Position nextPosition = new Position(0, 0);

            // up
            nextPosition.SetValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            // ne
            nextPosition.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            // right 
            nextPosition.SetValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            // se
            nextPosition.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            // down
            nextPosition.SetValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            // so
            nextPosition.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            // left
            nextPosition.SetValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            // no
            nextPosition.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            return matrix;
        }
    }
}
