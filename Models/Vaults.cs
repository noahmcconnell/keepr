namespace keepr.Models
{
    public class VaultToDelete
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SendingUserId { get; set; }
    }
    public class Vault
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description {get; set;}

        public string UserId { get; set; }

        public Vault() { }
        public Vault(string name, string description, string userid)
        {
            Name = name;
            Description = description;
            UserId = userid;
        }
    }

    public class DeleteUserVault
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SendUserId { get; set; }
    }
}