using StajEntity.DAL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajEntity.DAL.DbOperations
{
    public class UserDb
    {

        string connectionString = ConfigurationManager.ConnectionStrings["SchoolDb"].ConnectionString;

        private static UserDb _instance;

        private UserDb()
        {

        }

        public static UserDb GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserDb();
            }

            return _instance;
        }

        public User AddNewUser(User _user)
        {
            var connection = new SqlConnection(connectionString);

            try
            {

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var cmd = new SqlCommand();
                cmd.Connection = connection;

                var insertUserQuery = @"INSERT INTO [User]
                                             VALUES
                                                   (@name
                                                   ,@surname
                                                   ,@username
                                                   ,@password)
                                    SELECT @@IDENTITY ";
                cmd.CommandText = insertUserQuery;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@name", _user.Name);
                cmd.Parameters.AddWithValue("@surname", _user.Surname);
                cmd.Parameters.AddWithValue("@username", _user.Username);
                cmd.Parameters.AddWithValue("@password", _user.Password);

                var returnedObject = cmd.ExecuteScalar();

                int lastId = 0;

                if (int.TryParse(returnedObject.ToString(), out lastId))
                {
                    _user.Id = lastId;

                    return _user;
                }

                return null;



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        public void DeleteUserByID(int userid)
        {
            var conn = new SqlConnection(connectionString);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }


                var cmd = new SqlCommand();
                cmd.Connection = conn;

                var deleteUserQuery = @"DELETE FROM [User]
                                             WHERE ID = @ID ";
                cmd.CommandText = deleteUserQuery;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", userid);

            }

            catch(Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }
        
        public void UpdateUserByID(int userid,string newname, string newsurname)
        {
            var conn = new SqlConnection(connectionString);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                var updateUserQuery = @"UPDATE [User]
                                             SET name=@newname,
                                                 surname=@newsurname   
                                             WHERE ID = @ID ";
                cmd.CommandText = updateUserQuery;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", userid);
                cmd.Parameters.AddWithValue("@newname", newname);
                cmd.Parameters.AddWithValue("@newsurname", newsurname);
                     

                }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable readAllUsers()
        {
            var conn = new SqlConnection(connectionString);
            try
            {
               

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                var selectUsersQuery = @"SELECT * FROM [User];";
                cmd.CommandText = selectUsersQuery;
                cmd.CommandType = CommandType.Text;


                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;

                DataTable table1 = new DataTable();
                adp.Fill(table1);

                    return table1;
                
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }


        public User GetUserByUsernameAndPassword(string _username, string _password)
        {
            SqlConnection con = new SqlConnection(connectionString);

            try
            {

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM [User] WHERE Username =@username  AND Password = @password";
                cmd.Parameters.AddWithValue("@username", _username);
                cmd.Parameters.AddWithValue("@password", _password);


                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;

                DataTable tbl = new DataTable();
                adp.Fill(tbl);

                if (tbl.Rows.Count > 0)
                {
                    var user = new User()
                    {
                        Id = Convert.ToInt32(tbl.Rows[0]["Id"].ToString()),
                        Name = tbl.Rows[0]["Name"].ToString(),
                        Surname = tbl.Rows[0]["Surname"].ToString(),
                        Username = tbl.Rows[0]["Username"].ToString(),
                        Password = tbl.Rows[0]["Password"].ToString()
                    };
                    return user;
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

        }






    }
}
