using ChessGame.Board;

namespace ChessGame.Chess
{
    internal class Tower : Piece
    {
        public Tower(Color color, GameBoard board)
            : base(color, board)
        {
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            Position nextPosition = new Position(0, 0);

            // up
            nextPosition.SetValues(Position.Row - 1, Position.Column);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.Row = nextPosition.Row - 1;
            }

            // down
            nextPosition.SetValues(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.Row = nextPosition.Row + 1;
            }

            // left
            nextPosition.SetValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.Column = nextPosition.Column - 1;
            }

            // Right
            nextPosition.SetValues(Position.Row, Position.Column + 1);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.Column = nextPosition.Column + 1;
            }

            return matrix;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
