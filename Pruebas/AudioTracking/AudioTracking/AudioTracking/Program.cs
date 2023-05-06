using NAudio.CoreAudioApi;
using System.Reflection.Metadata.Ecma335;

namespace AudioTracking
{
    class AudioTracking
    {
        MMDeviceEnumerator en; //La variable que lleva los devices

        //Todos estos numeritos estaría bien poder tocarlos de alguna manera


        //Quizás a esto haya que ponerle estático, no lo se
        float defaultspakingVolume = 4000.0f;   //Basado en las pruebas que hemos hecho, esto da más o menos si hablas a un tono normal (Por si mi colegui no quiere hacer los test)
        float voiceMult = 100000.0f; //Para que el volumen sean números mayores que cero

        int voiceTests = 3; //El número de veces que tiene que hablar el usuario

        float backgroundNoise = 15.0f; //Basado en el ruido monstruoso que hace mi pc


        int backgroundTimer = 50;    //Iteraciones que nos pasamos grabando, si queremos ponerlo como tiempo pues hay que ver cuantas iteraciones hace por segundo


        //Hay que seleccionar el micro dentro de los devices
        //https://www.youtube.com/watch?v=HqZrDRwGkdI
        static void Main(string[] args)
        {
            AudioTracking audio = new AudioTracking();

            audio.initvoiceDetection();
            MMDeviceCollection devices = audio.getDevices();

            audio.getBackgroundNoise(ref devices); //Cogemos el sonido de fondo de donde este el men
            audio.voiceTest(ref devices);          //Le cogemos el volumen al que habla, para que saber bien cuando grita


            Console.WriteLine(devices[2].ToString());

            while (devices.Count > 0)
            {
                if ((devices[2].AudioMeterInformation.MasterPeakValue * audio.getVoiceMult()) > audio.getDefaultSpakingVolume())
                {
                    Console.WriteLine("Susto");
                }

            }

        }

        public void initvoiceDetection()
        {
            en = new MMDeviceEnumerator();
        }
        //Método para saber a que volumen habla de normal el usuario, para poder tenerlo como referenica
        //ASí si habla sin más no lo cuenta como grito.
        public void voiceTest(ref MMDeviceCollection devices)
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

        public void getBackgroundNoise(ref MMDeviceCollection devices)
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

        public MMDeviceCollection getDevices()
        {
            return en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
        }

        public float getVoiceMult() { return voiceMult; }

        public float getDefaultSpakingVolume() { return defaultspakingVolume; }

    }
}