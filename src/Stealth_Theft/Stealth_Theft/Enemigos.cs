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
    public class Enemigos : Personaje
    {
        public Vector2[] Ruta;
        int index;
        float TiempoMovimiento;
        float TiempoTotal;
        bool Valido;
        bool Vivo;
        bool Listo;

        private MotorAnimacion Explosion;


        ContentManager Content;

        public void Explotar(GameTime Time)
        {
            Explosion = new MotorAnimacion(Content, "Efectos/explo", 3);
            Explosion.CambiarTiempo(0.10f);
            Vivo = false;
            Listo = false;
        }

        public bool EstaVivo()
        {
            return Vivo;
        }

        public bool ListoBorrar()
        {
            return Listo;
        }

        public Enemigos(Game game, Vector2[] Patrulla)
        {
            Ruta=Patrulla;
            posicionLog = Ruta[0];
            Vivo = true;
            vida = 20;

            Content = game.Content;

            direccion=0;
            index = 0;
            Animador = new MotorAnimacion(game.Content, "Personaje/Enemigos/Nivel1/Imagenes/ENE", 17);
            VelMov = 4f;

            TiempoMovimiento = 0f;
            TiempoTotal = 0;
        }

        private void Movimiento( GameTime gameTime)
        {
            float Tiempo = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TiempoTotal += Tiempo;
            if (TiempoTotal > TiempoMovimiento)
            {

                if (Ruta[index].X > posicionLog.X)
                {
                    Animador.CambiarDireccion(2);
                    posicionLog.X += VelMov;
                }
                if (Ruta[index].X < posicionLog.X)
                {
                    Animador.CambiarDireccion(4);
                    posicionLog.X -= VelMov;
                }
                if (Ruta[index].Y > posicionLog.Y)
                {
                    Animador.CambiarDireccion(3);
                    posicionLog.Y += VelMov;
                }
                if (Ruta[index].Y < posicionLog.Y)
                {
                    Animador.CambiarDireccion(1);
                    posicionLog.Y -= VelMov;
                }
                if(Ruta[index].Y == posicionLog.Y)
                    if(Ruta[index].X == posicionLog.X)
                        index++;

                index = index % Ruta.Length;

                TiempoTotal -= TiempoMovimiento;
            }
        }

        private void Persecusion(Vector2 PosJug)
        {
            VelMov = 4f;
            if (EnsanduicharH(posicionLog, PosJug, Ruta[index]) || EnsanduicharV(posicionLog, PosJug, Ruta[index]))
                    VelMov = 8f;

        }

        private bool EnsanduicharV(Vector2 PosicionA, Vector2 PosicionB, Vector2 PosicionC)
        {
            int Delta = TamanoCelda / 2;

            int W = (int)Math.Abs((PosicionA.X - Delta) - (PosicionC.X + Delta));
            int H = (int)Math.Abs( PosicionA.Y - PosicionC.Y);
            int X; 
            int Y;

            if (PosicionA.Y < PosicionC.Y)
            {
                Y = (int)PosicionA.Y - Delta;
                X = (int)PosicionA.X - Delta;

            }
            else
            {
                Y = (int)PosicionC.Y - Delta;
                X = (int)PosicionC.X - Delta;
            }

            Rectangle Aux = new Rectangle(X, Y, W, H);

            return Aux.Contains((int)PosicionB.X, (int)PosicionB.Y);
        }

        private bool EnsanduicharH(Vector2 PosicionA, Vector2 PosicionB, Vector2 PosicionC)
        {
            int Delta = TamanoCelda / 2;

            int W;
            int H;
            int X;
            int Y;

            if (PosicionA.X < PosicionC.X)
            {
                Y = (int)PosicionA.Y - Delta;
                X = (int)PosicionA.X - Delta;
                W = (int)Math.Abs(PosicionA.X - PosicionC.X);
                H = (int)Math.Abs((PosicionA.Y - Delta) - (PosicionC.Y + Delta));
            }
            else
            {
                Y = (int)PosicionC.Y - Delta;
                X = (int)PosicionC.X - Delta;
                W = (int)Math.Abs(PosicionA.X - PosicionC.X);
                H = (int)Math.Abs((PosicionA.Y - Delta) - (PosicionC.Y + Delta));
            }

            Rectangle Aux = new Rectangle(X,Y,W,H);

            return Aux.Contains((int)PosicionB.X,(int)PosicionB.Y);
        }

        private void ActualizarPosisionDibujo(Vector2 PosMap)
        {
            Valido = false;
            float P1= AnchoNiv * PosMap.X;
            float P2 = AnchoNiv * (PosMap.X + 1);
            float P3 = AltoNiv * PosMap.Y;
            float P4 = AltoNiv * (PosMap.Y + 1);

            if (posicionLog.X > P1  && posicionLog.X < P2)
                if (posicionLog.Y > P3 && posicionLog.Y < P4)
                {
                    posicionDib.X = posicionLog.X - PosMap.X * AnchoNiv;
                    posicionDib.Y = posicionLog.Y - LargoPersonaje / 3 - PosMap.Y * AltoNiv;
                    Valido = true;
                }

            
        }

        private void DrawDeb(SpriteBatch Batch)
        {
            if(Valido)
                Batch.Draw(Content.Load<Texture2D>("Deb/P3"), new Rectangle((int)posicionDib.X, (int)posicionDib.Y, TamanoCelda / 2, TamanoCelda / 2), Color.White);
        }

        public void Update(GameTime gameTime, Vector2 PosJug, Vector2 PosMap)
        {
            if (!Vivo)
                Listo = Explosion.UnaPasada(gameTime);
            else
            {
                Persecusion(PosJug);
                Movimiento(gameTime);
                ActualizarPosisionDibujo(PosMap);
                Animador.MovimientoE(gameTime);
                //base.Update(gameTime);
            }
               
        }

        public void Draw(SpriteBatch Batch,Vector2 PosJugDib)
        {
            if (!Vivo)
                Explosion.draw(Batch, new Vector4(PosJugDib.X - TamanoCelda, PosJugDib.Y - TamanoCelda, TamanoCelda * 2, TamanoCelda * 2));
            else
                if (Valido)
                    //DrawDeb(Batch);
                    base.Draw(Batch);
        }

    }
}
