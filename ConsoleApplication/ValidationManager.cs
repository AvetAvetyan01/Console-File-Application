using System.Globalization;
using System.Text.RegularExpressions;

namespace ConsoleApplication;

static class ValidationManger
{
    public static bool IsValidPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return false;
        if (!File.Exists(path)) return false;

        return true;
    }

    public static bool IsValidAddress(int address)
    {
        if (address < 0 || address > 9) return false;

        return true;
    }

    public static bool IsValidDate(string date, string format)
    {
        if (string.IsNullOrEmpty(date)) return false;

        try
        { DateTime datePrototype = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture); }
        catch (FormatException)
        { return false; }

        return true;
    }

    public static bool IsValidIPv4(string IP)
    {
        string pattern = @"^([0-9]|[1-9][0-9]|1[[0-9][0-9]|2[0-4][0-9]|25[0-5])\.([0-9]|[1-9][0-9]|1[[0-9][0-9]|2[0-4][0-9]|25[0-5])\.([0-9]|[1-9][0-9]|1[[0-9][0-9]|2[0-4][0-9]|25[0-5])\.([0-9]|[1-9][0-9]|1[[0-9][0-9]|2[0-4][0-9]|25[0-5])$";

        bool checking = Regex.IsMatch(IP, pattern);

        return checking;
    }

    public static bool IsValidMenthodNumber/* :( */(string methodNumber)
    {
        if (string.IsNullOrEmpty(methodNumber)) return false;
        if (methodNumber != "1" && methodNumber != "2") return false;

        return true;
    }

    public static bool ValidPaths(string file_log, string file_output)
    {
        if (file_log == file_output)
        {
            Console.WriteLine("⚠︎ First path should not be equals to second path!");
            return false;
        }

        return true;
    }

    public static bool ValidAddresses(int address_start, int address_end)
    {
        if (address_start == 0 && address_end != 0)
        {
            Console.WriteLine("⚠︎ You can't give an second address without giving a first address!");
            return false;
        }

        if (address_start > address_end && address_end != 0)
        {
            Console.WriteLine("⚠︎ First address should not be big from second address!");
            return false;
        }

        return true;
    }

    public static bool ValidDates(DateTime time_start, DateTime time_end)
    {
        if (time_start >= time_end)
        {
            Console.WriteLine("⚠︎ First Date value should not be big or equals to second date value!");
            return false;
        }

        return true;
    }

    public static bool ValidFileData(string path)
    {
        var fileDataLines = File.ReadAllLines(path);

        try
        {
            bool validIpv4Data = fileDataLines.All(line => IsValidIPv4(line.Trim().Split(":", 2)[0]));
            bool validDateData = fileDataLines.All(line => IsValidDate(line.Trim().Split(":", 2)[1], "yyyy.MM.dd HH:mm:ss"));

            if (!(validIpv4Data && validDateData))
            {
                Console.WriteLine("⚠︎ Not all data is valid ⚠︎");
                Console.WriteLine("The data in the file should start with 'IPv4:yyyy.MM.dd' format, (e.g. '171.255.35.66:2024.04.08') and start with a new line.");
                Console.WriteLine("IPv4 chacking result: " + validIpv4Data);
                Console.WriteLine("Date Type chacking result: " + validDateData);
                return false;
            }

            return true;
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("⚠︎  The file you provided contains incorrect data  ⚠︎");
            Console.WriteLine("ℹ  The data in the file should start with 'IPv4:yyyy.MM.dd HH:mm:ss' format, (e.g. '171.255.35.66:2024.04.08 11:32:58') and start with a new line.");
            Console.WriteLine("ℹ  Empty lines are not allowed.");
            return false;
        }
    }

    public static bool ValidConfigData(string path)
    {
        var pathParameters = new Dictionary<string, string>(){
            ["file-log"]    = string.Empty,
            ["file-output"] = string.Empty
        };

        var addressParameters = new Dictionary<string, int>(){
            ["address-start"] = 0,
            ["address-start"] = 0
        };

        var dateParameters = new Dictionary<string, DateTime>(){
            ["time-start"] = new(),
            ["time-end"]   = new()
        };

        var configFileData = File.ReadAllLines(path);

        if (configFileData.Length != 6)
        {
            Console.WriteLine("⚠︎  The configuration file must contain 6 parameters");
            return false;
        }

        foreach (string line in configFileData)
        {
            var splitedLine = line.Trim().Split(":", 2);
            var parameterName = splitedLine[0];
            var parameterValue = splitedLine[1];

            switch (parameterName)
            {
                case "file-log":
                case "file-output":
                    if (IsValidPath(parameterValue))
                        pathParameters[parameterName] = parameterValue;
                    else
                    {
                        Console.WriteLine("⚠︎  File has invalid values");
                        return false;
                    }
                    break;
                
                case "address-start":
                case "address-end":
                    if (int.TryParse(parameterValue, out int convertedNumber))
                    {
                        if (IsValidAddress(convertedNumber))
                            addressParameters[parameterName] = convertedNumber;
                        else
                        {
                            Console.WriteLine("⚠︎ Address can be in `1-9 range (or default 0)");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("⚠︎  File has invalid values");
                        return false;
                    }
                    break;
                
                case "time-start":
                case "time-end":
                    if (IsValidDate(parameterValue, "dd.MM.yyyy"))
                        dateParameters[parameterName] = DateTime.ParseExact(parameterValue, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    else
                    {
                        Console.WriteLine("⚠︎  File has invalid values");
                        return false;
                    }
                    break;
                
                default:
                    Console.WriteLine("⚠︎  There is a parameter with an invalid name in the file.");
                    return false;
            }
        }

        return ValidDates(dateParameters["time-start"], dateParameters["time-end"]) &&
               ValidPaths(pathParameters["file-log"], pathParameters["file-output"]) &&
               ValidAddresses(addressParameters["address-start"], addressParameters["address-end"]);
    }
}