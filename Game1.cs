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
        private readonly Enemy _enemy;

        private KeyboardState _currentKeyboardState;
        private GamePadState _currentGamePadState;

        private float _playerMoveSpeed;

        private readonly ScrollingBackground _bgLayer1;
        private readonly ScrollingBackground _bgLayer2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _player = new Player();
            _enemy = new Enemy();
            _bgLayer1 = new ScrollingBackground();
            _bgLayer2 = new ScrollingBackground();
        }

        protected override void Initialize()
        {
            _playerMoveSpeed = 8.0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            LoadAndInitPlayer();
            LoadAndInitEnemy();

            _bgLayer1.Initialize(Content, "Graphics/bg1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            _bgLayer2.Initialize(Content, "Graphics/bg2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);
        }

        private void LoadAndInitPlayer()
        {
            var playerAnimation = new Animation();
            var playerTexture = Content.Load<Texture2D>("Graphics\\dudes_400x100");

            playerAnimation.Initialize(playerTexture, Vector2.Zero, 100, 100, 4, 300, Color.White, 1f, true);

            var playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y
                + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            _player.Initialize(playerAnimation, playerPosition);
        }

        private void LoadAndInitEnemy()
        {
            var texture = Content.Load<Texture2D>("Graphics\\enemy");
            
            var position = new Vector2(
                GraphicsDevice.Viewport.TitleSafeArea.X
                + GraphicsDevice.Viewport.TitleSafeArea.Width / 2,
                GraphicsDevice.Viewport.TitleSafeArea.Y
                + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            
            _enemy.Initialize(texture, position);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
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
            _bgLayer1.Draw(_spriteBatch);
            _bgLayer2.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}