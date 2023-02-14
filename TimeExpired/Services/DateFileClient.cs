using System.Reflection;

namespace TimeExpired.Services
{
  public class DateFileClient : IDateFileClient
  {
    string _dateFilePath = "";
    private string getAssemblyDirectory()
    {
      string codeBase = Assembly.GetExecutingAssembly().Location;
      return Path.GetDirectoryName(codeBase);
    }

    public DateFileClient()
    {
      _dateFilePath = Path.Join(getAssemblyDirectory(), "\\Data\\dates.csv");
      if (!File.Exists(_dateFilePath))
      {
        throw new Exception($"Could not find '{_dateFilePath}'");
      }
    }


    private async Task<string[]> getFileContent() => await File.ReadAllLinesAsync(_dateFilePath);
    private async Task writeLinesToFile(string[] lines) => await File.WriteAllLinesAsync(_dateFilePath, lines);
    private string[] getLineData(string line) => line.Split(',');
    private int getDateId(string line) => int.Parse(getLineData(line)[0]);
    private string getDateValue(string line) => getLineData(line)[1];
    private bool isDateLine(string line, int dateId) => getDateId(line) == dateId;


    public async Task<DateTime> AddDate(int dateId, DateTime date)
    {
      var updatedLine = $"{dateId},{date}";

      if (await GetDateInput(dateId) == "")
      {
        await File.AppendAllTextAsync(_dateFilePath, updatedLine + Environment.NewLine);
        return date;
      }

      string[] lines = await getFileContent();

      if (lines.Length == 0)
      {
        await writeLinesToFile(new string[] { updatedLine });
        return date;
      }

      for (int i = 0; i < lines.Length; i++)
      {
        if (isDateLine(lines[i], dateId))
        {
          lines[i] = updatedLine;
          break;
        }
      }

      await writeLinesToFile(lines);
      return date;
    }


    public async Task<string> GetDateInput(int dateId)
    {
      string[] lines = await getFileContent();

      foreach (string line in lines)
      {
        if (!string.IsNullOrWhiteSpace(line) && isDateLine(line, dateId))
        {
          return getDateValue(line);
        }
      }
      return "";
    }
  }
}