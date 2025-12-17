import random;
def IsValidInput(input):
    if input.isdigit() and 1<= int(input) <=100:
        return True
    else:
        return False

def main():
    randomNumber=random.randint(1,100)
    isPlaying=False
    userInput=input("Guess a number between 1 and 100:")
    guessCount=0
    while not isPlaying:
        if not IsValidInput(userInput):
            userInput=input("I wont count this one Please enter a number between 1 to 100")
            continue
        else:
            guessCount+=1
            userInput=int(userInput)

        if userInput<randomNumber:
            userInput=input("Too low. Guess again")
        elif userInput>randomNumber:
            userInput=input("Too High. Guess again")
        else:
            print("You guessed it in",guessCount,"guesses!")
            isPlaying=True

