public class Wallet
{
    private float value;
    public float getTotalMoney() { return value; }
    public void setTotalMoney(float newValue) { value = newValue; }
    public void subtractMoney(float debit) { value -= debit; }
    public boolean tryWithdraw(double amount) {
        if (value >= amount) {
            value -= amount;
            return true;
        }
        return false;
    }
}