using ChessGame.Board;

namespace ChessGame.Chess
{
    internal class King : Piece
    {

        private ChessMatch ChessMatch;
        public King(Color color, GameBoard board, ChessMatch chessMatch)
            : base(color, board)
        {
            ChessMatch = chessMatch;
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

        public bool TestTowerCastling(Position position)
        {
            Piece tower = Board.Piece(position);

            return tower != null && tower.MoveCount == 0 && tower is Tower && tower.Color == Color;
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

            // special move little castling
            if (MoveCount == 0 && !ChessMatch.Check)
            {
                Position rightTowerPosition = new Position(Position.Row, Position.Column + 3);
                if (TestTowerCastling(rightTowerPosition))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);

                    if (Board.Piece(p1) ==  null && Board.Piece(p2) == null)
                    {
                        matrix[Position.Row, Position.Column + 2] = true;
                    }
                }
            }

            // special move big castling
            if (MoveCount == 0 && !ChessMatch.Check)
            {
                Position leftTowerPosition = new Position(Position.Row, Position.Column - 4);
                if (TestTowerCastling(leftTowerPosition))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);

                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        matrix[Position.Row, Position.Column - 2] = true;
                    }
                }
            }
            return matrix;
        }
    }
}
