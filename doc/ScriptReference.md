# What is SCRIPT on Kusa-Mochi Auto ?

You can control mouse/keyboard inputs by your script file.

The scripts on "Kusa-Mochi Auto" are interpreted and executed sequently.

You can execute your script by 2 ways:

- Click "Open" button on the main window and select a script file to execute.
- Open a script file with Kusa-Mochi Auto.

# How to make scripts

## Sample

```
MouseMove(136,1079)
MouseLeftDown(132,1079)
MouseLeftUp(132,1079)
Wait(1000)
for i in 1..3
  KeyDown(16)
  KeyPress(75 + i)
  KeyUp(16)
MouseMove(867,426)
MouseWheel(869,434,-1)
MouseWheel(869,434,-1)
MouseWheel(869,434,-1)
MouseWheel(869,434,1)
MouseWheel(869,434,1)
MouseMove(522,1052)
MouseLeftDown(522,1053)
MouseLeftUp(522,1053)
Exec("c:\\MyBatchFiles\\test.bat", "MyOption")
```

## File Format

|||
|---|---|
|Encode|UTF-8|
|Line Feed Code|CR+LF|

## Methods

### MouseMoveTo(x,y)

### MouseClick(x,y)

### MouseRightClick(x,y)

### MouseLeftBDown(x,y)

### MouseLeftUp(x,y)

### MouseRightDown(x,y)

### MouseRightUp(x,y)

### MouseMiddleDown(x,y)

### MouseMiddleUp(x,y)

### MouseWheel(wheelCount)

### KeyPress(keyCode)

### KeyDown(keyCode)

### KeyUp(keyCode)

### Wait(time)

### Exec(filePath, option1, option2, ...)

## Operators

You can use +,-,*,/ in scripts.

## Other Syntax

### for

### while
