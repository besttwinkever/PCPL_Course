from contextlib import contextmanager
from time import time, sleep

# С помощью библиотеки
@contextmanager
def cm_timer_2():
    start = time()
    yield lambda: time() - start
    print('time: {}'.format(time() - start))

# С помощью класса
class cm_timer_1():
    def __enter__(self):
        self.start = time()
        return self
    
    def __exit__(self, type, value, traceback):
        print('time: {}'.format(time() - self.start))

def main():
    with cm_timer_1():
        sleep(1)
        
    with cm_timer_2():
        sleep(1)

if __name__ == '__main__':
    main()