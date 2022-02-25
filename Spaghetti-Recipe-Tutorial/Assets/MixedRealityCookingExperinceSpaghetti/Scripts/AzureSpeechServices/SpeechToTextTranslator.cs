using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using System.Threading.Tasks;
using TMPro;

public class SpeechToTextTranslator : MonoBehaviour
{
    [Header("Speech SDK Credentials")]
    public string SpeechServiceAPIKey = "";
    public string SpeechServiceRegion = "";

    public TextMeshProUGUI inputText;

    private object threadLocker = new object();
    private bool waitingForReco;
    private string translatedMessage;
    private string inputMessage;

    private string translatedString = "";
    public string sourceLanguage = "en-US";

    public string completeTranslatedText = string.Empty;
    public string completeRegonizedText = string.Empty;

    public string micStatus = "on";

    private TranslationRecognizer speechTranslator;

    public static TaskCompletionSource<int> stopRecognition = new TaskCompletionSource<int>();

    #region Speech Recognition Even Handlers
    private void HandleTranslatorRecognizing(object s, TranslationRecognitionEventArgs e)
    {
        if (e.Result.Reason == ResultReason.TranslatingSpeech)
        {
            if (micStatus == "on")
            {
                return;
            }
            if (e.Result.Text != "")
            {
                inputMessage = "Recognizing..... ";

                foreach (var element in e.Result.Translations)
                {
                    translatedString = element.Value;
                }

                lock (threadLocker)
                {
                    waitingForReco = false;
                }
            }
        }
    }

    private void HandleTranslatorRecognized(object s, TranslationRecognitionEventArgs e)
    {
        if (e.Result.Reason == ResultReason.TranslatedSpeech)
        {
            if (micStatus == "on")
            {
                return;
            }
            completeRegonizedText = completeRegonizedText ==string.Empty ? e.Result.Text : completeRegonizedText + " " + e.Result.Text;
            inputMessage = completeRegonizedText;
            foreach (var element in e.Result.Translations)
            {
                translatedString = element.Value;
            }
            lock (threadLocker)
            {
                completeTranslatedText = completeTranslatedText == string.Empty ? translatedString : completeTranslatedText + " " + translatedString;
                translatedMessage = completeTranslatedText;
                waitingForReco = false;
            }
        }
    }

    private void HandleTranslatorCanceled(object s, TranslationRecognitionEventArgs e)
    {
        lock (threadLocker)
        {
            if (micStatus == "on")
            {
                return;
            }
            inputMessage = "Recognization failed";
            translatedMessage = "Network issue present,Please try again";
            waitingForReco = false;
        }
    }

    private void HandleTranslatorSessionStarted(object s, SessionEventArgs e)
    {
        lock (threadLocker)
        {
            waitingForReco = false;
        }
    }

    public void HandleTranslatorSessionStopped(object s, SessionEventArgs e)
    {
        lock (threadLocker)
        {
            waitingForReco = false;
        }
    }

    #endregion


    #region Public Function

    // Registering to Speech Recognition and Translation Events
    public async void ButtonClick()
    {
        inputMessage = "Speak.....";
        translatedMessage = "Waiting for Translation.......";
        SpeechTranslationConfig config = SpeechTranslationConfig.FromSubscription(SpeechServiceAPIKey, SpeechServiceRegion);
        config.SpeechRecognitionLanguage = sourceLanguage;
        stopRecognition = new TaskCompletionSource<int>();
        config.AddTargetLanguage(sourceLanguage);
        using (speechTranslator = new TranslationRecognizer(config))
        {
            lock (threadLocker)
            {
                waitingForReco = true;
            }
            if (speechTranslator != null)
            {
                speechTranslator.Recognizing += HandleTranslatorRecognizing;
                speechTranslator.Recognized += HandleTranslatorRecognized;
                speechTranslator.Canceled += HandleTranslatorCanceled;
                speechTranslator.SessionStarted += HandleTranslatorSessionStarted;
                speechTranslator.SessionStopped += HandleTranslatorSessionStopped;
            }
            inputMessage = "Speak.....";
            translatedMessage = "Waiting for Translation.......";
            await speechTranslator.StartContinuousRecognitionAsync().ConfigureAwait(false);

            Task.WaitAny(new[] { stopRecognition.Task });    
            await speechTranslator.StopContinuousRecognitionAsync();
        }
    }

    // Enable Mic
    public void MicOnStatus()
    {
        if (micStatus == "on")
        {
            ButtonClick();
            micStatus = "off";
        }
    }

    // Disable Mic

    public void MicOffStatus()
    {
        if (micStatus == "off")
        {
            stopRecognition.TrySetResult(0);
            micStatus = "on";
            Invoke("MicReset", 0.3f);
        }
    }

    public void MicReset()
    {
        inputMessage = completeRegonizedText != "" && completeRegonizedText != " " ? completeRegonizedText : "Voice not recognized speak again";
        translatedMessage = completeTranslatedText != "" && completeTranslatedText != " " ? completeTranslatedText: "Translation";
    }

    #endregion


    // Updates the recognized text to the UI every frame
    void Update()
    {
        lock (threadLocker)
        {         
                inputText.text = inputMessage;
        }        
    }
}