using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simland
{
    internal abstract class State
    {
        public StateManager manager;
        internal abstract void OnEnter();
        internal abstract void OnExit();
        internal abstract void Update(GameTime gameTime, InputHandler input);
        internal abstract void Draw(SpriteBatch spriteBatch);
    }
}