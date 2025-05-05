using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.restore_ManzanoMiquel.connections
{
    public class SQLConnection
    {
        private String HOST = "postgresql-miquel.alwaysdata.net:5432"; // Ubicació de la BD.
        private String DB = "miquel_nf3ra6pr"; // nom de la BD.
        private String USER = "miquel_nf3ra6pruser";
        private String PASSWORD = "Sjo2025!";

        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(
                "Host=" + HOST + ";" + "Username=" + USER + ";" +
                "Password=" + PASSWORD + ";" + "Database=" + DB + ";"
            );
            conn.Open();
            return conn;
        }
    }
}
