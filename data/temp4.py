import json

with open('items_v2.json', 'r') as f:
    v2_items = json.load(f)

with open('items_v3.json', 'r') as f:
    v3_items = json.load(f)

v2_names = {item['name'] for item in v2_items}

for v2_item in v2_items:
    name = v2_item['name']
    v2_price = float(v2_item['priceco']) / v2_item['amount'] if v2_item['priceco'] > 0 else 0
    v3_price = next((item['price'] for item in v3_items if item['name'] == name), None)
    
    if v3_price is not None:
        print(f"Item: {name}, v2 price: {v2_price:.1f}, v3 price: {v3_price}")
    else:
        print(f"Item: {name}, v2 price: {v2_price:.1f}, not found in v3")

for v3_item in v3_items:
    name = v3_item['name']
    v3_price = v3_item['price']
    if name not in v2_names:
        print(f"Item: {name}, not found in v2, {v3_price}")
