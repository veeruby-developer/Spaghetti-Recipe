using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using System.Threading.Tasks;

public class SpeechToTextTranslator : MonoBehaviour
{
    public Text InputTextField;

    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    private string inputMessage;
    private TranslationRecognizer translator;

    private string translatedString = "";

    public static string targetLanguage = "en-US";
    public static string sourceLanguage = "en-US";

    public static string completeTranslatedText = string.Empty;
    public static string completeRegonizedText = string.Empty;

    public static string completeTranslatedTextForHistory = string.Empty;
    public static string completeRecognizedTextForHistory = string.Empty;

    public static string micStatus = "on";

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
            completeRecognizedTextForHistory = completeRecognizedTextForHistory == string.Empty ? e.Result.Text : completeRecognizedTextForHistory + " " + e.Result.Text;
            foreach (var element in e.Result.Translations)
            {
                translatedString = element.Value;
            }
            lock (threadLocker)
            {
                completeTranslatedText = completeTranslatedText == string.Empty ? translatedString : completeTranslatedText + " " + translatedString;
                completeTranslatedTextForHistory = completeTranslatedTextForHistory == string.Empty ? translatedString : completeTranslatedTextForHistory + " " + translatedString;
                message = completeTranslatedText;
                waitingForReco = false;
            }
        }
    }
    public void Reset()
    {
        stopRecognition.TrySetResult(0);
        micStatus = "on";
        inputMessage = "Click mic button";
        message = "Translation";
        completeRegonizedText = string.Empty;
        completeTranslatedText = string.Empty;
        completeRecognizedTextForHistory = string.Empty;
        completeTranslatedTextForHistory = string.Empty;
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
            message = "Network issue present,Please try again";
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
    public static TaskCompletionSource<int> stopRecognition = new TaskCompletionSource<int>();

    public async void ButtonClick()
    {
        inputMessage = "Speak.....";
        message = "Waiting for Translation.......";
        SpeechTranslationConfig config = SpeechTranslationConfig.FromSubscription("d30f369d26f44b5887f964358928ef8b", "eastus2");
        config.SpeechRecognitionLanguage = sourceLanguage;
        stopRecognition = new TaskCompletionSource<int>();
        config.AddTargetLanguage(targetLanguage);
        using (translator = new TranslationRecognizer(config))
        {
            lock (threadLocker)
            {
                waitingForReco = true;
            }

            if (translator != null)
            {
                translator.Recognizing += HandleTranslatorRecognizing;
                translator.Recognized += HandleTranslatorRecognized;
                translator.Canceled += HandleTranslatorCanceled;
                translator.SessionStarted += HandleTranslatorSessionStarted;
                translator.SessionStopped += HandleTranslatorSessionStopped;
            }
            inputMessage = "Speak.....";
            message = "Waiting for Translation.......";
            await translator.StartContinuousRecognitionAsync().ConfigureAwait(false);

            Task.WaitAny(new[] { stopRecognition.Task });    
            await translator.StopContinuousRecognitionAsync();

        }
    }

    public void MicOnStatus()
    {
        if (micStatus == "on")
        {
            ButtonClick();
            micStatus = "off";
        }
    }
    public void MicOffStatus()
    {
        if (micStatus == "off")
        {
            stopRecognition.TrySetResult(0);
            micStatus = "on";
            Invoke("MicReset",0.3f);
        }   
    }

    public void MicReset()
    {
        inputMessage = completeRegonizedText != "" && completeRegonizedText != " " ? completeRegonizedText : "Voice not recognized speak again";
        message = completeTranslatedText != "" && completeTranslatedText != " " ? completeTranslatedText: "Translation";
    }

    void Update()
    {
        lock (threadLocker)
        {         
                InputTextField.text = inputMessage;
        }        
    }
}