using ChessGame.Board;

namespace ChessGame.Chess
{
    internal class Horse : Piece
    {
        public Horse(Color color, GameBoard board)
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

            nextPosition.SetValues(Position.Row - 1, Position.Column);

            nextPosition.SetValues(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }
            
            nextPosition.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }
            
            nextPosition.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            nextPosition.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            nextPosition.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            nextPosition.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            nextPosition.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            nextPosition.SetValues(Position.Row + 1, Position.Column - 2);
            if (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;
            }

            return matrix;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
