def main():
    data = [4, -30, 30, 100, -100, 123, 1, 0, -1, -4]
    # Без lambda-функции
    print(sorted(data, key=abs, reverse=True))
    # С lambda-функцией
    print(sorted(data, key=lambda x: abs(x), reverse=True))

if __name__ == '__main__':
    main()