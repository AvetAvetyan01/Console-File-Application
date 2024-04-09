namespace ConsoleApplication;
internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            string   methodNumber  = string.Empty,
                     file_log      = string.Empty,
                     file_output   = string.Empty;
            int      address_start = 0,
                     address_end   = 0;
            DateTime time_start    = new(),
                     time_end      = new();

            Console.WriteLine("● Select the parameter transfer method (input number)");
            Console.WriteLine(" 1. By configuration file");
            Console.WriteLine(" 2. By input");
            ApplicationManager.SetParamTransferMethod(ref methodNumber);

            switch (methodNumber)
            {
                case "1":
                    string config_file = string.Empty;

                    while (true)
                    {
                        ApplicationManager.SetPath("config file", ref config_file);

                        if (ValidationManger.ValidConfigData(config_file)) break;
                    }

                    ApplicationManager.SetDataFromConfig(config_file, ref file_log, ref file_output, ref address_start, ref address_end, ref time_start, ref time_end);
                    break;

                case "2":
                    while (true)
                    {
                        ApplicationManager.SetPath("log file", ref file_log);
                        ApplicationManager.SetPath("output file", ref file_output);

                        if (ValidationManger.ValidPaths(file_log, file_output) && ValidationManger.ValidFileData(file_log)) break;
                    }
                    while (true)
                    {
                        ApplicationManager.SetAddress("address start", ref address_start);
                        ApplicationManager.SetAddress("address end", ref address_end);

                        if (ValidationManger.ValidAddresses(address_start, address_end)) break;
                    }
                    while (true)
                    {
                        ApplicationManager.SetDate("starting date", ref time_start);
                        ApplicationManager.SetDate("starting end", ref time_end);

                        if (ValidationManger.ValidDates(time_start, time_end)) break;
                    }
                    break;
            }

            var logFileData = File.ReadAllLines(file_log);
            var filteredByDateData = DataManager.FilteringByDate(logFileData, time_start, time_end);
            var hashedData = DataManager.HashingIPWithCount(filteredByDateData);
            var filteredByAddressData = DataManager.FilteringByAddress(hashedData, address_start, address_end);

            File.WriteAllLines(file_output, filteredByAddressData);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Data Sent Successfully\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
