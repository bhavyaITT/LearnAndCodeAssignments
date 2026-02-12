public class Paperboy
{
    public void collectPayment(Customer customer, double paymentAmount)
    {
        boolean paymentSuccessful = customer.makePayment(paymentAmount);

        if (!paymentSuccessful) {
            // come back later
        }
    }
}