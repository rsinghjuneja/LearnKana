using LearnKana.Domain;
using LearnKana.Domain.Kana;
using LearnKana.Shared.Extensions;

namespace LearnKana.Provider
{
    public class KanaService : IKanaService
    {
        public KanaService(DataProvider provider)
        {
            StandardSyllabary = provider.StandardSyllabary;
            DakutenSyllabary = provider.DakutenSyllabary;
            HandakutenSyllabary = provider.HandakutenSyllabary;
            YoonSyllabary = provider.YoonSyllabary;

            KanaSyllabary = [];
            StandardSyllabary.Values.ForEachElement(x => KanaSyllabary.Add(x.Romaji, x));
            DakutenSyllabary.Values.ForEachElement(x => KanaSyllabary.Add(x.Romaji, x));
            HandakutenSyllabary.Values.ForEachElement(x => KanaSyllabary.Add(x.Romaji, x));
            YoonSyllabary.Values.ForEachElement(x => KanaSyllabary.Add(x.Romaji, x));
        }

        /// <summary>
        /// Contains the entire syllabary including tenten and yoon.
        /// </summary>
        public Dictionary<string, KanaCharacter> KanaSyllabary { get; }
        public Dictionary<string, KanaCharacter> StandardSyllabary { get; }
        public Dictionary<string, KanaCharacter> DakutenSyllabary { get; }
        public Dictionary<string, KanaCharacter> HandakutenSyllabary { get; }
        public Dictionary<string, KanaCharacter> YoonSyllabary { get; }

        public IEnumerable<KanaCharacter> GetKanaCharacters(KanaType flags = KanaType.Standard)
        {
            return KanaSyllabary.Values
                .Where(x => flags.HasFlag(x.KanaType));
        }

        public KanaRow GetRow(KanaRowKey row, KanaType type)
        {
            return new KanaRow(KanaSyllabary.Values.Where(x => x.KanaRow == row && type.HasFlag(x.KanaType)).ToArray());
        }
    }
}