namespace Api.Dto.Comment
{
    public class CommentDTO
    {
        public string Text { get; set; }

    }
    public class ResponseCommentDTO
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public Guid QuestionId { get; set; }
    }
     public class CreateCommentDTO : CommentDTO
    {

        public Guid? UserId { get; set; }

        public Guid? QuestionId { get; set; }
    }

}