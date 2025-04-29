using System;

using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using LearnKana.Domain.Kana;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class KanaCharacterView : FrameLayout, IKanaCharacterView
    {
        private readonly TextView m_TextViewCharacter;
        private readonly TextView m_TextViewRomaji;
        private readonly TextView m_TextViewKanaAlternative;

        public KanaCharacterView(Context context) : base(context)
        {
            Inflate(context, Resource.Layout.layout_kana_character, this);
            m_TextViewCharacter = RequireViewById<TextView>(Resource.Id.textview_kana);
            m_TextViewRomaji = RequireViewById<TextView>(Resource.Id.textview_romaji);
            m_TextViewKanaAlternative = RequireViewById<TextView>(Resource.Id.textview_kana_alternative);
        }
        public KanaCharacterView(Context context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.layout_kana_character, this);
            m_TextViewCharacter = RequireViewById<TextView>(Resource.Id.textview_kana);
            m_TextViewRomaji = RequireViewById<TextView>(Resource.Id.textview_romaji);
            m_TextViewKanaAlternative = RequireViewById<TextView>(Resource.Id.textview_kana_alternative);
        }

        public KanaCharacter? KanaCharacter { get; private set; }
        public KanaScript? KanaScript { get; private set; }

        public void SetKanaCharacter(KanaCharacter character, KanaScript type)
        {
            m_TextViewRomaji.SetText(character.Romaji);
            if (type == Domain.Kana.KanaScript.Hiragana)
            {
                m_TextViewCharacter.SetText(character.Hiragana);
                m_TextViewKanaAlternative.SetText(character.Katakana);
            }
            else if (type == Domain.Kana.KanaScript.Katakana)
            {
                m_TextViewCharacter.SetText(character.Katakana);
                m_TextViewKanaAlternative.SetText(character.Hiragana);
            }
            else throw new NotImplementedException($"KanaCharacterView cannot handle the KanaScript value: {type.ToFullNameString()}");

            KanaCharacter = character;
            KanaScript = type;
        }
        public string? GetKanaString()
        {
            if (KanaCharacter.HasValue && KanaScript.HasValue)
                return KanaCharacter.Value.KanaFromScript(KanaScript.Value);
            return null;
        }
        public void SetKanaTextSize(int sp)
        {
            m_TextViewCharacter.SetTextSize(ComplexUnitType.Sp, sp);
        }
        public void SetRomajiVisible(bool value)
        {
            m_TextViewRomaji.SetVisible(value == true ? ViewStates.Visible : ViewStates.Gone);
        }
        public void SetAlternateKanaVisible(bool value)
        {
            m_TextViewKanaAlternative.SetVisible(value == true ? ViewStates.Visible : ViewStates.Gone);
        }
        public void ClearKana()
        {
            m_TextViewRomaji.SetText(string.Empty);
            m_TextViewCharacter.SetText(string.Empty);
            m_TextViewKanaAlternative.SetText(string.Empty);
            KanaCharacter = null;
            KanaScript = null;
        }
        public void UpdateKanaCharacter(KanaScript type)
        {
            if (KanaCharacter == null)
                return;

            SetKanaCharacter(KanaCharacter.Value, type);
        }
    }
}