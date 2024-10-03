using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace NBody
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        int _width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int _height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        Simulation Sim;
        const int PLANETS_COUNT = 400;
        const bool SPIN = true;
        const bool MASS_CENTER = true;
        const bool ENDLESS = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_graphics.PreferredBackBufferWidth = _width;
            //_graphics.PreferredBackBufferHeight = _height;
            //_graphics.GraphicsDevice.Adapter =
            _graphics.PreferredBackBufferWidth = _width;
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.IsFullScreen = false;
            _graphics.HardwareModeSwitch = true;
            Window.IsBorderless = true;
            Window.Position = Point.Zero;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Sim = new Simulation(_width, _height, PLANETS_COUNT, SPIN, MASS_CENTER, ENDLESS);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Sim.Step();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (Body body in Sim.bodies)
            {
                Primitives2D.DrawCircle(_spriteBatch, body.Position.X, body.Position.Y, body.Size, 10, Color.White);
                //Primitives2D.DrawLine(_spriteBatch, body.PrevPosition, body.Position, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
