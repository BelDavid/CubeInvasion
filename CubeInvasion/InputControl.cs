using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubeInvasion
{
    class InputControl
    {
        public InputControl()
        {
            keyPressed = new bool[256];
            keyDisposablePressed = new bool[256];
        }

        private bool[] keyPressed, keyDisposablePressed;

        public void setKeyState(Keys key, bool state)
        {
            if (state)
            {
                keyPressed[(int)key] = keyDisposablePressed[(int)key] = true;
            }
            else
            {
                keyPressed[(int)key] = false;
            }
        }

        public bool isKeyPressed(Keys key)
        {
            return keyPressed[(int)key];
        }
        public bool isKeyDisposablePressed(Keys key)
        {
            bool kDP = keyDisposablePressed[(int)key];
            keyDisposablePressed[(int)key] = false;
            return kDP;
        }
        public bool isKeyDisposablePressedOrJustPressed(Keys key)
        {
            bool kDP = keyDisposablePressed[(int)key] || keyPressed[(int)key];
            keyDisposablePressed[(int)key] = false;

            return kDP;
        }
        
        public void resetAllDisposableKeys()
        {
            for (int i = 0; i < keyDisposablePressed.Length; i++)
                keyDisposablePressed[i] = false;
        }
    }
}
