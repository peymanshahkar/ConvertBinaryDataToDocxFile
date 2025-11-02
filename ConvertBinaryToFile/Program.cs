using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConvertBinaryToFile
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Insert FileId To Read Data And -1 To Close:");
                string inputLine=Console.ReadLine();

            if(Convert.ToInt32(inputLine)>=1)
            {
                ReadBinaryData(Convert.ToInt32(inputLine));  
            }
           
        }



        private static void ReadBinaryData(int fileId)
        {
            string connectionString = "Server=.\\Gostar;Database=eData;User Id=sa;Password=abc1234;";
            string query = "SELECT TemplateFile FROM Office.LetterTemplateFile WHERE LetterTemplateFileID = @Id";
            //int fileId = 1; // شناسه فایلی که می‌خواهید بازیابی کنید

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", fileId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        byte[] fileData = (byte[])reader["TemplateFile"];

                        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileId + ".docx");
                        File.WriteAllBytes(outputPath, fileData);
                        Console.WriteLine("فایل با موفقیت بازیابی و ذخیره شد.");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("خطا: " + ex.Message);
                }
            }
        }
    }
}
