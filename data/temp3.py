import os
import json

for filename in os.listdir():
    if filename.endswith(".json"):
        print(f"Going through '{filename}'!")
        with open(filename, 'r') as f:
            data = json.load(f)
        with open(filename, 'w') as f:
            json.dump(data, f, indent='\t')
