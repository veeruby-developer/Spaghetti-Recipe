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

    [Header("Input and Translate Language TextMeshProUGUI")]
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI translatedText;

    [Header("Game Object to Instantiate")]
    public GameObject StickNote_prefab;
    private GameObject StickNote;
    //Parent object to instantiate stick note
    public GameObject Background;

    private object threadLocker = new object();
    private bool waitingForReco;
    private string translatedMessage;
    private string inputMessage;

    private string translatedString = "";
    private string sourceLanguage = "en-US";

    private string completeTranslatedText = string.Empty;
    private string completeRegonizedText = string.Empty;

    private string micStatus = "on";

    private TranslationRecognizer speechTranslator;

    public static TaskCompletionSource<int> stopRecognition = new TaskCompletionSource<int>();

    public enum TranslateToLanguage{Russian, German, Chinese};

    public TranslateToLanguage TargetLanguage = TranslateToLanguage.Russian;

    private string toLanguage = "";

    void Start()
    {
        switch(TargetLanguage)
        {
            case TranslateToLanguage.Russian:
                toLanguage = "ru-Ru";
                break;
            case TranslateToLanguage.German:
                toLanguage = "de-DE";
                break;
            case TranslateToLanguage.Chinese:
                toLanguage = "zh-HK";
                break;
        }
    }

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
        config.AddTargetLanguage(toLanguage);
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
                translatedText.text = translatedMessage;
        }        
    }

    //Notepad instantiation
    public void instantiateNotePad()
    {
        StickNote = Instantiate(StickNote_prefab, Background.transform.position,StickNote_prefab.transform.rotation);
        StickNote.transform.SetParent(Background.transform);
        StickNote.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

        GameObject SaveNoteSpeak = StickNote.gameObject.transform.GetChild(0).gameObject;
        SaveNoteSpeak.GetComponent<TextMeshProUGUI>().text = inputMessage;
        
        GameObject SaveNoteTranslated = StickNote.gameObject.transform.GetChild(1).gameObject;
        SaveNoteTranslated.GetComponent<TextMeshProUGUI>().text = translatedMessage;
    }
}