using System;

using LearnKana.Domain;
using LearnKana.Domain.Kana;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Bundles
{
    public class KanaBundle : IBundle
    {
        public KanaBundle(KanaSet set) =>
            m_KanaSet = set;

        public KanaBundle(KanaCharacter character, KanaScript script) =>
            m_KanaSet = new KanaSet(character, script);


        private readonly KanaSet m_KanaSet;

        public Bundle ToBundle() => ToBundle(new Bundle());
        public Bundle ToBundle(Bundle bundle)
        {
            bundle.PutString(Keys.KanaCharacter, m_KanaSet.Character.Romaji);
            bundle.PutInt(Keys.KanaScript, (int)m_KanaSet.Script);
            return bundle;
        }

        public static string GetRomajiKey(Arguments bundle)
        {
            string key = bundle.GetString(Keys.KanaCharacter);
            return key;
        }

        public static KanaScript GetKanaScript(Arguments bundle)
        {
            int type = bundle.GetInt(Keys.KanaScript);
            return (KanaScript)type;
        }

        public static KanaSet FromBundle(Bundle? bundle, IKanaService service)
        {
            if (bundle == null)
                throw new NotImplementedException();

            string romaji = bundle.GetString(Keys.KanaCharacter).ThrowIfNull();
            KanaScript script = bundle.GetEnum<KanaScript>(Keys.KanaScript);

            KanaCharacter character = service.KanaSyllabary[romaji];

            return new KanaSet(character, script);
        }

        public static implicit operator Bundle(KanaBundle bundle) => bundle.ToBundle();
    }
}
