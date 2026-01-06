import random;

MinValue = 1
MaxValue = 100

def IsValidGuess(guess: str) -> bool:
    return guess.isdigit() and MinValue <= int(guess) <= MaxValue

def GetUserInput() -> str:
    return input("Guess a number between 1 and 100: ")
    

def evaluate_guess(guess: int, target: int) -> str:
    if guess < target:
        return "Too low. Guess again: "
    elif guess > target:
        return "Too high. Guess again: "
    return "correct"

def PlayGame():
    targetNumber=random.randint(1,100)
    isPlaying=False
    guess = GetUserInput()
    guessNumber = 0
    while not isPlaying:
        if not IsValidGuess(guess):
            guess = input("I wont count this one Please enter a number between 1 to 100")
            continue
        else:
            guessNumber+=1
            guessInt=int(guess)

        result = evaluate_guess(guessInt, targetNumber)
        if result == "correct":
            print(f"You guessed it in {guessNumber} guesses!")
            break
        else:
            guess = input(result)
        