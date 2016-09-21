using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;

namespace KillProcess
{
  internal class Program
  {
    private static void Main()
    {
      if (!IsAdministrator())
      {
        Console.WriteLine("You must be administrator");
        return;
      }

      // Get all processes and kill some of them
      var processToKill = new List<string>
      {
        "Eraser", "hola", "hubiC", "iTunesHelper", "GrandRobertHA", "supercopier", "PrintScreen", "RobertHA", "Amazon Music Helper", "NvBackend",
        "ServiceProvider", "AppleMobileDeviceService", "armsvc", "GfExperienceService", "hola_svc", "hola_updater",
        "iPodService", "JetBrains.ETW.Collector.Host", "NvNetworkService", "SearchFilterHost", "SearchIndexer",
        "SearchProtocolHost", "spoolsv", "TeamViewer_Service", "vmware-converter", "vmware-converter-a", "vmware-usbarbitrator"
        
      };

      var processKilled = new List<string>();
      foreach (Process process in GetAllProcesses())
      {
        foreach (string processName in processToKill)
        {
          if (process.ProcessName == processName)
          {
            try
            {
              process.Kill();
              processKilled.Add(processName);
            }
            catch (Exception)
            {
            }
          }
        }
      }

      Console.WriteLine("{0} process{1} {2} been killed", processKilled.Count, GetPlural(processKilled.Count), Plural(processKilled.Count, "has"));
      foreach (string process in processKilled)
      {
        Console.WriteLine(process);
      }

      Console.WriteLine();
      Console.WriteLine("Press any key to exit:");
      Console.ReadKey();
    }

    private static string GetPlural(int number)
    {
      return number > 1 ? "es" : string.Empty;
    }

    private static IEnumerable<Process> GetAllProcesses()
    {
      Process[] processlist = Process.GetProcesses();
      return processlist.ToList();
    }

    private static bool IsProcessRunningById(Process process)
    {
      try { Process.GetProcessById(process.Id); }
      catch (InvalidOperationException) { return false; }
      catch (ArgumentException) { return false; }
      return true;
    }

    private static bool IsProcessRunningByName(string processName)
    {
      try { Process.GetProcessesByName(processName); }
      catch (InvalidOperationException) { return false; }
      catch (ArgumentException) { return false; }
      return true;
    }

    private static Process GetProcessByName(string processName)
    {
      Process result = new Process();
      foreach (Process process in GetAllProcesses())
      {
        if (process.ProcessName == processName)
        {
          result = process;
          break;
        }
      }

      return result;
    }

    private static string Plural(int number, string irregularNoun = "")
    {
      switch (irregularNoun)
      {
        case "":
          return number > 1 ? "s" : string.Empty;
        case "al":
          return number > 1 ? "aux" : "al";
        case "au":
          return number > 1 ? "aux" : "au";
        case "eau":
          return number > 1 ? "eaux" : "eau";
        case "eu":
          return number > 1 ? "eux" : "eu";
        case "landau":
          return number > 1 ? "landaus" : "landau";
        case "sarrau":
          return number > 1 ? "sarraus" : "sarrau";
        case "bleu":
          return number > 1 ? "bleus" : "bleu";
        case "émeu":
          return number > 1 ? "émeus" : "émeu";
        case "lieu":
          return number > 1 ? "lieux" : "lieu";
        case "pneu":
          return number > 1 ? "pneus" : "pneu";
        case "aval":
          return number > 1 ? "avals" : "aval";
        case "bal":
          return number > 1 ? "bals" : "bal";
        case "chacal":
          return number > 1 ? "chacals" : "chacal";
        case "carnaval":
          return number > 1 ? "carnavals" : "carnaval";
        case "festival":
          return number > 1 ? "festivals" : "festival";
        case "récital":
          return number > 1 ? "récitals" : "récital";
        case "régal":
          return number > 1 ? "régals" : "régal";
        case "cal":
          return number > 1 ? "cals" : "cal";
        case "serval":
          return number > 1 ? "servals" : "serval";
        case "choral":
          return number > 1 ? "chorals" : "choral";
        case "narval":
          return number > 1 ? "narvals" : "narval";
        case "bail":
          return number > 1 ? "baux" : "bail";
        case "corail":
          return number > 1 ? "coraux" : "corail";
        case "émail":
          return number > 1 ? "émaux" : "émail";
        case "soupirail":
          return number > 1 ? "soupiraux" : "soupirail";
        case "travail":
          return number > 1 ? "travaux" : "travail";
        case "vantail":
          return number > 1 ? "vantaux" : "vantail";
        case "vitrail":
          return number > 1 ? "vitraux" : "vitrail";
        case "bijou":
          return number > 1 ? "bijoux" : "bijou";
        case "caillou":
          return number > 1 ? "cailloux" : "caillou";
        case "chou":
          return number > 1 ? "choux" : "chou";
        case "genou":
          return number > 1 ? "genoux" : "genou";
        case "hibou":
          return number > 1 ? "hiboux" : "hibou";
        case "joujou":
          return number > 1 ? "joujoux" : "joujou";
        case "pou":
          return number > 1 ? "poux" : "pou";
        case "est":
          return number > 1 ? "sont" : "est";

        // English
        case " is":
          return number > 1 ? "s are" : " is"; // with a space before
        case "is":
          return number > 1 ? "are" : "is"; // without a space before
        case "has":
          return number > 1 ? "have" : "has";
        case "are":
          return number > 1 ? "are" : "is"; // without a space before
        case "have":
          return number > 1 ? "have" : "has";
        case "The":
          return "The"; // CAPITAL, useful because of French plural
        case "the":
          return "the"; // lower case, useful because of French plural
        default:
          return number > 1 ? "s" : string.Empty;
      }
    }

    public static bool IsAdministrator()
    {
      WindowsIdentity identity = WindowsIdentity.GetCurrent();
      WindowsPrincipal principal = new WindowsPrincipal(identity);
      return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
  }
}
