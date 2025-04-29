using LearnKana.Domain.Kana;

namespace LearnKana.Droid.Persistence
{
    public class Settings
    {
        public KanaScript KanaChartScript { get; set; } = KanaScript.Hiragana;
    }
}
