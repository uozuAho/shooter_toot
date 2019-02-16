using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ShooterToot3
{
    public class Player
    {
        public Vector2 Position;
        public int Width => _playerAnimation.FrameWidth;
        public int Height => _playerAnimation.FrameHeight;

        private bool _active;
        private int _health;
        private Animation _playerAnimation;

        public Player()
        {
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            _playerAnimation = animation;
            Position = position;
            _active = true;
            _health = 100;
        }

        public void Update(GameTime gameTime)
        {
            _playerAnimation.Position = Position;
            _playerAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _playerAnimation.Draw(spriteBatch);
        }
    }
}
