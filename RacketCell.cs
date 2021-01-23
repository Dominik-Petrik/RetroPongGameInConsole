using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class RacketCell
    {
        //positoion of the top cell of player racket
        public int positionWidth;
        public int positionHeight;

        public RacketCell(int width, int height) 
        {
            this.positionWidth = width;
            this.positionHeight = height;
        }

    }
}
