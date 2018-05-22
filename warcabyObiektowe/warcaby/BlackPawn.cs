using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    class BlackPawn
    {
        private int column;
        private int row;
        private FieldType[,] boardStatus;

        public BlackPawn(int column, int row, FieldType[,] boardStatus)
        {
            this.column = column;
            this.row = row;
            this.boardStatus = boardStatus;
        }

        public bool PossibilityOfMoving()
        {
            return false;
        }

        private int Move()
        {
            return 1;
        }

        private int Hit()
        {
            return 1;
        }
    }
}
