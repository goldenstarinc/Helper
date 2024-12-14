using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary
{
    /// <summary>
    /// Класс, содержащий функции хэширования входного параметра
    /// </summary>
    public class PasswordHasherClass : IDataHasher<string>
    {
        /// <summary>
        /// Вычисляет хэш данной строки
        /// </summary>
        /// <param name="password">Пароль, подлежащий хэшированию</param>
        /// <returns>Хэш код переданной строки</returns>
        public string Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hexadecimal string
                }
                return builder.ToString();
            }
        }
    }

    /// <summary>
    /// Маркер интерфейса для хэширования данных
    /// </summary>
    public interface IDataHasher<T>
    {
        string Hash(T valueToHash);
    }
}
