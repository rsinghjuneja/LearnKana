using System;
using System.IO;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Widget;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class GifView : ImageView
    {
        private Movie? m_Movie;
        private AnimatedImageDrawable? m_Drawable;

        public GifView(Context? context) : base(context)
        {
        }

        public GifView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
        }

        public bool IsPlaying { get; private set; }

        protected override void OnDraw(Canvas canvas)
        {
            if (!OperatingSystem.IsAndroidVersionAtLeast(28) && IsPlaying)
            {
                ArgumentNullException.ThrowIfNull(m_Movie);
                int startTime = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
                m_Movie.SetTime(((int)DateTimeOffset.Now.ToUnixTimeSeconds() - startTime) % m_Movie.Duration());
                m_Movie.Draw(canvas, 0, 0);
            }
            else
                base.OnDraw(canvas);
        }

        public void SetGif(string filePath)
        {
            if (Context?.Assets == null)
                throw new NullContextException();

            if (OperatingSystem.IsAndroidVersionAtLeast(28))
            {
                using ImageDecoder.Source source = ImageDecoder.CreateSource(Context.Assets, filePath);
                m_Drawable = ImageDecoder.DecodeDrawable(source) as AnimatedImageDrawable;
                SetImageDrawable(m_Drawable);
            }
            else
            {
                using Stream? stream = Context.Assets.Open(filePath)
                      ?? throw new FileNotFoundException();
                m_Movie = Movie.DecodeStream(stream);
            }
        }

        public void StartGif()
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(28))
            {
                ArgumentNullException.ThrowIfNull(m_Drawable);
                m_Drawable.Start();
                IsPlaying = true;
            }
            else
            {
                ArgumentNullException.ThrowIfNull(m_Movie);
                IsPlaying = true;
            }
        }

        public void StopGif()
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(28))
            {
                m_Drawable?.Stop();
                IsPlaying = false;
            }
            else
            {
                IsPlaying = false;
            }
        }

        public void Destroy()
        {
            StopGif();
            if (OperatingSystem.IsAndroidVersionAtLeast(28))
                DisposableObject.Dispose(ref m_Drawable);
            else
                DisposableObject.Dispose(ref m_Movie);
        }
    }
}