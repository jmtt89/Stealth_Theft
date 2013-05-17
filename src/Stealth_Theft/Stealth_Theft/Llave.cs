using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Stealth_Theft
{
    class Llave : Item
    {
        public Llave(Game game, string Path, int Cantidad, Vector2 Pos)
            : base(game.Content, Path, Cantidad, Pos)
        {
            TipoDeItem = 0;
            base.FactorScala(0.15f);
        }

        public override void Update(GameTime Time,Vector2 PosMap)
        {

            base.Update(Time,PosMap);
        }

        public override void Draw(SpriteBatch Batch)
        {

            base.Draw(Batch);
        }

    }
}
