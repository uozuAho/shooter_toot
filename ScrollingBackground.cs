using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterToot3
{
    public class ScrollingBackground
    {
        private Texture2D _texture;
        private int[] _texturePositions;
        private int _speed;
        
        private int _screenHeight;
        private int _screenWidth;

        public void Initialize(ContentManager content, string texturePath, int screenWidth, int screenHeight, int speed)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;

            _texture = content.Load<Texture2D>(texturePath);
            _speed = speed;
            _texturePositions = new int[2];

            for (var i = 0; i < _texturePositions.Length; i++)
            {
                _texturePositions[i] = i * screenWidth;
            }
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < _texturePositions.Length; i++)
            {
                _texturePositions[i] += _speed;

                if (_speed <= 0)
                {
                    if (_texturePositions[i] <= -_screenWidth)
                    {
                        _texturePositions[i] = _screenWidth * (_texturePositions.Length - 1);
                    }
                }
                else
                {
                    if (_texturePositions[i] >= _screenWidth * (_texturePositions.Length - 1))
                    {
                        _texturePositions[i] = -_screenWidth;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var pos in _texturePositions)
            {
                var rectBg = new Rectangle(pos, 0, _screenWidth, _screenHeight);
                spriteBatch.Draw(_texture, rectBg, Color.White);
            }
        }
    }
}
