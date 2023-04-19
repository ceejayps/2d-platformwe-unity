#!/bin/bash



for file in $(git status | grep 'modified:' | awk '{print $2}'); do
    echo "Committing changes to $file..."
    git add $file
    git commit -m "Update $file"
done

