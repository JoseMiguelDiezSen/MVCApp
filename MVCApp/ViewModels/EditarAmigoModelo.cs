using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.ViewModels
{
    public class EditarAmigoModelo:CrearAmigoModelo
    {

        public int Id { get; set; }

        public string rutaFotoExistente { get; set; }



    }
}
