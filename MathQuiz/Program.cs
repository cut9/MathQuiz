using System.Data;

Random random = new Random();
char[] signs = ['+', '-', '*', '/'];
int problemsCount = 10;
int userPoints = 0;
List<string> problems = new List<string>();

while (true)
{
    Console.WriteLine("Хотите сыграть в математическую игру? [1]. Да [2]. Нет");
    if (Choice(1, 2) == 2)
        return;
    ProblemsGenerator();
    UserProblemsSolver();
    problems.Clear();
}

void ProblemsGenerator()
{
    for (int i = 0; i < problemsCount; i++)
    {
        char mathSign = signs[random.Next(0, signs.Length)];
        double firstNumber = random.Next(2, 16);
        int secondNumber = random.Next(2, 16);
        if (mathSign == '/')
        {
            firstNumber = ProblemsSolver($"{firstNumber}" + '*' + $"{secondNumber}");
        }
        problems.Add($"{firstNumber}" + $"{mathSign}" + $"{secondNumber}");
    }
}

void UserProblemsSolver()
{
    int points = 0;
    int successCount = 0;
    bool success;
    for (int i = 0; i < problems.Count; i++)
    {
        Console.Write($"\nПример {i + 1}/{problemsCount}: {problems[i]}\nВаш ответ: ");
        int userAnswer = Choice();
        int answer = ProblemsSolver(problems[i]);
        if (userAnswer == answer)
        {
            Console.WriteLine("\nХорошая работа!");
            success = true;
            successCount++;
        }
        else
        {
            Console.WriteLine($"\nСтарайся лучше! Ответ был {answer}");
            success = false;
        }
        points = PointsCalculation(success, problems[i]);
        userPoints += points;
        Console.WriteLine($"Ты получаешь {points} очков! Текущие очки: {userPoints}");
    }
    Console.WriteLine($"Вы решили правильно {successCount} примеров из {problemsCount}\n");
}

int PointsCalculation(bool success, string problem)
{
    char mathSign = ' ';
    foreach (var item in signs)
        if (problem.Contains(item))
            mathSign = item;
    switch (mathSign)
    {
        case '+':
            if (success)
                return random.Next(1, 5);
            return random.Next(-7, 1);
        case '-':
            if (success)
                return random.Next(1, 5);
            return random.Next(-7, 1);
        case '*':
            if (success)
                return random.Next(2, 7);
            return random.Next(-6, 1);
        case '/':
            if (success)
                return random.Next(3, 9);
            return random.Next(-5, 1);
    }
    return 0;
}

int ProblemsSolver(string problem)
{
    return int.Parse(new DataTable().Compute(problem, "").ToString());
}

int Choice(int? min = null, int? max = null)
{
    while (true)
    {
        string userTextInput = Console.ReadLine();
        if (int.TryParse(userTextInput, out int userNumberInput))
        {
            if (min == null)
                return userNumberInput;
            if (userNumberInput <= max && userNumberInput >= min)
            {
                return userNumberInput;
            }
        }
        else
        {
            Console.WriteLine("Некорректное значение!");
        }
    }
}