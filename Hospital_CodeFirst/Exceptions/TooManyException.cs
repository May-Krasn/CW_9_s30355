namespace Hospital_CodeFirst.Exceptions;

public class TooManyException(string message) : Exception(message);