using ChessGame.Board;
using System.Runtime.ConstrainedExecution;

namespace ChessGame.Chess
{
    internal class Pawn : Piece
    {
        private ChessMatch ChessMatch;
        public Pawn(Color color, GameBoard board, ChessMatch chessMatch)
            : base(color, board)
        {
            ChessMatch = chessMatch;
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

                // special move En Passant
                if (Position.Row == 3)
                {
                    Position leftPosition = new Position(Position.Row, Position.Column - 1);

                    if (Board.ValidPosition(leftPosition) && HasEnemy(leftPosition) && Board.Piece(leftPosition) == ChessMatch.PossibleEnPassant)
                    {
                        matrix[leftPosition.Row - 1, leftPosition.Column] = true;
                    }

                    Position rightPosition = new Position(Position.Row, Position.Column + 1);

                    if (Board.ValidPosition(rightPosition) && HasEnemy(rightPosition) && Board.Piece(leftPosition) == ChessMatch.PossibleEnPassant)
                    {
                        matrix[rightPosition.Row - 1, rightPosition.Column] = true;
                    }
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

                // special move En Passant
                if (Position.Row == 4)
                {
                    Position leftPosition = new Position(Position.Row, Position.Column - 1);

                    if (Board.ValidPosition(leftPosition) && HasEnemy(leftPosition) && Board.Piece(leftPosition) == ChessMatch.PossibleEnPassant)
                    {
                        matrix[leftPosition.Row + 1, leftPosition.Column] = true;
                    }

                    Position rightPosition = new Position(Position.Row, Position.Column + 1);

                    if (Board.ValidPosition(rightPosition) && HasEnemy(rightPosition) && Board.Piece(leftPosition) == ChessMatch.PossibleEnPassant)
                    {
                        matrix[rightPosition.Row + 1, rightPosition.Column] = true;
                    }
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
