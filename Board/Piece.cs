
namespace ChessGame.Board
{
    internal abstract class Piece
    {
        public Position? Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }
        public GameBoard? Board { get; protected set; }

        public Piece() { }
        public Piece(Color color, GameBoard? board)
        {
            Position = null;
            Color = color;
            MoveCount = 0;
            Board = board;
        }

        public void IncrementMoveCount()
        {
            MoveCount++;
        }
        public bool HasPossibleMoves()
        {
            bool[,] matrix = PossibleMoves();

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (matrix[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool CanMoveTo(Position position)
        {
            return PossibleMoves()[position.Row, position.Column];
        }
        public abstract bool[,] PossibleMoves();
    }
}
