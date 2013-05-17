using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Stealth_Theft
{
    class Portales
    {
        Vector2 Entrada;
        Vector2 Salida;
        bool Tocado;

        public Portales(Vector2 In)
        {
            Entrada = In;
            Salida = new Vector2();
            Tocado = false;
        }

        public Portales(Vector2 In, Vector2 Out)
        {
            Entrada = In;
            Salida = Out;
            Tocado = false;
        }

        public void SetOut(Vector2 Out)
        {
            Salida = Out;
        }

        public Vector2 GetEntrada()
        {
            return Entrada;
        }

        public Vector2 GetSalida()
        {
            return Salida;
        }

        public bool Activado()
        {
            return Tocado;
        }

        public void Activar()
        {
            Tocado = true;
        }

        public void DeActivar()
        {
            Tocado = false;
        }


    }
}
