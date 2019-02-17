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

        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;
        private GamePadState _currentGamePadState;
        private GamePadState _previousGamePadState;
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;

        private float _playerMoveSpeed;
        
        Texture2D mainBackground;
        Rectangle rectBackground;
        float scale = 1f;
        private ParallaxBackground _bgLayer1;
        private ParallaxBackground _bgLayer2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _player = new Player();
            _bgLayer1 = new ParallaxBackground();
            _bgLayer2 = new ParallaxBackground();
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
            
            _bgLayer1.Initialize(Content, "Graphics/bg1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            _bgLayer2.Initialize(Content, "Graphics/bg2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _previousGamePadState = _currentGamePadState;
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
            _currentGamePadState = GamePad.GetState(PlayerIndex.One);
            
            UpdatePlayer(gameTime);
            
            _bgLayer1.Update(gameTime);
            _bgLayer2.Update(gameTime);

            base.Update(gameTime);
        }
        
        private void UpdatePlayer(GameTime gameTime)
        {
            _player.Update(gameTime);
            
            _player.Position.X += _currentGamePadState.ThumbSticks.Left.X * _playerMoveSpeed;
            _player.Position.Y -= _currentGamePadState.ThumbSticks.Left.Y * _playerMoveSpeed;
            
            if (_currentKeyboardState.IsKeyDown(Keys.Left) || _currentGamePadState.DPad.Left == ButtonState.Pressed)
                _player.Position.X -= _playerMoveSpeed;

            if (_currentKeyboardState.IsKeyDown(Keys.Right) || _currentGamePadState.DPad.Right == ButtonState.Pressed)
                _player.Position.X += _playerMoveSpeed;

            if (_currentKeyboardState.IsKeyDown(Keys.Up) || _currentGamePadState.DPad.Up == ButtonState.Pressed)
                _player.Position.Y -= _playerMoveSpeed;

            if (_currentKeyboardState.IsKeyDown(Keys.Down) || _currentGamePadState.DPad.Down == ButtonState.Pressed)
                _player.Position.Y += _playerMoveSpeed;
            
            _player.Position.X = MathHelper.Clamp(_player.Position.X, 0, GraphicsDevice.Viewport.Width - _player.Width);
            _player.Position.Y = MathHelper.Clamp(_player.Position.Y, 0, GraphicsDevice.Viewport.Height - _player.Height);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            _bgLayer1.Draw(_spriteBatch);
            _bgLayer2.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}