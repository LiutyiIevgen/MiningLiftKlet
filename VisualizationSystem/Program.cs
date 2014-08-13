using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ML.DataRepository.DataAccess;
using VisualizationSystem.View.Forms;

namespace VisualizationSystem
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            Database.SetInitializer(new MineDbInitializer());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormStart());

        }

    }
}
