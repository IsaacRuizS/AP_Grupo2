using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

namespace FB.Data
{
    public static class UserSync
    {
        /// Cambio de clase de login>controller a Data>UserSync, esta clase se encarga de sincronizar los usuarios
        public static void SyncUserToFoodbank(string username, bool updateLastLogin)
        {
            if (string.IsNullOrEmpty(username)) return;
            string efConn = ConfigurationManager.ConnectionStrings["APCEOneEntities"]?.ConnectionString;
            if (string.IsNullOrEmpty(efConn)) return;
            var efBuilder = new EntityConnectionStringBuilder(efConn);
            var providerConn = efBuilder.ProviderConnectionString;
            if (string.IsNullOrEmpty(providerConn)) return;
            using (var conn = new SqlConnection(providerConn))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT UserId FROM Users WHERE Username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var scalar = cmd.ExecuteScalar();
                    if (scalar == null || scalar == DBNull.Value)
                    {
                        using (var insert = new SqlCommand("INSERT INTO Users (Username, Email, FullName, IsActive, CreatedAt, LastLogin) VALUES (@Username, @Email, @FullName, @IsActive, @CreatedAt, @LastLogin)", conn))
                        {
                            insert.Parameters.AddWithValue("@Username", username);
                            insert.Parameters.AddWithValue("@Email", username);
                            insert.Parameters.AddWithValue("@FullName", string.Empty);
                            insert.Parameters.AddWithValue("@IsActive", true);
                            insert.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                            insert.Parameters.AddWithValue("@LastLogin", updateLastLogin ? (object)DateTime.Now : DBNull.Value);
                            insert.ExecuteNonQuery();
                        }
                    }
                    else if (updateLastLogin)
                    {
                        using (var upd = new SqlCommand("UPDATE Users SET LastLogin = @LastLogin, IsActive = @IsActive WHERE UserId = @id", conn))
                        {
                            upd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
                            upd.Parameters.AddWithValue("@IsActive", true);
                            upd.Parameters.AddWithValue("@id", Convert.ToInt32(scalar));
                            upd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}