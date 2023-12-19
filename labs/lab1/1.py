import math
import sys

def getArgument(index, placeHolder):
    coef = 0
    try:
        coef = float(sys.argv[index])
    except Exception:
        while 1:
            print(placeHolder, end=': ')
            try:
                coef = float(input())
                break
            except Exception:
                pass
    return coef

def calculateRoots(a, b, c):
    d = b*b - 4*a*c
    roots = []
    if d >= 0:
        roots.append(math.sqrt((-b + math.sqrt(d)) / (2*a))) # x1
        roots.append(-math.sqrt((-b + math.sqrt(d)) / (2*a))) # x2
        if d > 0:
            roots.append(math.sqrt((-b - math.sqrt(d)) / (2*a))) # x3
            roots.append(-math.sqrt((-b - math.sqrt(d)) / (2*a))) # x4
    return roots

def main():
    roots = calculateRoots(
        getArgument(1, 'Введите коэффициент A'),
        getArgument(2, 'Введите коэффициент B'),
        getArgument(3, 'Введите коэффициент C'),
    )
    for i in range(len(roots)):
        print('{}-й корень: {}'.format(i + 1, roots[i]))
    if len(roots) == 0:
        print('Действительных корней нет')

if __name__ == '__main__':
    main()