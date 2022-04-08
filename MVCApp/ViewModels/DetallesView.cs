using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.ViewModels
{
    public class DetallesView
    {
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public Amigo amigo { get; set; }
    }
}
