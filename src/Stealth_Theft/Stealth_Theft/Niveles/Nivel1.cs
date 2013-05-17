using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    class Nivel1 : Nivel
    {

        public Nivel1(Game game)
            : base(game,6)
        {
            base.CargarColisionador("/Nivel/Nivel1.txt");
            base.CargarEnemigos("/Personaje/Enemigos/Nivel1/Patrullas/Patrulla");
            base.Teleport("/Nivel/PortalesNivel1.txt");
            string[] Aux = new string[2];
            Aux[1]=  "Personaje/Item/Nivel1/Imagenes/llave";
            Aux[0] = "Personaje/Item/Nivel1/Imagenes/PocionVida";
            base.CargarItems(Aux, "/Personaje/Item/Nivel1/Datos/Datos.txt");
            base.CargarSonidoExplosion("Sonidos/Nivel1/Bomb");
            base.CargarTemaFondo("Sonidos/Nivel1/temaFondo");
            base.CargarFuente("Fuentes/Letras");
        }

    }
}
