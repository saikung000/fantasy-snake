
public class Singleton<T> where T : class, new()
{
    public static T Instance
    {
        get
        {
            return Nested.instance;
        }
    }

    protected Singleton()
    {
    }

    
    class Nested
    {
        
        static Nested()
        {
        }
        
        internal static readonly T instance = new T();
    }
}