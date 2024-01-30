using ChessGame.Board;

namespace ChessGame.Chess
{
    internal class Bishop : Piece
    {
        public Bishop(Color color, GameBoard board)
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

            // No
            nextPosition.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.SetValues(nextPosition.Row - 1, nextPosition.Column - 1);
            }

            // Ne
            nextPosition.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.SetValues(nextPosition.Row - 1, nextPosition.Column + 1);
            }

            // Se
            nextPosition.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.SetValues(nextPosition.Row + 1, nextPosition.Column + 1);
            }
            
            // S0
            nextPosition.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(nextPosition) && CanMove(nextPosition))
            {
                matrix[nextPosition.Row, nextPosition.Column] = true;

                if (Board.Piece(nextPosition) != null && Board.Piece(nextPosition).Color != Color)
                {
                    break;
                }

                nextPosition.SetValues(nextPosition.Row + 1, nextPosition.Column - 1);
            }

            return matrix;
        }
        public override string ToString()
        {
            return "B";
        }
    }
}
