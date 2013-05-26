EZKey Keyboard Visualization
============================

![Example Stream Setup](http://i.imgur.com/2Kpmorb.png)
_Example setup using chroma key_

![The software and the options menu with color picker](http://i.imgur.com/JAIobgP.jpg)
_The software and the options menu with color picker_

**Issues have been reported that the program does not work while SC2 is maximized for some people. If you are experiencing this issue, PLEASE send the following information to twofxsc@gmail.com**
* OBS version that you are using, including 32/64 Bit
* Windows version, 32/64 Bit
* Is Windows Aero enabled or disabled while streaming?
* Which version(s) of the .NET Framework is installed (see [this article](http://msdn.microsoft.com/en-us/library/hh925568.aspx) to find out how)?
* Is StarCraft added as a software capture of game capture in OBS?

Thank you for providing feedback to fix the problems.


***


### Work in progess
This is currently about as alpha as it gets. There are lots of awesome features to come and the program still has lots of problems. Here's a list of what's coming in the future:

* More customization (Multiple fonts, font colors, etc.)
* Better documentation (With pictures and more detailed explainations)
* Better options menu (Color picker, font selector)
* Lock hotkey function to prevent the software from displaying passwords
* Displaying mouse clicks
* Automatic reloading of the configuration of the last use
* Ability to save and load configs
* More

This page is also work in progress, btw. I will put a detailed explaination about how to set it up and usde it here once I'm actually done coding it.

### Setup guide for streamers
1. Download the ZIP file on the left.
2. Extract the ZIP.
3. Run EZKey.exe from the Release folder.
4. The application will open. Press the middle mouse button or click on options to bring up the options menu.
5. Play around with the sliders until you find a configuration you like.
6. The colors in the input fields can have the following formats (A=Alpha; R=Red; G=Green; B=Blue): #AARRGGBB or AARRGGBB or #RRGGBB or RRGGBB or #ARGB or ARGB or #RGB or RGB
7. If you plan on chroma keying the background in order to put an image behind it later (see the first image above for an example), I'd reccommend choosing #FFFF0000 as background color.
8. Software capture it using OBS. If you want to chroma key, you will get the best result with a similarity value around 60. [Click here for an example image](http://i.imgur.com/L2IqWeS.png)
9. Don't attempt to minimize the software, just start StarCraft.
10. If it doesn't work, please make sure to report you config as explained above. Thank you!


***

Contact me at:
twofxsc@gmail.com


The file EZKey/EZKey/InterceptKey.cs is mainly taken from Stephen Toub (http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx). Credit to him.