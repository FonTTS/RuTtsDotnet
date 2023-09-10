using System.Runtime.InteropServices;

namespace RuTtsInterrop;

[StructLayout(LayoutKind.Sequential)]
public class RuTtsConfig
{
    public int speechRate;
    public int voicePitch;
    public int intonation;
    public int generalGapFactor;
    public int commaGapFactor;
    public int dotGapFactor;
    public int semicolonGapFactor;
    public int colonGapFactor;
    public int questionGapFactor;
    public int exclamationGapFactor;
    public int intonationalGapFactor;
    public int flags;
}
