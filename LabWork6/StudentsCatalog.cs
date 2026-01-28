internal class StudentsCatalog
{
    private Student[] students;

    public StudentsCatalog()
    {
        students = new Student[5]
        {
            new Student("Иванов", "Информатика", 1),
            new Student("Петров", "Математика", 3),
            new Student("Сидорова", "Информатика", 2),
            new Student("Кузнецов", "Физика", 1),
            new Student("Смирнова", "Информатика", 4)
        };
    }

    public void Find()
    {
        foreach (var s in students)
            if (s.Course == 1) s.Print();
    }

    public void Find(string specialty)
    {
        foreach (var s in students)
            if (s.Specialty == specialty) s.Print();
    }

    public Student this[int index]
    {
        get
        {
            if (index >= 0 && index < students.Length) return students[index];
            return null;
        }
    }

    public object this[int index, int propertyIndex]
    {
        get
        {
            if (index < 0 || index >= students.Length)
                return -1;

            if (propertyIndex < 1 || propertyIndex > 3)
                return -1;

            if (propertyIndex == 1) return students[index].Name;
            if (propertyIndex == 2) return students[index].Specialty;
            if (propertyIndex == 3) return students[index].Course;

            return -1;
        }
    }
}