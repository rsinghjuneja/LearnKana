using LearnKana.Domain.Study;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Bundles
{
    public class QuestionResultBundle(int questionNumber, QuestionStatus status) : IBundle<QuestionResultBundle>
    {
        public int QuestionNumber { get; } = questionNumber;
        public QuestionStatus Status { get; } = status;

        public Bundle ToBundle() => ToBundle(new Bundle());
        public Bundle ToBundle(Bundle bundle)
        {
            bundle.PutInt(Keys.Question, QuestionNumber);
            bundle.PutEnum(Keys.QuestionStatus, Status);
            return bundle;
        }
        public static QuestionResultBundle FromBundle(Bundle? bundle)
        {
            Arguments arguments = new Arguments(bundle);

            int question = arguments.GetInt(Keys.Question);
            QuestionStatus status = arguments.GetEnum<QuestionStatus>(Keys.QuestionStatus);

            return new QuestionResultBundle(question, status);
        }

        public static implicit operator Bundle(QuestionResultBundle bundle) => bundle.ToBundle();
    }
}
