using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionConsolaVersion2
{
    internal class Appointment
    {
        //Attributes
        public string asunto { get; set; }
        public string lugar { get; set; }
        //public int dia { get; set; }
        //public int mes { get; set; }
        //public int year { get; set; }
        public DateTime fecha { get; set; }

        //Constructor
        public Appointment()
        {
            Console.WriteLine("Ingrese información de su cita\n___________________");
            Console.WriteLine("Ingrese asunto");
            string asuntoAfuera = Console.ReadLine();
            bool estadoAsunto = String.IsNullOrEmpty(asuntoAfuera);
            if (!estadoAsunto) this.asunto = asuntoAfuera;
            while (estadoAsunto)
            {
                Console.WriteLine("El asunto no puede quedar vacio");
                Console.WriteLine("Ingrese asunto de su tarea");
                asuntoAfuera = Console.ReadLine();
                estadoAsunto = String.IsNullOrEmpty(asuntoAfuera);
            }
            this.asunto = asuntoAfuera;



            Console.WriteLine("Ingrese lugar de la cita");
            string lugarAfuera = Console.ReadLine();
            bool estadoLugar = String.IsNullOrEmpty(lugarAfuera);

            if (!estadoLugar) this.lugar = lugarAfuera;

            while (estadoLugar)
            {
                Console.WriteLine("El lugar de la cita no puede quedar vacio");
                Console.WriteLine("Ingrese lugar de la cita");
                lugarAfuera = Console.ReadLine();
                estadoLugar = String.IsNullOrEmpty(lugarAfuera);
             
            }

            this.lugar=lugarAfuera;

           
            Console.WriteLine("Ingrese fecha:DIA/MES/AÑO");
            string fechaIngresada = Console.ReadLine();
            if (string.IsNullOrEmpty(fechaIngresada))
            {

                this.fecha = DateTime.Today;
              
            }

            else
            {

                string[] division = fechaIngresada.Split('/');


                try
                {

                    this.fecha = new DateTime(int.Parse(division[2]), int.Parse(division[1]), int.Parse(division[0]));
                }
                catch (Exception )
                {



                 
                    throw new Program.fechaMala();


                }

             

            }


            Console.WriteLine("¡Cita ingresada exitosamente!");

        }
        public string info()
        {
            return $"Asunto : {this.asunto} \n Lugar: {this.lugar} \n fecha: {this.fecha.ToShortDateString()}";
        }
    }
}
