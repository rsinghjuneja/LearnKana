using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using Google.Android.Material.SwitchMaterial;

using LearnKana.Droid.MVVM.Views.Factory;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class MaterialSwitchSettingView : BaseMaterialSettingView<MaterialSwitchSettingView>
    {
        public const int SwitchId = Resource.Id.material_switch_setting;

        private readonly SwitchMaterial m_Switch;

        private string? m_SubtitleOn;
        private string? m_SubtitleOff;

        private IOnSwitchCheckedChangeListener? m_Listener;

        public MaterialSwitchSettingView(Context? context) : this(context, null) { }
        public MaterialSwitchSettingView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            ArgumentNullException.ThrowIfNull(context);
            m_Switch = new SwitchMaterial(context)
            {
                LayoutParameters = GenerateChildContainerLayoutParams(ViewFactory.WrapContent, ViewFactory.WrapContent)
            }.SetResourceId(SwitchId);
            AddViewToChildContainer(m_Switch);
        }
        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            SetOnClickListener(this);
            m_Switch.CheckedChange += Switch_CheckedChange;
        }
        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            SetOnClickListener(null);
            m_Switch.CheckedChange -= Switch_CheckedChange;
        }

        public MaterialSwitchSettingView SetSubtitle(string on, string off)
        {
            m_SubtitleOn = on;
            m_SubtitleOff = off;
            UpdateSubtitle();
            return this;
        }
        public MaterialSwitchSettingView SetSwitchChecked(bool isChecked)
        {
            m_Switch.SetChecked(isChecked);
            UpdateSubtitle();
            return this;
        }

        private void UpdateSubtitle()
        {
            SetSubtitle(m_Switch.Checked ? m_SubtitleOn : m_SubtitleOff);
            UpdateView();
        }

        public void ToggleSwitch() => m_Switch.Checked = !m_Switch.Checked;
        public void SetOnSwitchCheckedChangeListener(IOnSwitchCheckedChangeListener? listener)
            => m_Listener = listener;

        public override void OnClick(View? view) => ToggleSwitch();
        private void Switch_CheckedChange(object? sender, CompoundButton.CheckedChangeEventArgs e)
        {
            m_Listener?.Switch_CheckedChange(this, e.IsChecked);
            UpdateSubtitle();
        }

        public interface IOnSwitchCheckedChangeListener
        {
            public void Switch_CheckedChange(MaterialSwitchSettingView? view, bool isChecked);
        }
    }
}
