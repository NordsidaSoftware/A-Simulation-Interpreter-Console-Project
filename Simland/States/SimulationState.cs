using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simland
{
    internal class SimulationState:State
    {
        internal override void OnEnter()
        {
                
        }

        internal override void OnExit()
        {
            
        }

        internal override void Update(GameTime gameTime, InputHandler input)
        {
            if (input.WasKeyPressed(Keys.Tab))
            {
                manager.Push(new ConsoleState());
            }
        }
      
        internal override void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}