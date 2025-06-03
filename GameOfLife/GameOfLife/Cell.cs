namespace GameOfLife
{
    class Cell
    {
        public const int CellSize = 5;

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Age { get; set; }

        public bool IsAlive { get; set; }


        public Cell(int row, int column, int age, bool alive)
        {
            PositionX = row * CellSize;
            PositionY = column * CellSize;
            Age = age;
            IsAlive = alive;

        }
    }
}