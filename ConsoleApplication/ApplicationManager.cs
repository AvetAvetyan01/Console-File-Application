using System.Globalization;
namespace ConsoleApplication;

static class ApplicationManager
{
    public static void SetPath(string fileName, ref string value)
    {
        Console.Write("● Enter {0}: ", fileName);
        string path = Console.ReadLine()!;

        if (!ValidationManger.IsValidPath(path))
        {
            Console.WriteLine("⚠︎ Path is invalid or not exists!");
            SetPath(fileName, ref value);
        }
        else
            value = path;
    }

    public static void SetAddress(string addressName, ref int value)
    {
        Console.Write("● Enter {0}: ", addressName);
        bool emptyOrString = !int.TryParse(Console.ReadLine(), out int address);

        if (!ValidationManger.IsValidAddress(address))
        {
            Console.WriteLine("⚠︎ Invalid address!");
            SetAddress(addressName, ref value);
        }
        else if (emptyOrString)
            Console.WriteLine("ℹ  Your entered date is not assigned to the parameter because it is not valid (but you can continue)");
        else
            value = address;
    }

    public static void SetDate(string dateName, ref DateTime value)
    {
        Console.Write("● Enter {0}: ", dateName);
        var date = Console.ReadLine()!;

        if (!ValidationManger.IsValidDate(date, "dd.MM.yyyy"))
        {
            Console.WriteLine("⚠︎ Invalid date!");
            SetDate(dateName, ref value);
        }
        else
            value = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
    }

    public static void SetParamTransferMethod(ref string value)
    {
        Console.Write("● Enter method number (1 / 2): ");
        var methodNumber = Console.ReadLine()!;

        if (ValidationManger.IsValidMenthodNumber(methodNumber))
            value = methodNumber;
        else
        {
            Console.WriteLine("⚠︎ Invalid value!");
            SetParamTransferMethod(ref value);
        }
    }

    public static void SetDataFromConfig(
        string       config_file,
        ref string   file_log,
        ref string   file_output,
        ref int      address_start,
        ref int      address_end,
        ref DateTime time_start,
        ref DateTime time_end)
    {
        var configFileData = File.ReadAllLines(config_file);

        foreach (string line in configFileData)
        {
            var splitedLine = line.Trim().Split(":", 2);
            var parameterName = splitedLine[0];
            var parameterValue = splitedLine[1];

            switch (parameterName)
            {
                case "file-log":
                    file_log = parameterValue; break;
                case "file-output":
                    file_output = parameterValue; break;
                case "time-start":
                    time_start = DateTime.ParseExact(parameterValue, "dd.MM.yyyy", CultureInfo.InvariantCulture); break;
                case "time-end":
                    time_end = DateTime.ParseExact(parameterValue, "dd.MM.yyyy", CultureInfo.InvariantCulture); break;
                case "address-start":
                    address_start = int.Parse(parameterValue); break;
                case "address-end":
                    address_end = int.Parse(parameterValue); break;
            }
        }
    }
}

