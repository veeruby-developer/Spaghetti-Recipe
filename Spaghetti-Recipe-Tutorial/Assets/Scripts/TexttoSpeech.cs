using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using CognitiveServicesTTS;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
/// using UniLang;
using Microsoft.Azure.SpatialAnchors.Unity;

public class TexttoSpeech : MonoBehaviour
{
    /// private PageNavigator pageNavigator;

    public AudioSource audioSource;
    public Text inputText;
    /// public GameObject loading;
    /// private SpeechToTextTranslator speechToTextTranslator;

    public VoiceName voiceName = VoiceName.enAUCatherine;
    public static string CompleteText = string.Empty;
    private string textToTextTranslated;

    public void Start()
    {
        /// pageNavigator = gameObject.GetComponent<PageNavigator>();
        /// speechToTextTranslator = gameObject.GetComponent<SpeechToTextTranslator>();
    }
    public async void SpeechPlayback()
    {
        string msg = inputText.text;
        // Required to insure non-blocking code in the main Unity UI thread.
        await Task.Run(() => SpeakWithSDKPlugin(msg));
        StartCoroutine(WaitForSpeaker());

    }
    public async void SpeechPlaybackTemp(string msg)
    {
        await Task.Run(() => SpeakWithSDKPlugin(msg));
        StartCoroutine(WaitForSpeaker());

    }

    IEnumerator WaitForSpeaker()
    {
        //loading.SetActive(true);
        //loading.GetComponentInChildren<Text>().text = "Please wait as we convert text to speech for you...";
        while (!audioSource.isPlaying)
        {
            yield return null;
        }
        //loading.SetActive(false);
        StartCoroutine(ResetSpeaker());
    }

    IEnumerator ResetSpeaker()
    {
        while (audioSource.isPlaying)
        {
            /// if (PageNavigator.speakerStatus == "on")
            /// {
            audioSource.Stop();
            //pageNavigator.SpeakerButtonOff(); 
            /// }
            //audioSource.Stop(); ;
            yield return null;
        }
        /// if (SpeechToTextTranslator.micStatus == "on")
        /// 
        ///  (!(SceneControl.deviceName == "mobile" && MasterPcChatManager.Master == "yes"))
        /// .SpeakerButtonOff();
        /// 
        //audioSource.Stop();
        /// SpeechToTextTranslator.speaker = "off";
    }

    public void SpeakWithSDKPlugin(string message)
    {
        Synthesize cortana = new Synthesize();
        SpeechSynthesizer synthesizer;

        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("d30f369d26f44b5887f964358928ef8b", "eastus2");
        //config.SpeechSynthesisLanguage = cortana.GetVoiceLocale(voiceName);

        /// config.SpeechSynthesisVoiceName = SpeechToTextTranslator.selectedVoice;

        /// config.SpeechSynthesisLanguage = SpeechToTextTranslator.targetLanguage;
        //config.SpeechRecognitionLanguage = "en-US";
        //config.SpeechSynthesisVoiceName = cortana.ConvertVoiceNametoString(voiceName);

        // Creates an audio out stream.
        //var stream = AudioOutputStream.CreatePullStream();
        // Creates a speech synthesizer using audio stream output.
        //var streamConfig = AudioConfig.FromStreamOutput(stream);
        synthesizer = new SpeechSynthesizer(config, null);


        Task<SpeechSynthesisResult> Speaking = synthesizer.SpeakTextAsync(message);
        Debug.Log(message);
        // We can't await the task without blocking the main Unity thread, so we'll call a coroutine to
        // monitor completion and play audio when it's ready.
        UnityDispatcher.InvokeOnAppThread(() => {
            StartCoroutine(WaitAndPlayRoutineSDK(Speaking));
        });
    }

    private IEnumerator WaitAndPlayRoutineSDK(Task<SpeechSynthesisResult> speakTask)
    {
        // Yield control back to the main thread as long as the task is still running
        while (!speakTask.IsCompleted)
        {
            yield return null;
        }

        var result = speakTask.Result;
        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            var audiodata = result.AudioData;
            Debug.Log($"Speech synthesized for text and the audio was written to output stream.");

            int sampleCount = 0;
            int frequency = 18000;
            var unityData = FixedRAWAudioToUnityAudio(audiodata, 1, 16, out sampleCount);

            // Convert data to a Unity audio clip
            if (sampleCount > 0)
            {
                Debug.Log($"Converting audio data of size {unityData.Length} to Unity audio clip with {sampleCount} samples at frequency {frequency}.");
                var clip = ToClip("Speech", unityData, sampleCount, frequency);

                // Set the source on the audio clip
                audioSource.clip = clip;
                Debug.Log($"Trigger playback of audio clip on AudioSource.");
                // Play audio
                audioSource.Play();
            }
            else
            {
                /// loading.GetComponentInChildren<Text>().text = "Sorry unable to convert, please try again";
                Invoke("LoadingOff", 3f);
                /// pageNavigator.SpeakerButtonOff();
            }
        }
        else if (result.Reason == ResultReason.Canceled)
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            Debug.Log($"CANCELED: Reason={cancellation.Reason}");

            if (cancellation.Reason == CancellationReason.Error)
            {
                Debug.Log($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                Debug.Log($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                Debug.Log($"CANCELED: Did you update the subscription info?");
            }
        }
    }

    /// public void LoadingOff()
    /// {
    ///     loading.SetActive(false);
    /// }
    private static float[] FixedRAWAudioToUnityAudio(byte[] wavAudio, int channelCount, int resolution, out int sampleCount)
    {
        // Pos is now positioned to start of actual sound data.
        int bytesPerSample = resolution / 8; // e.g. 2 bytes per sample (16 bit sound mono)
        sampleCount = wavAudio.Length / bytesPerSample;
        if (channelCount == 2) { sampleCount /= 2; }  // 4 bytes per sample (16 bit stereo)
        Debug.Log($"Audio data contains {sampleCount} samples. Starting conversion");

        // Allocate memory (supporting left channel only)
        var unityData = new float[sampleCount];

        int pos = 0;
        try
        {
            // Write to double array/s:
            int i = 0;
            while (pos < wavAudio.Length)
            {
                unityData[i] = BytesToFloat(wavAudio[pos], wavAudio[pos + 1]);
                pos += 2;
                if (channelCount == 2)
                {
                    pos += 2;
                }
                i++;
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Error occurred converting audio data to float array of size {wavAudio.Length} at position {pos}.");
        }

        return unityData;
    }

    private static AudioClip ToClip(string name, float[] audioData, int sampleCount, int frequency)
    {
        var clip = AudioClip.Create(name, sampleCount, 1, frequency, false);
        clip.SetData(audioData, 0);
        return clip;
    }
    private static float BytesToFloat(byte firstByte, byte secondByte)
    {
        // Convert two bytes to one short (little endian)
        short s = (short)((secondByte << 8) | firstByte);

        // Convert to range from -1 to (just below) 1
        return s / 32768.0F;
    }
}
