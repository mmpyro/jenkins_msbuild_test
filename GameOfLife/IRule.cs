namespace GameOfLife
{
    public interface IRule
    {
        void Perform(bool v, int numberOfLivesNeighbour, ref bool buffor);
    }

    public enum GoLPatterns
    {
        Blinker,
        Block
    }

    public class ConwaysRule : IRule
    {
        public void Perform(bool currentCell, int numberOfLivesNeighbour, ref bool buffor)
        {
            //todo: refactor this
            if (currentCell && numberOfLivesNeighbour < 2)
                buffor = false;
            else if (currentCell && numberOfLivesNeighbour == 2 || numberOfLivesNeighbour == 3)
                buffor = true;
            else if (currentCell && numberOfLivesNeighbour > 3)
                buffor = false;
            else if (currentCell == false && numberOfLivesNeighbour == 3)
                buffor = true;
        }
    }
}