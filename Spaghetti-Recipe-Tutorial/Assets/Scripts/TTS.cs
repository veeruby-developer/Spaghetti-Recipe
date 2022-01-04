using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

public class TTS 
{
    static async Task Main()
    {
        await SynthesizeAudioAsync();
    }
    //static async Task SynthesizeAudioAsync()
    //{
    //    var config = SpeechConfig.FromSubscription("a0d0cfba77c84a7e95383e99a90495c0", "centralindia");
    //    // Note: if only language is set, the default voice of that language is chosen.
    //    config.SpeechSynthesisLanguage = "de-DE"; // e.g. "de-DE"
    //                                                                  // The voice setting will overwrite language setting.
    //                                                                  // The voice setting will not overwrite the voice element in input SSML.
    //    config.SpeechSynthesisVoiceName = "<your-wanted-voice>";
    //}
    static async Task SynthesizeAudioAsync()
    {
        var config = SpeechConfig.FromSubscription("a0d0cfba77c84a7e95383e99a90495c0", "centralindia");
        using var synthesizer = new SpeechSynthesizer(config);
        await synthesizer.SpeakTextAsync("Synthesizing directly to speaker output.");
    }
}