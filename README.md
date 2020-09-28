# dropboxignore

Small windows utility to make Dropbox ignore files/folders specified into .dropboxignore file



Usage:

1.- Create a **.dropboxignore** file and type files or folders to be ignored by Dropbox, one per line. Lines starting with # will be ignored.

2.- Now close the file and launch the **dropboxignore.exe** file in the folder where you created the .dropboxignore file.



For example, you can create a .dropboxignore file into a nodejs project, with one line:

```
node_modules
```

Now close the file and exec dropboxignore. Now Dropbox will ignore the *node_modules* folder.