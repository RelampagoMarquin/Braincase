namespace Api.Dto.Favorites
{
    public class FavoritesDTO
    {
        public bool Own { get; set; }

        public String UserId { get; set; }

        public Guid? QuestionId { get; set; } 
    }

    public class ResponseFavoritesDTO
    {
        public bool Own { get; set; }

        public String UserId { get; set; }

        public Guid QuestionId { get; set; } 
    }
}