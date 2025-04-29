using Android.Content;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;

using LearnKana.Droid.MVVM.Views.Factory;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class MaterialEditTextSettingView : BaseMaterialSettingView<MaterialEditTextSettingView>
    {
        private readonly EditText m_EditText;
        public MaterialEditTextSettingView(Context? context) : this(context, null) { }
        public MaterialEditTextSettingView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            m_EditText = new EditText(context)
            {
                LayoutParameters = GenerateChildContainerLayoutParams(ViewFactory.WrapContent, ViewFactory.WrapContent)
            };
            AddViewToChildContainer(m_EditText);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            m_EditText.SetOnClickListener(this);
        }
        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            m_EditText.SetOnClickListener(null);
        }

        public MaterialEditTextSettingView SetText(string? text)
        {
            m_EditText.SetText(text);
            return this;
        }
        public MaterialEditTextSettingView SetInputType(InputTypes type)
        {
            m_EditText.InputType = type;
            return this;
        }
        public MaterialEditTextSettingView AddTextChangedListener(ITextWatcher? watcher)
        {
            m_EditText.AddTextChangedListener(watcher);
            return this;
        }

        public override void OnClick(View? view)
        {
            if (view == m_EditText)
                m_EditText.SelectAll();
        }
    }
}
