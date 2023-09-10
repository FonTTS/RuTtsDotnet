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
        byte[] temp = new byte[size];
for (int i = 0; i < size; i++)
        {
            temp[i] = (byte)(samples[i] * 100 / 100);
            temp[i] = temp[i]  ^= 128;
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
        using (stream = new MemoryStream())
        {
            RuTts.Transfer(config, koiText, waveConsumer);
            var sizeInBytes = (int)stream.Length;
            WavHeader wavHeader = new WavHeader(1, 10000, 8, sizeInBytes);
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
                }
            }
        }
        Console.ReadLine();

    }
}