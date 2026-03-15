public class Customer
{
    private String firstName;
    private String lastName;
    private Wallet myWallet;
    public String getFirstName() { return firstName; }
    public String getLastName() { return lastName; }
    public bool makePayment(double paymentAmount)
    {
        return myWallet.tryWithdraw(paymentAmount);
    }
}