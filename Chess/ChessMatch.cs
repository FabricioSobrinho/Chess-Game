﻿using ChessGame.Board;
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

            return capturedPiece;
        }

        public void PerformPlay(Position initialPosition, Position finalPosition)
        {
            Piece capturedPiece = ExecuteMove(initialPosition, finalPosition);

            if (IsInCheck(ActualPlayer))
            {
                RemadeMove(initialPosition, finalPosition, capturedPiece);
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

            Round++;

            ChangePlayerRound();
        }

        public void RemadeMove(Position initialPosition, Position finalPosition, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(finalPosition);
            piece.DecrementMoveCount();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, finalPosition);
                PlayerCapturedPieces.Remove(capturedPiece);
            }

            Board.PutPiece(piece, initialPosition);
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
