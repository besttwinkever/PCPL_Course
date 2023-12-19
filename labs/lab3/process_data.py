import json

# Импортируем предыдущие микрозадачи
from print_result import print_result
from cm_timer import cm_timer_1
from unique import Unique
from gen_random import gen_random

path = './data_light.json'

@print_result
def f1(arg):
   return sorted([x['job-name'] for x in Unique(arg)])

@print_result
def f2(arg):
    return list(filter(lambda x: x.lower().startswith('программист'), arg))

@print_result
def f3(arg):
    return list(map(lambda x: x + ' с опытом Python', arg))

@print_result
def f4(arg):
    salaries = gen_random(len(arg), 100000, 200000)
    return ['{}, зарплата {} руб.'.format(job, salary) for job, salary in zip(arg, salaries)]

def main():
    with open(path, encoding='utf-8') as f:
        data = json.load(f)
        f.close()
    with cm_timer_1():
        f4(f3(f2(f1(data))))
    
if __name__ == '__main__':
    main()