using GameOfLife;
using NUnit.Framework;
using Shouldly;
using System;

namespace GameOfLifeSpecificationGameOfLife
{

    internal class GoLHelper
    {
        private readonly int _x;
        private readonly int _y;

        public GoLHelper(int x, int y)
        {
            _x = x;
            _y = y;
        }

        private bool[][] InitTable()
        {
            bool[][] table = new bool[_x][];
            for (int i = 0; i < _x; i++)
            {
                table[i] = new bool[_y];
            }
            return table;
        }

        public bool[][] CreateBlinkerFirstState(int x, int y)
        {
            bool[][] table = InitTable();
            table[x][y] = true;
            table[x+1][y] = true;
            table[x+2][y] = true;
            return table;
        }

        public bool[][] CreateBlinkerSecondState(int x, int y)
        {
            bool[][] table = InitTable();
            table[x][y] = true;
            table[x][y+1] = true;
            table[x][y+2] = true;
            return table;
        }

        public bool[][] CreateBlock(int x, int y)
        {
            bool[][] table = InitTable();
            table[x][y] = true;
            table[x+1][y] = true;
            table[x][y+1] = true;
            table[x+1][y+1] = true;
            return table;
        }
    }

    [TestFixture]
    public class GoLSpecyfication
    {
        [Test]
        public void ShouldCreateEmptyBoard()
        {
            //Given
            Board board = new Board(5,5, new ConwaysRule());
            //When
            //Then
            board.Width.ShouldBe(5);
            board.Height.ShouldBe(5);
        }

        [Test]
        public void ShouldInitBlinkerObject()
        {
            //Given
            Board board = new Board(5, 5, new ConwaysRule());
            IGoLObject blinker = GoLObjectFactory.Create(GoLPatterns.Blinker);
            GoLHelper helper = new GoLHelper(5, 5);
            //When
            blinker.Fill(board, 1, 2);
            //Then
            board.Table.ShouldBe(helper.CreateBlinkerFirstState(1, 2));
        }

        [Test]
        public void ShouldInvokeBoardGeneration()
        {
            //Given
            Board board = new Board(5, 5, new ConwaysRule());
            IGoLObject blinker = GoLObjectFactory.Create(GoLPatterns.Blinker);
            GoLHelper helper = new GoLHelper(5, 5);
            //When
            blinker.Fill(board, 1, 2);
            board.NextGeneration();
            //Then
            board.Table.ShouldBe(helper.CreateBlinkerSecondState(2,1));
        }

        [Test]
        public void BlinkerAfter2GenerationShouldReturnToInitState()
        {
            //Given
            Board board = new Board(5, 5, new ConwaysRule());
            IGoLObject blinker = GoLObjectFactory.Create(GoLPatterns.Blinker);
            GoLHelper helper = new GoLHelper(5, 5);
            //When
            blinker.Fill(board, 1, 2);
            board.NextGeneration();
            board.NextGeneration();
            //Then
            board.Table.ShouldBe(helper.CreateBlinkerFirstState(1, 2));
        }

        [Test]
        public void BlockShouldBeTheSameBlock()
        {
            //Given
            Board board = new Board(4, 4, new ConwaysRule());
            IGoLObject block = GoLObjectFactory.Create(GoLPatterns.Block);
            GoLHelper helper = new GoLHelper(4, 4);
            //When
            block.Fill(board, 1, 1);
            board.NextGeneration();
            board.NextGeneration();
            //Then
            board.Table.ShouldBe(helper.CreateBlock(1,1));
        }

        [Test]
        public void ShouldThrowsArgumentExceptionWhenBoardIsOutOfRange()
        {
            //Given
            Board board = new Board(3, 3, new ConwaysRule());
            IGoLObject blinker = GoLObjectFactory.Create(GoLPatterns.Blinker);
            //When
            var ex = Assert.Throws<ArgumentException>(() => blinker.Fill(board, 1, 2));
            //Then
            ex.Message.ShouldBe("3 or 2 is not valid range. Board size is 3, 3");
        }
    }
}
