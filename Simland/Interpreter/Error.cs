namespace Simland
{
    public class Error
    {
        public string message;

        public int index;

        public int line;


        public Error(int index, int line, string message)
        {
            this.index = index;

            this.line = line;

            this.message = message;
        }

        public override string ToString()
        {
            return line.ToString().PadRight(8) + message;
        }
    }
}