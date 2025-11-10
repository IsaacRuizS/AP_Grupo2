using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FB.Login.Models;

namespace FB.Login.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private string GetProviderConnectionString()
        {
            // Try EF connection first
            var efConn = ConfigurationManager.ConnectionStrings["FoodbankEntities"]?.ConnectionString;
            if (!string.IsNullOrEmpty(efConn))
            {
                try
                {
                    var efBuilder = new EntityConnectionStringBuilder(efConn);
                    var providerConn = efBuilder.ProviderConnectionString;
                    if (!string.IsNullOrEmpty(providerConn))
                        return providerConn;
                }
                catch
                {
                    // ignore and fallback
                }
            }

            // Fallback to DefaultConnection
            var defaultConn = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            return defaultConn;
        }

        // GET: Users
        public ActionResult Index()
        {
            var list = new List<UserViewModel>();
            var connStr = GetProviderConnectionString();
            if (string.IsNullOrEmpty(connStr))
            {
                return View(list);
            }

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT UserId, Username, Email, FullName, IsActive, CreatedAt, LastLogin FROM Users ORDER BY Username", conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var u = new UserViewModel
                        {
                            UserId = rdr.GetInt32(0),
                            Username = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                            Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            FullName = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                            IsActive = !rdr.IsDBNull(4) && rdr.GetBoolean(4),
                            CreatedAt = !rdr.IsDBNull(5) ? rdr.GetDateTime(5) : DateTime.MinValue,
                            LastLogin = !rdr.IsDBNull(6) ? (DateTime?)rdr.GetDateTime(6) : null
                        };
                        list.Add(u);
                    }
                }
            }

            return View(list);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(400);
            var connStr = GetProviderConnectionString();
            if (string.IsNullOrEmpty(connStr)) return HttpNotFound();

            UserViewModel model = null;
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT UserId, Username, Email, FullName, IsActive, CreatedAt, LastLogin FROM Users WHERE UserId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id.Value);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            model = new UserViewModel
                            {
                                UserId = rdr.GetInt32(0),
                                Username = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                                Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                                FullName = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                                IsActive = !rdr.IsDBNull(4) && rdr.GetBoolean(4),
                                CreatedAt = !rdr.IsDBNull(5) ? rdr.GetDateTime(5) : DateTime.MinValue,
                                LastLogin = !rdr.IsDBNull(6) ? (DateTime?)rdr.GetDateTime(6) : null
                            };
                        }
                    }
                }
            }

            if (model == null) return HttpNotFound();
            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,IsActive")] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var connStr = GetProviderConnectionString();
            if (string.IsNullOrEmpty(connStr)) return HttpNotFound();

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Users SET IsActive = @IsActive WHERE UserId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                    cmd.Parameters.AddWithValue("@id", model.UserId);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
