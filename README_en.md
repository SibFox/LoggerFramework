# Little Logger for .NET Framework applications
> My implementation of logger library to use mostly with WinForms and .Net Framework applications  
## Functionalty
**This logger shortens command writing by providing few fluid methods to use**
#### ```SetLogPath(string path)```
Sets the path to where log file will be written.  
You can't set the path if it was already set.
#### ```AddTimeStamp()```
Adds the timestamp in 24 hour format at the start of the line.  
You can't add timestamp if it was already added.
### Adding info to the line
There's 3 different methods for adding the line into the log.  
#### ```AddInfoToLine(string line)```
Adds the string you've written and splits further lines with ','.
#### ```AddInfoToLineWithTime(string line)```
Adds the string you've written, but adds the timestamp before that, if it haven't been added and splits further lines with ','.  
If used mid sentence, timestamp is added at the start of the line.
#### ```AddInfoToLineWithHeader(string header, string line, int type)```
Adds the string you've written, but with header denoting further information and splits further lines with ','.  
Have 2 styles:  
```
1. Header: String.
2. [Header]: String.
```
### Adding an array or list
*Supports these array types: object, string, int.*  
Allows to add arrays into the information lines splitting contents with ';'.  
**You can add array into the line with:**  
- ```AddArray(type[] param)``` - add an array to the line.
- ```AddArray(string header, type[] param)``` - add an array with header denoting the array.  
### Adding a dictionary
*Currently supports only this type of dictionaries(<string, string[]>).*  
Allows to add dictionaries into the information lines in format: Key:~ value1; value2; value3 "*move to another line*" and it continues like earlier.  
As with arrays, you can add dictionaries with or without headers.  
### Adding an extended line
```AddExtendedInfo()``` transfers you to another class that allows you to set the line with a little bit more info.  
*Automatically sets the timestamp at the start of the line if it used not mid sentence.*
#### ```AddFromMethod(string methodName)```
Pass here name of the method where the information is coming from.  
Adds info to line in format [Method/methodName].  
You can't add another 'from method' if you've already added it.
#### ```AddFromObject(Control obj)```
*Used for WinForms*  
Pass here the Control object from which the information is coming from.  
Adds info to line in format [Object/object name].  
You can't add another 'from object' if you've already added it.
#### ```AddHighlight(string light, string str)```
Allows you to make your own highlights of information.  
Pass here the information you would like to highlight in form of [Parent/Children].  
Some examples of how you may use it:
- [Class name/Method name]
- [Object type/Name of the object]
- [Method name/Part of the method]
#### Adding info to the line
Allows you to add info to the line as it shown at the begining.  
There's no method to add a timestamp or line with it, because it is added at the start automatically.
### Accepting the line
To traverse to the other line you need to accept the line before that.  
There's 2 methods to accept the line:  
- ```AcceptWholeLine()``` - registers the whole edited line and moves to another line. Can be used inside ```Extended Editing``` to accept all edited info and move to another line.
- ```AcceptDeepLine()``` - used only inside ```Extended Editing```. Accepts the extended edited line and allows you to continue editing the whole line.
### Creating the log
After you've done all that you need, prepared all the lines, you can type ```CreateLog(int mode)``` to write down the log file in the path you've indicated in ```SetLogPath``` method.  
"mode" stands for how the file will be written. Originally the mode is 0, and it stands for ```FileMode.Append```, it appends all the lines into the file.  
1 stands for ```FileMode.Create```, it can rewrite already existing file. Useful for when you don't want to make kilometers of text in some file.
Remarks:
- You can use ```CreateLog``` in ```Extended Editing``` and in ```Normal Editing```
- You can use ```CreateLog``` even if you haven't accepted whole or extended line. It writes down all information automatically.
- If you have't added any info lines, the log will not be created.