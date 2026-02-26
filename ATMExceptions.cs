public class ATMException : Exception
{
    public ATMException(string message) : base(message) { }
}

public class InvalidDeviceException : ATMException
{
    public InvalidDeviceException(string message) : base(message) { }
}

public class DeviceLockedException : ATMException
{
    public DeviceLockedException(string message) : base(message) { }
}

public class NetworkConnectionException : ATMException
{
    public NetworkConnectionException(string message) : base(message) { }
}

public class InsufficientFundsException : ATMException
{
    public InsufficientFundsException(string message) : base(message) { }
}

