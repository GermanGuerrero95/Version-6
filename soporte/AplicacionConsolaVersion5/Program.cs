using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Schema;
using System.Runtime.Remoting.Messaging;

namespace AplicacionConsolaVersion2
{
    internal class Program
    {
        static List<Appointment> appointmentsList = new List<Appointment>();
        static List<MyTask> taskList = new List<MyTask>();
        static void Main(string[] args)
        {
            try
            {
                welcome();
                switch (numeroMenu())
                {
                    case 1:
                        Appointment appointment = new Appointment();
                        appointmentsList.Add(appointment); Program.Main(args); break;
                    case 2:
                        MyTask personalTask = new MyTask();
                        taskList.Add(personalTask); Program.Main(args); break;
                    case 3:
                        if (controlAgendaVacia()) Program.Main(args);
                        imprimirDiaExacto(); Program.Main(args); break;
                    case 4:
                        if (controlTareasVacias()) Program.Main(args);
                        showAllTask();
                        eliminarTareas(); Program.Main(args); break;
                    case 5:
                        Program.Main(args); break;
                }
            }
            catch (fechaMala)
            {
                Console.WriteLine("Fecha no válida");  Program.Main(args);
            }
            Console.ReadLine();
        }
    

        //Mensaje de bienvenida
        static void welcome() {
            Console.WriteLine("\n\t\r\n░█████╗░░██████╗░███████╗███╗░░██╗██████╗░░█████╗░\r\n██╔══██╗██╔════╝░██╔════╝████╗░██║██╔══██╗██╔══██╗\r\n███████║██║░░██╗░█████╗░░██╔██╗██║██║░░██║███████║\r\n██╔══██║██║░░╚██╗██╔══╝░░██║╚████║██║░░██║██╔══██║\r\n██║░░██║╚██████╔╝███████╗██║░╚███║██████╔╝██║░░██║\r\n╚═╝░░╚═╝░╚═════╝░╚══════╝╚═╝░░╚══╝╚═════╝░╚═╝░░╚═╝\n");

            Console.WriteLine("██╗░░░██╗███████╗██████╗░░██████╗██╗░█████╗░███╗░░██╗  ███████╗░░░░█████╗░");
            Console.WriteLine("██║░░░██║██╔════╝██╔══██╗██╔════╝██║██╔══██╗████╗░██║  ██╔════╝░░░██╔══██╗");
            Console.WriteLine("╚██╗░██╔╝█████╗░░██████╔╝╚█████╗░██║██║░░██║██╔██╗██║  ██████╗░░░░██║░░██║");
            Console.WriteLine("░╚████╔╝░██╔══╝░░██╔══██╗░╚═══██╗██║██║░░██║██║╚████║  ╚════██╗░░░██║░░██║");
            Console.WriteLine("░░╚██╔╝░░███████╗██║░░██║██████╔╝██║╚█████╔╝██║░╚███║  ██████╔╝██╗╚█████╔╝");
            Console.WriteLine("░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═════╝░╚═╝░╚════╝░╚═╝░░╚══╝  ╚═════╝░╚═╝░╚════╝░");
            //Welcome Message :)
            int hora = DateTime.Now.Hour;

            Console.WriteLine(((hora >= 00 && hora <= 12) ? "\tBuenos días \n" : (hora >= 13 && hora <= 18) ? "\tBuenas tardes \n" : "\tBuenas noches\n"));

        }

        static int numeroMenu()
        {

            int optionMenu;
            Console.WriteLine("1.Crear una cita \n2.Crear una tarea \n3.Ver agenda del día \n4.Finalizar una tarea \n5.Salir"); 

            try
            {   
                optionMenu = int.Parse(Console.ReadLine());

                if (optionMenu >= 1 && optionMenu <= 4) return optionMenu;
                else Console.WriteLine("Opción no válida. Ingrese por favor una opción válida"); optionMenu = 5; return optionMenu;

                
            }
            catch (Exception)
            {
                Console.WriteLine("Opción no válida. Ingrese por favaor una opción válida");
                return 5;
            }


        }


        //Agenda de hoy    
        static void imprimirHoy()
        {
            if (appointmentsList.FindAll(x => x.fecha.Date == DateTime.Now.Date).Count == 0)
                Console.WriteLine("No hay citas asignadas para el día de hoy\n");
            if (taskList.FindAll(x => x.fecha == DateTime.Today).Count == 0)
                Console.WriteLine("No hay tareas agendadas para el día de hoy\n");
            if (appointmentsList.FindAll(x => x.fecha == DateTime.Today).Count > 0)
                Console.WriteLine($"El número de citas para el día de hoy es  : {appointmentsList.FindAll(x=> x.fecha == DateTime.Today).Count}");
            if (taskList.FindAll(x => x.fecha == DateTime.Today).Count > 0)
                Console.WriteLine($"El número de tareas para el día de hoy es : {taskList.FindAll(x => x.fecha == DateTime.Today).Count}");

            foreach (Appointment x in appointmentsList.FindAll(x => x.fecha.Day == DateTime.Now.Day))
            {
                Console.WriteLine("******** La cita de hoy   *******");
                Console.WriteLine(x.info());
            }

            foreach (MyTask x in taskList.FindAll(x => x.fecha.Day == DateTime.Now.Day))
            {
                Console.WriteLine("******** La tarea de hoy  *******");
                Console.WriteLine(x.info());
            }

        }



        static void imprimirAyer()
        {

            DateTime ayer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);

            int count = 0;
            int countTareas = 0;

            foreach (Appointment x in appointmentsList)
            {
                if (x.fecha.ToShortDateString() == ayer.ToShortDateString()) count++;
            }

         

            Console.WriteLine($"El numero de citas para el {ayer.ToShortDateString()} eran: {count}");

            

            foreach (MyTask x in taskList)
            {
                if (x.fecha.ToShortDateString() == ayer.ToShortDateString()) countTareas++;
            }

            Console.WriteLine($"El numero de tareas para el {ayer.ToShortDateString()} eran: {countTareas}");

            foreach (Appointment x in appointmentsList.FindAll(x => x.fecha.Day == DateTime.Now.Day - 1 && x.fecha.Month == DateTime.Now.Month && x.fecha.Year == DateTime.Now.Year))
            {
                Console.WriteLine("******** La cita del día de ayer   *******");
                Console.WriteLine(x.info());
            }

            foreach (MyTask x in taskList.FindAll(x => x.fecha.Day == DateTime.Now.Day - 1 && x.fecha.Month == DateTime.Now.Month && x.fecha.Year == DateTime.Now.Year))
            {
                Console.WriteLine("******** La tarea del día de ayer  *******");
                Console.WriteLine(x.info());
            }

        }

        static void imprimirTomorrow()
        {
            DateTime tomorrowES = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);

            int count = 0;
            int countTareas = 0;

            foreach (Appointment x in appointmentsList)
            {
                if (x.fecha.ToShortDateString() == tomorrowES.ToShortDateString()) count++;
            }

            Console.WriteLine($"El número de citas para mañana son: {count}");

            foreach (MyTask x in taskList)
            {
                if (x.fecha.ToShortDateString() == tomorrowES.ToShortDateString()) countTareas++;
            }

            Console.WriteLine($"El número de tareas para mañana son: {countTareas}");

            foreach (Appointment x in appointmentsList.FindAll(x => x.fecha.Day == DateTime.Now.Day + 1 && x.fecha.Month == DateTime.Now.Month && x.fecha.Year == DateTime.Now.Year))
            {
                Console.WriteLine("******** La cita del día de mañana   *******");
                Console.WriteLine(x.info());
            }

            foreach (MyTask x in taskList.FindAll(x => x.fecha.Day == DateTime.Now.Day + 1 && x.fecha.Month == DateTime.Now.Month && x.fecha.Year == DateTime.Now.Year))
            {


                Console.WriteLine("******** La tarea del día de mañana   *******");
                Console.WriteLine(x.info());
            }
        }
        static void verAgenda()
        {
            //Console.WriteLine("Ingrese dia");
            //int dia = int.Parse(Console.ReadLine());
            //Console.WriteLine("Ingrese mes");
            //int mes = int.Parse(Console.ReadLine());
            //Console.WriteLine("Ingrese año");
            //int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese fecha:DIA/MES/AÑO de búsqueda");
            string fechaIngresada = Console.ReadLine();
            DateTime fecha = DateTime.Now;
            if (string.IsNullOrEmpty(fechaIngresada))
            {

               fecha = DateTime.Now;

            }

            else
            {

                string[] division = fechaIngresada.Split('/');


                try
                {

                   fecha = new DateTime(int.Parse(division[2]), int.Parse(division[1]), int.Parse(division[0]));
                }
                catch (Exception)
                {




                    throw new Program.fechaMala();


                }



            }



            //Citas
            foreach (Appointment item in appointmentsList.FindAll(item => item.fecha == fecha))
            {
                Console.WriteLine("Citas encontradas");
                Console.WriteLine(item.info());
            }
            //Tareas
            foreach (MyTask item in taskList.FindAll(item => item.fecha == fecha))
            {
                Console.WriteLine("Tareas encontradas");
                Console.WriteLine(item.info());
            }
        }

        static void imprimirDiaExacto()
        {
            Console.WriteLine("Ver agenda \nSeleccione fecha \n1.Hoy \n2.Ayer\n3.Mañana \n4.Ingrese fecha en especifico \n5.Salir"); int optionFecha = int.Parse(Console.ReadLine());
            switch (optionFecha)
            {
                case 1:
                    imprimirHoy();
                    break;
                  
                case 2:
                    imprimirAyer();
                    break;
                   
                case 3:
                    imprimirTomorrow();
                    break;

                case 4:
                    verAgenda();
                    break;

                case 5: welcome(); break;
            }

            
        }

        static void showAllTask()
        {
            // *** Lista de tareas ***
            foreach (MyTask x in taskList)
            {
                if (x.estado == true) Console.WriteLine($" Tarea pendiente-->  {taskList.IndexOf(x)}.{x.summary()}");
               
            }
        }

        static void eliminarTareas()
        {
            Console.WriteLine("¿Que tarea desea finalizar?");
            int indexBorrar = int.Parse(Console.ReadLine());
            Console.WriteLine("¿Esta seguro de su operacion S/N?");
            char answer = Char.Parse(Console.ReadLine().ToLower());
            switch (answer) {
                case 's':
                    try
                    {
                        taskList[indexBorrar].estado = false;
                        //taskList.RemoveAt(indexBorrar);
                        Console.WriteLine($"¡Tarea # {indexBorrar} ha cambiado su estado a completado!");
                        //if (taskList.Count == 0) Console.WriteLine("******* NO HAY TAREAS DISPONIBLES ***********");

                        if (taskList.FindAll(x => x.estado == true).Count() == 0) Console.WriteLine("No hay tareas pendientes");


                        Console.WriteLine($"El número de tareas activas es {taskList.FindAll(x => x.estado == true).Count()}");
                       
                        
                    }
                    catch (Exception )
                    {
                        Console.WriteLine("Opción no válida");
                    }
                    break;
                case 'n':
                    Console.WriteLine("Tarea declinada");
                    break;
                default:
                    Console.WriteLine("Error");

                    break;
            }
        }

      

        static  bool controlAgendaVacia()
        {
            if (appointmentsList.Count == 0 && taskList.Count == 0)
            {
                Console.WriteLine("No hay tareas ni citas asiganadas");
                return true;
              
               
            }

            return false;
        }

        static bool controlTareasVacias () {
            if (taskList.Count == 0)
            {
                Console.WriteLine("No hay tareas agendadas");
              
                return true;
            }

            else return false;
            
        }

        internal class fechaMala : Exception
        {
            
        }


    }

}


 
  


