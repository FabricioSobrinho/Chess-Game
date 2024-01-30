using ChessGame.Board;
using ChessGame.Board.BoardExceptions;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

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
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new GameBoard(8, 8);
            Round = 1;
            ActualPlayer = Color.White;
            FinishedMatch = false;
            Pieces = new HashSet<Piece>();
            PlayerCapturedPieces = new HashSet<Piece>();
            Check = false;
            InitiatePieces();
        }

        public Piece ExecuteMove(Position originPos, Position finalPos)
        {
            Piece piece = Board.RemovePiece(originPos);
            piece.IncrementMoveCount();
            Piece capturedPiece = Board.RemovePiece(finalPos);

            if (capturedPiece != null)
            {
                PlayerCapturedPieces.Add(capturedPiece);
            }

            Board.PutPiece(piece, finalPos);

            // special move little castling
            if (piece is King && finalPos.Column == originPos.Column + 2)
            {
                Position towerOrigin = new Position(originPos.Row, originPos.Column + 3);
                Position towerDestiny = new Position(originPos.Row, originPos.Column + 1);

                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncrementMoveCount();
                Board.PutPiece(tower, towerDestiny);
            }
            
            // special move big castling
            if (piece is King && finalPos.Column == originPos.Column + 3)
            {
                Position towerOrigin = new Position(originPos.Row, originPos.Column - 4);
                Position towerDestiny = new Position(originPos.Row, originPos.Column - 1);

                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncrementMoveCount();
                Board.PutPiece(tower, towerDestiny);
            }

            return capturedPiece;
        }

        public void PerformPlay(Position initialPosition, Position finalPosition)
        {
            Piece capturedPiece = ExecuteMove(initialPosition, finalPosition);

            if (IsInCheck(ActualPlayer))
            {
                UndoMove(initialPosition, finalPosition, capturedPiece);
                throw new BoardException("You can't put yourself in check.");
            }

            if (IsInCheck(Rival(ActualPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsInCheckMate(Rival(ActualPlayer)))
            {
                FinishedMatch = true;
            }
            else
            {

                Round++;

                ChangePlayerRound();
            }
        }

        public void UndoMove(Position initialPosition, Position finalPosition, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(finalPosition);
            piece.DecrementMoveCount();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, finalPosition);
                PlayerCapturedPieces.Remove(capturedPiece);
            }

            Board.PutPiece(piece, initialPosition);

            // special move little castling
            if (piece is King && finalPosition.Column == initialPosition.Column + 2)
            {
                Position towerOrigin = new Position(initialPosition.Row, initialPosition.Column + 3);
                Position towerDestiny = new Position(initialPosition.Row, initialPosition.Column + 1);

                Piece tower = Board.RemovePiece(towerDestiny);
                tower.DecrementMoveCount();
                Board.PutPiece(tower, towerOrigin);
            }
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

        private Color Rival(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece KingPosition(Color color)
        {
            foreach (Piece inGamePiece in InGamePieces(color))
            {
                if (inGamePiece is King)
                {
                    return inGamePiece;
                }
            }
            return null;
        }

        private bool IsInCheck(Color color)
        {
            Piece king = KingPosition(color);

            if (king == null)
            {
                throw new BoardException("This piece isn't at the board.");
            }

            foreach (Piece inGamePiece in InGamePieces(Rival(color)))
            {
                bool[,] possibleMoves = inGamePiece.PossibleMoves();

                if (possibleMoves[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsInCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece inGamePiece in InGamePieces(color))
            {
                bool[,] possibleMoves = inGamePiece.PossibleMoves();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMoves[i, j])
                        {
                            Position origin = inGamePiece.Position;
                            Position destiny = new Position(i, j);

                            Piece capturedPiece = ExecuteMove(origin, destiny);

                            bool isInCheck = IsInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);

                            if (!isInCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void InitiatePieces()
        {
            PutNewPiece('a', 1, new Tower(Color.White, Board));
            PutNewPiece('b', 1, new Horse(Color.White, Board));
            PutNewPiece('c', 1, new Bishop(Color.White, Board));
            PutNewPiece('d', 1, new Queen(Color.White, Board));
            PutNewPiece('e', 1, new King(Color.White, Board, this));
            PutNewPiece('f', 1, new Bishop(Color.White, Board));
            PutNewPiece('g', 1, new Horse(Color.White, Board));
            PutNewPiece('h', 1, new Tower(Color.White, Board));
            PutNewPiece('a', 2, new Pawn(Color.White, Board));
            PutNewPiece('b', 2, new Pawn(Color.White, Board));
            PutNewPiece('c', 2, new Pawn(Color.White, Board));
            PutNewPiece('d', 2, new Pawn(Color.White, Board));
            PutNewPiece('e', 2, new Pawn(Color.White, Board));
            PutNewPiece('f', 2, new Pawn(Color.White, Board));
            PutNewPiece('g', 2, new Pawn(Color.White, Board));
            PutNewPiece('h', 2, new Pawn(Color.White, Board));

            PutNewPiece('a', 8, new Tower(Color.Black, Board));
            PutNewPiece('b', 8, new Horse(Color.Black, Board));
            PutNewPiece('c', 8, new Bishop(Color.Black, Board));
            PutNewPiece('d', 8, new Queen(Color.Black, Board));
            PutNewPiece('e', 8, new King(Color.Black, Board, this));
            PutNewPiece('f', 8, new Bishop(Color.Black, Board));
            PutNewPiece('g', 8, new Horse(Color.Black, Board));
            PutNewPiece('h', 8, new Tower(Color.Black, Board));
            PutNewPiece('a', 7, new Pawn(Color.Black, Board));
            PutNewPiece('b', 7, new Pawn(Color.Black, Board));
            PutNewPiece('c', 7, new Pawn(Color.Black, Board));
            PutNewPiece('d', 7, new Pawn(Color.Black, Board));
            PutNewPiece('e', 7, new Pawn(Color.Black, Board));
            PutNewPiece('f', 7, new Pawn(Color.Black, Board));
            PutNewPiece('g', 7, new Pawn(Color.Black, Board));
            PutNewPiece('h', 7, new Pawn(Color.Black, Board));
        }
    }
}
