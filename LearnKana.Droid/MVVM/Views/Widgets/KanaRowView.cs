using System;

using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using LearnKana.Domain.Kana;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class KanaRowView : LinearLayout
    {
        public KanaRowView(Context context) : base(context)
        {
            Orientation = Orientation.Horizontal;
            AddView(new KanaCharacterView(context).SetResourceId(Resource.Id.view_kana_character_1));
            AddView(new KanaCharacterView(context).SetResourceId(Resource.Id.view_kana_character_2));
            AddView(new KanaCharacterView(context).SetResourceId(Resource.Id.view_kana_character_3));
            AddView(new KanaCharacterView(context).SetResourceId(Resource.Id.view_kana_character_4));
            AddView(new KanaCharacterView(context).SetResourceId(Resource.Id.view_kana_character_5));
        }

        public KanaRowView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.layout_kana_row, this);
            InitializeView();
        }

        public KanaCharacterView[] KanaCharacters { get; } = new KanaCharacterView[5];
        public int RowLength { get; private set; }

        private void InitializeView()
        {
            KanaCharacters[0] = RequireViewById<KanaCharacterView>(Resource.Id.view_kana_character_1);
            KanaCharacters[1] = RequireViewById<KanaCharacterView>(Resource.Id.view_kana_character_2);
            KanaCharacters[2] = RequireViewById<KanaCharacterView>(Resource.Id.view_kana_character_3);
            KanaCharacters[3] = RequireViewById<KanaCharacterView>(Resource.Id.view_kana_character_4);
            KanaCharacters[4] = RequireViewById<KanaCharacterView>(Resource.Id.view_kana_character_5);
        }

        public void SetKanaRow(KanaRow row, KanaScript type)
        {
            int length = row.CharacterCount;

            switch (length)
            {
                case KanaRow.RowCountFive:
                    KanaCharacters[0].SetKanaCharacter(row.Characters[0], type);
                    KanaCharacters[1].SetKanaCharacter(row.Characters[1], type);
                    KanaCharacters[2].SetKanaCharacter(row.Characters[2], type);
                    KanaCharacters[3].SetKanaCharacter(row.Characters[3], type);
                    KanaCharacters[4].SetKanaCharacter(row.Characters[4], type);
                    break;
                case KanaRow.RowCountThree:
                    KanaCharacters[0].SetKanaCharacter(row.Characters[0], type);
                    KanaCharacters[1].ClearKana();
                    KanaCharacters[1].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[2].SetKanaCharacter(row.Characters[1], type);
                    KanaCharacters[3].ClearKana();
                    KanaCharacters[3].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[4].SetKanaCharacter(row.Characters[2], type);
                    break;
                case KanaRow.RowCountTwo:
                    KanaCharacters[0].SetKanaCharacter(row.Characters[0], type);
                    KanaCharacters[1].ClearKana();
                    KanaCharacters[1].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[2].ClearKana();
                    KanaCharacters[2].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[3].ClearKana();
                    KanaCharacters[3].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[4].SetKanaCharacter(row.Characters[1], type);
                    break;
                case KanaRow.RowCountOne:
                    KanaCharacters[0].SetKanaCharacter(row.Characters[0], type);
                    KanaCharacters[1].ClearKana();
                    KanaCharacters[1].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[2].ClearKana();
                    KanaCharacters[2].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[3].ClearKana();
                    KanaCharacters[3].GetParent<ViewGroup>().SetVisible(false);
                    KanaCharacters[4].ClearKana();
                    KanaCharacters[4].GetParent<ViewGroup>().SetVisible(false);
                    break;
                default:
                    throw new NotImplementedException();
            }

            RowLength = length;
        }

        public void UpdateKanaRow(KanaScript type)
        {
            KanaCharacters.ForEachElement(x => x.UpdateKanaCharacter(type));
        }
    }
}