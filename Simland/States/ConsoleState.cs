using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Simland
{
    internal class ConsoleState : State
    {

        Screen console;

        Interpreter interpreter;

        internal override void Draw(SpriteBatch spriteBatch)
        {
            console.Draw(spriteBatch);
        }

        internal override void OnEnter()
        {
            console = new Screen(manager.game.Content.Load<Texture2D>("CP437"),
                                 manager.game.Content.Load<Texture2D>("White"), 0, 0, 60, 25);

            interpreter = new Interpreter();


        }

        internal override void OnExit()
        {

        }

        internal override void Update(GameTime gameTime, InputHandler input)
        {
            if (input.WasKeyPressed(Keys.Enter))
            {
                string source = console.ReadLine();
                
                interpreter.Interpret(source);
                if (interpreter.HasErrors) { console.WriteLine(interpreter.PresentErrors()); }
                else console.WriteLine(interpreter.result);
                
            }

            console.Update(gameTime, input);
        }
    }
}