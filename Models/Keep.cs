namespace keepr.Models
{
    public class KeepToDelete
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SendingUserId { get; set; }
    }
    public class Keep
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string Img { get; set; }

        public int Views { get; set; }

        public int Shares { get; set; }

        public int Keeps { get; set; }

        public int IsPrivate { get; set; }
        public Keep() { }
        public Keep(int id, string name, string description, string img, int views, int shares)
        {
            Id = id;
            Name = name;
            Description = description;
            Img = img;
            Views = views;
            Shares = shares;

        }
    }

    public class DeleteUserKeep
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SendUserId { get; set; }
    }

}