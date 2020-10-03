public class Sword : Item {
    public Guid Id { get; set; }
    [Range(0, 99)]
    public int Level { get; set; }
    public DateTime CreationTime { get; set; }
    public int damage { get; set; }
}