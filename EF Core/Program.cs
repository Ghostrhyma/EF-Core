using Microsoft.EntityFrameworkCore;


using AppDbContext context = new AppDbContext();

context.Database.EnsureCreated();


static void ShowAllStudents(AppDbContext context)
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

static void PrintStudent(Student student)
{
    Console.WriteLine($"{student.Id} {student.FullName} Age:{student.Age} {student.Email}");
}


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<||||>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
static void ShowAdultStudents(AppDbContext context)
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


static void SearchStudentByName(AppDbContext context)
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

static void ShowStudentsSortedByAgeAndName(AppDbContext context)
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

static void ShowStudentCards(AppDbContext context)
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

static Student FillStudentInfo()
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


static void AddStudent(AppDbContext context)
{
    Student student = FillStudentInfo();
    context.Students.Add(student);
    context.SaveChanges();
    Console.WriteLine($"Added students ID: {student.Id}");
}

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<||||>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;

    public List<Enrollment> Enrollments { get; set; } = new();
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationHours { get; set; }

    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
}


public class Teacher
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<Course> Courses { get; set; } = new();
}


public class Enrollment
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null;

    public int CourseId{ get; set; }
    public Course Course { get; set; } = null;

    public DateTime EnrolledAt { get; set; }
    public int Grade { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Course> Cources => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=school.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(s => s.Age)
                .IsRequired();

        });

    }

}


//fkjbngkjdfgnbdfjhbvg;dfkjbvg;dfjbvgfjbgd;fjbvgd;