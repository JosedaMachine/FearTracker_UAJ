using NAudio.CoreAudioApi;
using System;
using GameTracker;
using NAudio.Wave;

namespace AudioTracking
{
    public class AudioTracker
    {
        private MMDeviceEnumerator en; //La variable que lleva los devices

        private MMDevice selectedDevice; //El microfono del usuario

        private WaveIn recorder; // Grabadora de micro

        //Quizás a esto haya que ponerle estático, no lo se
        private float defaultSpeakingVolume = 4000.0f;   //Basado en las pruebas que hemos hecho, esto da más o menos si hablas a un tono normal (Por si mi colegui no quiere hacer los test)
        private const float voiceMult = 100.0f; //Para que el volumen sean números mayores que cero


        private int speakingCont = 0;               //Las veces que ha hablado
        private const int voiceTests = 3;         //El número de veces que tiene que hablar el usuario
        private float acumVoice = 0.0f;     //La suma de todas las muestras para hacer la media
        private bool speaking = false;

        private float backgroundNoise = 15.0f;
        //TODO : Cambiarlo a tiempo
        private const int backgroundTimer = 100;    //s que nos pasamos grabando
        private int backgroundTimeRecording = 0;    //s actuales
        private float backgroundAcum = 0;

        private int screamMultiplicator = 4;   //Gritar X veces más alto de lo que hablas para que cuente como grito
        private const int speakingMultiplicator = 15; //Hablar X veces más alto de lo que suena el sonido de fondo para que lo pille
        private bool screaming = false;

        private float acumlatedVoice = 0.0f; //acumulador de la voz durante X segundos
        private int timesCalled = 0;    //Numero de veces que se llama al readInput

        //Hay que seleccionar el micro dentro de los devices
        //https://www.youtube.com/watch?v=HqZrDRwGkdI
        //static void Main(string[] args)
        //{
        //    bool screaming = false;
        //    AudioTracking audio = new AudioTracking();

        //    audio.initvoiceDetection();
        //    MMDeviceCollection devices = audio.getDevices();

        //    audio.getBackgroundNoise(ref devices); //Cogemos el sonido de fondo de donde este el men
        //    audio.voiceTest(ref devices);          //Le cogemos el volumen al que habla, para que saber bien cuando grita


        //    //Console.WriteLine(devices[4].ToString());

        //    Console.WriteLine("Funcionando");

        //    //TODO: HAcer timer para medir entre gritos
        //    while (devices.Count > 0)
        //    {
        //        float voice = devices[4].AudioMeterInformation.MasterPeakValue * audio.getVoiceMult();

        //        if (!screaming && (voice > audio.getDefaultSpakingVolume() * audio.getScreamMult()))
        //        {
        //            Console.WriteLine(voice);
        //            Console.WriteLine("Susto");
        //        }
        //        else if (screaming && voice < audio.getDefaultSpakingVolume()) screaming = false;

        //    }

        //}
        private static AudioTracker instance = null;
        public static AudioTracker GetInstance()
        {
            if (instance == null)
            {
                instance = new AudioTracker();
            }

            return instance;
        }

        public AudioTracker()
        {
            InitvoiceDetection();
        }

        public void InitvoiceDetection()
        {
            en = new MMDeviceEnumerator();
        }

        public bool IsVoiceTestOver()
        {
            return speakingCont >= voiceTests;
        }

        public bool VoiceTest(out float voiceValue)
        {
            float voice;

            voice = selectedDevice.AudioMeterInformation.MasterPeakValue;
            Console.WriteLine(voice);

            //Representa que ya esta hablando
            if ((voice * voiceMult) > backgroundNoise * speakingMultiplicator && !speaking)
            {
                acumVoice += voice;
                speakingCont++;

                speaking = true;
            }

            if (voice * voiceMult <= backgroundNoise && speaking)
            {
                speaking = false;        // Cuando se calle
            }

            if (IsVoiceTestOver())
            {
                defaultSpeakingVolume = (acumVoice / voiceTests) * voiceMult;
                Console.WriteLine(defaultSpeakingVolume);
            }

            voiceValue = voice;

            return speaking;
        }

        public void ResetMicTesting()
        {
            speakingCont = 0;
            acumVoice = 0;
            speaking = false;

            backgroundNoise = 0;
            backgroundTimeRecording = 0;
            backgroundAcum = 0;
        }

        public bool IsBackgroundNoiseRecordingFinished()
        {
            return backgroundTimeRecording >= backgroundTimer;
        }

        //Método para saber a que volumen habla de normal el usuario, para poder tenerlo como referenica
        //Así si habla sin más no lo cuenta como grito.
        public void GetBackgroundNoise()
        {
            if (recorder == null)
            {
                recorder = new WaveIn();
                recorder.StartRecording();
            }

            Console.WriteLine("Tomando audio de fondo");

            backgroundAcum += selectedDevice.AudioMeterInformation.MasterPeakValue;

            backgroundTimeRecording++;

            if (IsBackgroundNoiseRecordingFinished())
            {
                backgroundNoise = (backgroundAcum / backgroundTimer) * voiceMult;
                Console.WriteLine(backgroundNoise);
            }
        }

        public void MeasureVoice()
        {
            float voice = selectedDevice.AudioMeterInformation.MasterPeakValue * voiceMult;

            if (!screaming && (voice > defaultSpeakingVolume * screamMultiplicator))
                screaming = true;
            else if (screaming && voice < defaultSpeakingVolume)
                screaming = false;
        }

        public MMDeviceCollection GetDevices()
        {
            return en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
        }

        public void SetSelectedDevice(object device)
        {
            selectedDevice = (MMDevice)device;
        }

        public float GetVoiceMult() { return voiceMult; }

        public float GetDefaultSpakingVolume() { return defaultSpeakingVolume; }

        public int GetScreamMult() { return screamMultiplicator; }

        public void sendEventAndRecord()
        {
            TrackerSystem ts = TrackerSystem.GetInstance();
            MicrophoneEvent microphone = ts.CreateEvent<MicrophoneEvent>();
            microphone.setDecibels(acumlatedVoice / timesCalled);
            ts.trackEvent(microphone);

            timesCalled = 0;
            acumlatedVoice = 0;
        }

        public void SetScareMultiplyer(decimal value)
        {
            screamMultiplicator = (int)value;
        }

        public void ReadInput()
        {
            acumlatedVoice += selectedDevice.AudioMeterInformation.MasterPeakValue * GetVoiceMult();
            timesCalled++;
        }
        public float scareThreshold()
        {
            return defaultSpeakingVolume * screamMultiplicator;
        }
    }
}