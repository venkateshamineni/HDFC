using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication8
{
    public class BusinessLogic
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder cmb;
        DataRow row;
        DataSet ds;
        public BusinessLogic()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=HDFC;Integrated Security=True");
            da = new SqlDataAdapter("select * from Tbl_Account", con);
            cmb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Tbl_Account");
            da.Update(ds.Tables["Tbl_Account"]);
            ds.Tables["Tbl_Account"].Constraints.Add("userid_pk", ds.Tables["Tbl_Account"].Columns["UserId"], true);
        }
        public bool CreateAccount(Account account)
        {
            row = ds.Tables["Tbl_Account"].NewRow();
            row["AcNo"] = account.AcNo;
            row["AcName"] = account.AcName;
            row["userid"] = account.UserId;
            row["password"] = account.password;
            row["Balance"] = account.Balance;
            ds.Tables["Tbl_Account"].Rows.Add(row);
            return da.Update(ds.Tables["Tbl_Account"]) == 1;
        }
        public bool LoginUser(Account account)
        {
            try
            {
                row = ds.Tables["Tbl_Account"].Rows.Find(account.UserId);
                return row["password"].ToString() == account.password;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ChangePassword(Account account)
        {
            row = ds.Tables["Tbl_Account"].Rows.Find(account.UserId);
            row["password"] = account.password;
            return da.Update(ds.Tables["Tbl_Account"]) == 1;

        }
        public bool TransferFunds(Transaction trans)
        {
            Account fraccount = GetAccountByAcno(new Account() { AcNo = trans.FromAccount });
            if (fraccount.Balance >= trans.Amount)
            {
                fraccount.Balance = fraccount.Balance - trans.Amount;
                Account toaccount = GetAccountByAcno(new Account() { AcNo = trans.ToAccount });
                toaccount.Balance = toaccount.Balance + trans.Amount;

                DataRow row1 = ds.Tables["Tbl_Account"].Rows.Find(fraccount.UserId);
                row1["Balance"] = fraccount.Balance;

                DataRow row2 = ds.Tables["Tbl_Account"].Rows.Find(toaccount.UserId);
                row2["Balance"] = toaccount.Balance;
                return da.Update(ds.Tables["Tbl_Account"]) == 2;
            }
            else
            {
                return false;
            }
        }
        public Account GetAccountByAcno(Account account)//Transfer fund
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Close();
            cmd.CommandText = string.Format($"select * from Tbl_Account where Acno='{account.AcNo}'");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                account.AcName = dr[1].ToString();
                account.UserId = dr[2].ToString();
                account.Balance = Convert.ToInt32(dr[4]);
            }
            con.Close();
            return account;
        }
        public Account GetAccountByUserID(Account account)
        {
            row = ds.Tables["Tbl_Account"].Rows.Find(account.UserId);
            account.AcNo = row["Acno"].ToString();
            account.AcName = row["AcName"].ToString();
            account.Balance = int.Parse(row["Balance"].ToString());
            return account;
        }
    }
 }