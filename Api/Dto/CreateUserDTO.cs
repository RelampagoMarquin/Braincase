using System.ComponentModel.DataAnnotations;

namespace Api.Dto
{
   public class CreateUserDTO
    {
        public String Name;

        public String Email;

        public String Password;

        public String ConfirmedPassword;

    }
}
