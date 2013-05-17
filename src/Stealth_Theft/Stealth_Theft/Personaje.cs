using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Stealth_Theft
{

    public abstract class Personaje
    {
        protected int TamanoCelda = 140;
        protected int vida;
        protected int direccion;
        protected MotorAnimacion Animador;

        protected float VelMov;
        protected Vector2 posicionDib;
        protected Vector2 posicionLog;

        protected float AnchoPersonaje = 140 / 2;
        protected float LargoPersonaje = 200 / 2;

        protected int AnchoNiv = 800;
        protected int AltoNiv = 600;
        protected int CantidadColumnasNiv = 7;
        protected int CantidadLineasNiv = 7;


        public Vector2 GetPos()
        {
            return posicionLog;
        }

        public void MenosVida()
        {
            vida -= 50;
        }

        public void Update(GameTime Time)
        {
            Animador.Movimiento(Time);
            
        }

        public void Draw(SpriteBatch Batch)
        {

            Animador.draw(Batch, new Vector4(posicionDib, AnchoPersonaje, LargoPersonaje));

        }

    }
}
