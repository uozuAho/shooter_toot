using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterToot3
{
    public class ScrollingBackground
    {
        private Texture2D _texture;
        private Vector2[] _texturePositions;
        private int _speed;
        
        private int _bgHeight;
        private int _bgWidth;

        public void Initialize(ContentManager content, string texturePath, int screenWidth, int screenHeight, int speed)
        {
            _bgHeight = screenHeight;
            _bgWidth = screenWidth;

            _texture = content.Load<Texture2D>(texturePath);
            _speed = speed;
            _texturePositions = new Vector2[screenWidth / _texture.Width + 2];

            for (var i = 0; i < _texturePositions.Length; i++)
            {
                _texturePositions[i] = new Vector2(i * _texture.Width, 0);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < _texturePositions.Length; i++)
            {
                _texturePositions[i].X += _speed;

                if (_speed <= 0)
                {
                    if (_texturePositions[i].X <= -_texture.Width)
                    {
                        _texturePositions[i].X = _texture.Width * (_texturePositions.Length - 1);
                    }
                }
                else
                {
                    if (_texturePositions[i].X >= _texture.Width * (_texturePositions.Length - 1))
                    {
                        _texturePositions[i].X = -_texture.Width;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _texturePositions.Length; i++)
            {
                var rectBg = new Rectangle((int)_texturePositions[i].X, (int)_texturePositions[i].Y, _bgWidth, _bgHeight);
                spriteBatch.Draw(_texture, rectBg, Color.White);
            }
        }
    }
}
