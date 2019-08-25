using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simland
{
    internal class StateManager
    {
        internal Game1 game;
        private Stack<State> stack;


        public bool StackHasState { get { return stack.Count > 0; } }
        public StateManager(Game1 game)
        {
            this.game = game;
            stack = new Stack<State>();
        }

        internal void Push(State state)
        {
            state.manager = this;
            state.OnEnter();
            stack.Push(state);
        }

        internal void Pop()
        {
            if (StackHasState) { stack.Peek().OnExit(); stack.Pop(); }
        }

        internal void Update(GameTime gameTime, InputHandler input)
        {
           if (StackHasState ) { stack.Peek().Update(gameTime, input); }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            if (StackHasState) { stack.Peek().Draw(spriteBatch); }
        }
    }
}