using System.Collections;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using CognitiveServicesTTS;
using System.Threading.Tasks;
using System;
using Microsoft.Azure.SpatialAnchors.Unity;
using TMPro;

public class TexttoSpeech : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text inputText;
    public VoiceName voiceName = VoiceName.enAUCatherine;

    public async void SpeechPlayback()
    {
        string msg = inputText.text;
        // Required to insure non-blocking code in the main Unity UI thread.
        await Task.Run(() => SpeakWithSDKPlugin(msg));
    }

    public void SpeakWithSDKPlugin(string message)
    {
        Synthesize cortana = new Synthesize();
        SpeechSynthesizer synthesizer;

        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("a0d0cfba77c84a7e95383e99a90495c0", "centralindia");        
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