using System;

namespace GameOfLife
{
    public class Blinker : IGoLObject
    {
        public void Fill(Board board, int x, int y)
        {
            board.Fill(x ,y);
            board.Fill(x+1, y);
            board.Fill(x+2, y);
        }
    }
}