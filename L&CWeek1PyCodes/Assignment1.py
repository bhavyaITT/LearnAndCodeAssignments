import random
def GetRandomNumber(diceMaxLimit):
    randomNumber=random.randint(1, diceMaxLimit)
    return randomNumber


def main():
    diceMaxLimit=6
    isPlaying=True
    while isPlaying:
        userInput=input("Ready to roll? Enter Q to Quit")
        if userInput.lower() !="q":
            randomNumber=GetRandomNumber(diceMaxLimit)
            print("You have rolled a",randomNumber)
        else:
            isPlaying=False