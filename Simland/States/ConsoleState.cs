using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simland
{
    internal class ConsoleState : State
    {

        Screen console;
        Lexer lex;
        Parser parser;

        internal override void Draw(SpriteBatch spriteBatch)
        {
            console.Draw(spriteBatch);
        }

        internal override void OnEnter()
        {
            console = new Screen(manager.game.Content.Load<Texture2D>("CP437"),
                                 manager.game.Content.Load<Texture2D>("White"), 0, 0, 60, 25);

            lex = new Lexer();
            parser = new Parser();


        }

        internal override void OnExit()
        {

        }

        internal override void Update(GameTime gameTime, InputHandler input)
        {
            if (input.WasKeyPressed(Keys.Enter))
            {
                string source = console.ReadLine();

            }

            console.Update(gameTime, input);
        }
    }
}