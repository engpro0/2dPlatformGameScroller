using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Scroller
{
    class Sprite
    {
        public Vector2 position;
        public Texture2D texture;
        public Rectangle Size;
        public float Scale = 1.0f;
        

        public Sprite()
        {
            position = new Vector2(0,0);
        }

        public void LoadContent(ContentManager theContentManager, string img)
        {
            texture = theContentManager.Load<Texture2D>(img);
            Size = new Rectangle(0,0,(int)(texture.Width*Scale), (int)(texture.Height*Scale));
        }
        public void Update()
        {
            position.X += 1;
            position.Y += 1;
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White,
                0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0); 
        }

        public bool InsideScreen(GraphicsDeviceManager screen)
        {
            int height = screen.PreferredBackBufferHeight;
            int width = screen.PreferredBackBufferWidth;

            int x = (int) position.X + texture.Width;
            int y = (int) position.Y + texture.Height;

            if (position.X < 0 || position.Y < 0){
                return false;
            }
            if (x > width || y > height ){
                return false;
            }
            return true;

        }
        public Vector2 getSize()
        {
            return new Vector2(Size.Width, Size.Height);
        }

    }
}
