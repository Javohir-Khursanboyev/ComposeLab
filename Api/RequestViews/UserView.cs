namespace Api.RequestViews;

public class UserView
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AssetView? AssetView { get; set; }
}