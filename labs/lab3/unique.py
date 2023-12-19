from gen_random import gen_random

class Unique(object):
    def __init__(self, items, **kwargs):
        self.ignore_case = 'ignore_case' in kwargs and kwargs['ignore_case']
        self.items = items
        self.originals = []
        self.item_index = -1
        pass

    def __next__(self):
        self.item_index += 1
        if self.item_index >= len(self.items): raise StopIteration
        is_item_string = isinstance(self.items[self.item_index], str)
        if (not self.items[self.item_index] in self.originals and (self.ignore_case and is_item_string or not is_item_string)) or (is_item_string and not self.ignore_case and len(list(filter(lambda item: isinstance(item, str) and item.lower() == self.items[self.item_index].lower(), self.originals))) == 0):
            self.originals.append(self.items[self.item_index])
            return self.items[self.item_index]
        return self.__next__()

    def __iter__(self):
        return self

def main():
    unique = Unique(['A', 'B', 'a', 'b', 'c', 'c'], ignore_case=False)
    for element in unique:
        print(element, end=' ')
    print('')
    unique = Unique(gen_random(10, 1, 6))
    for element in unique:
        print(element, end=' ')

if __name__ == '__main__':
    main()