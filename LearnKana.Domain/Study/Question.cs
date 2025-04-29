using System.Diagnostics;

using LearnKana.Domain.Kana;

namespace LearnKana.Domain.Study
{
    [DebuggerDisplay("{ToString()}")]
    public class Question(int questionNumber, QuestionType questionType, KanaScript kanaScript, KanaCharacter answer, List<KanaCharacter>? choices)
    {
        public int QuestionNumber { get; } = questionNumber;
        public QuestionType QuestionType { get; } = questionType;
        public KanaScript KanaScript { get; } = kanaScript;
        public KanaCharacter Answer { get; } = answer;
        public List<KanaCharacter>? Choices { get; } = choices;

        public string GetKanaAnswerString() => Answer.KanaFromScript(KanaScript);

        public override string ToString()
        {
            if (Choices?.Count > 0)
                return $"[{Answer.Romaji}] {QuestionType} - {Answer.KanaFromScript(KanaScript)}: ({Choices[0].Romaji}, {Choices[1].Romaji}, {Choices[2].Romaji}, {Choices[3].Romaji}),";
            else
                return $"[{Answer.Romaji}] {QuestionType} - {Answer.KanaFromScript(KanaScript)}: ({Answer.Romaji})";
        }
    }
}