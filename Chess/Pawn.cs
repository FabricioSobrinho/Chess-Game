using ChessGame.Board;
using System.Runtime.ConstrainedExecution;

namespace ChessGame.Chess
{
    internal class Pawn : Piece
    {
        public Pawn(Color color, GameBoard board)
            : base(color, board)
        {
        }

        private bool HasEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool FreePosition(Position position)
        {
            return Board.Piece(position) == null;
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

            if (Color == Color.White)
            {
                nextPosition.SetValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(nextPosition) && FreePosition(nextPosition))
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }

                nextPosition.SetValues(Position.Row - 2, Position.Column);
                Position p2 = new Position(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(p2) && FreePosition(p2) && Board.ValidPosition(nextPosition) && FreePosition(nextPosition) && MoveCount == 0)
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }

                nextPosition.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(nextPosition) && HasEnemy(nextPosition))
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }

                nextPosition.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(nextPosition) && HasEnemy(nextPosition))
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }
            }
            else
            {
                nextPosition.SetValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(nextPosition) && FreePosition(nextPosition))
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }

                nextPosition.SetValues(Position.Row + 2, Position.Column);
                Position p2 = new Position(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(p2) && FreePosition(p2) && Board.ValidPosition(nextPosition) && FreePosition(nextPosition) && MoveCount == 0)
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }

                nextPosition.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(nextPosition) && HasEnemy(nextPosition))
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }

                nextPosition.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(nextPosition) && HasEnemy(nextPosition))
                {
                    matrix[nextPosition.Row, nextPosition.Column] = true;
                }
            }
            return matrix;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
