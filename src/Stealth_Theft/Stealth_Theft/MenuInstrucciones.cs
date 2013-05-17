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
    class MenuInstrucciones : Menu
    {
        public MenuInstrucciones(Game game)
            : base(game.Content, "Menu/superpausa", 4)
        {
            SplashAnimado.CambiarTiempo(0.45f);
        }

        public override void Update(GameTime Time)
        {
            base.Update(Time);
        }

        public override void Draw(SpriteBatch Batch)
        {
            base.Draw(Batch);
        }
    }
}
