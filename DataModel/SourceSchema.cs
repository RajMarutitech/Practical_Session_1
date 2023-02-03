namespace DataModel
{
    public class SourceSchema
    {
        public int Id { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public SourceSchema(int id, int firstnumber, int secondnumber)
        {
            Id = id;
            FirstNumber = firstnumber;
            SecondNumber = secondnumber;
        }
    }
}