using Api.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Api.examples
{
    public class CreateUserDtoExample : IExamplesProvider<CreateUserDTO>
{
    public CreateUserDTO GetExamples()
    {
        return new CreateUserDTO
        {
            Name = "John Doe",
            Email = "johndoe@example.com",
            Password = "password123"
        };
    }
}
}



