using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using LearnKana.Domain.Kana;
using LearnKana.Domain.Study;
using LearnKana.Droid.Persistence;

namespace LearnKana.Droid.Repositories
{
    public class QuizRepository : Repository
    {
        public QuizFactory QuizFactory { get; }
        public QuizSettings QuizSettings { get; }

        public Quiz? Quiz { get; set; }

        public QuizRepository(FileManager manager) : base(manager)
        {
            QuizSettings = manager.AppDatabase.QuizSettings;
            QuizFactory = new QuizFactory(QuizSettings);
        }

        [MemberNotNull(nameof(Quiz))]
        public void GenerateNewQuiz(int count, KanaScript kanaScript, IReadOnlyList<KanaCharacter> syllabary)
        {
            Quiz = QuizFactory.GenerateQuiz(count, kanaScript, syllabary);
        }
    }
}
