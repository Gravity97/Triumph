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
        readonly public int remindTimeSpan;
        readonly public Timer killer;
        readonly public Timer message;

        public event Action<ProcKiller> ProcessKilled;
        public event Action<int> Reminded;

        public ProcKiller(string App, DateTime time, int remindTimeSpan = 1)
        {
            this.App = App;
            this.time = time;
            this.remindTimeSpan = remindTimeSpan;

            DateTime now = DateTime.Now;
            TimeSpan timeUntilAim = this.time - now;

            // 创建定时器，到达时间后执行任务
            if (timeUntilAim.TotalMilliseconds > 0.0)
            {
                this.killer = new Timer(ShutdownProcess, null, timeUntilAim, Timeout.InfiniteTimeSpan);
                Console.WriteLine("Start killing proc: " + App + " after " + timeUntilAim.ToString());

                TimeSpan timeUntilRemind = timeUntilAim.Subtract(TimeSpan.FromMinutes(remindTimeSpan));
                if (timeUntilRemind.TotalMilliseconds > 0.0)
                    this.message = new Timer(Reminding, null, timeUntilAim.Subtract(TimeSpan.FromMinutes(remindTimeSpan)), Timeout.InfiniteTimeSpan);
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

        private void Reminding(object state)
        {
            Reminded?.Invoke(remindTimeSpan);
        }
    }
}
