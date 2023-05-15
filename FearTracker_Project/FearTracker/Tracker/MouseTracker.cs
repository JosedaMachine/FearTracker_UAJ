using System;
using System.Windows.Forms;
using System.Drawing;
using GameTracker;

namespace FT
{
    internal class MouseTracker
    {

        #region MouseVariables
        Point oldMousePosition;   //Position of the mouse in the previous iteration
        DateTime lastMouseScareTime; //Saves when was the last iteration to calculate the movement in seconds
        int mouseMovementInPixels; //Difference in position between previous iteration and current one

        //Variables to determine if the user was scared
        float scaredMouseMultiplier; //Percentage of the screen that the mouse needs to move for the user to be considered scared
        double screenSize;     //Size of the screen (width*height), can get pretty big
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

            scaredMouseMultiplier = 0.1f;

            //Size of the width of the screen
            screenSize = Screen.PrimaryScreen.Bounds.Width * Screen.PrimaryScreen.Bounds.Height;
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
            double averageDifference = 0.0f;
            if (mouseMovementInPixels > 1.0f)
                averageDifference = (double)(mouseMovementInPixels) / ((double)((currentTime - lastMouseScareTime).Milliseconds) / 1000.0f);
            mouse.setMouseDisplacement((float)averageDifference);
            ts.trackEvent(mouse);

            lastMouseScareTime = currentTime;
            mouseMovementInPixels = 0;
        }

        public void readInput()
        {
            Point currentMousePosition = Cursor.Position;

            //We calculate how much the muse has moved in this iteration and add it
            int mouseDifferenceX = Math.Abs(oldMousePosition.X - currentMousePosition.X);
            int mouseDifferenceY = Math.Abs(oldMousePosition.Y - currentMousePosition.Y);
            mouseMovementInPixels += mouseDifferenceX + mouseDifferenceY;
            oldMousePosition = currentMousePosition;
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
