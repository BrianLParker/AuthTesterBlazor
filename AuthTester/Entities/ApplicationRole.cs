namespace AuthTester.Entities
{
    public class ApplicationRole
    {
        public int ApplicationRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
