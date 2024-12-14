using MySqlConnector;
using UL = UtilitiesLibrary;

using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using System.Xml.Linq;
using UtilitiesLibrary;
using System.Collections.Generic;
using AS = AccountService;
using PostingService;
using AccountService;

namespace TestProject1
{
    public class UnitTest1
    {
        /// <summary>
        /// Проверка на содержание банворда в имени пользователя
        /// </summary>

        [Fact]
        public void BadNameInputTest()
        {
            PasswordHasherClass hasher = new PasswordHasherClass();

            string userName = "moron";
            string hashedPassword = hasher.Hash("12345");
            string phoneNumber = "81111111111";

            var exception = Assert.Throws<Exception>(() => RegistrationClass.SignUp(userName, hashedPassword, phoneNumber));

            Assert.Equal("Некорректное имя.", exception.Message);
        }

        /// <summary>
        /// Проверка на введение неверного пароля
        /// </summary>

        [Fact]
        public void IncorrectPasswordCheck()
        {
            PasswordHasherClass hasher = new PasswordHasherClass();
            string userName = "testbot";
            string hashedPassword = hasher.Hash("incorrectPasswordForTesting");


            var exception = Assert.Throws<Exception>(() =>
            {
                QueryExecutor.ExecuteQuery(() =>
                {
                    AuthenticationClass.LogIn(userName, hashedPassword);
                });
            });

            Assert.Equal("Неверный пароль.", exception.Message);
        }

        /// <summary>
        /// Проверка на валидацию номера телефона
        /// </summary>

        [Fact]
        public void CorrectPhoneNumberCheck()
        {
            string phoneNumber = "88005553535";

            PhoneNumberValidator validator = new PhoneNumberValidator();
            validator.IsValid(phoneNumber);

            Assert.True(validator.IsValid(phoneNumber));
        }

        /// <summary>
        /// Проверка на валидацию номера телефона
        /// </summary>

        [Fact]
        public void IncorrectPhoneNumberCheck()
        {
            string phoneNumber = "99988005553535";

            PhoneNumberValidator validator = new PhoneNumberValidator();
            validator.IsValid(phoneNumber);

            Assert.False(validator.IsValid(phoneNumber));
        }

        /// <summary>
        /// Проверка функции хэширования паролей
        /// </summary>

        [Fact]
        public void HashFunctionCheck()
        {
            string passwordToHash = "12345";
            string hashedPasswordToCheck = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5";

            PasswordHasherClass hasher = new PasswordHasherClass();
            string hashedPassword = hasher.Hash("12345");

            Assert.Equal(hashedPasswordToCheck, hashedPassword);
        }


        /// <summary>
        /// Проверка существования пользователя в базе данных
        /// </summary>

        [Fact]
        public void UserExistsCheck()
        {
            bool userExists = RegistrationClass.CheckIfUserExists("testbot");

            Assert.True(userExists);
        }

        /// <summary>
        /// Проверка успешного входа в аккаунт
        /// </summary>

        [Fact]
        public void LogInCheck()
        {
            string userName = "testbot";
            PasswordHasherClass hasher = new PasswordHasherClass();
            string hashedPassword = hasher.Hash("12345");

            AuthenticationClass.LogIn(userName, hashedPassword);
        }

        /// <summary>
        /// Проверка успешного входа в аккаунт
        /// </summary>

        [Fact]
        public void AccountModifiersCheck()
        {
            string userName = "testbot";
            PasswordHasherClass hasher = new PasswordHasherClass();
            string hashedPassword = hasher.Hash("12345");

            AuthenticationClass.LogIn(userName, hashedPassword);

            AccountPasswordModifier passwordModifier = new AccountPasswordModifier();
            AccountUsernameModifier usernameModifier = new AccountUsernameModifier();
            AccountPhoneNumberModifier phoneNumberModifier = new AccountPhoneNumberModifier();

            string newPassword = hasher.Hash("54321");
            string newUserName = "bottest";
            string newPhoneNumber = "89999999999";

            passwordModifier.Modify(newPassword);
            usernameModifier.Modify(newUserName);
            phoneNumberModifier.Modify(newPhoneNumber);

            string query = "SELECT COUNT(*) FROM users WHERE\n" +
                           "username = @username AND\n" +
                           "hashedPass = @hashedPass AND\n" +
                           "phoneNumber = @phoneNumber";

            int count = 0;
            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@username", newUserName);
                    command.Parameters.AddWithValue("@hashedPass", newPassword);
                    command.Parameters.AddWithValue("@phoneNumber", newPhoneNumber);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            });

            Console.WriteLine(count);
            string oldPassword = hasher.Hash("12345");
            string oldUserName = "testbot";
            string oldPhoneNumber = "81111111111";

            passwordModifier.Modify(oldPassword);
            usernameModifier.Modify(oldUserName);
            phoneNumberModifier.Modify(oldPhoneNumber);
        }

        /// <summary>
        /// Проверка удаления пользователя из системы
        /// </summary>

        [Fact]
        public void RemoveUserCheck()
        {
            string userName = "tempuser";
            PasswordHasherClass hasher = new PasswordHasherClass();
            string hashedPassword = hasher.Hash("tempuserpass");
            string phoneNumber = "+79999999999";

            RegistrationClass.SignUp(userName, hashedPassword, phoneNumber);

            AuthenticationClass.LogIn(userName, hashedPassword);

            AccountRemover.RemoveUser(hashedPassword);

        }

        /// <summary>
        /// Проверка выхода из системы
        /// </summary>

        [Fact]
        public void LogOutCheck()
        {
            string userName = "testbot";
            PasswordHasherClass hasher = new PasswordHasherClass();
            string hashedPassword = hasher.Hash("12345");

            AuthenticationClass.LogIn(userName, hashedPassword);

            AuthenticationClass.LogOut();

            Assert.Equal(CurrentSession.CurrentUserId, 0);
            Assert.Equal(ProfileDataProvider.userData, null);
        }

        /// <summary>
        /// Проверка наличия роли guest
        /// </summary>

        [Fact]
        public void CheckRoleTest()
        {
            string userName = "testbot";
            PasswordHasherClass hasher = new PasswordHasherClass();
            string hashedPassword = hasher.Hash("12345");

            AuthenticationClass.LogIn(userName, hashedPassword);

            RoleValidator roleValidator = new RoleValidator();
            bool result = roleValidator.IsValid("guest");

            Assert.True(result);
        }
    }
}