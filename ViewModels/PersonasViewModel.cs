using Ej3Personas.Models;
using Ej3Personas.repositories;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Xaml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ej3Personas.ViewModels
{
    public enum Sexo {Hombre=1,Mujer=2 }
    public class PersonasViewModel:INotifyPropertyChanged
    {
        PersonasRepository repos = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Personas> Personas { get; set; }=new();
        public Personas Persona { get; set; }
        public long Hombres { get; set; }
        public long Mujeres { get; set; }
        public ICommand VerAgregarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }
        public ICommand EditarCommand {  get; set; }

        public ICommand EliminarCommand { get; set; }

        public ICommand CancelarCommand { get; set; }

        public ICommand AgregarCommand { get; set; }

        string error;
        public string Error
        {
            get { return error; }
            set { error=value; }

        }

        public string Modo { get; set; } = "Ver";
        public PersonasViewModel() 
        {
            VerAgregarCommand=new RelayCommand(VerAgregar);
            AgregarCommand=new RelayCommand(Agregar);
            CancelarCommand=new RelayCommand(Cancelar);
            VerEditarCommand=new RelayCommand(VerEditar);
            EliminarCommand=new RelayCommand<Personas>(Eliminar);
            
            EditarCommand=new RelayCommand(Editar);

            Actualizar();
        }

        private void Eliminar(Personas p)
        {
            if (p != null)
            {
                repos.Eliminar(p);
                Actualizar();
            }
        }

        private void Cancelar()
        {
            Modo="Ver";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void Editar()
        {
            if (Persona !=null)
            {
                var x=repos.GetById(Persona.Id);
                if (x!=null)
                {
                    x.Edad=Persona.Edad;
                    x.Nombre=Persona.Nombre;
                    x.Genero=Persona.Genero;
                    repos.Editar(x);
                    Actualizar();
                }


                //if (repos.Validar(Persona, out error))
                //{
                //    repos.Editar(Persona);
                //    Actualizar();
                //}
            }
        }

        private void VerEditar()
        {
            if (Persona!=null)
            {
                Personas c = new Personas()
                {
                    Edad=Persona.Edad,
                    Nombre=Persona.Nombre,
                     Genero=Persona.Genero,
                     Id=Persona.Id
                };
                Persona = c;
                Modo="VerEditar";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        private void Agregar()
        {
            //Validar
            //Crear una propiedad de tipo string llamada error
            //invocar el metodo validar
            if (!repos.Validar(Persona, out error))
            {
                repos.Agregar(Persona);
                //Personas.Clear();
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                Actualizar();
                Modo="Ver";

            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

            //repos.Agregar(Persona);
            //Modo="Ver";
        }

        private void VerAgregar()
        {
            Persona=new();
            Modo="Agregar";
            Error="";
            //PropertyChanged?.Invoke(this, new(nameof(Modo)));
            //Actualizar();
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(null));
        }

        void Actualizar()
        {
            Personas.Clear();
            foreach (var item in repos.GetAll())
            {
                Personas.Add(item);
            }

            Hombres=repos.GetNumeroPersonas(Sexo.Hombre);
            
            Mujeres=repos.GetNumeroPersonas(Sexo.Mujer);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
