using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShyFoxStudio.Badger;

namespace Badger.Example.DesktopGL
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;

        private int _count;
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        private System.Drawing.Color _systemMonoGameOrange;
        private System.Drawing.Color _systemWhite;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Badge.Instance.Initialize(Window);

            _systemMonoGameOrange = System.Drawing.Color.FromArgb(Color.MonoGameOrange.A, Color.MonoGameOrange.R, Color.MonoGameOrange.G, Color.MonoGameOrange.B);
            _systemWhite = System.Drawing.Color.FromArgb(Color.White.A, Color.White.R, Color.White.G, Color.White.B);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleKeyboardInput();
        }

        private void HandleKeyboardInput()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            if (_currentKeyboardState.IsKeyDown(Keys.Up) && _previousKeyboardState.IsKeyUp(Keys.Up))
            {
                _count++;
                Badge.Instance.SetBadge(_count, Color.MonoGameOrange, Color.White);
            }

            if (_currentKeyboardState.IsKeyDown(Keys.Down) && _previousKeyboardState.IsKeyUp(Keys.Down))
            {
                _count = Math.Max(0, _count - 1); // Fixed decrement logic
                Badge.Instance.SetBadge(_count, Color.MonoGameOrange, Color.White);
            }

            if (_currentKeyboardState.IsKeyDown(Keys.C) && _previousKeyboardState.IsKeyUp(Keys.C))
            {
                _count = 0;
                Badge.Instance.SetBadge(_count, Color.MonoGameOrange, Color.White);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
