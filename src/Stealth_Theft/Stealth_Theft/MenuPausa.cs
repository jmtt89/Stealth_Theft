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
    class MenuPausa : Menu
    {
        bool T;
        public bool ActivaPausa;
        public MenuPausa(Game game)
            : base(game.Content, "Menu/superpausa", 4)
        {
            T = true;
            ActivaPausa = false;
            SplashAnimado.CambiarTiempo(0.45f);
        }

        public override void Update(GameTime Time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P) && T)
            {
                if (ActivaPausa)
                    ActivaPausa = false;
                else
                    ActivaPausa = true;
                T = false;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.P))
                T = true;

            if(ActivaPausa)
                base.Update(Time);

        }

        public override void Draw(SpriteBatch Batch)
        {
            if(ActivaPausa)
                base.Draw(Batch);
        }

    }
}
