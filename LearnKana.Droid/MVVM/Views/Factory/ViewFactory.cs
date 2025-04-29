using System;
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;
using LearnKana.Droid.MVVM.Views.Toolbars;
using LearnKana.Shared;

namespace LearnKana.Droid.MVVM.Views.Factory
{
    public partial class ViewFactory(Context? context)
    {
        private readonly Context? m_Context = context;

        public TView Create<TView, TViewParent>(LayoutParameters layoutparameters, int id = 0) where TView : View where TViewParent : View
        {
            return ViewFactory.Create<TView, TViewParent>(m_Context, layoutparameters, id);
        }
        public TView Create<TView>(LayoutParameters layoutparameters, int id = 0) where TView : View
        {
            return ViewFactory.Create<TView>(m_Context, layoutparameters, id);
        }
    }

    public partial class ViewFactory
    {
        public const int MatchParent = LayoutParameters.MatchParent;
        public const int WrapContent = LayoutParameters.WrapContent;

        private static readonly Dictionary<Type, Func<Context, View>> m_Factory;
        static ViewFactory()
        {
            m_Factory = new Dictionary<Type, Func<Context, View>>
            {
                [typeof(LinearLayout)] = (context) => new LinearLayout(context),
                [typeof(TextView)] = (context) => new TextView(context),
                [typeof(Button)] = (context) => new Button(context),
                [typeof(DialogToolbarView)] = (context) => new DialogToolbarView(context),
            };
        }

        public static LayoutParamFactory LayoutParameterFactory { get; } = new();

        public static TView Create<TView, TViewParent>(Context? context, LayoutParameters layoutparameters, int id = 0) where TView : View where TViewParent : View
        {
            TView view = Create<TView>(context, id);
            view.LayoutParameters = GenerateLayoutParameters<TViewParent>(layoutparameters);
            return view;
        }
        public static TView Create<TView>(Context? context, LayoutParameters layoutparameters, int id = 0) where TView : View
        {
            TView view = Create<TView>(context, id);
            view.LayoutParameters = GenerateLayoutParameters(layoutparameters);
            return view;
        }
        private static TView Create<TView>(Context? context, int id = 0) where TView : View
        {
            ArgumentNullException.ThrowIfNull(context);
            View view;
            if (m_Factory.TryGetValue(typeof(TView), out var factory))
                view = factory.Invoke(context);
            else
            {
                view = TypeFactory.Create<TView>([context]);
                Debug.WriteLine($"The view: {typeof(TView)} did not have a factory so it was created with reflection.");
            }

            if (id == 0)
                id = View.GenerateViewId();
            view.SetResourceId(id);
            return (TView)view;
        }
        public static ViewGroup.LayoutParams GenerateLayoutParameters<T>(LayoutParameters layoutparameters)
        {
            return LayoutParameterFactory.Create<T>(layoutparameters);
        }
        public static ViewGroup.LayoutParams GenerateLayoutParameters(LayoutParameters layoutparameters)
        {
            return LayoutParamFactory.Create(layoutparameters);
        }
    }
}