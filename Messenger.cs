using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Lab2_2
{
    public class Messenger
    {
        readonly SqlConnection db;
        int ID;

        public Messenger(string connectionString)
        {
            db = new SqlConnection(connectionString);
            db.Open();
            ID = db.Query<int>("SELECT MAX(Id) FROM [Chat]").AsList()[0];
        }

        public async Task SendMessage(Message message)
        {
            await db.ExecuteAsync("INSERT INTO [Chat] VALUES (@Name, @Text)", message);
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            int newID = db.Query<int>("SELECT MAX(Id) FROM [Chat]").AsList()[0];
            var q = await db.QueryAsync<Message>("SELECT * FROM [Chat] WHERE [ID] > " + ID + " AND [ID] <= " + newID);
            ID = newID;
            return q;
        }
    }
}
