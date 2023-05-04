using NAudio.CoreAudioApi;

namespace AudioTracking
{
    internal class Program
    {
        static MMDeviceEnumerator en;

        static void initvoiceDetection()
        {
            en = new MMDeviceEnumerator();
        }

        //Hay que seleccionar el micro dentro de los devices
        //https://www.youtube.com/watch?v=HqZrDRwGkdI
        static void Main(string[] args)
        {
            initvoiceDetection();

            var devices = en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

            Console.WriteLine(devices[0].ToString());

            while (devices.Count > 0)
            {
                if ((devices[2].AudioMeterInformation.MasterPeakValue * 100000.0f) > 10.0f)
                {
                    Console.WriteLine((devices[1].AudioMeterInformation.MasterPeakValue * 100000.0f));
                }

            }

        }

    }
}