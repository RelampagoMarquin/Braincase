namespace Api.Dto.User
{
    public class CreateUserDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Registration { get; set; }

        public string ConfirmedPassword { get; set; }
    }

    public class UpdateUserDTO
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Registration { get; set; }

        public string? ConfirmedPassword { get; set; }
    }

    public class ResposeUserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Registration { get; set; }
    }
}
