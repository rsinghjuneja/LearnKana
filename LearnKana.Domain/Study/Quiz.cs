namespace LearnKana.Domain.Study
{
    public class Quiz
    {
        public const int DefaultQuestions = 20;
        public const int MinQuestions = 10;
        public const int MaxQuestions = 100;

        public Quiz(List<Question> questions, int currentQuestion = 1)
        {
            Questions = questions;
            CurrentQuestion = currentQuestion;
            Results = questions
                .Select(x => x.QuestionNumber)
                .ToDictionary(x => x, x => new QuestionResult(null, QuestionStatus.NotAnswered));
        }

        public List<Question> Questions { get; }
        public Dictionary<int, QuestionResult> Results { get; init; }

        public int QuestionCount => Questions.Count;
        public int QuestionsRemaining => Questions.Count - CurrentQuestion;
        public int CurrentQuestion { get; private set; }

        public Question GetCurrentQuestion() => GetQuestion(CurrentQuestion);
        public Question GetQuestion(int question) => Questions[question - 1];
        public QuestionResult GetResult(int question) => Results[question];
        public QuestionResult GetResult(Question question) => Results[question.QuestionNumber];
        public void NextQuestion() => CurrentQuestion++;
        public void SetResult(int question, QuestionResult result)
        {
            Results[question] = result;
        }
    }
}