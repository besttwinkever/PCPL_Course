import random

def gen_random(amount, min, max):
    return [random.randint(min, max) for i in range(amount)]

def main():
    print(gen_random(5, 1, 3))

if __name__ == '__main__':
    main()