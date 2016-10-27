namespace GameOfLife
{
    public class Block : IGoLObject
    {
        public void Fill(Board board, int x, int y)
        {
            board.Fill(x, y);
            board.Fill(x, y+1);
            board.Fill(x+1, y+1);
            board.Fill(x+1, y);
        }
    }
}