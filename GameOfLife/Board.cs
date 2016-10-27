using System;

namespace GameOfLife
{
    public class Board
    {
        private bool[][] _table;
        private readonly int _x;
        private readonly int _y;
        private readonly IRule _rule;

        public Board(int x, int y, IRule rule)
        {
            _x = x;
            _y = y;
            _rule = rule;
            _table = Init();
        }

        public void Fill(int x, int y)
        {
            if (x >= 0 && x < _x && y >= 0 && y < _y)
                _table[x][y] = true;
            else
                throw new ArgumentException(string.Format("{0} or {1} is not valid range. Board size is {2}, {3}",x,y,_x,_y));
        }

        public bool[][] Table
        {
            get
            {
                return _table;
            }
        }

        public int Width
        {
            get
            {
                return _x;
            }
        }

        public int Height
        {
            get
            {
                return _y;
            }
        }

        public void NextGeneration()
        {
            bool[][] buffor = Init();
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    int numberOfLivesNeighbour = GetLiveNeighbours(i,j);
                    _rule.Perform(_table[i][j], numberOfLivesNeighbour, ref buffor[i][j]);
                }
            }
            _table = buffor;
        }

        private int GetLiveNeighbours(int x, int y)
        {
            int sum = 0;
            sum = CheckNeighbour(x-1, y) + CheckNeighbour(x - 1, y-1)
                + CheckNeighbour(x, y-1) + CheckNeighbour(x + 1, y-1)
                + CheckNeighbour(x +1, y) + CheckNeighbour(x - 1, y+1)
                + CheckNeighbour(x, y+1) + CheckNeighbour(x + 1, y+1);
            return sum;
        }

        private int CheckNeighbour(int x, int y)
        {
            if (x >= 0 && x < _x && y >= 0 && y < _y)
                return _table[x][y] ? 1 : 0;
            return 0;
        }

        private bool[][] Init()
        {
            var table = new bool[_x][];
            for (int i = 0; i < _x; i++)
            {
                table[i] = new bool[_y];
            }
            return table;
        }
    }
}