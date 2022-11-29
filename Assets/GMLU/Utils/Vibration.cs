
public static class Vibration
{
    public static void Vibrate(float amplitude, float frequency)
    {
        //if (UserData.Instance.VibrationOn)
        //{
            Lofelt.NiceVibrations.HapticPatterns.PlayEmphasis(amplitude, frequency);
        //}
    }
}
