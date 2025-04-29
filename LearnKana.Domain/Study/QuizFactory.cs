using LearnKana.Domain.Kana;
using LearnKana.Shared.Extensions;

namespace LearnKana.Domain.Study
{
    public interface IQuizFactory
    {

    }

    public class QuizFactory(QuizSettings settings) : IQuizFactory
    {
        public QuizSettings Settings { get; } = settings;

        public Quiz GenerateQuiz(int count, KanaScript kanaScript, IReadOnlyList<KanaCharacter> syllabary, Random? random = default)
        {
            if (syllabary.Count < 4)
                throw new ArgumentException("The syllabary does not contain enough items. A minimum of 4 items will create a single question.");

            random ??= Random.Shared;

            List<KanaCharacter> characters = new(syllabary);

            List<Question> questions = [];

            int questionNumber = 0;

            for (int i = 0; i < count; i++)
            {
                Question question;
                questionNumber++;

                int headsOrTail = random.HeadsOrTails();
                if (headsOrTail == 0)
                    question = CreateMultiChoiceQuestion(questionNumber, syllabary, characters, kanaScript, random);
                else
                    question = CreateInputQuestion(questionNumber, syllabary, characters, kanaScript, random);
                questions.Add(question);
            }

            return new Quiz(questions);
        }

        private Question CreateMultiChoiceQuestion(int number, IReadOnlyList<KanaCharacter> syllabary, List<KanaCharacter> characters, KanaScript kanaScript, Random random)
        {
            KanaCharacter answer = syllabary[random.Next(syllabary.Count)];
            characters.Remove(answer);

            List<KanaCharacter> choices = [answer];

            int choiceCount = characters.Count > 4 ? 4 : characters.Count;
            while (choices.Count < choiceCount)
            {
                KanaCharacter choice = characters.GetRandomItem(random);
                if (choices.Contains(choice))
                    continue;
                else
                    choices.Add(choice);
            }

            choices.Shuffle();
            Question question = new Question(number, QuestionType.MultipleChoice, kanaScript, answer, choices);
            return question;
        }

        private Question CreateInputQuestion(int number, IReadOnlyList<KanaCharacter> syllabary, List<KanaCharacter> characters, KanaScript kanaScript, Random random)
        {
            KanaCharacter answer = syllabary[random.Next(syllabary.Count)];
            characters.Remove(answer);
            Question question = new Question(number, QuestionType.Input, kanaScript, answer, null);
            return question;
        }
    }
}