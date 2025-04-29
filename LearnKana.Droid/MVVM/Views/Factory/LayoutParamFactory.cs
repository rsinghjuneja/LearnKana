using System;
using System.Collections.Generic;

using Android.Views;
using Android.Widget;

namespace LearnKana.Droid.MVVM.Views.Factory
{
    public class LayoutParamFactory
    {
        private readonly Dictionary<Type, Func<LayoutParameters, ViewGroup.LayoutParams>> m_Factory;

        public LayoutParamFactory()
        {
            m_Factory = new Dictionary<Type, Func<LayoutParameters, ViewGroup.LayoutParams>>
            {
                [typeof(FrameLayout)] = (layoutparameters) => new FrameLayout.LayoutParams(layoutparameters.Width, layoutparameters.Height, layoutparameters.Gravity),
                [typeof(LinearLayout)] = (layoutparameters) => new LinearLayout.LayoutParams(layoutparameters.Width, layoutparameters.Height),
            };
        }

        public ViewGroup.LayoutParams Create<T>(LayoutParameters layoutparameters)
        {
            if (m_Factory.TryGetValue(typeof(T), out var factory))
                return factory.Invoke(layoutparameters);
            return Create(layoutparameters);
        }

        public static ViewGroup.LayoutParams Create(LayoutParameters layoutparameters)
        {
            return new ViewGroup.LayoutParams(layoutparameters.Width, layoutparameters.Height);
        }
    }
}