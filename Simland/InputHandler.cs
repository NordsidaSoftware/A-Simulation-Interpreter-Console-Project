using System;
using Microsoft.Xna.Framework.Input;

namespace Simland
{
    internal class InputHandler
    {
        private KeyboardState keyboardState;
        private KeyboardState old_keyboardState;
        private MouseState mouseState;
        private MouseState old_mouseState;


        public InputHandler()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }
        internal void Update()
        {
            old_keyboardState = keyboardState;
            old_mouseState = mouseState;
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        internal bool WasKeyPressed(Keys key)
        {
            return old_keyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
            
        }

        internal Keys[] getPressedKeys()
        {
            if (old_keyboardState.GetPressedKeys() != keyboardState.GetPressedKeys())
                return keyboardState.GetPressedKeys();
            else return null;
        }
    }
}