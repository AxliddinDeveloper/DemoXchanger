using System;
using System.Threading.Tasks;
using Bytescout.Spreadsheet;
using Tarteeb.Importer.Brokers.Storages;
using Xchanger.Models;

namespace readDataExcel
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Students student = new Students();

            Spreadsheet document = new Spreadsheet();
            document.LoadFromFile(@"C:\Users\User\Desktop\Xchanger\Students.xlsx");
            Worksheet worksheet = document.Workbook.Worksheets.ByName("Sheet1");

            using (StorageBroker broker = new StorageBroker())
            {
                for (int row = 1; row <= worksheet.UsedRangeRowMax; row++)
                {
                    Students students = new Students();
                    students.FirstName = worksheet.Cell(row, 0).ToString();
                    students.LastName = worksheet.Cell(row, 1).ToString();
                    students.Email = worksheet.Cell(row, 2).ToString();
                    students.PhoneNumber = worksheet.Cell(row, 3).ToString();
                    Console.WriteLine(students.FirstName);
                    Console.WriteLine(students.LastName);
                    Console.WriteLine(students.Email);
                    Console.WriteLine(students.PhoneNumber);
                    await broker.InsertStudentAsync(students);
                }

                await broker.SaveChangesAsync();
            }
            document.Close();
            Console.ReadKey();
        }
    }
}