using System.Runtime.InteropServices;

namespace RuTtsInterrop;

public class RuTts
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RuTtsCallback(IntPtr buffer, int size, IntPtr userData);

private static int waveBufferSize = 4096;
    private static IntPtr waveBuffer = Marshal.AllocHGlobal(waveBufferSize);
    [DllImport("ru_tts.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ru_tts_config_init")]
    public static extern void ConfigInit(RuTtsConfig config);
    [DllImport("ru_tts.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ru_tts_transfer", CharSet = CharSet.Ansi)]
    private static extern void Transfer(RuTtsConfig config, [MarshalAs(UnmanagedType.LPStr)] string text, IntPtr waveBuffer, int waveBufferSize, RuTtsCallback callback, IntPtr userData);

public static void Transfer(RuTtsConfig config, string text, RuTtsCallback callback)
    {
        Transfer(config, text, waveBuffer, waveBufferSize, callback, IntPtr.Zero);
            //Marshal.FreeHGlobal(waveBuffer);
    }

}
