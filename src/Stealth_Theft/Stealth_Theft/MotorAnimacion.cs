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

    public class MotorAnimacion
    {

        List<Texture2D> Animar;

        int direccion;
        int Frameactual;
        int TotalFrames;
        int FramePerMove;

        float TiempoPerFrame;

        float TiempoTotal;

        public MotorAnimacion(ContentManager content, string path, int cantidad)
        {
            Animar = new List<Texture2D>();
            int i = 0;
            for (i = 0; i < cantidad; i++)
            {
                Animar.Add(content.Load<Texture2D>(path + i));
            }
            TotalFrames = cantidad;
            direccion = 0;
            FramePerMove = (TotalFrames - 1) / 4;
            TiempoTotal = 0f;
            TiempoPerFrame = cantidad / 150f;

        }

        /* Direcciones
         * 0 Normal
         * 1 Arriba
         * 2 Derecha
         * 3 Abajo
         * 4 Inzquierda
         */
        public void Movimiento(GameTime gameTime)
        {
            float Tiempo = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TiempoTotal += Tiempo;
            if (TiempoTotal > TiempoPerFrame)
            {
                if (Keyboard.GetState().GetPressedKeys().Count() != 0)
                {
                    if (FramePerMove * (direccion - 1) < Frameactual + 1  && Frameactual + 1 < (FramePerMove * direccion) + 1)
                        Frameactual++;
                    else
                        Frameactual = FramePerMove * (direccion-1) + 1;
                }
                else
                {
                    Frameactual = direccion * FramePerMove;
                }
                 
                TiempoTotal -= TiempoPerFrame;
            }
        }

        public void MovimientoE(GameTime gameTime)
        {
            float Tiempo = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TiempoTotal += Tiempo;
            if (TiempoTotal > TiempoPerFrame)
            {
                    if (FramePerMove * (direccion - 1) < Frameactual + 1 && Frameactual + 1 < (FramePerMove * direccion) + 1)
                        Frameactual++;
                    else
                        Frameactual = FramePerMove * (direccion - 1) + 1;

                TiempoTotal -= TiempoPerFrame;
            }
        }

        public bool UnaPasada(GameTime gameTime)
        {
            float Tiempo = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TiempoTotal += Tiempo;
            if (TiempoTotal > TiempoPerFrame)
            {
                Frameactual++;
                TiempoTotal -= TiempoPerFrame;
            }

            return Frameactual == TotalFrames;
        }

        public void AnimacionFija(GameTime gameTime)
        {
            float Tiempo = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TiempoTotal += Tiempo;
            if (TiempoTotal > TiempoPerFrame)
            {
                if (Frameactual + 1 < TotalFrames)
                    Frameactual++;
                else
                    Frameactual = 0;

                TiempoTotal -= TiempoPerFrame;
            }
        }

        public void CambiarTiempo(float Time)
        {
            TiempoPerFrame = Time;
        }

        public void CambiarDireccion()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                direccion = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                direccion = 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                direccion = 3;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                direccion = 4;
        }

        public void CambiarDireccion(int i)
        {
            direccion = i;
        }

        public void draw(SpriteBatch batch, Vector4 Posicion)
        {

            if (Posicion.Y < 0)
                Posicion.Y = 5f;
            if (Frameactual < 0)
                Frameactual = 0;

            batch.Draw(Animar.ElementAt(Frameactual), new Rectangle((int)Posicion.X, (int)Posicion.Y, (int)Posicion.Z, (int)Posicion.W), Color.White);

        }

        public void draw(SpriteBatch batch, Vector2 Posicion)
        {
            batch.Draw(Animar.ElementAt(Frameactual), Posicion, Color.White);
        }
    }
}
