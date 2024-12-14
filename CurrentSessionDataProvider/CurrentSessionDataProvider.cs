using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentSessionDataProvider
{
    /// <summary>
    /// Класс, отвечающий за хранение данных пользователя в текущей сессии
    /// </summary>
    public static class CurrentSessionDataProvider
    {
        public static int CurrentUserId { get; set; }

        public static string ConnectionString = "Server=localhost; user id=root; Password=12345; Database=appdb;";
    }
}
