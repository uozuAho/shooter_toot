using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterToot3
{
    public class Enemy
    {
        public Vector2 Position;
        public int Width => _texture.Width;
        public int Height => _texture.Height;

        private Texture2D _texture;

        public Enemy()
        {
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, 0f);
        }
    }
}
