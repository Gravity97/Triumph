using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Triumph_0._1
{
    internal class ProcKiller
    {
        readonly public DateTime time;
        readonly public string App;
        readonly public Timer timer;

        public event Action<ProcKiller> ProcessKilled;

        public ProcKiller(string App, DateTime time)
        {
            this.App = App;
            this.time = time;

            DateTime now = DateTime.Now;
            TimeSpan timeUntilAim = this.time - now;

            // 创建定时器，到达时间后执行任务
            if(timeUntilAim.TotalMilliseconds > 0.0)
            {
                this.timer = new Timer(ShutdownProcess, null, timeUntilAim, Timeout.InfiniteTimeSpan);
                Console.WriteLine("Start killing proc: " + App + " after " + timeUntilAim.ToString());
            }
        }

        private void ShutdownProcess(object state)
        {
            Process[] processes = Process.GetProcesses();
            
            foreach (Process process in processes)
            {
                if (process.ProcessName.ToLower().Contains(App.ToLower()))
                {
                    process.CloseMainWindow();
                    process.WaitForExit(5000);
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
            }
            Console.WriteLine("finished killing.");

            ProcessKilled?.Invoke(this);
        }
    }
}
