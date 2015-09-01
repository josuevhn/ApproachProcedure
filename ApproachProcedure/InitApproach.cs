using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApproachProcedure
{

    public delegate bool ILSAltitudeDelegate(int currentAltitude);

    public class InitApproach
    {

        private int currentSpeed { get; set; }

        private float targetDistance { get; set; }

        public int currentAltitude { get; set; }

        private int targetAltitude { get; set; }

        public InitApproach(int currentSpeed, float targetDistance, int currentAltitude, int targetAltitude)
        {

            this.currentSpeed = currentSpeed;

            this.targetDistance = targetDistance;

            this.currentAltitude = currentAltitude;

            this.targetAltitude = targetAltitude;

        } // Constructor

        private int altitudeDifference()
        {

            return (currentAltitude - targetAltitude);

        } // altitudeDifference

        public int distanceToStartDescent()
        {

            return ((altitudeDifference() * 3) / 1000);

        } // distanceToStartDescent

        public float timeToExecuteDescent()
        {

            return (60 * (targetDistance /  currentSpeed));

        } // timeToExecuteDescent

        public int verticalSpeedToDescent()
        {

            return (altitudeDifference() / Convert.ToInt32(timeToExecuteDescent()));

        } // verticalSpeed

        public Boolean interceptILSSignal(ILSAltitudeDelegate ilsAltitude)
        {

            if (ilsAltitude(currentAltitude))
            {


                // Procedimientos de aterrizaje en modo ILS

                return true;

            } // if
            else
            {

                // Seguimos a la espera de la altura exacta

                return false;

            } // else

        } // interceptILSSignal

    } // InitApproach

} // ApproachProcedure
