
using System.Diagnostics;

namespace Hackathon
{
    internal class Time
    {
        private static Stopwatch sw;
        public static float deltaTime;
        private static float lastTime;
        public static float time
        {
            get
            {
                return sw.ElapsedMilliseconds/1000f;
            }
        }
        public static void init()
        {

            sw = new Stopwatch();
            sw.Start();
        }
        public static void update()
        {
            deltaTime = (sw.ElapsedMilliseconds/1000f) - lastTime;
            lastTime = sw.ElapsedMilliseconds/1000f;
        }
    }
}
