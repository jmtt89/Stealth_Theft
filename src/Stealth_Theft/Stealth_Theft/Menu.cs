using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Stealth_Theft
{
    class Menu
    {
        //Soporta Un Menu con Animaciones

        Texture2D SplashImage;
        
        bool Animacion;
        protected MotorAnimacion SplashAnimado;
        Vector2 DatosImagen;

        protected Menu(ContentManager Content, string PathA, string PathB,int Cantidad)
        {
            SplashImage = Content.Load<Texture2D>(PathA);

            SplashAnimado = new MotorAnimacion(Content, PathB, Cantidad);
            Animacion = true;
            DatosImagen = new Vector2(SplashImage.Width / 4, SplashImage.Height / 4);
        }

        protected Menu(ContentManager Content, string PathB, int Cantidad)
        {
            SplashAnimado = new MotorAnimacion(Content, PathB, Cantidad);
            Animacion = true;
            DatosImagen = Vector2.Zero;
        }

        protected Menu(ContentManager Content, string Path)
        {
            SplashImage = Content.Load<Texture2D>(Path);
            Animacion = false;
        }

        public virtual void Update(GameTime Time)
        {
            if(Animacion)
                SplashAnimado.AnimacionFija(Time);
        }

        public virtual void Draw(SpriteBatch Batch)
        {
            if(SplashImage != null)
                Batch.Draw(SplashImage, Vector2.Zero, Color.White);
            if (Animacion)
                SplashAnimado.draw(Batch, DatosImagen);
        }

    }
}
