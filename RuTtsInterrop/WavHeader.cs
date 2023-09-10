namespace RuTtsInterrop;

public class WavHeader
{
    private const int HeaderSize = 44;
    private const short FormatCode = 1;

    public string ChunkID = "RIFF";
    public int ChunkSize;
    public string Format = "WAVE";
    public string Subchunk1ID = "fmt ";
    public int Subchunk1Size = 16;
    public short AudioFormat = FormatCode;
    public short NumChannels;
    public int SampleRate;
    public int ByteRate;
    public short BlockAlign;
    public short BitsPerSample;
    public string Subchunk2ID = "data";
    public int Subchunk2Size;

    public WavHeader(int numChannels, int sampleRate, short bitsPerSample, int numSamples)
    {
        NumChannels = (short)numChannels;
        SampleRate = sampleRate;
        BitsPerSample = bitsPerSample;
        Subchunk2Size = numSamples * NumChannels * BitsPerSample / 8;

        ByteRate = SampleRate * NumChannels * BitsPerSample / 8;
        BlockAlign = (short)(NumChannels * BitsPerSample / 8);
        ChunkSize = HeaderSize + Subchunk2Size - 8;
    }

    public void WriteHeader(BinaryWriter writer)
    {
        writer.Write(ChunkID.ToCharArray());
        writer.Write(ChunkSize);
        writer.Write(Format.ToCharArray());
        writer.Write(Subchunk1ID.ToCharArray());
        writer.Write(Subchunk1Size);
        writer.Write(AudioFormat);
        writer.Write(NumChannels);
        writer.Write(SampleRate);
        writer.Write(ByteRate);
        writer.Write(BlockAlign);
        writer.Write(BitsPerSample);
        writer.Write(Subchunk2ID.ToCharArray());
        writer.Write(Subchunk2Size);
    }
}
