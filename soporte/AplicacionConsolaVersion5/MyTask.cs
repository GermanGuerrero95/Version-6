using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionConsolaVersion2
{
    internal class MyTask
    {
        //Attributes
        public string asunto { get; set; }
        public string description { get; set; }
        //public int dia { get; set; }
        //public int mes { get; set; }
        //public int year { get; set; }
        public DateTime fecha { get; set; }
        public bool estado { get; set; }


        //Constructor 
        public MyTask()
        {
            Console.WriteLine("Ingrese información de su tarea\n" +
                "___________________");

            Console.WriteLine("Ingrese asunto de su tarea");

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

            Console.WriteLine("Ingrese descripción de la tarea");

            string descripcionAfuera = Console.ReadLine();

            bool estadoDescription = String.IsNullOrEmpty(descripcionAfuera);

            if (!estadoDescription) this.description = descripcionAfuera;

            while (estadoDescription)
            {
                Console.WriteLine("La descripción no puede quedar vacia");
                Console.WriteLine("Ingrese descripción de la tarea");
                descripcionAfuera= Console.ReadLine();
                estadoDescription = String.IsNullOrEmpty(descripcionAfuera);
            }
            this.description = descripcionAfuera;

            //Fechas.

            //Console.WriteLine("Ingrese el día");
            //int tipoFinal;
            //string input = Console.ReadLine();
            //bool estado = int.TryParse(input, out tipoFinal);
            //while (!estado || tipoFinal > 31 || tipoFinal < 0)
            //{
            //    Console.WriteLine("Ingrese el día");
            //    string inputWhile = Console.ReadLine();
            //    estado = int.TryParse(inputWhile, out tipoFinal);
            //}
            //this.dia = tipoFinal;

            //Console.WriteLine("Ingrese el mes");
            //int tipoFinal2;
            //string input2 = Console.ReadLine();
            //bool estado2 = int.TryParse(input2, out tipoFinal2);
            //while (!estado2 || tipoFinal2 > 12 || tipoFinal2 < 0)
            //{
            //    Console.WriteLine("Ingrese el mes");
            //    string inputWhile = Console.ReadLine();
            //    estado2 = int.TryParse(inputWhile, out tipoFinal2);
            //}
            //this.mes = tipoFinal2;

            //Console.WriteLine("Ingrese el año");
            //int tipoFinal3;
            //string input3 = Console.ReadLine();
            //bool estado3 = int.TryParse(input3, out tipoFinal3);
            //while (!estado3 || tipoFinal3 < 0)
            //{
            //    Console.WriteLine("Ingrese el año");
            //    string inputWhile = Console.ReadLine();
            //    estado3 = int.TryParse(inputWhile, out tipoFinal3);
            //}
            //this.year = tipoFinal3;

            //this.fecha = new DateTime(this.year, this.mes, this.dia);
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
                catch (Exception)
                {

                    throw new Program.fechaMala();

                }

            }


            //Console.WriteLine("ADVERTENCIA : Sí su tarea tiene una fecha inferior a la del día de hoy quedara en status Pendiente / sino Activa");

            //this.estado = this.fecha >= DateTime.Today ? "Activa" : "Pendiente";

           
            this.estado = true;

            Console.WriteLine("¡Tarea ingresada exitosamente!");

        }


        //Metodos
        public string info()
        {
            return $"El asunto de la tarea es : {this.asunto} \n La descripcion es : {this.description} \n La fecha para la realización es : {this.fecha.ToShortDateString()} \n El estado es {this.estado}";
        }

        public string summary()
        {

            return $"Asunto: {this.asunto} -- Estado: {this.estado}  Fecha: {this.fecha.ToShortDateString()}";
        }






    }
}
