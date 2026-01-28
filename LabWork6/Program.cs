Console.WriteLine("1/5 = " + Calculator.RationalFunctionCalculator(5, 1));
Console.WriteLine("1/0 = " + Calculator.RationalFunctionCalculator(0, 1));
Console.WriteLine("1/2.5 = " + Calculator.FunctionCalculator(2.5));
Console.WriteLine("1/0.0 = " + Calculator.FunctionCalculator(0.0));
Console.WriteLine("1/5 = " + Calculator.RationalFunctionCalculator(5, 2));

new Student("Иванов", "Информатика", 1);
new Student("Петров", "Математика", 3);
new Student("Сидорова", "Информатика", 2);
new Student("Кузнецов", "Физика", 1);
new Student("Смирнова", "Информатика", 4);

Student s1 = new Student("Иванов", "Информатика", 1);
Student s2 = new Student();

s1.Print();
Console.WriteLine($"1 курс? {s1.IsFirstCourse}");

StudentsCatalog Student = new StudentsCatalog();

Student.Find();                    
Student.Find("Информатика");       

Console.WriteLine($"\nStudent[0]: {Student[0]?.Name}");
Console.WriteLine($"Student[0,1]: {Student[0, 1]}");
Console.WriteLine($"Student[0,2]: {Student[0, 2]}");
Console.WriteLine($"Student[0,3]: {Student[0, 3]}");
