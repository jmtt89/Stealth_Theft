using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;

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
    public class Nivel : Microsoft.Xna.Framework.GameComponent
    {
        int TamanoGrid = 140;
        Jugador JJ;
        List<Enemigos> LEE;
        List<Item> Items;

        MotorColisiones Colisionador;

        int AnchoNiv = 800;
        int AltoNiv = 600;
        int CantidadColumnasNiv = 7;
        int CantidadLineasNiv = 7;


        Vector2 PosMap;

        SpriteFont Fuente;

        ContentManager Content;
        Game Juego;

        int CantidadEnemigos = 0;


        Song TemaFondo;
        SoundEffect Bomb;


        public bool Ganar;
        public bool Perder;


        protected Nivel(Game game, int NumeroEnemigos)
            : base(game)
        {
            Ganar = false;
            Perder = false;

            LEE = new List<Enemigos>();
            Items = new List<Item>();

            Juego = game;
            Content = game.Content;
            JJ = new Jugador(game);

            CantidadEnemigos = NumeroEnemigos;
        }

        protected void CargarFuente(string path)
        {
            Fuente = Content.Load<SpriteFont>(path);
        }

        protected void CargarTemaFondo(string path)
        {
            TemaFondo = Content.Load<Song>(path);
            MediaPlayer.Play(TemaFondo);
            MediaPlayer.IsRepeating = true;
        }

        protected void CargarSonidoExplosion(string path)
        {
            Bomb = Content.Load<SoundEffect>(path);
        }

        protected void CargarColisionador(string path)
        {
            Colisionador = new MotorColisiones(Content.RootDirectory + path);
        }

        protected void CargarEnemigos(string path)
        {
            int i;
            string filename;
            for (i = 0; i < CantidadEnemigos; i++)
            {
                filename = path + i + ".txt";
                LEE.Add(new Enemigos(Juego, FunAuxA(filename)));
            }
            
        }

        protected void Teleport(string filename)
        {
            Colisionador.SalidaPortales(FunAuxB(filename));
        }

        protected void CargarItems(string[] path,string filename)
        {
            Vector4[] Aux = FunAuxC(filename);
            int i;


            for(i=0;i<Aux.Length;i++)
                if(Aux[i].Z==0)
                    Items.Add(new PocionVida(Juego, path[(int)Aux[i].Z], (int)Aux[i].W, new Vector2(Aux[i].X, Aux[i].Y)));
                else
                    Items.Add(new Llave(Juego, path[(int)Aux[i].Z], (int)Aux[i].W, new Vector2(Aux[i].X, Aux[i].Y)));

        }

        private Vector2[] FunAuxA(string filename)
        {

            string path = Content.RootDirectory + filename;
            Vector2[] Temp;
            int i = 0;
            string line = "";
            String[] Vector;
            using (StreamReader tr = new StreamReader(path))
            {
                line = tr.ReadLine();
                Temp = new Vector2[int.Parse(line)];
                line = tr.ReadLine();

                while (line != null)
                {
                    Vector = line.Split(' ');
                    Temp[i] = new Vector2(float.Parse(Vector[0]) * TamanoGrid + TamanoGrid / 4, float.Parse(Vector[1]) * TamanoGrid + TamanoGrid / 4);
                    i++;
                    line = tr.ReadLine();
                }
            }
            return Temp;
        }

        private Vector2[] FunAuxB(string filename)
        {

            string path = Content.RootDirectory + filename;
            Vector2[] Temp;
            int i = 0;
            string line = "";
            String[] Vector;
            using (StreamReader tr = new StreamReader(path))
            {
                line = tr.ReadLine();
                Temp = new Vector2[int.Parse(line)];
                line = tr.ReadLine();

                while (line != null)
                {
                    Vector = line.Split(' ');
                    Temp[i] = new Vector2(float.Parse(Vector[0]) * TamanoGrid, float.Parse(Vector[1]) * TamanoGrid);
                    i++;
                    line = tr.ReadLine();
                }
            }
            return Temp;
        }

        /// <summary>
        /// FunAxuC Funciona Para Cargar Los Items y los Lee de la Siguiente manera
        /// PosicionX PosicionY Tipo CantidadSprites
        /// </summary>
        /// <param name="filename">Direccion del Documento</param>
        /// <returns>Arreglo de Vectores de 4 dimensiones en las que se guarda los datos anteriores</returns>
        private Vector4[] FunAuxC(string filename)
        {

            string path = Content.RootDirectory + filename;
            Vector4[] Temp;
            int i = 0;
            string line = "";
            String[] Vector;
            using (StreamReader tr = new StreamReader(path))
            {
                line = tr.ReadLine();
                Temp = new Vector4[int.Parse(line)];
                line = tr.ReadLine();

                while (line != null)
                {
                    Vector = line.Split(' ');
                    Temp[i] = new Vector4(float.Parse(Vector[0]) * TamanoGrid + TamanoGrid / 4, float.Parse(Vector[1]) * TamanoGrid + TamanoGrid / 4,float.Parse(Vector[2]),float.Parse(Vector[3]));
                    i++;
                    line = tr.ReadLine();
                }
            }
            return Temp;
        }

        public override void Update(GameTime gameTime)
        {
            

            JJ.Update(gameTime,Colisionador,PosMap);
            Colisionador.Update(JJ.GetPos());

            JJ.SetPos(Colisionador.ChocarPortal(JJ.GetKey()));

            List<Enemigos> AElim = new List<Enemigos>();
            List<Item> AElim2 = new List<Item>();

            foreach (Enemigos E in LEE)
            {
                E.Update(gameTime,JJ.GetPos(),PosMap);
                if(E.EstaVivo() && Colisionador.ChocharEnemigo(E.GetPos()))
                {
                    Bomb.Play();
                    E.Explotar(gameTime);
                    JJ.MenosVida();
                }

                if(E.ListoBorrar())
                    AElim.Add(E);
            }

            foreach (Enemigos E in AElim)
                LEE.Remove(E);

            foreach (Item I in Items)
            {
                I.Update(gameTime, PosMap);

                if ((I.GetTipo() == 1) && Colisionador.CogerPosionVida(I.GetRectangle()))
                {
                    JJ.MasVida();
                    AElim2.Add(I);
                }

                if ((I.GetTipo() == 0) && Colisionador.CogerLlave(I.GetRectangle()))
                {
                    JJ.OnKey();
                    AElim2.Add(I);
                }

            }

            foreach (Item I in AElim2)
                Items.Remove(I);

            if (!JJ.GetViva())
                Perder = true;

            if (Colisionador.NivelTerminado() && JJ.GetKey())
                Ganar = true;

            base.Update(gameTime);
        }

        private void CambioMapa()
        {
            int i;
            int j;
            bool Find = false;
            Vector2 Posicion = JJ.GetPos();

            for (i = 0; i < CantidadColumnasNiv && !Find; i++)
                if (Posicion.X > AnchoNiv * i && Posicion.X < AnchoNiv * (i + 1))
                    for (j = 0; j < CantidadLineasNiv && !Find; j++)
                        if (Posicion.Y > AltoNiv * j && Posicion.Y < AltoNiv * (j + 1))
                        {
                            PosMap = new Vector2(i, j);
                            Find = true;
                        }
        }

        private void DrawMundoActual(SpriteBatch batch)
        {
            CambioMapa();
            batch.Draw(Content.Load<Texture2D>("Mundo/Mundo1/" + PosMap.X + "." + PosMap.Y), new Vector2(0), Color.White);

        }

        public void Draw(SpriteBatch batch)
        {

                DrawMundoActual(batch);
                JJ.Draw(batch);

                foreach (Enemigos E in LEE)
                    E.Draw(batch,JJ.GetPosDib());

                foreach (Item I in Items)
                    I.Draw(batch);

                batch.DrawString(Fuente, "Vida: " + JJ.Vidas(), new Vector2(650, 20), Color.White);
        }

    }
}