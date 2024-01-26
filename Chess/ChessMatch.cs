using ChessGame.Board;
using ChessGame.Board.BoardExceptions;

namespace ChessGame.Chess
{
    internal class ChessMatch
    {
        public GameBoard Board { get; private set; }
        public int Round { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool FinishedMatch { get; private set; }

        public ChessMatch()
        {
            Board = new GameBoard(8, 8);
            Round = 1;
            ActualPlayer = Color.White;
            FinishedMatch = false;
            InitiatePieces();
        }

        public void ExecuteMove(Position originPos, Position finalPos)
        {
            Piece piece = Board.RemovePiece(originPos);
            piece.IncrementMoveCount();
            Piece capturedPiece = Board.RemovePiece(finalPos);

            Board.PutPiece(piece, finalPos);
        }

        public void PerformPlay(Position initialPosition, Position finalPosition)
        {
            ExecuteMove(initialPosition, finalPosition);
            Round++;

            ChangePlayerRound();
        }

        public void ChangePlayerRound()
        {
            if (Round % 2 == 0)
            {
                ActualPlayer = Color.Black;
            } 
            else
            {
                ActualPlayer = Color.White;
            }
        }

        public void ValidateOriginPosition(Position originPosition)
        {
            if (Board.Piece(originPosition) == null)
            {
                throw new BoardException("Didn't has any piece at the origin move place.");
            }

            if (Board.Piece(originPosition).Color != ActualPlayer)
            {
                throw new BoardException("Ins't your turn.");
            }

            if (!Board.Piece(originPosition).HasPossibleMoves())
            {
                throw new BoardException("Didn't has any moves for this piece.");
            }
        }

        public void ValidateFinalPosition(Position originPosition, Position finalPosition)
        {
            if (!Board.Piece(originPosition).CanMoveTo(finalPosition))
            {
                throw new BoardException("This piece can't be moved to this location.");
            }
        }

        public void InitiatePieces()
        {
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.PutPiece(new Tower(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());

        }
    }
}
