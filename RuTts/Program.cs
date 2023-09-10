using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using RuTtsInterrop;

public class Program
{
    static MemoryStream? stream;

    static int WaveConsumerCallback(IntPtr buffer, int size, IntPtr userData)
    {
        byte[] samples = new byte[size];
       Marshal.Copy(buffer, samples, 0, samples.Length);
        byte[] temp = new byte[size * 2];
        for (int i = 0; i < size; i++)
        {
            sbyte sample8bit = (sbyte)samples[i];
            short sample16bit = (short)(sample8bit << 8);
            temp[i * 2] = (byte)(sample16bit & 0xFF);
            temp[i * 2 + 1] = (byte)((sample16bit >> 8) & 0xFF);
        }
        stream.Write(temp, 0, temp.Length);
        return 0;
    }

    static void Main(string[] args)
    {
RuTtsConfig config = new RuTtsConfig();
        RuTts.ConfigInit(config);
config.speechRate = 425;
        config.intonation = 0;
        config.voicePitch = 40;
        config.generalGapFactor = 0;
        RuTts.RuTtsCallback waveConsumer = WaveConsumerCallback;
        string text = "Тестирование синтеза речи!";
        string koiText = Utils.ConvertUtf8ToKoi8r(text);
        int sampleWidth = 2;
        using (stream = new MemoryStream())
        {
            RuTts.Transfer(config, koiText, waveConsumer);
            var sizeInBytes = (int)stream.Length;
            WavHeader wavHeader = new WavHeader(1, 10000, 16, sizeInBytes / sampleWidth);
            using (MemoryStream wStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(wStream))
                {
                    wavHeader.WriteHeader(writer);
                    stream.Position = 0;
                    stream.CopyTo(wStream);
                    var player = new SoundPlayer(wStream);
                    wStream.Position = 0;
                    player.Play();
                    using (var file = File.Create("file.wav"))
                    {
                        wStream.Position = 0;
                        wStream.CopyTo(file);
                    }
                }
            }
        }
        Console.ReadLine();

    }
}