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
     var processToKill = new List<string> { "Eraser", "hola", "hubiC", "iTunesHelper", "GrandRobertHA", "supercopier", "PrintScreen", "RobertHA", "Amazon Music Helper" };
     IEnumerable<Process> allProc = GetAllProcesses();
      foreach (Process process in GetAllProcesses())
      {
        foreach (string processName in processToKill)
        {
          if (process.ProcessName == processName)
          {
            try
            {
              process.Kill();
            }
            catch (Exception)
            {
            }
          }
        }
      }

      //foreach (string proc in processToKill)
      //{
      //  if (IsProcessRunningByName(proc))
      //  {
      //    try
      //    {
      //      Process pc = GetProcessByName(proc);
      //      if (pc != new Process())
      //      {
      //        pc.Kill();
      //      }
      //    }
      //    catch (Exception)
      //    {
      //    }
      //  }
      //}

      Console.WriteLine("Press any key to exit:");
      Console.ReadKey();
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

    public static bool IsAdministrator()
    {
      WindowsIdentity identity = WindowsIdentity.GetCurrent();
      WindowsPrincipal principal = new WindowsPrincipal(identity);
      return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
  }
}