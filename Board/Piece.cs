
namespace ChessGame.Board
{
    internal class Piece
    {
        public Position? Position { get; set; }
        public Color Color { get; set; }
        public int MoveCount { get; protected set; }
        public GameBoard? Board { get; protected set; }

        public Piece() { }
        public Piece(Position? position, Color color, GameBoard? board)
        {
            Position = position;
            Color = color;
            MoveCount = 0;
            Board = board;
        }
    }
}
