using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;


namespace FourierNewton.Common.Database.AccountInformation
{
    public class AccountInformationManager
    {

        public static bool IsThereAnyRecordWithGivenEmail(string email) {

            string connectionString = DatabaseConstants.ConnectionStringOfDatabase202008261632;

            var EmailExists = false;

            if (!string.IsNullOrEmpty(email))
            {
                using (IDbConnection iDbConnection = new SQLiteConnection(connectionString))
                {

                    string query = "select * from AccountInformation where Email = @Email";
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Email", email.Trim());
                    var output = iDbConnection.Query<AccountInformation>(query, dynamicParameters);
                    EmailExists = output.ToList().Any();

                }
            }

            return EmailExists;

        }

        public static List<AccountInformation> GetData()
        {

            List<AccountInformation> listOfAccountInformation;

            string connectionString = DatabaseConstants.ConnectionStringOfDatabase202008261632;

            using (IDbConnection iDbConnection = new SQLiteConnection(connectionString)) {

                string query = "select * from AccountInformation";
                var output = iDbConnection.Query<AccountInformation>(query, new DynamicParameters());
                listOfAccountInformation = output.ToList();

            }

            return listOfAccountInformation;
        }

        public static void InsertData(AccountInformation accountInformation)
        {

            string connectionString = DatabaseConstants.ConnectionStringOfDatabase202008261632;

            using (IDbConnection iDbConnection = new SQLiteConnection(connectionString))
            {

                string query = "insert into AccountInformation (Email, Password, Date) values (@Email, @Password, @Date)";
                iDbConnection.Execute(query, accountInformation);
                
            }

        }

        public static void UpdatePassword(AccountInformation accountInformation)
        {

            string connectionString = DatabaseConstants.ConnectionStringOfDatabase202008261632;

            using (IDbConnection iDbConnection = new SQLiteConnection(connectionString))
            {

                string query = "update AccountInformation set Password = @Password where Email = @Email";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Email", accountInformation.Email);
                dynamicParameters.Add("@Password", accountInformation.Password);
                iDbConnection.Execute(query, dynamicParameters);

            }

        }

        public static List<AccountInformation> GetDataWithGivenEmail(string email)
        {

            List<AccountInformation> listOfAccountInformation;

            string connectionString = DatabaseConstants.ConnectionStringOfDatabase202008261632;

            using (IDbConnection iDbConnection = new SQLiteConnection(connectionString))
            {

                string query = "select * from AccountInformation where Email = @Email";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Email", email);
                var output = iDbConnection.Query<AccountInformation>(query, dynamicParameters);
                listOfAccountInformation = output.ToList();

            }

            return listOfAccountInformation;
        }


    }
}
