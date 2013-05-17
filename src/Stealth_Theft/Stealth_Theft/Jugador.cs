using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// No se cuales son los que se usan
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Stealth_Theft
{

    public class Jugador : Personaje
    {
        bool LLave;

        public Vector2 GetPosDib()
        {
            return posicionDib;
        }

        public void OnKey()
        {
            LLave = true;
        }

        public bool GetViva()
        {
            return vida>0;
        }

        public int Vidas()
        {
            return vida;
        }

        public bool GetKey()
        {
            return LLave;
        }

        public Jugador(Game game)
        {

            LLave = false;

            vida = 100;
            direccion = 0;
            VelMov = 4f;

            Animador = new MotorAnimacion(game.Content, "Personaje/Jugador/JJ", 17);


            int i, j;
            i = 0;

            string path = game.Content.RootDirectory + "/Nivel/Nivel1.txt";
            using (StreamReader tr = new StreamReader(path))
            {
                string line = tr.ReadLine();

                while (line != null)
                {
                    j = 0;
                    foreach (char c in line)
                    {

                        if (c == '2')
                        {
                            posicionLog = new Vector2(j * TamanoCelda, i * TamanoCelda);
                            posicionDib = new Vector2(j * TamanoCelda, i * TamanoCelda);
                            break;
                        }
                        if (c != ' ')
                            j++;
                    }

                    line = tr.ReadLine();
                    i++;
                }
            }
        }

        public void MasVida()
        {
            vida += 25;
        }

        private void Movimiento(MotorColisiones Colisionador)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                if (!Colisionador.ChocaPared(new Vector2(posicionLog.X, posicionLog.Y - VelMov)))
                    posicionLog.Y -= VelMov;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                if (!Colisionador.ChocaPared(new Vector2(posicionLog.X + VelMov, posicionLog.Y)))
                    posicionLog.X += VelMov;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                if (!Colisionador.ChocaPared(new Vector2(posicionLog.X, posicionLog.Y + VelMov)))
                    posicionLog.Y += VelMov;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                if (!Colisionador.ChocaPared(new Vector2(posicionLog.X - VelMov, posicionLog.Y)))
                    posicionLog.X -= VelMov;

            if (posicionLog.Y < 0)
                posicionLog.Y = 0;

        }

        public void SetPos(Vector2 Pos)
        {
            posicionLog = Pos;
        }

        private void ActualizarPosisionDibujo(Vector2 Posicion)
        {
            posicionDib.X = posicionLog.X - Posicion.X * AnchoNiv;
            posicionDib.Y = posicionLog.Y - LargoPersonaje / 3 - Posicion.Y * AltoNiv;

        }

        public void DrawDeb(ContentManager Content, SpriteBatch Batch)
        {
            Batch.Draw(Content.Load<Texture2D>("Deb/P2"), new Rectangle((int)posicionDib.X, (int)posicionDib.Y, TamanoCelda / 2, TamanoCelda / 2), Color.White);
        }

        public void Update(GameTime Time, MotorColisiones Colisionador, Vector2 PosMap)
        {
            Movimiento(Colisionador);
            ActualizarPosisionDibujo(PosMap);
            Animador.CambiarDireccion();
            base.Update(Time);
        }
    }
}
