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
        
        private int _direction;    
        private int _speed;
        private Vector2 Dimension;

        private const int POS_DIR = -1;
        private const int NEG_DIR = 1;

        public int Direction            
        {
            get { return _direction; }
            set 
            {
                _direction = value; 
                if (Dimension.Equals(new Vector2(0, 1))) //if scrolling is set to vertical, correct the direction
                {
                    _direction *= -1;
                }    
            }
        }
        public int Speed
        {
            get { return _speed; }
            set { _speed = value;  }
        }

      
        public BackgroundScroller(List<string> Img)
        {
            if (!Img.First().Equals(Img.Last()))  //ensure that there is atleast two images in the array
            {
                Background = new List<Sprite>();
                this.Img = Img;

                for (int i = 0; i < Img.Count; i++)
                {
                    Background.Add(new Sprite());
                    Background[i].Scale = 2.0f;
                }
               this.SetHorizontal();
              //this.SetVertical();
               this.Direction = POS_DIR;
              // this.Direction = NEG_DIR;
                Speed = 400;
            }
            else
            {
                throw new ArgumentException("Expected atleast 2 background images.");
            }
        }

        public void SetHorizontal() { Dimension = new Vector2(1,0); }
        public void SetVertical() {  Dimension = new Vector2(0, 1); }

        public void LoadContent(ContentManager Content) //Load the background images and set them to the correct pos.
        {
            Background[0].LoadContent(Content, Img[0]);
            Background[0].position = new Vector2(0, 0);
            for (int i = 1; i < Background.Count; i++)
            {
                Background[i].LoadContent(Content,Img[i]);
                Background[i].position = (Background[i - 1].position +Background[i - 1].getSize()) * Dimension;
            }
        }
        public void Update(GameTime gameTime)
        {
            
           for (int i = 0; i < Background.Count; i++ )
            {
                bool ImageOutOfBound;

                if (Direction == POS_DIR)
                {
                    //Checks if the image have passed behind the screen
                    ImageOutOfBound = Vector2.Dot(Background[i].position, Dimension) < -Vector2.Dot((Background[i].getSize()), Dimension);
                } 
                else
                {
                    //checks if the image have passed before the screen
                    ImageOutOfBound = Vector2.Dot(Background[i].position, Dimension) > Vector2.Dot((Background[i].getSize()), Dimension);
                }

                if (ImageOutOfBound)  //Reposition the image out of bound
                {
                    if (Background[i].Equals(Background.First())){

                        Background.First().position = Background.Last().position - Direction*(Background.Last().getSize())*Dimension;
                    }
                    else
                    {
                        Background[i].position = Background[i - 1].position - Direction*(Background[i - 1].getSize()) * Dimension;     
                    } 
                }     
            }

            for (int i = 0; i < Background.Count; i++) //move all images in the same direction and speed.
            {
                Background[i].position += Direction*Dimension * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
