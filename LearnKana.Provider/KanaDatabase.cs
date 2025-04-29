using LearnKana.Domain.Kana;

namespace LearnKana.Provider;
public class KanaDatabase
{
    public static Dictionary<string, KanaCharacter> CreateStandardDatabase() => new Dictionary<string, KanaCharacter>
    {
        ["a"] = new KanaCharacter("a", "あ", "ア", KanaRowKey.A, KanaType.Standard),
        ["i"] = new KanaCharacter("i", "い", "イ", KanaRowKey.A, KanaType.Standard),
        ["u"] = new KanaCharacter("u", "う", "ウ", KanaRowKey.A, KanaType.Standard),
        ["e"] = new KanaCharacter("e", "え", "エ", KanaRowKey.A, KanaType.Standard),
        ["o"] = new KanaCharacter("o", "お", "オ", KanaRowKey.A, KanaType.Standard),

        ["ka"] = new KanaCharacter("ka", "か", "カ", KanaRowKey.KA, KanaType.Standard),
        ["ki"] = new KanaCharacter("ki", "き", "キ", KanaRowKey.KA, KanaType.Standard),
        ["ku"] = new KanaCharacter("ku", "く", "ク", KanaRowKey.KA, KanaType.Standard),
        ["ke"] = new KanaCharacter("ke", "け", "ケ", KanaRowKey.KA, KanaType.Standard),
        ["ko"] = new KanaCharacter("ko", "こ", "コ", KanaRowKey.KA, KanaType.Standard),

        ["sa"] = new KanaCharacter("sa", "さ", "サ", KanaRowKey.SA, KanaType.Standard),
        ["shi"] = new KanaCharacter("shi", "し", "シ", KanaRowKey.SA, KanaType.Standard),
        ["su"] = new KanaCharacter("su", "す", "ス", KanaRowKey.SA, KanaType.Standard),
        ["se"] = new KanaCharacter("se", "せ", "セ", KanaRowKey.SA, KanaType.Standard),
        ["so"] = new KanaCharacter("so", "そ", "ソ", KanaRowKey.SA, KanaType.Standard),

        ["ta"] = new KanaCharacter("ta", "た", "タ", KanaRowKey.TA, KanaType.Standard),
        ["chi"] = new KanaCharacter("chi", "ち", "チ", KanaRowKey.TA, KanaType.Standard),
        ["tsu"] = new KanaCharacter("tsu", "つ", "ツ", KanaRowKey.TA, KanaType.Standard),
        ["te"] = new KanaCharacter("te", "て", "テ", KanaRowKey.TA, KanaType.Standard),
        ["to"] = new KanaCharacter("to", "と", "ト", KanaRowKey.TA, KanaType.Standard),

        ["na"] = new KanaCharacter("na", "な", "ナ", KanaRowKey.NA, KanaType.Standard),
        ["ni"] = new KanaCharacter("ni", "に", "ニ", KanaRowKey.NA, KanaType.Standard),
        ["nu"] = new KanaCharacter("nu", "ぬ", "ヌ", KanaRowKey.NA, KanaType.Standard),
        ["ne"] = new KanaCharacter("ne", "ね", "ネ", KanaRowKey.NA, KanaType.Standard),
        ["no"] = new KanaCharacter("no", "の", "ノ", KanaRowKey.NA, KanaType.Standard),

        ["ha"] = new KanaCharacter("ha", "は", "ハ", KanaRowKey.HA, KanaType.Standard),
        ["hi"] = new KanaCharacter("hi", "ひ", "ヒ", KanaRowKey.HA, KanaType.Standard),
        ["fu"] = new KanaCharacter("fu", "ふ", "フ", KanaRowKey.HA, KanaType.Standard),
        ["he"] = new KanaCharacter("he", "へ", "ヘ", KanaRowKey.HA, KanaType.Standard),
        ["ho"] = new KanaCharacter("ho", "ほ", "ホ", KanaRowKey.HA, KanaType.Standard),

        ["ma"] = new KanaCharacter("ma", "ま", "マ", KanaRowKey.MA, KanaType.Standard),
        ["mi"] = new KanaCharacter("mi", "み", "ミ", KanaRowKey.MA, KanaType.Standard),
        ["mu"] = new KanaCharacter("mu", "む", "ム", KanaRowKey.MA, KanaType.Standard),
        ["me"] = new KanaCharacter("me", "め", "メ", KanaRowKey.MA, KanaType.Standard),
        ["mo"] = new KanaCharacter("mo", "も", "モ", KanaRowKey.MA, KanaType.Standard),

        ["ya"] = new KanaCharacter("ya", "や", "ヤ", KanaRowKey.YA, KanaType.Standard),
        ["yu"] = new KanaCharacter("yu", "ゆ", "ユ", KanaRowKey.YA, KanaType.Standard),
        ["yo"] = new KanaCharacter("yo", "よ", "ヨ", KanaRowKey.YA, KanaType.Standard),

        ["ra"] = new KanaCharacter("ra", "ら", "ラ", KanaRowKey.RA, KanaType.Standard),
        ["ri"] = new KanaCharacter("ri", "り", "リ", KanaRowKey.RA, KanaType.Standard),
        ["ru"] = new KanaCharacter("ru", "る", "ル", KanaRowKey.RA, KanaType.Standard),
        ["re"] = new KanaCharacter("re", "れ", "レ", KanaRowKey.RA, KanaType.Standard),
        ["ro"] = new KanaCharacter("ro", "ろ", "ロ", KanaRowKey.RA, KanaType.Standard),

        ["wa"] = new KanaCharacter("wa", "わ", "ワ", KanaRowKey.WA, KanaType.Standard),
        ["wo"] = new KanaCharacter("wo", "を", "ヲ", KanaRowKey.WA, KanaType.Standard),

        ["n"] = new KanaCharacter("n", "ん", "ン", KanaRowKey.N, KanaType.Standard),
    };

    public static Dictionary<string, KanaCharacter> CreateDakutenDatabase() => new Dictionary<string, KanaCharacter>
    {
        ["ka"] = new KanaCharacter("ga", "が", "ガ", KanaRowKey.GA, KanaType.Dakuten),
        ["ki"] = new KanaCharacter("gi", "ぎ", "ギ", KanaRowKey.GA, KanaType.Dakuten),
        ["ku"] = new KanaCharacter("gu", "ぐ", "グ", KanaRowKey.GA, KanaType.Dakuten),
        ["ke"] = new KanaCharacter("ge", "げ", "ゲ", KanaRowKey.GA, KanaType.Dakuten),
        ["ko"] = new KanaCharacter("go", "ご", "ゴ", KanaRowKey.GA, KanaType.Dakuten),

        ["sa"] = new KanaCharacter("za", "ざ", "ザ", KanaRowKey.ZA, KanaType.Dakuten),
        ["shi"] = new KanaCharacter("ji", "じ", "ジ", KanaRowKey.ZA, KanaType.Dakuten),
        ["su"] = new KanaCharacter("zu", "ず", "ズ", KanaRowKey.ZA, KanaType.Dakuten),
        ["se"] = new KanaCharacter("ze", "ぜ", "ゼ", KanaRowKey.ZA, KanaType.Dakuten),
        ["so"] = new KanaCharacter("zo", "ぞ", "ゾ", KanaRowKey.ZA, KanaType.Dakuten),

        ["ta"] = new KanaCharacter("da", "だ", "ダ", KanaRowKey.DA, KanaType.Dakuten),
        ["chi"] = new KanaCharacter("dji", "ぢ", "ヂ", KanaRowKey.DA, KanaType.Dakuten),
        ["tsu"] = new KanaCharacter("dzu", "づ", "ヅ", KanaRowKey.DA, KanaType.Dakuten),
        ["te"] = new KanaCharacter("de", "で", "デ", KanaRowKey.DA, KanaType.Dakuten),
        ["to"] = new KanaCharacter("do", "ど", "ド", KanaRowKey.DA, KanaType.Dakuten),

        ["ha"] = new KanaCharacter("ba", "ば", "バ", KanaRowKey.BA, KanaType.Dakuten),
        ["hi"] = new KanaCharacter("bi", "び", "ビ", KanaRowKey.BA, KanaType.Dakuten),
        ["fu"] = new KanaCharacter("bu", "ぶ", "ブ", KanaRowKey.BA, KanaType.Dakuten),
        ["he"] = new KanaCharacter("be", "べ", "ベ", KanaRowKey.BA, KanaType.Dakuten),
        ["ho"] = new KanaCharacter("bo", "ぼ", "ボ", KanaRowKey.BA, KanaType.Dakuten),
    };

    public static Dictionary<string, KanaCharacter> CreateHandakutenDatabase() => new Dictionary<string, KanaCharacter>
    {
        ["ha"] = new KanaCharacter("pa", "ぱ", "パ", KanaRowKey.PA, KanaType.Handakuten),
        ["hi"] = new KanaCharacter("pi", "ぴ", "ピ", KanaRowKey.PA, KanaType.Handakuten),
        ["fu"] = new KanaCharacter("pu", "ぷ", "プ", KanaRowKey.PA, KanaType.Handakuten),
        ["he"] = new KanaCharacter("pe", "ぺ", "ペ", KanaRowKey.PA, KanaType.Handakuten),
        ["ho"] = new KanaCharacter("po", "ぽ", "ポ", KanaRowKey.PA, KanaType.Handakuten),
    };

    public static Dictionary<string, KanaCharacter> CreateYoonDatabase() => new Dictionary<string, KanaCharacter>
    {
        ["kya"] = new KanaCharacter("kya", "きゃ", "キャ", KanaRowKey.KA, KanaType.Yoon),
        ["kyu"] = new KanaCharacter("kyu", "きゅ", "キュ", KanaRowKey.KA, KanaType.Yoon),
        ["kyo"] = new KanaCharacter("kyo", "きょ", "キョ", KanaRowKey.KA, KanaType.Yoon),

        ["gya"] = new KanaCharacter("gya", "ぎゃ", "ギャ", KanaRowKey.KA, KanaType.Yoon),
        ["gyu"] = new KanaCharacter("gyu", "ぎゅ", "ギュ", KanaRowKey.KA, KanaType.Yoon),
        ["gyo"] = new KanaCharacter("gyo", "ぎょ", "ギョ", KanaRowKey.KA, KanaType.Yoon),

        ["sha"] = new KanaCharacter("sha", "しゃ", "シャ", KanaRowKey.SA, KanaType.Yoon),
        ["shu"] = new KanaCharacter("shu", "しゅ", "シュ", KanaRowKey.SA, KanaType.Yoon),
        ["sho"] = new KanaCharacter("sho", "しょ", "ショ", KanaRowKey.SA, KanaType.Yoon),

        ["ja"] = new KanaCharacter("ja", "じゃ", "ジャ", KanaRowKey.SA, KanaType.Yoon),
        ["ju"] = new KanaCharacter("ju", "じゅ", "ジュ", KanaRowKey.SA, KanaType.Yoon),
        ["jo"] = new KanaCharacter("jo", "じょ", "ジョ", KanaRowKey.SA, KanaType.Yoon),

        ["cha"] = new KanaCharacter("cha", "ちゃ", "チャ", KanaRowKey.TA, KanaType.Yoon),
        ["chu"] = new KanaCharacter("chu", "ちゅ", "チュ", KanaRowKey.TA, KanaType.Yoon),
        ["cho"] = new KanaCharacter("cho", "ちょ", "チョ", KanaRowKey.TA, KanaType.Yoon),

        ["d/ja"] = new KanaCharacter("d/ja", "ぢゃ", "ヂャ", KanaRowKey.TA, KanaType.Yoon),
        ["d/ju"] = new KanaCharacter("d/ju", "ぢゅ", "ヂュ", KanaRowKey.TA, KanaType.Yoon),
        ["d/jo"] = new KanaCharacter("d/jo", "ぢょ", "ヂョ", KanaRowKey.TA, KanaType.Yoon),

        ["nya"] = new KanaCharacter("nya", "にゃ", "ニャ", KanaRowKey.NA, KanaType.Yoon),
        ["nyu"] = new KanaCharacter("nyu", "にゅ", "ニュ", KanaRowKey.NA, KanaType.Yoon),
        ["nyo"] = new KanaCharacter("nyo", "にぃ", "ニョ", KanaRowKey.NA, KanaType.Yoon),

        ["hya"] = new KanaCharacter("hya", "ひゃ", "ヒャ", KanaRowKey.HA, KanaType.Yoon),
        ["hyu"] = new KanaCharacter("hyu", "ひゅ", "ヒュ", KanaRowKey.HA, KanaType.Yoon),
        ["hyo"] = new KanaCharacter("hyo", "ひょ", "ヒョ", KanaRowKey.HA, KanaType.Yoon),

        ["bya"] = new KanaCharacter("bya", "びゃ", "ビャ", KanaRowKey.HA, KanaType.Yoon),
        ["byu"] = new KanaCharacter("byu", "びゅ", "ニュ", KanaRowKey.HA, KanaType.Yoon),
        ["byo"] = new KanaCharacter("byo", "びょ", "ニョ", KanaRowKey.HA, KanaType.Yoon),

        ["pya"] = new KanaCharacter("pya", "ぴゃ", "ピャ", KanaRowKey.HA, KanaType.Yoon),
        ["pyu"] = new KanaCharacter("pyu", "ぴゅ", "ピュ", KanaRowKey.HA, KanaType.Yoon),
        ["pyo"] = new KanaCharacter("pyo", "ぴょ", "ピョ", KanaRowKey.HA, KanaType.Yoon),

        ["mya"] = new KanaCharacter("mya", "みゃ", "ミャ", KanaRowKey.MA, KanaType.Yoon),
        ["myu"] = new KanaCharacter("myu", "みゅ", "ミュ", KanaRowKey.MA, KanaType.Yoon),
        ["myo"] = new KanaCharacter("myo", "みょ", "ミョ", KanaRowKey.MA, KanaType.Yoon),

        ["rya"] = new KanaCharacter("rya", "りゃ", "リャ", KanaRowKey.RA, KanaType.Yoon),
        ["ryu"] = new KanaCharacter("ryu", "りゅ", "リュ", KanaRowKey.RA, KanaType.Yoon),
        ["ryo"] = new KanaCharacter("ryo", "りょ", "リョ", KanaRowKey.RA, KanaType.Yoon),
    };
}