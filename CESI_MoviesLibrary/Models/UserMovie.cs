namespace CESI_MoviesLibrary.Models
{
    public class UserMovie
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string Status { get; set; } = "ToWatch"; // ou "Seen"
    }
}
