namespace sc.contracts
{
    public enum Permissions
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class PermissionSet 
    {
        public PermissionSet()
        {
        }

        public PermissionSet(Permissions[] permissions)
        {
            Permissions = permissions;
        }

        public Permissions[] Permissions { get; set; }
    }
}