using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterToot3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly Player _player;
        
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;
        MouseState currentMouseState;
        MouseState previousMouseState;

        private float _playerMoveSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _player = new Player();
        }

        protected override void Initialize()
        {
            _playerMoveSpeed = 8.0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            var playerAnimation = new Animation();
            var playerTexture = Content.Load<Texture2D>("Graphics\\dudes_400x100");

            playerAnimation.Initialize(playerTexture, Vector2.Zero, 100, 100, 4, 300, Color.White, 1f, true);

            var playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y
                + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            _player.Initialize(playerAnimation, playerPosition);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            
            UpdatePlayer(gameTime);

            base.Update(gameTime);
        }
        
        private void UpdatePlayer(GameTime gameTime)
        {
            _player.Update(gameTime);
            
            _player.Position.X += currentGamePadState.ThumbSticks.Left.X * _playerMoveSpeed;
            _player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * _playerMoveSpeed;
            
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
                _player.Position.X -= _playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
                _player.Position.X += _playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
                _player.Position.Y -= _playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
                _player.Position.Y += _playerMoveSpeed;
            
            _player.Position.X = MathHelper.Clamp(_player.Position.X, 0, GraphicsDevice.Viewport.Width - _player.Width);
            _player.Position.Y = MathHelper.Clamp(_player.Position.Y, 0, GraphicsDevice.Viewport.Height - _player.Height);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}