using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETapp
{
    public class DBHandler
    {
        readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PETdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private int ExecuteNonQuery(string query)
        {
            int AffectedRows = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand com = new SqlCommand(query, con))
            {
                con.Open();
                AffectedRows = com.ExecuteNonQuery();
            }

            return AffectedRows;

        }

        private DataSet ExecuteQuery(string query)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, con))
            {
                da.Fill(ds);

            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("No results");
            }
            return ds;
        }

        public List<Person> AllPersons()
        {
            List<Person> li = new List<Person>();
            DataSet ds = new DataSet();
            ds = ExecuteQuery("select * from Persons");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                switch ((string)r["role"].ToString().ToUpper())
                {
                    case "AGENT":
                        li.Add(new Agent((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"]));
                        break;
                    case "INFORMANT":
                        li.Add(new Informant((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"], (string)r["methodOfPayment"], (string)r["currency"]));
                        break;
                    case "OBSERVANT":
                        li.Add(new Observant((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"]));
                        break;
                    default:
                        break;
                }
            }
            return li;
        }

        public List<Report> AllReports()
        {
            List<Report> li = new List<Report>();
            DataSet ds = new DataSet();
            ds = ExecuteQuery("select * from Reports");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                li.Add(new Report((string)r["text"], GetPerson((int)r["subjectId"]), GetPerson((int)r["authorId"]), (int)r["id"]));
            }
            return li;
        }
        //Returns all persons of the specified role
        public List<Person> AllSubPerson(string role)
        {
            List<Person> li = new List<Person>();
            DataSet ds = new DataSet();
            switch (role.ToUpper())
            {
                case "AGENT":
                    ds = ExecuteQuery($"select * from Persons where role = '{role.ToUpper()}'");
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        li.Add(new Agent((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"]));

                    }
                    break;
                case "INFORMANT":
                    ds = ExecuteQuery($"select * from Persons where role = '{ role.ToUpper()}'");
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        li.Add(new Informant((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"], (string)r["methodOfPayment"], (string)r["currency"]));
                    }
                    break;
                case "OBSERVANT":
                    ds = ExecuteQuery($"select * from Persons where role = '{role.ToUpper()}'");
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        li.Add(new Observant((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"]));
                    }
                    break;
                default:
                    throw new ArgumentException("Role Not Valid");
            }

            return li;
        }

        public Person GetPerson(int id)
        {
            DataSet ds = new DataSet();
            ds = ExecuteQuery($"Select top 1 * from Persons where id = '{id}'");
            DataRow r = ds.Tables[0].Rows[0];
            switch ((string)r["role"].ToString().ToUpper())
            {
                case "AGENT":
                    Person a = new Agent((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"]);
                    return a;
                case "INFORMANT":
                    Person i = new Informant((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"], (string)r["methodOfPayment"], (string)r["currency"]);
                    return i;
                case "OBSERVANT":
                    Person o = new Observant((int)r["id"], (string)r["name"], (string)r["address"], (string)r["nationality"], (string)r["serializedImage"], (string)r["description"]);
                    return o;
                default:
                    throw new ArgumentException("Role Not Valid");
            }
            
        }

        public Report GetReport(int id)
        {
            DataSet ds = new DataSet();
            ds = ExecuteQuery($"select top 1 * from Reports where id = '{id}'");
            DataRow r = ds.Tables[0].Rows[0];
            return new Report((string)r["text"], GetPerson((int)r["subjectId"]), GetPerson((int)r["authorId"]), (int)r["id"]); 
        }

        public List<Comment> GetCommentsForReport(int id)
        {
            List<Comment> li = new List<Comment>();
            DataSet ds = new DataSet();
            ds = ExecuteQuery($"select * from Comments where parentId = '{id}'");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                li.Add(new Comment((string)r["text"], GetPerson((int)r["authorId"]), GetReport((int)r["parentId"]), (int)r["id"]));
            }
            return li;
        }

        public Comment GetComment(int id)
        {
            DataSet ds = new DataSet();
            ds = ExecuteQuery($"select top 1 * from Comments where id = '{id}'");
            DataRow r = ds.Tables[0].Rows[0];
            return new Comment((string)r["text"], GetPerson((int)r["authorId"]), GetReport((int)r["parentId"]), (int)r["id"]);
        }
        //Object i stedet for ID måske?
        public List<Report> GetReportsByAuthor(int authorId)
        {
            List<Report> li = new List<Report>();
            DataSet ds = ExecuteQuery($"select * from Reports where authorId = '{authorId}'");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                li.Add(new Report((string)r["text"], GetPerson((int)r["subjectId"]), GetPerson((int)r["authorId"]), (int)r["id"]));
            }
            return li;
        }

        public List<Report> GetReportsBySubject(int subjectId)
        {
            List<Report> li = new List<Report>();
            DataSet ds = ExecuteQuery($"select * from Reports where subjectId = '{subjectId}'");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                li.Add(new Report((string)r["text"], GetPerson((int)r["subjectId"]), GetPerson((int)r["authorId"]), (int)r["id"]));
            }
            return li;
        }

        public List<Comment> GetCommentsByAuthor(int authorId)
        {
            List<Comment> li = new List<Comment>();
            DataSet ds = ExecuteQuery($"select * from Comments where authorId = '{authorId}'");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                li.Add(new Comment((string)r["text"], GetPerson((int)r["authorId"]), GetReport((int)r["parentId"]), (int)r["id"]));
            }
            return li;
        }

        public int NewPerson(Person p)
        {
            if (p.GetType() == typeof(Agent))
            {
                return ExecuteNonQuery("insert into Persons (role, name, address, nationality, serializedImage, description) " +
                    $"values ('AGENT', '{p.Name}', '{p.Address}', '{p.Nationality}', '{p.SerializedImage}', '{p.Description}')");
            }
            else if (p.GetType() == typeof(Informant))
            {
                Informant i = p as Informant;
                return ExecuteNonQuery("insert into Persons (role, name, address, nationality, serializedImage, description, methodOfPayment, currency) " +
                    $"values ('INFORMANT', '{p.Name}', '{p.Address}', '{p.Nationality}', '{p.SerializedImage}', '{p.Description}', '{i.MethodOfPayment}', '{i.Currency}')");
            }
            else if (p.GetType() == typeof(Observant))
            {
                return ExecuteNonQuery("insert into Persons (role, name, address, nationality, serializedImage, description) " +
                    $"values ('OBSERVANT', '{p.Name}', '{p.Address}', '{p.Nationality}', '{p.SerializedImage}', '{p.Description}')");
            }
            else
            {
                throw new ArgumentException("Person Type Not Implemented");
            }
        }

        public int NewReport(Report r)
        {
            return ExecuteNonQuery("insert into Reports (text, authorId, subjectId) " +
                $"values ('{r.Text}', '{r.Author.Id}', '{r.Subject.Id}')");
        }

        public int NewComment(Comment c)
        {
            return ExecuteNonQuery("insert into Comments (text, authorId, parentId) " +
                $"values ('{c.Text}', '{c.Author.Id}', '{c.ParentReport.Id}')");
        }

        public int UpdatePerson(Person p)
        {
            if (p.GetType() == typeof(Agent))
            {
                return ExecuteNonQuery($"update Persons set role = 'AGENT', name = '{p.Name}', address = '{p.Address}', nationality = '{p.Nationality}', serializedImage = '{p.SerializedImage}', description = '{p.Description}' " +
                $"Where ID = '{p.Id}'");
            }
            else if (p.GetType() == typeof(Informant))
            {
                Informant i = p as Informant;
                return ExecuteNonQuery($"update Persons set role = 'AGENT', name = '{p.Name}', address = '{p.Address}', nationality = '{p.Nationality}', serializedImage = '{p.SerializedImage}', description = '{p.Description}', methodOfPayment = '{i.MethodOfPayment}', currency = '{i.Currency}' " +
                $"Where ID = '{p.Id}'");
            }
            else if (p.GetType() == typeof(Observant))
            {
                return ExecuteNonQuery($"update Persons set role = 'AGENT', name = '{p.Name}', address = '{p.Address}', nationality = '{p.Nationality}', serializedImage = '{p.SerializedImage}', description = '{p.Description}' " +
                $"Where ID = '{p.Id}'");
            }
            else
            {
                throw new ArgumentException("Role Invalid or Not Implemented");
            }
        }

        public int UpdateReport(Report r)
        {
            return ExecuteNonQuery($"update Reports set text = '{r.Text}', authorId = '{r.Author.Id}', subjectId = '{r.Subject.Id}' " +
                $"Where ID = '{r.Id}'");
        }

        public int UpdateComment(Comment c)
        {
            return ExecuteNonQuery($"update Comments set text = '{c.Text}', authorId = '{c.Author.Id}', parentId = {c.ParentReport.Id}' " +
                $"Where id = '{c.Id}'");
        }

        public int DeletePerson(Person p)
        {
            List<Report> reports = GetReportsByAuthor(p.Id);
            List<Report> subjectReports = GetReportsBySubject(p.Id);
            List<Comment> comments = GetCommentsByAuthor(p.Id);
            //Check if the person to be deleted is author or subject anywhere
            if (reports.Count > 0 || comments.Count > 0 || subjectReports.Count > 0)
            {
                throw new Exception("You cannot delete a person while they are the subject or author of a report or comment");
            }
            else
            {
                return ExecuteNonQuery($"delete from Persons where ID='{p.Id}';");
            }
        }

        public int DeleteReport(Report r)
        {
            List<Comment> comments = GetCommentsForReport(r.Id);
            foreach (Comment c in comments)
            {
                DeleteComment(c);
            }
            return ExecuteNonQuery($"delete from Reports where id='{r.Id}';");
        }

        public int DeleteComment(Comment c)
        {
            return ExecuteNonQuery($"delete from Comments where id='{c.Id}';");
        }
    }
}
