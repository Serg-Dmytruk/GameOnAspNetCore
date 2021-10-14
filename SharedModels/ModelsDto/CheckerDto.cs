namespace Game.Common.ModelsDto
{
    public class CheckerDto
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CheckerDirection Direction { get; set; }
        public string Color { get; set; }
    }

    public enum CheckerDirection
    { 
        Down, Up, Both
    }

}
