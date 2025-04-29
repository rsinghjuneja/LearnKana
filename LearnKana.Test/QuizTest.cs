using LearnKana.Domain.Kana;
using LearnKana.Domain.Study;
using LearnKana.Provider;

namespace LearnKana.Test
{
    [TestClass]
    public class QuizTest : BaseTest
    {
        private QuizFactory QuizFactory { get; } = new QuizFactory(new QuizSettings());

        [TestMethod]
        [Ignore]
        public async Task CreateQuizTest()
        {
            List<KanaCharacter> characters = KanaService.GetKanaCharacters(KanaType.StandardTenTen).ToList();

            CancellationTokenSource source = new();
            source.CancelAfter(TimeSpan.FromSeconds(3));

            Quiz quiz;

            try
            {
                await Task.Run(() =>
                {
                    quiz = QuizFactory.GenerateQuiz(35, KanaScript.Hiragana, characters);
                }, source.Token);
            }
            catch (OperationCanceledException ex)
            {
                Assert.Fail(ex.ToString());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

            Assert.IsTrue(true);
        }
    }
}
