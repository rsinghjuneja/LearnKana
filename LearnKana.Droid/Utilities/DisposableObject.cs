using System;

namespace LearnKana.Droid.Utilities
{
    public abstract class DisposableObject : IDisposable
    {
        public static void Dispose<T>(ref T? disposable, bool nullify = true) where T : class, IDisposable
        {
            disposable?.Dispose();
            if (nullify)
                disposable = null;
        }

        protected bool m_Disposed;
        protected abstract void OnDispose();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (m_Disposed)
                return;
            if (disposing)
                OnDispose();

            m_Disposed = true;
        }

        ~DisposableObject()
        {
            Dispose(false);
        }
    }
}
