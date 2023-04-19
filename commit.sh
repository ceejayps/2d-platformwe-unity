#!/bin/bash


for file in $(git status | grep 'modified:' | awk '{print $2}' | grep 'My project/Assets/tile map/'); do
    echo "Committing changes to $file..."
    git add $file
    git commit -m "Update $file"
done