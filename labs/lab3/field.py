goods = [
    {'title': 'Ковер', 'price': 2000, 'color': 'green'},
    {'title': 'Диван для отдыха', 'color': 'black'}
]

def field(items, *args):
    result = []
    for item in items:
        constructed_item = {}
        for key in args:
            if key in item:
                constructed_item[key] = item[key]
        if constructed_item:
            result.append(constructed_item)
    return result

def main():
    print(field(goods))
    print(field(goods, 'title'))
    print(field(goods, 'title', 'price'))

if __name__ == '__main__':
    main()