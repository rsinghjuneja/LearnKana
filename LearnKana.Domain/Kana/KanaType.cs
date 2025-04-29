namespace LearnKana.Domain.Kana
{
    [Flags]
    public enum KanaType
    {
        None = 0,
        Standard = 1,
        Dakuten = 2,
        Handakuten = 4,
        Yoon = 8,
        StandardTenTen = Standard | Dakuten | Handakuten
    }
}
