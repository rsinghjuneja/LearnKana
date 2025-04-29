using System.Diagnostics;

namespace LearnKana.Shared
{
    [DebuggerStepThrough]
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        static Singleton() => m_Lock = new object();
        protected static readonly object m_Lock;

        private static T? m_Instance;
        public static T Instance
        {
            get
            {
                try
                {
                    lock (m_Lock)
                        if (m_Instance == null)
                            lock (m_Lock)
                                m_Instance ??= new T();

                    return m_Instance;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }
            }
        }
    }
}