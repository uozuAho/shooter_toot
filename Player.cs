using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ShooterToot3
{
    public class Player
    {
        private Texture2D _playerTexture;
        private Vector2 _position;
        private bool _active;
        private int _health;

        public int Width => _playerTexture.Width;

        public int Height => _playerTexture.Height;

        public Player()
        {
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            _playerTexture = texture;
            _position = position;
            _active = true;
            _health = 100;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, 0f);
        }
    }
}
