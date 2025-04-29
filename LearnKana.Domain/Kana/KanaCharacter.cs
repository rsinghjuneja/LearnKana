using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LearnKana.Domain.Kana
{
    [method: SetsRequiredMembers]
    [DebuggerDisplay("{romaji}")]
    public readonly struct KanaCharacter(string romaji, string hiragana, string katakana, KanaRowKey kanaRow, KanaType type)
    {
        public required string Romaji { get; init; } = romaji;
        public required string Hiragana { get; init; } = hiragana;
        public required string Katakana { get; init; } = katakana;
        public required KanaRowKey KanaRow { get; init; } = kanaRow;
        public required KanaType KanaType { get; init; } = type;

        public static KanaRowKey GetKanaRowKey(string romaji) => romaji switch
        {
            RomajiKey.A or RomajiKey.I or RomajiKey.U or RomajiKey.E or RomajiKey.O => KanaRowKey.A,
            RomajiKey.KA or RomajiKey.KI or RomajiKey.KU or RomajiKey.KE or RomajiKey.KO => KanaRowKey.KA,
            RomajiKey.SA or RomajiKey.SHI or RomajiKey.SU or RomajiKey.SE or RomajiKey.SO => KanaRowKey.SA,
            RomajiKey.TA or RomajiKey.CHI or RomajiKey.TSU or RomajiKey.TE or RomajiKey.TO => KanaRowKey.TA,
            RomajiKey.NA or RomajiKey.NI or RomajiKey.NU or RomajiKey.NE or RomajiKey.NO => KanaRowKey.NA,
            RomajiKey.HA or RomajiKey.HI or RomajiKey.FU or RomajiKey.HE or RomajiKey.HO => KanaRowKey.HA,
            RomajiKey.MA or RomajiKey.MI or RomajiKey.MU or RomajiKey.ME or RomajiKey.MO => KanaRowKey.MA,
            RomajiKey.YA or RomajiKey.YU or RomajiKey.YO => KanaRowKey.YA,
            RomajiKey.RA or RomajiKey.RI or RomajiKey.RU or RomajiKey.RE or RomajiKey.RO => KanaRowKey.RA,
            RomajiKey.WA or RomajiKey.WO => KanaRowKey.WA,
            RomajiKey.N => KanaRowKey.N,
            RomajiKey.GA or RomajiKey.GI or RomajiKey.GU or RomajiKey.GE or RomajiKey.GO => KanaRowKey.GA,
            RomajiKey.ZA or RomajiKey.JI or RomajiKey.ZU or RomajiKey.ZE or RomajiKey.ZO => KanaRowKey.ZA,
            RomajiKey.DA or RomajiKey.DJI or RomajiKey.DZU or RomajiKey.DE or RomajiKey.DO => KanaRowKey.DA,
            _ => throw new NotImplementedException(),
        };

        public string KanaFromScript(KanaScript type) => type switch
        {
            KanaScript.Hiragana => Hiragana,
            KanaScript.Katakana => Katakana,
            _ => throw new NotImplementedException()
        };

        public override string ToString() =>
            $"{Romaji} - {Hiragana} | {Katakana}";

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is KanaCharacter other)
                return Romaji == other.Romaji;
            return false;
        }

        public override int GetHashCode() => Romaji.GetHashCode();

        public static bool operator ==(KanaCharacter first, KanaCharacter second) =>
            first.Equals(second);

        public static bool operator !=(KanaCharacter first, KanaCharacter second) =>
            !first.Equals(second);
    }
}