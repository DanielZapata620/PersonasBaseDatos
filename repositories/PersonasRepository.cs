using Ej3Personas.Models;
using Ej3Personas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ej3Personas.repositories
{
    public class PersonasRepository
    {
        RegistroPoblacionalContext context = new();

        public IEnumerable<Personas> GetAll()
        {
            return context.Personas.OrderBy(p => p.Nombre);
        }

        

        public void Agregar(Personas p)
        {
            context.Personas.Add(p);
            context.SaveChanges();
        }

        public Personas GetById(int id) 
        { 
           return  context.Personas.Find(id);
        }
        public void Editar(Personas p) 
        {
            //Personas? pm=context.Personas.Find(p.Id);
            //pm.Edad=p.Edad;
            //pm.Nombre=p.Nombre;
            //pm.Genero=p.Genero;
            //context.Personas.FirstOrDefault(X => X.Id == p.Id)
            context.Personas.Update(p);
            context.SaveChanges();
        }


        public void Eliminar(Personas p)
        {
            context.Personas.Remove(p);
            context.SaveChanges();
        }

        public bool Validar(Personas p, out string Error)
        {
            List<string> ListaErrores = new List<string>();
            if(context.Personas.Any(x=>x.Nombre.ToUpper()==p.Nombre.ToUpper())&&p.Id==0)
            {
                ListaErrores.Add("La persona ya se encuentra registrada");

            } 
            if(context.Personas.Any(x=>x.Nombre.ToUpper()==p.Nombre.ToUpper()&&p.Id!=x.Id))
            {
                ListaErrores.Add("La persona ya se encuentra registrada");

            }

            if (string.IsNullOrWhiteSpace(p.Nombre))
            {
                ListaErrores.Add("Ingrese el nombre de la persona");

            }
            if (p.Edad<0&&p.Edad>100)
            {
                ListaErrores.Add("La edad debe estar entre 0 y 100");
            }



            if (p.Nombre!=null&&p.Nombre.Length>100)
            {
                ListaErrores.Add("El nombre debe de ser de menos de 100 acracteres");
            }
            Error=string.Join(Environment.NewLine, ListaErrores);

            return ListaErrores.Count!=0;
            //Validar que el nombre no este vacio y no se repita
            //Acepte puras letras de la A a la Z ......
            //Genero...
            //Edad entre 0 y 100
            //Validar que el total de caracteres sean 100 y mayor a 1 

            //Metodo para mostrar cantidad de hombres y mujeres
            
        }

        public long GetNumeroPersonas(Sexo s)
        {
            long x = context.Personas.Count(x => x.Genero==s);
            return x;
        }

    }
}
