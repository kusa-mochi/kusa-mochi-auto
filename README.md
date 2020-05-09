- [About](#about)
- [What to use this for](#what-to-use-this-for)
- [How to record your operations](#how-to-record-your-operations)
- [How to scripting](#how-to-scripting)
  - [Mouse](#mouse)
  - [Keyboard](#keyboard)
  - [Others](#others)
- [Sample scripts](#sample-scripts)
- [License](#license)
---
# About
kusa-mochi-auto is the mouse/keyboard automation tool on windows.

# What to use this for
You can automate your workflow on PC using kusa-mochi-auto that is commonly used for:
- Record your mouse/keyboard operations on PC, and "replay" it.
- Operate your PC by scripting C#.
- Find template images on screen by scripting. You can use it to trigger to do something.
- Run external programs (*.exe, *.bat) by scripting.

# How to record your operations
![Main Window 1](https://raw.githubusercontent.com/kusa-mochi/kusa-mochi-auto/master/doc/img/main-window-1.PNG "Main Window 1")

On the main window, you can start recording all of your mouse & keyboard operations on your PC by click the red "Rec" button.

![Main Window 2](https://raw.githubusercontent.com/kusa-mochi/kusa-mochi-auto/master/doc/img/main-window-2.PNG "Main Window 2")

After recording, click the blue "Stop" button.

The save file dialog is opened on which you can name a recording file (C# file).

If you want to "replay" your operatins, click the "open" button and select the recorded C# file.

# How to scripting

## Mouse

### MouseMoveTo(int x, int y)
move mouse pointer to a position (x,y): x px from left, y px from top of the screen.

usage:
```
MouseMoveTo(100, 200);
```

### Click()
click on a current mouse position.

usage:
```
Click();
```

### Click(int x, int y)
click on a position (x,y): x px from left, y px from top of the screen.

usage:
```
Click(100, 200);
```

### RightClick()
right click on a current mouse position.

usage:
```
RightClick();
```

### RightClick(int x, int y)
right click on a position (x,y): x px from left, y px from top of the screen.

usage:
```
RightClick(100, 200);
```

### MiddleClick()
middle button click on a current mouse position.

usage:
```
MiddleClick();
```

### MiddleClick(int x, int y)
middle button click on a position (x,y): x px from left, y px from top of the screen.

usage:
```
MiddleClick(100, 200);
```

### MouseDown()
press mouse left button down on a current mouse position.

usage:
```
MouseDown();
```

### MouseDown(int x, int y)
press mouse left button down on a position (x,y): x px from left, y px from top of the screen.

usage:
```
MouseDown(100, 200);
```

### MouseUp()
press mouse left button up on a current mouse position.

usage:
```
MouseUp();
```

### MouseUp(int x, int y)
press mouse left button up on a position (x,y): x px from left, y px from top of the screen.

usage:
```
MouseUp(100, 200);
```

### MouseRightDown()
press mouse right button down on a current mouse position.

usage:
```
MouseRightDown();
```

### MouseRightDown(int x, int y)
press mouse right button down on a position (x,y): x px from left, y px from top of the screen.

usage:
```
MouseRightDown(100, 200);
```

### MouseRightUp()
press mouse right button up on a current mouse position.

usage:
```
MouseRightUp();
```

### MouseRightUp(int x, int y)
press mouse right button up on a position (x,y): x px from left, y px from top of the screen.

usage:
```
MouseRightUp(100, 200);
```

### MouseMiddleDown()
press mouse middle button down on a current mouse position.

usage:
```
MouseMiddleDown();
```

### MouseMiddleDown(int x, int y)
press mouse middle button down on a position (x,y): x px from left, y px from top of the screen.

usage:
```
MouseMiddleDown(100, 200);
```

### MouseMiddleUp()
press mouse middle button up on a current mouse position.

usage:
```
MouseMiddleUp();
```

### MouseMiddleUp(int x, int y)
press mouse middle button up on a position (x,y): x px from left, y px from top of the screen.

usage:
```
MouseMiddleUp(100, 200);
```

### MouseWheel(int amount)
rotate mouse wheel by amount value on a current mouse position.

if amount value > 0, rotate direction is to up.

if amount value < 0, is to down.

the absolute value of the amount means "steps" on wheel rotation.

usage:
```
MouseWheel(-3); // rotate wheel down by 3 steps.
```

### MouseWheel(int x, int y, int amount)
rotate mouse wheel by amount value on a position (x,y): x px from left, y px from top of the screen.

if amount value > 0, rotate direction is to up.

if amount value < 0, is to down.

the absolute value of the amount means "steps" on wheel rotation.

usage:
```
MouseWheel(-3); // rotate wheel down by 3 steps.
```

## Keyboard

### KeyPress(System.Windows.Forms.Keys key)
key down+up using keycode enum.

usage:
```
// input "Kusamochi"
KeyDown(Keys.ShiftKey);
KeyDown(Keys.K);
KeyUp(Keys.K);
KeyUp(Keys.ShiftKey);
KeyPress(Keys.U);
KeyPress(Keys.S);
KeyPress(Keys.A);
KeyPress(Keys.M);
KeyPress(Keys.O);
KeyPress(Keys.C);
KeyPress(Keys.H);
KeyPress(Keys.I);
```

### KeyPress(short key)
key down+up using keycode.

usage:
```
// input "Kusamochi"
KeyDown(16);
KeyDown(75);
KeyUp(75);
KeyUp(16);
KeyPress(85);
KeyPress(83);
KeyPress(65);
KeyPress(77);
KeyPress(79);
KeyPress(67);
KeyPress(72);
KeyPress(73);
```

### KeyDown(System.Windows.Forms.Keys key)
key down using keycode enum.

usage:
```
// input "Kusamochi"
KeyDown(Keys.ShiftKey);
KeyDown(Keys.K);
KeyUp(Keys.K);
KeyUp(Keys.ShiftKey);
KeyPress(Keys.U);
KeyPress(Keys.S);
KeyPress(Keys.A);
KeyPress(Keys.M);
KeyPress(Keys.O);
KeyPress(Keys.C);
KeyPress(Keys.H);
KeyPress(Keys.I);
```

### KeyDown(short key)
key down+up using keycode.

usage:
```
// input "Kusamochi"
KeyDown(16);
KeyDown(75);
KeyUp(75);
KeyUp(16);
KeyPress(85);
KeyPress(83);
KeyPress(65);
KeyPress(77);
KeyPress(79);
KeyPress(67);
KeyPress(72);
KeyPress(73);
```

### KeyUp(System.Windows.Forms.Keys key)
key up using keycode enum.

usage:
```
// input "Kusamochi"
KeyDown(Keys.ShiftKey);
KeyDown(Keys.K);
KeyUp(Keys.K);
KeyUp(Keys.ShiftKey);
KeyPress(Keys.U);
KeyPress(Keys.S);
KeyPress(Keys.A);
KeyPress(Keys.M);
KeyPress(Keys.O);
KeyPress(Keys.C);
KeyPress(Keys.H);
KeyPress(Keys.I);
```

### KeyUp(short key)
key down+up using keycode.

usage:
```
// input "Kusamochi"
KeyDown(16);
KeyDown(75);
KeyUp(75);
KeyUp(16);
KeyPress(85);
KeyPress(83);
KeyPress(65);
KeyPress(77);
KeyPress(79);
KeyPress(67);
KeyPress(72);
KeyPress(73);
```

## Others

### Wait(int t)
wait until the time t [msec] is passed after this method is called.

usage:
```
Wait(1000); // wait 1000 msec (= 1 sec).
```

### List< Point2d > GetImagePosition(string imageFilePath [, double threshold = 0.95] )
search image patterns on screen, and return a list of matched positions.

if there are no match, a return value is empty list: list length is zero. not null.

"threshold" is an option argument which specifies a threshold on image similarity. the threshold range is 0.0 to 1.0.

if "threshold" is 0.0, all of matching result is returned.

if "threshold" is 1.0, only "perfect matched" result is returned.

the returned value is type of Point2d. that is a struct as following:

```
public struct Point2d
{
    public double X;
    public double Y;
}
```

usage:
```
List<Point2d> positions = GetImagePosition(@"c:\tmp\test.png", 0.95);

string result = "result:\n";
foreach(Point2d p in positions)
{
    result += $"({p.X}, {p.Y})\n";
}
MessageBox.Show(result);
```

### List< Point2d > GetImagePosition(string imageFilePath, int x, int y, int width, int height [, double threshold = 0.95] )
search image patterns on a part of the screen: left-top position is (x, y) and size is (width, height), and return a list of matched positions.

if there are no match, a return value is empty list: list length is zero. not null.

"threshold" is an option argument which specifies a threshold on image similarity. the threshold range is 0.0 to 1.0.

if "threshold" is 0.0, all of matching result is returned.

if "threshold" is 1.0, only "perfect matched" result is returned.

the returned value is type of Point2d. that is a struct as following:

```
public struct Point2d
{
    public double X;
    public double Y;
}
```

usage:
```
List<Point2d> positions = GetImagePosition(@"c:\tmp\test.png", 960, 0, 480, 540, 0.95);

string result = "result:\n";
foreach(Point2d p in positions)
{
    result += $"({p.X}, {p.Y})\n";
}
MessageBox.Show(result);
```

### Run(string filePath [, string args] )
run an external program such as *.exe or *.bat asynchronously.

usage:
```
Run(@"c:\tmp\test.exe");
```
```
Run(@"c:\tmp\test.exe", "testarg1 testarg2");
```

# Sample scripts

- [search youtube on google using chrome](https://github.com/kusa-mochi/kusa-mochi-auto/blob/master/doc/sample-scripts/seearch-youtube-on-google-using-chrome.cs)

# License
kusa-mochi-auto is licensed under the [MIT-license](LICENSE).
