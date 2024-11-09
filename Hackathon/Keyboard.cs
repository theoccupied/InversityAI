using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hackathon
{
    internal class Keyboard
    {
        //Keys that are currently down
        private static List<Keys> keys = new List<Keys>();

        private static List<char> typedCharacters = new List<char>();

        private static List<Keys> specialKeys = new List<Keys>();

        /// <summary>
        /// Check if a key is down
        /// </summary>
        /// <param name="key">The key to checking for</param>
        /// <returns>True if the key is down</returns>
        public static bool getKey(Keys key)
        {
            return keys.Contains(key);
        }
        public static bool getSpecialKey(Keys key)
        {
            if (specialKeys.Contains(key))
            {
                specialKeys.Remove(key);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Call when a key has been pressed
        /// </summary>
        /// <param name="key">The key that has been pressed</param>
        public static void keyDown(KeyboardKeyEventArgs key)
        {
            if(!keys.Contains(key.Key))
            {
                keys.Add(key.Key);
            }
            if ((int)key.Key >= 65 && (int)key.Key <= 90)
            {
                char keyChar = (char)key.Key;
                typedCharacters.Add(key.Shift ? keyChar : ("" + keyChar).ToLower()[0]);
            }
            else if (key.Key == Keys.Space) typedCharacters.Add(' ');

            if(key.Key == Keys.Backspace || key.Key == Keys.Tab)
            {
                specialKeys.Add(key.Key);
            }
        }

        public static char getTypedKey()
        {
            if(typedCharacters.Count > 0)
            {
                char c = typedCharacters[0];
                typedCharacters.RemoveAt(0);
                return c;
            }
            return '\0';
        }
        /// <summary>
        /// Call when a key has been released
        /// </summary>
        /// <param name="key">The key that has been released</param>
        public static void keyUp(Keys key)
        {
            keys.Remove(key);
        }
    }
}
