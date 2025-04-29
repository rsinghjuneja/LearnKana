namespace LearnKana.Domain.Kana
{
    public class KanaRow(params KanaCharacter[] characters)
    {
        public const int RowCountOne = 1;
        public const int RowCountTwo = 2;
        public const int RowCountThree = 3;
        public const int RowCountFive = 5;

        public const int StandardSyllabaryRowCount = 11;

        public int CharacterCount { get; } = characters.Length;
        public KanaCharacter[] Characters { get; } = characters;

    }
}
