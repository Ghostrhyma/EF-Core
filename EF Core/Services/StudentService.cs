using EF_Core.Data;
using EF_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Core.Services
{
    public class StudentService
    {
        public static void ShowAllStudents(AppDbContext context)
        {
            var students = context.Students.OrderBy(s => s.Id).ToList();

            if (students.Count == 0)
            {
                Console.WriteLine("DB is Empty");
                return;
            }

            foreach (var student in students)
            {
                PrintStudent(student);
            }
        }

        public static void PrintStudent(Student student)
        {
            Console.WriteLine($"{student.Id} {student.FullName} Age:{student.Age} {student.Email}");
        }


        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<||||>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public static void ShowAdultStudents(AppDbContext context)
        {
            var adults = context.Students
                .Where(s => s.Age >= 18)
                .OrderBy(s => s.FullName)
                .ToList();

            Console.WriteLine("====== Adult students ======");

            if (adults.Count == 0)
            {
                Console.WriteLine("No adult students");
                return;
            }

            foreach (var student in adults)
            {
                PrintStudent(student);
            }
        }


        public static void SearchStudentByName(AppDbContext context)
        {
            Console.Write("Input part of name");

            string searchText = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                Console.WriteLine("String must not empty");
                return;
            }

            var students = context.Students
                .Where(s => s.FullName.Contains(searchText))
                .OrderBy(s => s.FullName)
                .ToList();


            Console.WriteLine();
            Console.WriteLine($"======= Search: {searchText} =======");

            if (students.Count == 0)
            {
                Console.WriteLine("No results");
                return;
            }

            foreach (var student in students)
            {
                PrintStudent(student);
            }
        }

        public static void ShowStudentsSortedByAgeAndName(AppDbContext context)
        {
            var students = context.Students
                .OrderByDescending(s => s.Age)
                .ThenBy(s => s.FullName)
                .ToList();


            Console.WriteLine("======= Results =======");


            foreach (var student in students)
            {
                Console.WriteLine($"Age: {student.Age} | FullName: {student.FullName} | Email: {student.Email}");
            }
        }

        public static void ShowStudentCards(AppDbContext context)
        {
            var cards = context.Students
                .OrderBy(s => s.FullName)
                .Select(s => new
                {
                    StudentId = s.Id,
                    Name = s.FullName,
                    Contact = s.Email,
                    IsAdult = s.Age >= 18
                })
                .ToList();

            Console.WriteLine("====== Students Cards ======");

            foreach (var card in cards)
            {
                string adultStatus = card.IsAdult ? "Yes" : "No";

                Console.WriteLine($"{card.StudentId}\n{card.Name}\n{card.Contact}\nAdult:{adultStatus}");
            }
        }

        public static Student FillStudentInfo()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter age: ");
            string ageInput = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(ageInput, out int age))
            {
                Console.WriteLine("Wrong type of input");
            }

            var student = new Student
            {
                FullName = name,
                Age = age,
                Email = email,
            };

            return student;
        }


        public static void AddStudent(AppDbContext context)
        {
            Student student = FillStudentInfo();
            context.Students.Add(student);
            context.SaveChanges();
            Console.WriteLine($"Added students ID: {student.Id}");
        }
    }
}
