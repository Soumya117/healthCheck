[Serializable]
class InvalidValueException : Exception
{
    public InvalidValueException() { }

    public InvalidValueException(string message)
        : base(message) { }
}