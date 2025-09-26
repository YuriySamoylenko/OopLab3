using StOopLab;

Console.ForegroundColor = ConsoleColor.Green;
Console.OutputEncoding = System.Text.Encoding.UTF8;

var dbSize = ReadUserNumber($"Enter database size (1 - {int.MaxValue}): ", 1, int.MaxValue);
var database = new List<WaterVendingMachine>(dbSize);

bool isRunning = true;
while (isRunning)
{
    Console.WriteLine();
    Console.WriteLine("Menu (2)");
    Console.WriteLine(@"
1 – Додати об’єкт
2 – Переглянути всі об’єкти
3 – Знайти об’єкт
4 – Продемонструвати поведінку
5 – Видалити об’єкт
0 – Вийти з програми");
    Console.WriteLine();
    var menuNumber = ReadUserNumber("Enter menu number: ", 0, 5);
    switch (menuNumber)
    {
        case 1:
            AddItem();
            break;
        case 2:
            DisplayAllItems();
            break;
        case 3:
            FindItem();
            break;
        case 4:
            DemoFull();
            break;
        case 5:
            Delete();
            break;
        case 0:
            Console.WriteLine("You selected Option 0. Exiting the program.");
            isRunning = false;
            break;
        default:
            Console.WriteLine("Invalid option. Please enter a number between 0 and 5.");
            break;
    }
}

void AddItem()
{
    Console.WriteLine("1 – Додати об’єкт");
    Console.WriteLine();
    bool find = true;
    while (find)
    {
        Console.WriteLine("Menu");
        Console.WriteLine(@"
1 – Create default
2 – Manually enter data
3 – Manually enter data partly
0 – Exit to main menu");
        Console.WriteLine();
        var findNumber = ReadUserNumber("Enter menu number: ", 0, 2);
        switch (findNumber)
        {
            case 1:
                var machineDefault = new WaterVendingMachine();
                machineDefault.Address = "1 floor";
                machineDefault.OperatorName = "Tom";
                machineDefault.Phone = "123456";
                machineDefault.CompanyName = "Aqua";
                machineDefault.WaterCapacityLiters = 1000;
                database.Add(machineDefault);
                Display(database);
                break;
            case 2:
                var machine = new WaterVendingMachine();
                // Address is an example of autoproperty
                machine.Address = ReadUserString("Enter machine address (string length 3 - 20): ", 3, 50);
                SetStringProperty("Enter operator name (string length 3 - 20): ", (string val) => machine.OperatorName = val);
                SetStringProperty("Enter operator phone number (digits length 6): ", (string val) => machine.Phone = val);
                SetStringProperty("Enter operating company name (string length 3 - 20): ", (string val) => machine.CompanyName = val);
                SetNumberProperty("Enter water capacity in liters (number 500 - 2000): ", (int val) => machine.WaterCapacityLiters = val);
                database.Add(machine);
                Display(database);
                break;
            case 3:
                var machine1 = new WaterVendingMachine();
                SetNumberProperty("Enter water capacity in liters (number 500 - 2000): ", (int val) => machine1.WaterCapacityLiters = val);
                database.Add(machine1);
                Display(database);
                break;
            case 0:
                Console.WriteLine("You selected Option 0. Exiting to main menu.");
                find = false;
                break;
            default:
                Console.WriteLine("Invalid option. Please enter a number between 0 and 2.");
                break;
        }
    }
}

void DisplayAllItems()
{
    Console.WriteLine("2 – Переглянути всі об’єкти");
    Display(database);
}

void FindItem()
{
    Console.WriteLine("3 – Знайти об’єкт");
    bool find = true;
    while (find)
    {
        Console.WriteLine("Menu");
        Console.WriteLine(@"
1 – Find by company name
2 – Find by operator name
0 – Exit to main menu");
        Console.WriteLine();
        var findNumber = ReadUserNumber("Enter menu number: ", 0, 2);
        switch (findNumber)
        {
            case 1:
                var companyName = ReadUserString("Enter operating company name (string length 3 - 20): ", 3, 20);
                var itemsByName = database.FindAll(i => i.CompanyName == companyName);
                Display(itemsByName);
                break;
            case 2:
                var operatorName = ReadUserString("Enter operator name (string length 3 - 20): ", 3, 20);
                var itemsByCompany = database.FindAll(i => i.OperatorName == operatorName);
                Display(itemsByCompany);
                break;
            case 0:
                Console.WriteLine("You selected Option 0. Exiting to main menu.");
                find = false;
                break;
            default:
                Console.WriteLine("Invalid option. Please enter a number between 0 and 2.");
                break;
        }
    }
}

void DemoFull()
{
    Console.WriteLine("4 – Продемонструвати поведінку");
    if (database.Count == 0)
    {
        Console.WriteLine("Create at least 1 machine");
        return;
    }

    var idx = ReadUserNumber("Enter machine index: ", 0, database.Count);

    var machine = database[idx];
    bool work = true;
    while (work)
    {
        Console.WriteLine("Menu");
        Console.WriteLine(@"
1 – Refill machine
2 – Put money
3 - Take water
4 - Withdraw cache
5 - Move
6 - How many water sold (calculated property example)
7 - Refill overload
0 – Exit to main menu");
        Console.WriteLine();
        var findNumber = ReadUserNumber("Enter menu number: ", 0, 5);
        switch (findNumber)
        {
            case 1:
                if (machine.WaterLeftLiters == machine.WaterCapacityLiters)
                {
                    Console.WriteLine("Machine is full");
                }
                else
                {
                    Console.WriteLine(machine.Refill());
                }
                break;
            case 2:
                if (machine.State == MachineState.RequiresMoneyWithraw)
                {
                    Console.WriteLine("Macine can't take cash");
                }
                else if (machine.State == MachineState.RequiresRefill)
                {
                    Console.WriteLine("No water");
                }
                else
                {
                    var money = ReadUserDecimal("Enter cash: ", 1, machine.GetMoneyCapacity());
                    Console.WriteLine(machine.PutMoney(money));
                }
                break;
            case 3:
                if (machine.State == MachineState.RequiresRefill)
                {
                    Console.WriteLine("No water");
                }
                else
                {
                    var water = ReadUserNumber("Enter water amount: ", 1, machine.WaterLeftLiters);
                    var waterGot = machine.TakeWater(water);
                    Console.WriteLine($"You got {waterGot} liters of water");
                }
                break;
            case 4:
                var cash = machine.WithdrawCash();
                Console.WriteLine($"We earned {cash} money");
                break;
            case 5:
                var address = ReadUserString("Enter new address: ", 3, 50);
                var moveResult = machine.Move(address);
                Console.WriteLine(moveResult);
                break;
            case 6:
                Console.WriteLine(machine.WaterSoldLiters);
                break;
            case 7:
                if (machine.WaterLeftLiters == machine.WaterCapacityLiters)
                {
                    Console.WriteLine("Machine is full");
                }
                else
                {
                    var water = ReadUserNumber("Enter water amount: ", 1, machine.WaterSoldLiters);
                    Console.WriteLine(machine.Refill(water));
                }

                break;
            case 0:
                Console.WriteLine("You selected Option 0. Exiting to main menu.");
                work = false;
                break;
            default:
                Console.WriteLine("Invalid option. Please enter a number between 0 and 2.");
                break;
        }
    }
}

void Delete()
{
    Console.WriteLine("5 – Видалити об’єкт");
    if (database.Count == 0)
    {
        Console.WriteLine("Create at least 1 machine");
        return;
    }

    var index = ReadUserNumber("Enter item index to delete: ", 0, database.Count);
    database.RemoveAt(index);
    Console.WriteLine($"Element {index} removed");
    Display(database);
}

void Display(List<WaterVendingMachine> items)
{
    if (items.Count == 0)
    {
        Console.WriteLine("Database is empty.");
        return;
    }
    var headers = new List<string> { "Index", "CompanyName", "OperatorName", "Phone", "Address" };
    var lines = new List<List<string>>();
    for (int i = 0; i < items.Count; i++)
    {
        var item = items[i];
        lines.Add(new List<string> { i.ToString(), item.CompanyName, item.OperatorName, item.Phone, item.Address });
    }

    var widths = headers.Select(h => h.Length).ToList();
    for (int i = 0; i < widths.Count; i++)
    {
        var itemColumnWidth = lines.Max(line => line[i].Length);
        if (itemColumnWidth > widths[i])
        {
            widths[i] = itemColumnWidth;
        }
    }

    var headerLine = string.Join(" | ", headers.Select((h, i) => h.PadRight(widths[i])));
    var separator = new string('-', headerLine.Length);

    Console.WriteLine(separator);
    Console.WriteLine(headerLine);
    Console.WriteLine(separator);

    foreach (var line in lines)
    {
        var row = string.Join(" | ", line.Select((i, idx) => i.PadRight(widths[idx])));
        Console.WriteLine(row);
    }

    Console.WriteLine(separator);
}

string ReadUserDigits(string message, int from, int to)
{
    while (true)
    {
        Console.Write(message);
        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine("Value is empty");
        }
        else if (value.Length < from || value.Length > to)
        {
            Console.WriteLine("Value is out of range");
        }
        else if (int.TryParse(value, out int number))
        {
            return value;
        }
        else
        {
            Console.WriteLine("Invalid format");
        }
    }
}

int ReadUserNumber(string message, int from, int to)
{
    while (true)
    {
        Console.Write(message);
        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine("Value is empty");
        }
        else if (int.TryParse(value, out int number))
        {
            if (number < from || number > to)
            {
                Console.WriteLine("Value is out of range");
            }
            else
            {
                return number;
            }
        }
        else
        {
            Console.WriteLine("Invalid format");
        }
    }
}

decimal ReadUserDecimal(string message, decimal from, decimal to)
{
    while (true)
    {
        Console.Write(message);
        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine("Value is empty");
        }
        else if (decimal.TryParse(value, out decimal number))
        {
            if (number < from || number > to)
            {
                Console.WriteLine("Value is out of range");
            }
            else
            {
                return number;
            }
        }
        else
        {
            Console.WriteLine("Invalid format");
        }
    }
}

string ReadUserString(string message, int from, int to)
{
    while (true)
    {
        Console.Write(message);
        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine("Value is empty");
        }
        else if (value.Length < from || value.Length > to)
        {
            Console.WriteLine("Value is out of range");
        }
        else
        {
            return value;
        }
    }
}

void SetStringProperty(string message, Action<string> seter)
{
    while (true)
    {
        try
        {
            Console.Write(message);
            var value = Console.ReadLine();
            seter(value);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

void SetNumberProperty(string message, Action<int> seter)
{
    while (true)
    {
        try
        {
            Console.Write(message);
            var value = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Value is empty");
            }
            else if (int.TryParse(value, out int number))
            {
                seter(number);
                return;
            }
            else
            {
                Console.WriteLine("Invalid format");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}