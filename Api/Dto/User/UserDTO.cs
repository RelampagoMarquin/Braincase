namespace Api.Dto.User
{
    public class UserCreateDTO
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Registration { get; set; }

        public string ConfirmedPassword { get; set; }
    }

    public class UserUpdateDTO
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Registration { get; set; }

        public string? ConfirmedPassword { get; set; }
    }

    public class UserResponseDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Registration { get; set; }
    }
}
