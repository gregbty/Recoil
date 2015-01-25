using System;
using System.Collections.Generic;
using System.Linq;
using Recoil.Shared;

namespace Recoil.Windows
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GameEngine())
                game.Run();
        }
    }
}
