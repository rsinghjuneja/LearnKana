using LearnKana.Domain.Kana;

namespace LearnKana.Provider
{
    public class DataProvider()
    {
        public Dictionary<string, KanaCharacter> StandardSyllabary { get; } = KanaDatabase.CreateStandardDatabase();
        public Dictionary<string, KanaCharacter> DakutenSyllabary { get; } = KanaDatabase.CreateDakutenDatabase();
        public Dictionary<string, KanaCharacter> HandakutenSyllabary { get; } = KanaDatabase.CreateHandakutenDatabase();
        public Dictionary<string, KanaCharacter> YoonSyllabary { get; } = KanaDatabase.CreateYoonDatabase();
    }
}