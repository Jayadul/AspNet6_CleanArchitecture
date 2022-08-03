public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Category { get; set; }
    public DateTime Date { get; set; }
    public ProductCondition Condition { get; set; }
    public decimal Price { get; set; }
    public string? Comment { get; set; }
    public DateTime AddedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? AddedBy { get; set; }
    public int? ModifiedBy { get; set; }
}