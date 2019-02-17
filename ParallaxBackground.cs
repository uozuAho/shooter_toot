using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterToot3
{
    public class ParallaxBackground
    {
        Texture2D texture;
        Vector2[] positions;
        int speed;
        
        private int _bgHeight;
        private int _bgWidth;

        public void Initialize(ContentManager content, string texturePath, int screenWidth, int screenHeight, int speed)
        {
            _bgHeight = screenHeight;
            _bgWidth = screenWidth;

            texture = content.Load<Texture2D>(texturePath);
            this.speed = speed;
            positions = new Vector2[screenWidth / texture.Width + 1];

            for (var i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(i * texture.Width, 0);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < positions.Length; i++)
            {
                positions[i].X += speed;

                if (speed <= 0)
                {
                    if (positions[i].X <= -texture.Width)
                    {
                        positions[i].X = texture.Width * (positions.Length - 1);
                    }
                }
                else
                {
                    if (positions[i].X >= texture.Width * (positions.Length - 1))
                    {
                        positions[i].X = -texture.Width;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < positions.Length; i++)
            {
                var rectBg = new Rectangle((int)positions[i].X, (int)positions[i].Y, _bgWidth, _bgHeight);
                spriteBatch.Draw(texture, rectBg, Color.White);
            }
        }
    }
}
