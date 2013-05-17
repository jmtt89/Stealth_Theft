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
    class MenuGanar : Menu
    {

        public bool Finalizar;

        public MenuGanar(Game game)
            : base(game.Content, "Menu/RoboExitoso", 1)
        {
            Finalizar = false;
            SplashAnimado.CambiarTiempo(0.45f);
        }

        public override void Update(GameTime Time)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Finalizar = true;

            base.Update(Time);
        }

        public override void Draw(SpriteBatch Batch)
        {
            base.Draw(Batch);
        }

    }
}
