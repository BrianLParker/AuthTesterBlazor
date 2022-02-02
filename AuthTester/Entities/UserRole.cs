namespace AuthTester.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
    }
}
