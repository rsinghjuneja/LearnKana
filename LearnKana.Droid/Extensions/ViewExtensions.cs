using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using Android.Content.Res;
using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

using AndroidX.Core.Content.Resources;
using AndroidX.RecyclerView.Widget;
using LearnKana.Droid.Text;
using LearnKana.Droid.Values;

namespace LearnKana.Droid.Extensions
{
    public static class ViewExtensions
    {
        public static T ThrowIfNull<T>(this T? view) where T : View
        {
            return view ?? throw new ArgumentNullException(nameof(view));
        }
        public static T ThrowIfNull<T>(this T? view, string message) where T : View
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view), message);
            return view;
        }
        public static IEnumerable<T> FindViewsById<T>(this View? view, int[] resourceIds) where T : View
        {
            ArgumentNullException.ThrowIfNull(view);

            int length = resourceIds.Length;
            for (int i = 0; i < length; i++)
            {
                int id = resourceIds[i];
                T v = view.RequireViewById<T>(id);
                yield return v;
            }
        }
        public static T Inflate<T>(this LayoutInflater inflater, int resourceId, ViewGroup? container, bool attachToRoot = false) where T : View
        {
            View? view = inflater.Inflate(resourceId, container, attachToRoot);
            ArgumentNullException.ThrowIfNull(view);
            return (T)view;
        }
        [return: NotNullIfNotNull(nameof(view))]
        public static T? SetVisible<T>(this T? view, bool visible) where T : View
        {
            if (view != null)
                view.Visibility = visible ? ViewStates.Visible : ViewStates.Invisible;
            return view;
        }

        [return: NotNullIfNotNull(nameof(view))]
        public static T? SetVisible<T>(this T? view, ViewStates state) where T : View
        {
            if (view != null)
                view.Visibility = state;
            return view;
        }
        public static T SetText<T>(this T textView, AndroidText text) where T : TextView
        {
            if (text.IsFormatted(out ISpanned? spanned))
                SetSpan(textView, spanned);
            else
                SetText(textView, text.ToString());
            return textView;
        }
        public static T SetText<T>(this T textView, string? text) where T : TextView
        {
            textView.SetText(text, TextView.BufferType.Normal);
            return textView;
        }
        public static T SetSpan<T>(this T textView, SpanBuilder builder) where T : TextView
        {
            textView.SetSpan(builder.Build());
            return textView;
        }
        public static T SetSpan<T>(this T textView, ISpanned spanned) where T : TextView
        {
            textView.SetText(spanned, TextView.BufferType.Spannable);
            if (spanned.IsClickable())
                textView.MovementMethod = LinkMovementMethod.Instance;
            return textView;
        }
        public static bool IsClickable(this ISpanned span)
        {
            Java.Lang.Object[]? spans = span.GetSpans(0, span.Length(), typeof(ClickableSpan).ToJavaClass());
            return spans?.Length > 0;
        }
        public static T SetTypeface<T>(this T textView, int resourceId, TypefaceStyle? style = null) where T : TextView
        {
            Typeface? font = ResourcesCompat.GetFont(App.Context, resourceId);
            textView.SetTypeface(font, style ?? textView.Typeface?.Style ?? TypefaceStyle.Normal);
            return textView;
        }
        public static T ClearText<T>(this T textView) where T : TextView
        {
            textView.Text = string.Empty;
            return textView;
        }
        public static T SetMargin<T>(this T view, Margin margin) where T : View
        {
            return SetMargin(view, margin.Top, margin.Right, margin.Bottom, margin.Left);
        }
        public static T SetMargin<T>(this T view, DensityPixel left, DensityPixel top, DensityPixel right, DensityPixel bottom) where T : View
        {
            if (view.LayoutParameters is ViewGroup.MarginLayoutParams layoutParams)
            {
                layoutParams.LeftMargin = left;
                layoutParams.TopMargin = top;
                layoutParams.RightMargin = right;
                layoutParams.BottomMargin = bottom;
            }
            return view;
        }
        public static T SetPadding<T>(this T view, Margin margin) where T : View
        {
            return SetPadding(view, margin.Top, margin.Right, margin.Bottom, margin.Left);
        }
        public static T SetPadding<T>(this T view, DensityPixel left, DensityPixel top, DensityPixel right, DensityPixel bottom) where T : View
        {
            view.SetPadding(left, top, right, bottom);
            return view;
        }
        public static T SetEnabled<T>(this T view, bool enabled) where T : View
        {
            view.Enabled = enabled;
            return view;
        }
        public static T SetClickable<T>(this T view, bool clickable) where T : View
        {
            view.Clickable = clickable;
            return view;
        }
        public static void SetImageAsset<T>(this T imageView, string filePath) where T : ImageView
        {
            if (App.Context?.Assets is not AssetManager assets)
                throw new NullContextException();
            Stream stream = assets.Open(filePath) ?? throw new FileNotFoundException(filePath);
            Bitmap? bitmap = BitmapFactory.DecodeStream(stream);
            imageView.SetImageBitmap(bitmap);
        }
        [return: NotNullIfNotNull(nameof(checkable))]
        public static T? SetChecked<T>(this T? checkable, bool value) where T : ICheckable
        {
            if (checkable != null)
                checkable.Checked = value;
            return checkable;
        }
        [return: NotNullIfNotNull(nameof(view))]
        public static T? SetOnFocusChangedListener<T>(this T? view, View.IOnFocusChangeListener? listener) where T : View
        {
            if (view != null)
                view.OnFocusChangeListener = listener;
            return view;
        }

        [return: NotNullIfNotNull(nameof(view))]
        public static T? SetResourceId<T>(this T? view, int id) where T : View
        {
            if (view != null)
                view.Id = id;
            return view;
        }
        public static void ShowKeyboard(this EditText? editText, ShowFlags flags = ShowFlags.Implicit)
        {
            editText?.RequestFocus();
            editText?.SelectAll();
            ShowKeyboard(editText, flags);
        }
        public static void ShowKeyboard<T>(this T? view, ShowFlags flags = ShowFlags.Implicit) where T : View
        {
            App.ShowKeyboard(view, flags);
        }
        public static void HideKeyboard<T>(this T? view, HideSoftInputFlags flags = HideSoftInputFlags.None) where T : View
        {
            App.HideKeyboard(view, flags);
        }

        public static ViewGroup? GetParent<TView>(this TView view) where TView : View
        {
            return GetParent<ViewGroup, TView>(view);
        }
        public static TParent? GetParent<TParent, TView>(this TView view) where TParent : ViewGroup where TView : View
        {
            if (view.Parent is TParent parent)
                return parent;
            return null;
        }

        public static RecyclerView SetGrid(this RecyclerView recycler, int rows)
        {
            recycler.SetLayoutManager(new GridLayoutManager(recycler.Context, rows));
            return recycler;
        }
    }
}