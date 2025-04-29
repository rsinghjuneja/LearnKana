using LearnKana.Domain.Kana;

namespace LearnKana.Domain
{
    public interface IKanaService
    {
        public Dictionary<string, KanaCharacter> KanaSyllabary { get; }
        public Dictionary<string, KanaCharacter> StandardSyllabary { get; }
        public Dictionary<string, KanaCharacter> DakutenSyllabary { get; }
        public Dictionary<string, KanaCharacter> HandakutenSyllabary { get; }
        public Dictionary<string, KanaCharacter> YoonSyllabary { get; }

        public IEnumerable<KanaCharacter> GetKanaCharacters(KanaType flags);
        public KanaRow GetRow(KanaRowKey row, KanaType type);
    }
}