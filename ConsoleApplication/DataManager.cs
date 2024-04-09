namespace ConsoleApplication;

static class DataManager
{
    public static Dictionary<string, int> HashingIPWithCount(List<string> lines)
    {
        var Hash_IP_Count = new Dictionary<string, int>() { };

        foreach (string line in lines)
        {
            var splitedLine = line.Trim().Split(':', 2);
            var IP = splitedLine[0];

            _ = Hash_IP_Count.ContainsKey(IP)
            ? Hash_IP_Count[IP]++
            : Hash_IP_Count[IP] = 1;
        }

        return Hash_IP_Count;
    }

    public static List<string> FilteringByDate(string[] lines, DateTime time_start, DateTime time_end)
    {
        var filteredData = new List<string>() { };

        foreach (string line in lines)
        {
            var splitedLine = line.Trim().Split(":", 2);
            var Date = DateTime.Parse(splitedLine[1]);

            if (Date >= time_start && Date <= time_end)
                filteredData.Add(line);
        }

        return filteredData;
    }

    public static List<string> FilteringByAddress(Dictionary<string, int> hashedData, int address_start, int address_end)
    {
        var filteredData = new List<string>() { };

        if (address_start == 0)
            foreach (var datum_2 in hashedData)
                filteredData.Add(datum_2.Key + " - " + datum_2.Value);
        else
            foreach (var datum_1 in hashedData)
                if (datum_1.Value >= address_start && (address_end != 0 ? datum_1.Value <= address_end : true))
                    filteredData.Add(datum_1.Key + " - " + datum_1.Value);

        return filteredData;
    }
}