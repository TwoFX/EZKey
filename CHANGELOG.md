#####Version 0.1 (Apr 15, 2013)
* Basic Functionality

#####Version 0.2 (Apr 20, 2013)
* Disabled minimizing due to the program not working while minimized
* Keys now are labeled
* Shift/Control Keys, Space Bar, Enter, Backpace, Escape and F-Keys added
* Options menu: Rounded edges for keys, window size, F-key offset, background, foreground and foregound on keystroke colors
* Minor fixes

#####Version 0.3 (May 18, 2013)
* Lock hotkey added for diplaying passwords etc.
* Lock mask that covers the keys while lock is enabled
* Options menu: Set lock hotkey, lock mask symbol/enable/disable, stroke color
* Color picker
* Minor fixes

#####Version 0.4 (May 19, 2013)
* Options menu: Text color, text color on keystroke, stroke color on keystroke, font, font weight (bold), font style (italic)

#####Version 0.5 (May 20, 2013)
* Configs can be saved and loaded
 
#####Version 0.5.1 (May 20, 2013)
* Improved format for config files
* Added restore default settings button
* Applicatgion now completely closes if main window is closed

#####Version 0.5.2 (Jul 22, 2013)
* Fixed a bug in the key aligning algorithm that caused the backspace key to be misplaced and of the wrong size
* The project is now licensed under the GNU General Public License Version 3
* Saving no longer writes values that do not differ from the default values (Config files from Version 0.5.1 will still work)
* Refined the way default values are handled by the manager
* Minor fixes and cleanup

#####Version 0.5.3 (Jul 23, 2013)
* The options label now automatically changes color so it is visible regardless of the selected background color
* Open and save dialog now point to the Themes folder when first opened
* The program now scans the Themes folder upon starup and imports all configs into a list. The user can import other configs and access all easily from within the program
* Removed the default settings button as it can be accessed through the new menu
* The options menu now no longer changes its position when refreshed
* Changes to the config can now be saved to the last loaded file using the save to selectede button
* Text that would exceed the key gets automatically downscaled
* Updated the included theme files to the optimized format introduced in 0.5.2