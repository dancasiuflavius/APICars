namespace CarsCrudApi.Cars.Dto
{
    public class UpdateCarRequest
    {
        public int Id { get; set; }
        public double? Price { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public DateTime? DateOfFabrication { get; set; }
    }
}
