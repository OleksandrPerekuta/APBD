using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IncorrectCredentials(firstName, lastName))
            {
                return false;
            }

            if (IncorrectEmail(email))
            {
                return false;
            }
            
            var age = GetAgeFromBirthdate(dateOfBirth);

            if (NotOldEnough(age))
            {
                return false;
            }

            var client = GetClientById(clientId);
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            SetUserCreditLimit(user,client);
            if (NotEnoughCreditLimit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static bool IncorrectCredentials(string firstName, string lastName)
        {
            return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
        }

        private static bool IncorrectEmail(string email)
        {
            return !(email.Contains('@') && email.Contains('.'));
        }

        private static int GetAgeFromBirthdate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            var result = (now.Month < dateOfBirth.Month) ||
                         (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day);
            if (result)
            {
                age--;
            }

            return age;
        }

        private static Client GetClientById(int clientId)
        {
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(clientId);
        }

        private void SetUserCreditLimit(User user,Client client)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit * 2;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
        }

        private static bool NotEnoughCreditLimit(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private static bool NotOldEnough(int age)
        {
            return age < 21;
        }
    }
}