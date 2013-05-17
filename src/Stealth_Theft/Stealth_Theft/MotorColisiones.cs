using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;



namespace Stealth_Theft
{
    public class MotorColisiones
    {
        int TamanoGrid = 140;

        Rectangle Personaje;
        Rectangle FinNivel;

        List<Rectangle> Paredes;
        List<Rectangle> Trampas;
        List<Portales> Portal;

        int AnchoPared,AnchoTrampa,AnchoPortal,AnchoItem,AnchoLLave;
        int LargoPared,LargoTrampa,LargoPortal,LargoItem,LargoLLave;

        /// <summary>
        /// Modifica Las Longitudes
        /// </summary>
        /// <param name="Modificar">Valor Con el que Se Modificaran</param>
        public void modLong(int Modificar)
        {

            AnchoPared = Modificar;
            LargoPared = Modificar;
            AnchoTrampa = Modificar/2;
            LargoTrampa = Modificar/2;
            AnchoPortal = Modificar;
            LargoPortal = Modificar;
            AnchoItem = Modificar / 2;
            LargoItem = Modificar / 2;
            AnchoLLave = Modificar / 2;
            LargoLLave = Modificar / 2;

        }

        /// <summary>
        /// Constructor de la Clase
        /// </summary>
        /// <param name="path">Direccion del Grid</param>
        public MotorColisiones(string path)
        {
            modLong(TamanoGrid);

            Personaje = new Rectangle();
            Paredes = new List<Rectangle>();
            Trampas = new List<Rectangle>();
            Portal = new List<Portales>();


            string line = "";

            int i = 0;
            int j;


            Personaje.X = 0;
            Personaje.Y = 0;
            Personaje.Width = TamanoGrid / 2;
            Personaje.Height = TamanoGrid / 2;



            using (StreamReader tr = new StreamReader(path))
            {
                line = tr.ReadLine();

                while (line != null)
                {
                    j = 0;
                    foreach (char c in line)
                    {
                        if (c == '0')
                            Paredes.Add(new Rectangle(j * AnchoPared, i * LargoPared, AnchoPared, LargoPared));

                        if (c == '5')
                        {
                            Portal.Add(new Portales(new Vector2(j * AnchoPortal, i * LargoPortal)));
                            Paredes.Add(new Rectangle(j * AnchoPared, i * LargoPared, AnchoPared - (AnchoPared * 10/100), LargoPared));
                        }

                        if (c == '3')
                            FinNivel = new Rectangle(j * AnchoPared, i * LargoPared, AnchoPared, LargoPared);

                        if (c != ' ')
                        {
                            j++;
                        }


                    }
                    line = tr.ReadLine();
                    i++;
                }
            }


        }

        /// <summary>
        /// Carga la Direccion de los Portales
        /// </summary>
        /// <param name="Aux">Arreglo que contiene La Direccion de Los Portales</param>
        public void SalidaPortales(Vector2[] Aux)
        {
            int i;
            bool Encontrado;
            foreach (Portales P in Portal)
            {
                Encontrado = false;
                for (i = 0; i < Aux.Length && !Encontrado; i = i + 2)
                    if (P.GetEntrada() == Aux[i])
                    {
                        P.SetOut(Aux[i + 1]);
                        Encontrado = true;
                    }
            }
        }
        
        /// <summary>
        /// Verifica Si Se Toma Alguna Posion De Vida
        /// </summary>
        /// <param name="PosionVida">Objeto Posion De Vida</param>
        /// <returns>True En Caso de que la tome</returns>
        public bool CogerPosionVida(Rectangle PosionVida)
        {
            return Personaje.Intersects(PosionVida);
        }

        /// <summary>
        /// Verifica Si TIma Alguna LLave
        /// </summary>
        /// <param name="LLave">Objeto Posicion de Llave</param>
        /// <returns>True En Caso de Que haya tomado Alguna LLave</returns>
        public bool CogerLlave(Rectangle LLave)
        {
            return Personaje.Intersects(LLave);
        }

        /// <summary>
        /// Verifica Si el Jugador Colisiona Con Alguna Pared
        /// </summary>
        /// <param name="Posicion">Posicion Futura</param> 
        /// <returns>True En Caso de Colision, False en Caso Contrario</returns>
        public bool ChocaPared(Vector2 Posicion)
        {
            Personaje.X = (int)Posicion.X;
            Personaje.Y = (int)Posicion.Y;

            foreach (Rectangle P in Paredes)
            {
                if (Personaje.Intersects(P))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica Si Hay Choque Con ALgun Enemigo
        /// </summary>
        /// <param name="PosJug">Posicion Actual Del Jugador</param>
        /// <param name="PosEne">Posicion Actual Del Enemigo</param>
        /// <returns>True En Caso de Colision, False en Caso Contrario</returns>
        public bool ChocharEnemigo(Vector2 PosEne)
        {
            Rectangle AuxE = new Rectangle((int)PosEne.X,(int)PosEne.Y,TamanoGrid/2,TamanoGrid/2);
            return Personaje.Intersects(AuxE);
        }

        /// <summary>
        /// Verifica Si Hay Colision Con Alguna Trampa
        /// </summary>
        /// <returns>True En Caso de Colision, False en Caso Contrario</returns>
        public bool ChocaTramapa()
        {
            foreach (Rectangle T in Trampas)
            {
                if (Personaje.Intersects(T))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica Si El Jugador Entra En Algun Portal
        /// </summary>
        /// <returns>True En Caso de Colision, False en Caso Contrario</returns>
        public Vector2 ChocarPortal(bool haveKey)
        {
            foreach (Portales P in Portal)
                if (haveKey && !P.Activado() && Personaje.Intersects(new Rectangle((int)P.GetEntrada().X, (int)P.GetEntrada().Y, AnchoPortal, LargoPortal)))
                    foreach (Portales P2 in Portal)
                        if (P.GetSalida() == P2.GetEntrada())
                        {
                            P2.Activar();
                            return P.GetSalida() + new Vector2(TamanoGrid,0);
                        }
            return new Vector2(Personaje.X,Personaje.Y);
        }

        /// <summary>
        /// Verifica Si el Jugador LLega Al Final Del Nivel
        /// </summary>
        /// <returns>True En Caso de Colision, False en Caso Contrario</returns>
        public bool NivelTerminado()
        {
            return Personaje.Intersects(FinNivel);
        }

        /// <summary>
        /// Updatea La Posicion DEl Jugador
        /// </summary>
        /// <param name="Posicion">Posicion Actual Del Jugador</param>
        public void Update(Vector2 Posicion)
        {
            Personaje.X = (int)Posicion.X;
            Personaje.Y = (int)Posicion.Y;
        }

        /// <summary>
        /// Funcion Que Dibuja Los Bloques Colisionables en el Mapa Solo Para Verificar Congruencia Total Con El mapa
        /// </summary>
        /// <param name="batch">SpriteBatch En el que se dibuja El nivel</param>
        /// <param name="Content">ContentManager Donde se guardan LAs Texturas</param>
        public void DrawDebug(SpriteBatch batch, ContentManager Content)
        {
            foreach (Rectangle T in Paredes)
            {
                //batch.Draw(Content.Load<Texture2D>("Deb/P1"),new Rectangle (T.X -(int)Pos.X,T.Y -(int)Pos.Y,T.Width,T.Height),Color.White);
                batch.Draw(Content.Load<Texture2D>("Deb/P1"), new Rectangle(T.X, T.Y, T.Width, T.Height), Color.White);
            }
        }

    }
}