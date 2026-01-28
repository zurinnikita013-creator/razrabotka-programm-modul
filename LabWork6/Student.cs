
internal class Student
{
    public string Name { get; set; }
    public string Specialty { get; set; }
    public int Course { get; set; }

    public Student() : this("Неизвестно", "Неизвестно", 1)
    {

    }


    public Student(string name, string specialty, int course)
    {
        Name = name;
        Specialty = specialty;
        Course = course;
    }

    public void Print()
    {
        Console.WriteLine($"Имя: {Name}");
        Console.WriteLine($"Специальность: {Specialty}");
        Console.WriteLine($"Курс: {Course}");
    }

    public bool IsSpecialty(string spec)
    {
        return Specialty == spec;
    }


    public bool IsFirstCourse => Course == 1;
}
