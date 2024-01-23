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
        public Piece[,]? Pieces { get; set; }
        public GameBoard() { }
        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[Rows, Columns];
        }
    }
}
