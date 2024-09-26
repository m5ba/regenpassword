namespace ReGenPass
{
    public class PasswordContext
    {
        public string Resource { get; set; } = null!;
        public string Login { get; set; } = null!;
        public int Iteration { get; set; }
        public string Secret { get; set; } = null!;
    }
}
