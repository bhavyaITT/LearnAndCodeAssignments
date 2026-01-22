package com.payment.processing;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.*;

public class PaymentProcessor 
{
    private static final BigDecimal MIN_AMOUNT = new BigDecimal("0.01");
    private static final int MAX_RETRIES = 2;
    private static final String PAYMENT_SUCCESS = "Payment successful";
    private static final String PAYMENT_FAILED = "Payment failed";

    private Logger logger;
    private NotificationService notifier;
    private Map<String, PaymentRecord> history;

    public PaymentProcessor(Logger logger, NotificationService notifier) 
    {
        this.logger = logger;
        this.notifier = notifier;
        this.history = new HashMap<>();
    }

    public PaymentResult process(PaymentRequest request) 
    {
        validate(request);
    
        int attempt = 0;
        while (attempt < MAX_RETRIES) 
        {
            try 
            {
                execute(request);
                record(request);
                notifySuccess(request);
                return new PaymentResult(true, PAYMENT_SUCCESS, generateId());
            } catch (PaymentException e) 
            {
                attempt++;
                logger.log("Retry attempt: " + attempt);
            }
        }
        return new PaymentResult(false, PAYMENT_FAILED, null);
    }

    private void validate(PaymentRequest request) 
    {
        String customerID = request.customerId();
        BigDecimal requestAmount =  request.amount();

        if (customerID == null || customerID.isBlank()) 
        {
            throw new IllegalArgumentException("Customer ID required");
        }

        if (requestAmount == null || requestAmount.compareTo(MIN_AMOUNT) < 0) 
        {
            throw new IllegalArgumentException("Invalid amount");
        }
    }

    private void execute(PaymentRequest request) 
    {
        BigDecimal requestAmount =  request.amount();

        logger.log("Executing payment of " + request.amount());
        if (requestAmount.compareTo(new BigDecimal("5000")) > 0) 
        {
            throw new PaymentException("Limit exceeded");
        }
    }

    private void record(PaymentRequest request) 
    {
        PaymentRecord storeNewRecord = new PaymentRecord(request.customerId(), request.amount(), LocalDateTime.now());
        
        history.put(generateId(),storeNewRecord);
    }

    private void notifySuccess(PaymentRequest request) 
    {
        notifier.send(request.customerId(), "Payment of " + request.amount() + " processed");
    }

    private String generateId() 
    {
        return "TXN-" + System.currentTimeMillis();
    }
}
