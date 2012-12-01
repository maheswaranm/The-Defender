using System;

namespace TheDefender
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TheDefenderGame game = new TheDefenderGame())
            {
                game.Run();
            }
        }
    }
#endif
}

