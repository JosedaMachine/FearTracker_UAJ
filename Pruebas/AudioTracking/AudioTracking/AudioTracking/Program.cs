using NAudio.CoreAudioApi;

namespace AudioTracking
{
    internal class Program
    {
        static MMDeviceEnumerator en; //La variable que lleva los devices

        //Todos estos numeritos estaría bien poder tocarlos de alguna manera


        //Quizás a esto haya que ponerle estático, no lo se
        static float defaultspakingVolume = 4000.0f;   //Basado en las pruebas que hemos hecho, esto da más o menos si hablas a un tono normal (Por si mi colegui no quiere hacer los test)
        static float voiceMult = 100000.0f; //Para que el volumen sean números mayores que cero

        static int voiceTests = 3; //El número de veces que tiene que hablar el usuario

        static float backgroundNoise = 15.0f; //Basado en el ruido monstruoso que hace mi pc


        static int backgroundTimer = 50;    //Iteraciones que nos pasamos grabando, si queremos ponerlo como tiempo pues hay que ver cuantas iteraciones hace por segundo

        static void initvoiceDetection()
        {
            en = new MMDeviceEnumerator();
        }

        //Hay que seleccionar el micro dentro de los devices
        //https://www.youtube.com/watch?v=HqZrDRwGkdI
        static void Main(string[] args)
        {
            initvoiceDetection();
            MMDeviceCollection devices = en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

            getBackgroundNoise(ref devices); //Cogemos el sonido de fondo de donde este el men
            voiceTest(ref devices);          //Le cogemos el volumen al que habla, para que saber bien cuando grita


            Console.WriteLine(devices[2].ToString());

            while (devices.Count > 0)
            {
                if ((devices[2].AudioMeterInformation.MasterPeakValue * voiceMult) > defaultspakingVolume)
                { 
                    Console.WriteLine("Susto");
                }

            }

        }

        //Método para saber a que volumen habla de normal el usuario, para poder tenerlo como referenica
        //ASí si habla sin más no lo cuenta como grito.
        static void voiceTest(ref MMDeviceCollection devices)
        {
            float voice = 0.0f;
            int speakingCont = 0;               //Las veces que ha hablado
            bool speaking = false;              //Para que no te pille las tres de golpe
            float[] samples = new float[voiceTests];    //Seguramente este array no sea necesario, pero así tenemos cada muestra de sonido, por si acaso
            float acumVoice = 0.0f;     //La suma de todas las muestras para hacer la media


            while (speakingCont < voiceTests)
            {
                voice = devices[2].AudioMeterInformation.MasterPeakValue;
                                                                //Representa que ya esta hablando
                if ((voice * voiceMult) > backgroundNoise && !speaking)
                {
                    samples[speakingCont] = voice * voiceMult;
                    speakingCont++;
                }
                else if (voice * voiceMult <= backgroundNoise) speaking = false;        //Cuando se calle

            }

            defaultspakingVolume = acumVoice / voiceTests;
        }

        static void getBackgroundNoise(ref MMDeviceCollection devices)
        {
            int timeRecording = 0;
            float backgroundAcum = 0.0f;

            while (timeRecording < backgroundTimer)
            {
                backgroundAcum += devices[2].AudioMeterInformation.MasterPeakValue;

                timeRecording++;
            }

            backgroundNoise = backgroundAcum / backgroundTimer;
        }

    }
}