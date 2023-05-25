namespace Api.Dto.Favorites
{
    public class FavoritesCreateDTO
    {
        public bool Own { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }  
    }

}