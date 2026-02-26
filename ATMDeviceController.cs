public class ATMDeviceController
{
    public void processWithdrawal(string accountId, double amount)
    {
        try
        {
            withdraw(accountId, amount);
            Console.WriteLine("Withdrawal successful.");
        }
        catch (ATMException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Unknown error " + exception.message);
        }
    }

    public void withdraw(string accountId, double amount)
    {
        DeviceHandle handle = getValidHandle();

        DeviceRecord record = getActiveDeviceRecord(handle);

        ensureNetworkConnected(record);

        ensureSufficientBalance(accountId, amount);

        dispenseCash(handle, amount);
    }

    private DeviceHandle getValidHandle()
    {
        var handle = getHandle("DEV1");

        if (handle == DeviceHandle.Invalid)
            throw new InvalidDeviceException("Invalid device handle.");

        return handle;
    }

    private DeviceRecord getActiveDeviceRecord(DeviceHandle handle)
    {
        var record = retrieveDeviceRecord(handle);

        if (record.getStatus() == DEVICE_SUSPENDED)
            throw new DeviceLockedException("Device is suspended.");

        return record;
    }


    private void ensureNetworkConnected(DeviceRecord record)
    {
        if (record.getWifiConnection() != WIFI_CONNECTED)
            throw new NetworkConnectionException("Network connection error.");
    }

    private void ensureSufficientBalance(string accountId, double amount)
    {
        if (getBalance(accountId) < amount)
            throw new InsufficientFundsException("Insufficient funds.");
    }

}

