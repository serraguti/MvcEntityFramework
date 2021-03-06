﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEntityFramework.Models
{
    public interface ICoche
    {
        //SOLAMENTE ES UNA DECLARACION DE CARACTERISTICAS
        //COMPUESTAS POR METODOS Y PROPIEDADES
        String Marca { get; set; }
        String Modelo { get; set; }
        String Imagen { get; set; }
        int Velocidad { get; set; }
        int VelocidadMaxima { get; set; }
        void Acelerar();
        void Frenar();
    }
}
