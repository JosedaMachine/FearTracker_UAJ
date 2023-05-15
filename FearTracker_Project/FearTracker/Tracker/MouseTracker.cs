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
        //int mousePositionDifference;    //Difference in position in this time iteration for graphic representation

        double screenSize;     //Size of the screen to compare divided by 10000

        //Variables to determine if the user was scared
        int mouseMovementInPixels; //Difference in position between previous iteration and current one
        //int offsetMouseDifference;  //Offset
        double averageDifference;  //Average movement, this variable changes during execution and decides if a movement is sporadic or not
        //long numberOfMovements;     //Number of times the average has been determined
        float scaredMouseMultiplier; //Multiplier applied to the average to determine if the movement of the mouse was sporadic or not
                                     //Decrease to increase sensitivity

        DateTime lastMouseScareTime; //Saves when was the last mouse scare to avoid repeating the same input
        //int mouseScareTimeOffset;   //Minimum offset between scares in seconds

        //bool userScared;
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

            //offsetMouseDifference = 20;
            averageDifference = 0;
            //numberOfMovements = 0;
            scaredMouseMultiplier = 0.1f;

            //Size of the width of the screen
            screenSize = Screen.PrimaryScreen.Bounds.Width * Screen.PrimaryScreen.Bounds.Height;

            //mouseScareTimeOffset = 8;

            //userScared = false;
        }

        public void initialize()
        {
            lastMouseScareTime = DateTime.Now;
        }

        public void sendEventAndRecord(DateTime currentTime)
        {
            //We send the corresponding event
            TrackerSystem ts = TrackerSystem.GetInstance();
            MouseEvent mouse = ts.CreateEvent<MouseEvent>();
            //We calculate the average if mouseMovement isn't 0
            if (mouseMovementInPixels > 1.0f)
                averageDifference = (double)(mouseMovementInPixels) / ((double)((currentTime - lastMouseScareTime).Milliseconds) / 1000.0f);
            else
                averageDifference = 0.0f;
            mouse.setMouseDisplacement((float)averageDifference);
            ts.trackEvent(mouse);

            //userScared = false;
            //We reset the cumulative value
            //mousePositionDifference = 0;
            lastMouseScareTime = currentTime;
            //numberOfMovements = 0;
            averageDifference = 0;
            mouseMovementInPixels = 0;
        }

        public void readInput()
        {
            Point currentMousePosition = Cursor.Position;

            //We calculate how much the muse has moved in this iteration and add it
            int mouseDifferenceX = Math.Abs(oldMousePosition.X - currentMousePosition.X);
            int mouseDifferenceY = Math.Abs(oldMousePosition.Y - currentMousePosition.Y);
            //mousePositionDifference += mouseDifferenceX + mouseDifferenceY;
            mouseMovementInPixels += mouseDifferenceX + mouseDifferenceY;


            //We only determine the average and register the input if the difference surpasses the offset
            //if (!userScared && currentMousePositionDifference >= offsetMouseDifference)
            //{
            //    //We avoid getting various mouseScareEvents if one has already ocurred recently
            //    if ((currentTime - lastMouseScareTime).Seconds > mouseScareTimeOffset)
            //    {
            //        //Determining if the user was scared or not
            //        if (currentMousePositionDifference > averageDifference * scaredMouseMultiplier)
            //        {
            //            userScared = true;
            //            //TrackerSystem ts = TrackerSystem.GetInstance();
            //            //MouseScareEvent susto = ts.CreateEvent<MouseScareEvent>();
            //            //ts.trackEvent(susto);

            //            lastMouseScareTime = DateTime.Now;
            //            Console.WriteLine("SUSTO RATON");
            //        }
            //    }

            //We determine the new average
            //numberOfMovements++;
            oldMousePosition = currentMousePosition;
            //Console.WriteLine("Diferencia de movimiento: X={0}", mousePositionDifference);

            //}

        }

        /// <summary>
        /// Determines if the user is scared,
        /// which happens if the mouse was moved a considerable distance compared to screen width
        /// and multiplier
        /// </summary>
        /// <returns></returns>
        public double ScareThreshold()
        {
            return screenSize * (scaredMouseMultiplier/100.0f);
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
