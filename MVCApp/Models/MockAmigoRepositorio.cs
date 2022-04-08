using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models

{
    public class MockAmigoRepositorio : IAmigoAlmacen
    {
        private List <Amigo> amigosLista;

        // Aqui en un futuro sacaremos los dastos de un sql server
        public MockAmigoRepositorio()
        {
            amigosLista = new List <Amigo>();
            amigosLista.Add(new Amigo() { Id = 1, Nombre = "Jose M", Ciudad = Provincia.Madrid, Email = "jsm1989@gmail.com" });
            amigosLista.Add(new Amigo() { Id = 2, Nombre = "E.Musk", Ciudad = Provincia.Barcelona, Email = "elonMusk@gmail.com" });
            amigosLista.Add(new Amigo() { Id = 3, Nombre = "S.Ramos", Ciudad = Provincia.Sevilla, Email = "sr4@gmail.com" });
        }

        
        public Amigo getDataFriend(int Id)
        {
            // Devuelve el registro correspondiente a el id introducido ya a salida por pantalla
            return this.amigosLista.FirstOrDefault(e => e.Id == Id);
        }

        //LISTAR USUARIOS
        public List<Amigo> getAllFriends()
        {
            return amigosLista;
        }
        
        // CREAR USUARIO
        public Amigo nuevo(Amigo amigo)
        {
            amigo.Id = amigosLista.Max(a => a.Id) + 1;
            amigosLista.Add(amigo);
            return amigo;
        }

        //MODIFICAR USUARIO
        public Amigo modificar(Amigo modificarAmigo)
        {
            Amigo amigo = amigosLista.FirstOrDefault(e => e.Id == modificarAmigo.Id);

            if(amigo!= null)
            {
                amigo.Nombre = modificarAmigo.Nombre;
                amigo.Email = modificarAmigo.Email;
                amigo.Ciudad = modificarAmigo.Ciudad;
            }
            return amigo;
        }

        //BORRAR USUARIO
        public Amigo borrar(int Id)
        {
            Amigo amigo = amigosLista.FirstOrDefault(e => e.Id == Id);

            if (amigo != null) {
                amigosLista.Remove(amigo);
            }
            return amigo;
        }
        
    }
}
