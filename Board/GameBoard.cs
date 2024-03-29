﻿using ChessGame.Board.BoardExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Board
{
    internal class GameBoard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces { get; set; }

        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[Rows, Columns];
        }
        public Piece Piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            return Pieces[position.Row, position.Column];
        }

        public Piece ReturnPiece(int row, int column)
        {
            return Pieces[row, column];
        }
        public bool HasPiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void PutPiece(Piece piece, Position position)
        {
            if (HasPiece(position))
            {
                throw new BoardException("Already has a piece at this position.");
            }

            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public Piece? RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }

            Piece aux = Piece(position);
            Pieces[position.Row, position.Column] = null;

            return aux;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid Position.");
            }
        }
    }
}
