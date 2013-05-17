using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Stealth_Theft
{
    class Item
    {

        int AnchoNiv = 800;
        int AltoNiv = 600;

        Vector2 posicionLog;
        Vector2 posicionDib;
        MotorAnimacion Animador;

        int AnchoItem;
        int LargoItem;

        bool Valido;

        protected int TipoDeItem;

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)posicionLog.X, (int)posicionLog.Y, AnchoItem, LargoItem);
        }

        public int GetTipo()
        {
            return TipoDeItem;
        }

        public void FactorScala(float Delta)
        {
            AnchoItem = (int)(AnchoItem * Delta);
            LargoItem = (int)(LargoItem * Delta);
        }

        public Item(ContentManager Content,string Path, int Cantidad, Vector2 Pos)
        {
            Animador = new MotorAnimacion(Content, Path, Cantidad);
            posicionLog = Pos;
            Texture2D Aux = Content.Load<Texture2D>(Path + "0");
            AnchoItem = Aux.Width;
            LargoItem = Aux.Height;

        }

        public virtual void Update(GameTime Time, Vector2 PosMap)
        {
            ActualizarPosisionDibujo(PosMap);
            Animador.AnimacionFija(Time);
        }

        public virtual void Draw(SpriteBatch Batch)
        {
            if(Valido)
                Animador.draw(Batch, new Vector4(posicionDib,AnchoItem,LargoItem));
        }


        private void ActualizarPosisionDibujo(Vector2 PosMap)
        {
            Valido = false;
            float P1 = AnchoNiv * PosMap.X;
            float P2 = AnchoNiv * (PosMap.X + 1);
            float P3 = AltoNiv * PosMap.Y;
            float P4 = AltoNiv * (PosMap.Y + 1);

            if (posicionLog.X > P1 && posicionLog.X < P2)
                if (posicionLog.Y > P3 && posicionLog.Y < P4)
                {
                    posicionDib.X = posicionLog.X - PosMap.X * AnchoNiv;
                    posicionDib.Y = posicionLog.Y - PosMap.Y * AltoNiv;
                    Valido = true;
                }


        }
    }
}
