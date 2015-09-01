using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ApproachProcedure;

namespace ATCDelegateTest
{

    class Program
    {

        static void Main(string[] args)
        {

            // Creamos la aeronave
            Airplane boeing787 = new Airplane();

            // Altitud (en pies)
            boeing787.currentAltitude = 5000;

            // Velocidad (en nudos)
            boeing787.horizontalSpeed = 200;

            // Velocidad verticual (en pies / minutos)
            boeing787.verticalSpeed = 0;

            // Rumbo (en grados magnéticos)
            boeing787.heading = 120;

            //
            // El ATC nos da permiso para comenzar la aproximación
            // y nos brinda información necesaria para ello.
            //

            // Rumbo donde se encuentra la pista
            boeing787.heading = 230;

            // Ajustamos velocidad
            boeing787.horizontalSpeed = 160;

            // Altura a la que se encuentra el aeropuerto
            boeing787.targetAltitude = 100;

            // Millas de distancia al aeropuerto
            boeing787.checkpointDistance = 50;

            // Preguntamos al ATC la altura de intercepción de la señal ILS
            boeing787.airportILSAltitude = 3000;

            // Comenzamos aproximación
            boeing787.startApproach();

            Console.WriteLine();

            Console.ReadKey();

        } // Main

    } // Program

    class Airplane
    {

        public int currentAltitude { get; set; }

        public int targetAltitude { get; set; }

        public int horizontalSpeed { get; set; }

        public int verticalSpeed { get; set; }

        public int heading { get; set; }

        public int airportILSAltitude { get; set; }

        public float checkpointDistance { get; set; }

        private ILSAltitudeDelegate ilsAltitude;

        private InitApproach initApproach;

        public Airplane()
        {

            ilsAltitude = new ILSAltitudeDelegate(ilsAltitudeReached);

        } // Constructor

        public Boolean startApproach()
        {

            initApproach = new InitApproach(horizontalSpeed, checkpointDistance, currentAltitude, targetAltitude);

            Console.WriteLine();

            Console.WriteLine(String.Format("Rumbo: {0} / HS: {1} knots / VS: {2} fpm / Altura: {3} ft / Altura aeropuerto: {4} ft / Distancia a comenzar descenso: {5} millas.", heading, horizontalSpeed, verticalSpeed, currentAltitude, targetAltitude, initApproach.distanceToStartDescent()));

            verticalSpeed = initApproach.verticalSpeedToDescent();

            Console.WriteLine();

            Console.WriteLine("Comenzando descenso...");

            Console.WriteLine();

            while (!initApproach.interceptILSSignal(ilsAltitude))
            {

                currentAltitude = currentAltitude - 25;

                initApproach.currentAltitude = currentAltitude;

                Console.WriteLine(String.Format("Rumbo: {0} / HS: {1} knots / VS: {2} fpm / Altura: {3} ft / Altura aeropuerto: {4} ft / Tiempo de descenso: {5} min.", heading, horizontalSpeed, verticalSpeed, currentAltitude, targetAltitude, initApproach.timeToExecuteDescent()));

            } // while

            Console.WriteLine();

            Console.WriteLine("Interceptando ILS...");

            Console.WriteLine();

            Console.WriteLine("Aterrizando...");

            return true;

        } // startApproach

        public Boolean ilsAltitudeReached(int currentAltitude)
        {

            if(currentAltitude == airportILSAltitude)
            {

                return true;

            } // if
            else
            {

                return false;

            } // else

        } // ilsAltitudeReached

    } // Airplane

} // ATCDelegateTest
