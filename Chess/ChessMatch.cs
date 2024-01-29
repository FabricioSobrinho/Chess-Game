using ChessGame.Board;
using ChessGame.Board.BoardExceptions;
using System.Collections.Generic;

namespace ChessGame.Chess
{
    internal class ChessMatch
    {
        public GameBoard Board { get; private set; }
        public int Round { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool FinishedMatch { get; private set; }
        private HashSet<Piece> Pieces { get; set; }
        private HashSet<Piece> PlayerCapturedPieces { get; set; }

        public ChessMatch()
        {
            Board = new GameBoard(8, 8);
            Round = 1;
            ActualPlayer = Color.White;
            FinishedMatch = false;
            Pieces = new HashSet<Piece>();
            PlayerCapturedPieces = new HashSet<Piece>();
            InitiatePieces();
        }

        public void ExecuteMove(Position originPos, Position finalPos)
        {
            Piece piece = Board.RemovePiece(originPos);
            piece.IncrementMoveCount();
            Piece capturedPiece = Board.RemovePiece(finalPos);

            if (capturedPiece != null)
            {
                PlayerCapturedPieces.Add(capturedPiece);
            }

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

        public void PutNewPiece(char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }
        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in PlayerCapturedPieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }

            return aux;
        }

        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void InitiatePieces()
        {
            PutNewPiece('c', 1, new Tower(Color.White, Board));
            PutNewPiece('c', 2, new Tower(Color.White, Board));
            PutNewPiece('d', 2, new Tower(Color.White, Board));
            PutNewPiece('e', 2, new Tower(Color.White, Board));
            PutNewPiece('e', 1, new Tower(Color.White, Board));
            PutNewPiece('d', 1, new King(Color.White, Board));

            PutNewPiece('c', 7, new Tower(Color.Black, Board));
            PutNewPiece('c', 8, new Tower(Color.Black, Board));
            PutNewPiece('d', 7, new Tower(Color.Black, Board));
            PutNewPiece('e', 7, new Tower(Color.Black, Board));
            PutNewPiece('e', 8, new Tower(Color.Black, Board));
            PutNewPiece('d', 8, new King(Color.Black, Board));
        }
    }
}
