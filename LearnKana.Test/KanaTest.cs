using LearnKana.Domain.Kana;
using LearnKana.Provider;
using LearnKana.Shared.Extensions;

namespace LearnKana.Test
{
    [TestClass]
    public class KanaTest : BaseTest
    {
        [TestMethod]
        public void KanaCharacterComparisonTest()
        {
            KanaCharacter character = KanaService.KanaSyllabary.Select(x => x.Value).ToList().GetRandomItem();

            KanaCharacter first = character;
            KanaCharacter second = character;

            Assert.AreEqual(first, second);
            Assert.IsTrue(first == second);
        }
    }
}
