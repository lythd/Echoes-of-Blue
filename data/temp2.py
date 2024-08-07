import json

with open('items_old.json') as f:
    data = json.load(f)

new_data = {d.pop('name'): d for d in data}

with open('items.json', 'w') as f:
    json.dump(new_data, f, indent='\t')
