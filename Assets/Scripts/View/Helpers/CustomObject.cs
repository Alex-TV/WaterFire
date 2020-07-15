
namespace View.Helpers
{
    public class CustomObject
    {
        public static CustomObject Empty { get; } = new CustomObject(null);
        private readonly object _value;

        public CustomObject(object value)
        {
            _value = value;
        }

        public T GetValue<T>()
        {
            return (T) _value;
        }
    }
}
