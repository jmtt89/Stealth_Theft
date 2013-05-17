using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Stealth_Theft
{
    class MenuInicio : Menu
    {

        SpriteFont Fuente;

        public bool Inicio;
        public bool Salir;


        // Datos Relacionados con El Boton de Inicio
        Texture2D BotonInicio;
        Vector2 PosBotonInicio;
        Rectangle ZoomInicio;
        bool ZoomI;
        bool ClickInicio;

        // Datos Relacionados con El Boton de Salir
        Texture2D BotonSalir;
        Vector2 PosBotonSalir;
        Rectangle ZoomSalir;
        bool ZoomS;
        bool ClickSalir;

        // Datos Relacionados con El Titulo Del Juego
        Texture2D TituloJuego;
        Vector2 PosTituloJuego;
        
        // Inicializa Todo
        public MenuInicio(Game game)
            : base(game.Content, "Menu/Portada_Juego")
        {
            Fuente = game.Content.Load<SpriteFont>("Fuentes/Letras");
            Inicio = false;
            Salir = false;

            game.IsMouseVisible = true;


            BotonSalir = game.Content.Load<Texture2D>("Menu/boton salir");
            PosBotonSalir = new Vector2(100,500);
            ZoomSalir = new Rectangle((int)PosBotonSalir.X - BotonSalir.Width / 2, (int)PosBotonSalir.Y - BotonSalir.Height / 2, BotonSalir.Width + BotonSalir.Width / 2, BotonSalir.Height + BotonSalir.Height / 2);
            ZoomS = false;
            ClickSalir = false;

            BotonInicio = game.Content.Load<Texture2D>("Menu/boton Inicio");
            PosBotonInicio = new Vector2(550,500);
            ZoomInicio = new Rectangle((int)PosBotonInicio.X - BotonInicio.Width / 2, (int)PosBotonInicio.Y - BotonInicio.Height / 2, BotonInicio.Width + BotonInicio.Width / 2, BotonInicio.Height + BotonInicio.Height / 2);
            ZoomI = false;
            ClickInicio = false;

            TituloJuego = game.Content.Load<Texture2D>("Menu/titulo Juego");
            PosTituloJuego = new Vector2(100,25);
        }


       
        //Funcion Para Posicionar y Reescalar Los Botones en caso de Seleccion
        private void Mause()
        {
            MouseState CurrentMouse = Mouse.GetState();

            Rectangle Aux = new Rectangle();

            Aux.X = (int)PosBotonSalir.X;
            Aux.Y = (int)PosBotonSalir.Y;
            Aux.Width = BotonSalir.Width;
            Aux.Height = BotonSalir.Height;
            if (Aux.Contains(CurrentMouse.X, CurrentMouse.Y))
            {
                ZoomS = true;
                if (CurrentMouse.LeftButton == ButtonState.Pressed)
                    ClickSalir = true;
            }
            else
                ZoomS = false;

            Aux.X = (int)PosBotonInicio.X;
            Aux.Y = (int)PosBotonInicio.Y;
            Aux.Width = BotonInicio.Width;
            Aux.Height = BotonInicio.Height;
            if (Aux.Contains(CurrentMouse.X, CurrentMouse.Y))
            {
                ZoomI = true;
                if (CurrentMouse.LeftButton == ButtonState.Pressed)
                    ClickInicio = true;
            }
            else
                ZoomI = false;

        }

        public override void Update(GameTime Time)
        {
            if (!Inicio)
            {
                Mause();

                if (ClickInicio)
                    Inicio = true;
                else if (ClickSalir)
                    Salir = true;


                base.Update(Time);
            }
        }

        public override void Draw(SpriteBatch Batch)
        {
            if (!Inicio)
            {
                base.Draw(Batch);

                if (!ZoomS)
                    Batch.Draw(BotonSalir, PosBotonSalir, Color.White);
                else
                    Batch.Draw(BotonSalir, ZoomSalir, Color.White);

                if (!ZoomI)
                    Batch.Draw(BotonInicio, PosBotonInicio, Color.White);
                else
                    Batch.Draw(BotonInicio, ZoomInicio, Color.White);

                //if (!ZoomC)
                //    Batch.Draw(BotonCreditos, PosBotonCreditos, Color.White);
                //else
                //    Batch.Draw(BotonCreditos, ZoomCreditos, Color.White);

                Batch.DrawString(Fuente, " Elaborado por. Alfredo Gallardo y Jesus Torres", new Vector2(20, 570), Color.White);

                Batch.Draw(TituloJuego, PosTituloJuego, Color.White);

            }
        }

    }
}