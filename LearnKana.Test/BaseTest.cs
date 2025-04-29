using LearnKana.Provider;

namespace LearnKana.Test
{
    [TestClass]
    public abstract class BaseTest
    {
        public KanaService KanaService { get; } = new KanaService(new DataProvider());
    }
}
