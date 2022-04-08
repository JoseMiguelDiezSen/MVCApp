using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{

    // Aqui meteremos los metodos para agregar modificar y eliminar datos.Definimos los metodos que vamos a utilizar
    public interface IAmigoAlmacen
    {
        // Devuelve los datos del amigo a partir del id
        Amigo getDataFriend(int Id);
   
        List <Amigo> getAllFriends();

        Amigo nuevo(Amigo amigo);

        Amigo modificar(Amigo modificarAmigo);

        Amigo borrar(int id);
    }
}
