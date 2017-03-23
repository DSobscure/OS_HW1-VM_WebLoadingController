using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VM_LoadingController.Controllers
{
    public class HomeController : Controller
    {
        List<string> ips = new List<string>
        {
            "192.168.122.9",
            "192.168.122.209"
        };
        static Process cpuMonitor1;
        static Process cpuMonitor2;

        public IActionResult Index(string ip = null, int loading = 0, int duration = 0)
        {
            if(ips.Contains(ip))
            {
                using(Process terminal = new Process())
                {
                    terminal.StartInfo.FileName = "bash";
                    terminal.StartInfo.RedirectStandardInput = true;
                    terminal.StartInfo.CreateNoWindow = true;
                    terminal.Start();
                    terminal.StandardInput.WriteLine($"ssh vm1@{ip}");
                    terminal.StandardInput.Flush();
                    terminal.StandardInput.WriteLine($"timeout {duration}s cpulimit -l {loading} ./busy");
                    terminal.StandardInput.Flush();
                    terminal.StandardInput.Dispose();
                }
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            if(cpuMonitor1 == null)
            {
                cpuMonitor1 = new Process();
                cpuMonitor1.StartInfo.FileName = "bash";
                cpuMonitor1.StartInfo.RedirectStandardInput = true;
                cpuMonitor1.StartInfo.RedirectStandardOutput = true;
                cpuMonitor1.StartInfo.CreateNoWindow = true;
                cpuMonitor1.Start();
                cpuMonitor1.StandardInput.WriteLine($"ssh vm1@192.168.122.9");
                cpuMonitor1.StandardInput.Flush();
                while (cpuMonitor1.StandardOutput.Peek() > -1)
                {
                    cpuMonitor1.StandardOutput.ReadLine();
                }
            }
            cpuMonitor1.StandardInput.WriteLine("top -d 0.5 -b -n2 | grep \"Cpu(s)\"|tail -n 1 | awk '{print $2 + $4}'");
            cpuMonitor1.StandardInput.Flush();
            ViewData["VM1Loading"] = cpuMonitor1.StandardOutput.ReadLine();
            while (cpuMonitor1.StandardOutput.Peek() > -1)
            {
                cpuMonitor1.StandardOutput.ReadLine();
            }
            
            if(cpuMonitor2 == null)
            {
                cpuMonitor2 = new Process();
                cpuMonitor2.StartInfo.FileName = "bash";
                cpuMonitor2.StartInfo.RedirectStandardInput = true;
                cpuMonitor2.StartInfo.RedirectStandardOutput = true;
                cpuMonitor2.StartInfo.CreateNoWindow = true;
                cpuMonitor2.Start();
                cpuMonitor2.StandardInput.WriteLine($"ssh vm1@192.168.122.209");
                cpuMonitor2.StandardInput.Flush();
                while (cpuMonitor2.StandardOutput.Peek() > -1)
                {
                    cpuMonitor2.StandardOutput.ReadLine();
                }
            }
            cpuMonitor2.StandardInput.WriteLine("top -d 0.5 -b -n2 | grep \"Cpu(s)\"|tail -n 1 | awk '{print $2 + $4}'");
            cpuMonitor2.StandardInput.Flush();
            ViewData["VM2Loading"] = cpuMonitor2.StandardOutput.ReadLine();
            while (cpuMonitor2.StandardOutput.Peek() > -1)
            {
                cpuMonitor2.StandardOutput.ReadLine();
            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
