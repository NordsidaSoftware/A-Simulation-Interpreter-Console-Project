using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace Simland
{
    internal class Screen
    {
        private Texture2D charSet;
        private Texture2D background;
        private Rectangle rectangle;
        private int[,] grid;
        private Point Cursor;
        private bool CapsLock;
        private Point charSize;
        private Color ForegroundColor;
        private Color BackgroundColor;
        private Color CursorColor;
        private float CursorBlinkTime;
        private bool CursorBlink;
        private float Time_Since_Last_Cursor_Blink;

        private readonly List<Keys> recognizedKeys = new List<Keys>()
        {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M,
            Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z,
            Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0,
            Keys.Space, Keys.CapsLock, Keys.Back, //Keys.Enter
        };

        public Rectangle Rectangle { get { return rectangle; } }
        public Color FColor { get { return ForegroundColor; } set { ForegroundColor = value; } }
        public Color BColor { get { return BackgroundColor; } set { BackgroundColor = value; } }
        public Color CColor { get { return CursorColor; } set { CursorColor = value; } }
        public float CTime { get { return CursorBlinkTime; } set { CursorBlinkTime = value; } }

        public Screen(Texture2D charSet, Texture2D background, int X, int Y, int width, int height)
        {
            this.charSet = charSet;
            this.background = background;
            grid = new int[width, height];
            Cursor = new Point(0, 0);
            this.CapsLock = !Keyboard.GetState().CapsLock;

            rectangle = new Rectangle(X, Y, width, height);
            charSize = new Point(10, 10);
            ForegroundColor = Color.White;
            BackgroundColor = Color.Black;
            CursorColor = Color.Green;
            CursorBlinkTime = 0.5f;
        }


        internal void Draw(SpriteBatch spriteBatch)
        {
            //   =====   DRAW BACKGROUND RECTANGLE  ======
            spriteBatch.Draw(background, new Rectangle(rectangle.X * charSize.X,
                                                       rectangle.Y * charSize.Y,
                                                       rectangle.Width * charSize.X,
                                                       rectangle.Height * charSize.Y),
                             BackgroundColor);

            //    =====    DRAW CHAR GRID  / FOREGROUND   ======
            for (int x = 0; x < rectangle.Width; x++)
            {
                for (int y = 0; y < rectangle.Height; y++)
                {
                    if (grid[x, y] == 0) { continue; }
                    int x_char = (grid[x, y] % 16) * charSize.X;
                    int y_char = (grid[x, y] / 16) * charSize.Y;

                    spriteBatch.Draw(charSet,
                        new Rectangle(x * charSize.X, y * charSize.Y, charSize.X, charSize.Y),
                        new Rectangle(x_char, y_char, charSize.X, charSize.Y),
                        ForegroundColor);
                }
            }

            if (CursorBlink)
            {
                spriteBatch.Draw(background, new Rectangle(Cursor.X * charSize.X, Cursor.Y * charSize.Y, charSize.X,
                    charSize.Y), CursorColor);
            }
        }


        internal void Update(GameTime gameTime, InputHandler input)
        {
            
            Time_Since_Last_Cursor_Blink += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Time_Since_Last_Cursor_Blink > CursorBlinkTime)
            {
                CursorBlink = !CursorBlink;
                Time_Since_Last_Cursor_Blink -= CursorBlinkTime;
            }

            foreach (Keys key in recognizedKeys)
            {
                if (input.WasKeyPressed(key)) { HandleInput(key); }
            }


    
        }

        private void HandleInput(Keys key)
        {
            // if (key == Keys.Enter) { LineShift(); return; }
            if (key == Keys.Space) { PutChar(0); return; }
            if (key == Keys.CapsLock) { if (CapsLock) { CapsLock = false; } else { CapsLock = true; } return; }
            if (key == Keys.Back) { BackSpace(); return; }

            PutChar((int)key);
        }

        private void BackSpace()
        {
            if (Cursor == Point.Zero) { ClearScreen(); return; }
            if (Cursor.X <= 1)
            {
                PutCharExp(0, Cursor.Y, 0);
                Cursor.Y--;
                Cursor.X = rectangle.Width - 1;
                if (Cursor.Y < 0) { ClearScreen(); }
                return;
            }

            Cursor.X--;
            PutCharExp(Cursor.X, Cursor.Y, 0);

        }



        private void LineShift()
        {
            Cursor.Y++;
            Cursor.X = 0;
            if (Cursor.Y >= rectangle.Width) { ClearScreen(); }
        }

        private void PutChar(int key)
        {
            if (CapsLock) { key += 32; }
            grid[Cursor.X, Cursor.Y] = key;
            AdvanceCursor();
        }

        private void PutCharExp(int x, int y, int key)
        {
            grid[x, y] = key;
        }

        private void AdvanceCursor()
        {
            Cursor.X++;
            if (Cursor.X >= rectangle.Width - 1)
            {
                Cursor.X = 0;
                Cursor.Y++;

                if (Cursor.Y >= rectangle.Height)
                {

                    ClearScreen();
                }
            }
        }

        public void ClearScreen()
        {
            Cursor = new Point(0, 0);

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = 0;
                }
            }
        }


        public void WriteLine(string txt)
        {
            foreach (char c in txt) { PutChar(c); }
            LineShift();
        }

        public string ReadLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < Cursor.X; index++)
            {
                sb.Append((char)grid[index, Cursor.Y]);
            }

            return sb.ToString();
        }
    }
}