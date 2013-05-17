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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        MenuInicio MenuInicial;
        MenuPausa MenuPausa;
        MenuGanar MenuGanar;
        MenuPerder MenuPerder;
        MenuCreditos MenuCreditos;

        public bool ActivarMenuInicial;
        public bool ActivarMenuPausa;
        public bool ActivarMenuGanar;
        public bool ActivarMenuPerder;
        public bool ActivarMenuCreditos;


        Nivel[] Nv;
        int Index;

        int CantidadNiveles = 1;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Index = 0;
            Nv = new Nivel[CantidadNiveles];

            MenuInicial = new MenuInicio(this);
            MenuPausa = new MenuPausa(this);
            MenuGanar = new MenuGanar(this);
            MenuPerder = new MenuPerder(this);
            MenuCreditos = new MenuCreditos(this);

            ActivarMenuInicial = true;
            ActivarMenuPausa = false;
            ActivarMenuGanar = false;
            ActivarMenuPerder = false;
            ActivarMenuCreditos = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Nv[Index] = new Nivel1(this);

        }

        protected override void Update(GameTime gameTime)
        {

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();

                if (MenuPerder.Restart)
                {
                    LoadContent();
                    MenuPerder.Restart = false;
                }

                if (MenuGanar.Finalizar)
                {
                    Index++;
                    if (Index == CantidadNiveles)
                        this.Exit();
                    else
                        LoadContent();
                }

                MenuPausa.Update(gameTime);

                MenuInicial.Update(gameTime);

           if (Index < CantidadNiveles)
           {

                if (MenuInicial.Inicio && !Nv[Index].Perder && !Nv[Index].Ganar && !MenuPausa.ActivaPausa)
                    Nv[Index].Update(gameTime);

                if (MenuInicial.Salir)
                    this.Exit();

                if (Nv[Index].Perder)
                    MenuPerder.Update(gameTime);
                else if (Nv[Index].Ganar)
                    MenuGanar.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Fuchsia);

            spriteBatch.Begin();

            MenuPausa.Draw(spriteBatch);
            MenuInicial.Draw(spriteBatch);


            if (Nv[Index].Ganar)
                MenuGanar.Draw(spriteBatch);
            else if (Nv[Index].Perder)
                MenuPerder.Draw(spriteBatch);
            
            if (MenuInicial.Inicio && !Nv[Index].Perder && !Nv[Index].Ganar && !MenuPausa.ActivaPausa)
                Nv[Index].Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}