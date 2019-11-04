namespace _07___Arrays
{
    internal class Coordinates
    {
        public Coordinates() { }
        public Coordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }
        //properties
        public int Row { get; private set; }
        public int Column { get; private set; }

        public override string ToString() => $"Coordinates: Row {Row}, Column {Column}";
    }
}