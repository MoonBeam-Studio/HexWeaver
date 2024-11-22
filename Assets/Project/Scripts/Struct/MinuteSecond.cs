using System.Runtime.CompilerServices;
using System;

[System.Serializable]
public struct MinutesSeconds
{
    public int Min;
    public int Sec;

    private static readonly MinutesSeconds zero = new MinutesSeconds(0, 0);
    public int this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return index switch
            {
                0 => Min,
                1 => Sec,
                _ => throw new IndexOutOfRangeException("Invalid Vector2 index!"),
            };
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            switch (index)
            {
                case 0:
                    Min = value;
                    break;
                case 1:
                    Sec = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector2 index!");
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MinutesSeconds(int min, int sec)
    {
        this.Min = min;
        this.Sec = sec;
    }
}