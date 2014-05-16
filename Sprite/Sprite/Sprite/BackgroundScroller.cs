using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Scroller
{
    class BackgroundScroller
    {
        List<Sprite> Background;
        List<string> Img;
        
        private int Direction;
        private Vector2 Dimension;

        public BackgroundScroller()
        {
            
            Background = new List<Sprite>();
            Img = new List<string>();
            Img.Add("Background01");
            Img.Add("Background02");
            Img.Add("Background03");
            Img.Add("Background04");
            Img.Add("Background05");
            //this.SetVertical();
            this.SetHorizontal();
            this.Direction = -1;

            for (int i = 0; i < Img.Count; i++ )
            {
                Background.Add(new Sprite());
                Background[i].Scale = 2.0f;
            }
       
        }
      
        public void SetHorizontal()
        {
            Dimension = new Vector2(1,0);
            
        }
        public void SetVertical()
        {
            Dimension = new Vector2(0, 1);
           
        }
        public void LoadContent(ContentManager Content)
        {
            Background[0].LoadContent(Content, Img[0]);
            Background[0].position = new Vector2(0, 0);
            for (int i = 1; i < Background.Count; i++)
            {
                Background[i].LoadContent(Content,Img[i]);
               // Background[i].position = new Vector2(Background[i - 1].position.X + Background[i - 1].Size.Width, 0);
                //Background[i].position = new Vector2(0, Background[i - 1].position.Y + Background[i - 1].Size.Height);
               Background[i].position = (Background[i - 1].position + Background[i - 1].getSize())*Dimension;
 
            }
      
        }
 
        public void Update(GameTime gameTime)
        {
            
           for (int i = 0; i < Background.Count; i++ )
            {
                
                //if (Background[i].position.X < -Background[i].Size.Width ) 
                //if (Background[i].position.Y < -Background[i].Size.Height) 
                if (Vector2.Dot(Background[i].position, Dimension) < -Vector2.Dot((Background[i].getSize()),Dimension)) 
                {

                   
                    if (Background[i].Equals(Background.First())){
                        
                        //Background.First().position.X = Background.Last().position.X + Background.Last().Size.Width;
                        //Background.First().position.Y = Background.Last().position.Y + Background.Last().Size.Height;
                        Background.First().position = Background.Last().position + (Background.Last().getSize())*Dimension;

                    }
                 
                    else
                    {
                        //Background[i].position.X = Background[i-1].position.X + Background[i-1].Size.Width;
                        //Background[i].position.Y = Background[i - 1].position.Y + Background[i - 1].Size.Height;
                        Background[i].position = Background[i - 1].position + (Background[i - 1].getSize()) * Dimension;
                        
                    }
                
                }
                 
            }
            
            
            Vector2 aSpeed = new Vector2(300, 300);
            for (int i = 0; i < Background.Count; i++)
            {
                Background[i].position += Direction*Dimension * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
       
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //
            foreach(Sprite s in Background){
                s.Draw(spriteBatch);
            }
         
            //
        }

    }
}
