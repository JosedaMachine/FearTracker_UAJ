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

        //TODO : Cambiarlo a tiempo
        int backgroundTimer = 25000;    //Iteraciones que nos pasamos grabando, si queremos ponerlo como tiempo pues hay que ver cuantas iteraciones hace por segundo

        int screamMultiplicator = 17;   //Tienes que gritar 17 veces más alto de lo que hablas para que cuente como grito

        int speakingMultiplicator = 15; //Tienes que hablar 15 veces más alto de lo que suena el sonido de fondo para que lo pille

        //Hay que seleccionar el micro dentro de los devices
        //https://www.youtube.com/watch?v=HqZrDRwGkdI
        static void Main(string[] args)
        {
            bool screaming = false;
            AudioTracking audio = new AudioTracking();

            audio.initvoiceDetection();
            MMDeviceCollection devices = audio.getDevices();

            audio.getBackgroundNoise(ref devices); //Cogemos el sonido de fondo de donde este el men
            audio.voiceTest(ref devices);          //Le cogemos el volumen al que habla, para que saber bien cuando grita


            //Console.WriteLine(devices[4].ToString());

            Console.WriteLine("Funcionando");

            //TODO: HAcer timer para medir entre gritos
            while (devices.Count > 0)
            {
                float voice = devices[4].AudioMeterInformation.MasterPeakValue * audio.getVoiceMult();

                if (!screaming && (voice > audio.getDefaultSpakingVolume() * audio.getScreamMult()))
                {
                    Console.WriteLine(voice);
                    Console.WriteLine("Susto");
                }
                else if (screaming && voice < audio.getDefaultSpakingVolume()) screaming = false;

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

            Console.WriteLine("Habla");

            while (speakingCont < voiceTests)
            {
                voice = devices[4].AudioMeterInformation.MasterPeakValue;

                //Representa que ya esta hablando
                if ((voice * voiceMult) > backgroundNoise * speakingMultiplicator && !speaking)
                {
                    samples[speakingCont] = voice * voiceMult;
                    acumVoice += voice;
                    speakingCont++;

                    speaking = true;
                }
                else if (voice * voiceMult <= backgroundNoise && speaking)
                {
                    speaking = false;        //Cuando se calle
                    Console.WriteLine("Habla de nuevo");
                }

            }

            defaultspakingVolume = (acumVoice / voiceTests) * voiceMult;
            Console.WriteLine(defaultspakingVolume);

        }

        public void getBackgroundNoise(ref MMDeviceCollection devices)
        {
            int timeRecording = 0;
            float backgroundAcum = 0.0f;

            Console.WriteLine("Tomando audio de fondo");

            while (timeRecording < backgroundTimer)
            {
                backgroundAcum += devices[4].AudioMeterInformation.MasterPeakValue;

                timeRecording++;
            }

            backgroundNoise = (backgroundAcum / backgroundTimer) * voiceMult;

            Console.WriteLine(backgroundNoise);
        }

        public MMDeviceCollection getDevices()
        {
            return en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
        }

        public float getVoiceMult() { return voiceMult; }

        public float getDefaultSpakingVolume() { return defaultspakingVolume; }

        public int getScreamMult() { return screamMultiplicator; }

    }
}