def SumOfCubeOfDigits(number):
    totalSum = 0
    numberOfDigits = 0

    tempNumber = number
    while tempNumber > 0:
        numberOfDigits = numberOfDigits + 1
        tempNumber = tempNumber // 10

    tempNumber = number
    for n in range(1, tempNumber + 1):
        remainder = tempNumber % 10
        totalSum = totalSum + (remainder ** numberOfDigits)
        tempNumber //= 10
    return totalSum

userInput = int(input("\nPlease Enter the Number to Check for Armstrong: "))

if (userInput == SumOfCubeOfDigits(userInput)):
    print("\n %d is Armstrong Number.\n" % userInput)
else:
    print("\n %d is Not a Armstrong Number.\n" % userInput)
