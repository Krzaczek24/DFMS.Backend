namespace DFMS.Database.Tools
{
    public class Specifiable<T>
    {
        private T _value = default(T);
        public virtual T Value
        {
            get => _value;
            set
            {
                _value = value;
                IsSpecified = true;
            }
        }

        public virtual bool IsSpecified { get; private set; } = false;

        public void Unspecify()
        {
            _value = default(T);
            IsSpecified = false;
        }

        public static Specifiable<T> Unspecified { get; } = new Specifiable<T>();
        public static Specifiable<T> Specified { get; } = new Specifiable<T>() { IsSpecified = true };

        public static implicit operator Specifiable<T>(T t) => new Specifiable<T>() { Value = t };
    }
}