using System;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_OPERATION
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();

            string connStr = @"Data Source=DESKTOP-0A8E9N0\SQLEXPRESS;Initial Catalog=saravanan;Integrated Security=True";

            int choice;

            do
            {
                Console.WriteLine("\n=== STUDENT DETAIL ===");
                Console.WriteLine("1. Insert Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. View Students");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();

                        switch (choice)
                        {
                            case 1:
                                obj.InsertStudent(con);
                                break;
                            case 2:
                                obj.UpdateStudent(con);
                                break;
                            case 3:
                                obj.DeleteStudent(con);
                                break;
                            case 4:
                                obj.ViewStudents(con);
                                break;
                            case 5:
                                Console.WriteLine("Exiting...");
                                break;
                            default:
                                Console.WriteLine("Invalid choice!");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            } while (choice != 5);
        }

        // ------------------------ INSERT ------------------------
        public void InsertStudent(SqlConnection con)
        {
            Console.Write("Enter Name: ");
            string sname = Console.ReadLine();

            Console.Write("Enter Course: ");
            string scourse = Console.ReadLine();

            Console.Write("Enter Email: ");
            string semail = Console.ReadLine();

            Console.Write("Enter Age: ");
            int sage = Convert.ToInt32(Console.ReadLine());

            // INSERT without manually entering Sid (assuming Sid is IDENTITY)
            string query = @"INSERT INTO Student_Details (Sname, Scourse, Semail, Sage)
                             VALUES (@Sname, @Scourse, @Semail, @Sage)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Sname", sname);
            cmd.Parameters.AddWithValue("@Scourse", scourse);
            cmd.Parameters.AddWithValue("@Semail", semail);
            cmd.Parameters.AddWithValue("@Sage", sage);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Student Added Successfully!");
        }

        // ------------------------ UPDATE ------------------------
        public void UpdateStudent(SqlConnection con)
        {
            Console.Write("Enter Sid to Update: ");
            int sid = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            string sname = Console.ReadLine();

            Console.Write("Enter Course: ");
            string scourse = Console.ReadLine();

            Console.Write("Enter Email: ");
            string semail = Console.ReadLine();

            Console.Write("Enter Age: ");
            int sage = Convert.ToInt32(Console.ReadLine());

            string query = @"UPDATE Student_Details
                             SET Sname=@Sname, Scourse=@Scourse, Semail=@Semail, Sage=@Sage
                             WHERE Sid=@Sid";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Sid", sid);
            cmd.Parameters.AddWithValue("@Sname", sname);
            cmd.Parameters.AddWithValue("@Scourse", scourse);
            cmd.Parameters.AddWithValue("@Semail", semail);
            cmd.Parameters.AddWithValue("@Sage", sage);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                Console.WriteLine("Student Updated Successfully!");
            else
                Console.WriteLine("No student found with the given Sid.");
        }

        // ------------------------ DELETE ------------------------
        public void DeleteStudent(SqlConnection con)
        {
            Console.Write("Enter Sid to Delete: ");
            int sid = Convert.ToInt32(Console.ReadLine());

            string query = "DELETE FROM Student_Details WHERE Sid=@Sid";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Sid", sid);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                Console.WriteLine("Student Deleted Successfully!");
            else
                Console.WriteLine("No student found with the given Sid.");
        }

        // ------------------------ VIEW ------------------------
        public void ViewStudents(SqlConnection con)
        {
            string query = "SELECT * FROM Student_Details";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\n------------------------------- STUDENT RECORDS --------------------------------");
            Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-25} {4,-5}", "Sid", "Name", "Course", "Email", "Age");
            Console.WriteLine("---------------------------------------------------------------------------------");

            if (!dr.HasRows)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                while (dr.Read())
                {
                    Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-25} {4,-5}",
                        dr["Sid"],
                        dr["Sname"],
                        dr["Scourse"],
                        dr["Semail"],
                        dr["Sage"]);
                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------");
            dr.Close();
        }
    }
}
