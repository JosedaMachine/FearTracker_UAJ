using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameTracker;

namespace FT
{
    internal class MouseTracker
    {

        #region MouseVariables
        Point oldMousePosition;   //Position of the mouse in the previous iteration
        int mousePositionDifference;    //Difference in position in this time iteration for graphic representation

        //Variables to determine if the user was scared
        int currentMousePositionDifference; //Difference in position between previous iteration and current one
        int offsetMouseDifference;  //Offset
        double averageDifference;  //Average movement, this variable changes during execution and decides if a movement is sporadic or not
        long numberOfMovements;     //Number of times the average has been determined
        float scaredMouseMultiplier; //Multiplier applied to the average to determine if the movement of the mouse was sporadic or not
                                     //Decrease to increase sensitivity

        DateTime lastMouseScareTime; //Saves when was the last mouse scare to avoid repeating the same input
        int mouseScareTimeOffset;   //Minimum offset between scares in seconds

        bool userScared;
        #endregion

        private static MouseTracker instance = null;
        public static MouseTracker GetInstance()
        {
            if (instance == null)
            {
                instance = new MouseTracker();
            }

            return instance;
        }

        public MouseTracker()
        {
            oldMousePosition = Cursor.Position;

            offsetMouseDifference = 20;
            averageDifference = 0;
            numberOfMovements = 0;
            scaredMouseMultiplier = 1.7f;

            lastMouseScareTime = DateTime.Now;
            mouseScareTimeOffset = 4;

            userScared = false;
        }

        public void sendEventAndRecord()
        {
            //We send the corresponding event
            TrackerSystem ts = TrackerSystem.GetInstance();
            MouseEvent mouse = ts.CreateEvent<MouseEvent>();
            mouse.setMouseDisplacement(mousePositionDifference);
            ts.trackEvent(mouse);

            //We reset the cumulative value
            mousePositionDifference = 0;
        }

        public void readInput(DateTime currentTime)
        {
            Point currentMousePosition = Cursor.Position;

            //We calculate how much the muse has moved in this iteration and add it
            int mouseDifferenceX = Math.Abs(oldMousePosition.X - currentMousePosition.X);
            int mouseDifferenceY = Math.Abs(oldMousePosition.Y - currentMousePosition.Y);
            mousePositionDifference += mouseDifferenceX + mouseDifferenceY;


            //We only determine the average and register the input if the difference surpasses the offset
            if (!userScared && mousePositionDifference >= offsetMouseDifference)
            {
                //We avoid getting various mouseScareEvents if one has already ocurred recently
                if ((currentTime - lastMouseScareTime).Seconds > mouseScareTimeOffset)
                {
                    //Determining if the user was scared or not
                    if (mousePositionDifference > averageDifference * scaredMouseMultiplier)
                    {
                        userScared = true;
                        //TrackerSystem ts = TrackerSystem.GetInstance();
                        //MouseScareEvent susto = ts.CreateEvent<MouseScareEvent>();
                        //ts.trackEvent(susto);

                        //lastMouseScareTime = DateTime.Now;
                        Console.WriteLine("SUSTO RATON");
                    }
                }

                //We determine the new average
                numberOfMovements++;
                averageDifference = (double)((averageDifference * (numberOfMovements - 1) + mousePositionDifference)) / (double)numberOfMovements;
                oldMousePosition = currentMousePosition;
                //Console.WriteLine("Diferencia de movimiento: X={0}", mousePositionDifference);

            }

        }

        /// <summary>
        /// Determines if the user is scared, which happens if the mouse was moved faster than before
        /// </summary>
        /// <returns></returns>
        public double ScareThreshold()
        {
            return averageDifference * scaredMouseMultiplier;
        }

        public float GetScareMultiplyer()
        {
            return scaredMouseMultiplier;
        }

        public void SetScareMultiplyer(decimal value)
        {
            scaredMouseMultiplier = (float)value;
        }
    }
}
