About AmpShell
--------------
AmpShell is a DOSBox front-end for Windows. 
It is written in C# with VS 2008, so you'll need to have the .NET Framework installed on your machine in order to run it.


It is free software licensed under the GNU GPL v3.


In AmpShell, you can create/remove/modify tabs (called 'categories') in wich you can create/remove/modify your games.
 Example of categories : 'First Person Shooters' and 'Strategy games'. 


For each game you can also provide an icon, an optional DOSBox configuration file (you can also edit it from inside AmpShell with your text editor of choice), provide a CD image path, and set some other options.

For each category you have 5 different view modes (saved on exit for each of them) : Small icons, Large icons, Tile, List, Details

AmpShell will prompt the user for the location of DOSBox.exe if it cannot be found, and prompt for the name of the first category (so it can be created).

AmpShell will also look for the user's default DOSBox configuration file if has not been found nor set in the Preferences menu. It will also set mousepad/gedit/kate/notepad as the default text editor for configuration files of games if no text editor is set (it can be set in the Preferences).

In the "Tools" menu you can :
-Launch DOSBox with the default configuration files given
-Launch your text editor

In the Preferences window, you can :
-choose DOSBox binary (executable) file

-choose a default .conf file (that will be used for games without a custom conf file and for wich the checkbox "don't use any config file" is unchecked).

-choose a DOSBox language file
-choose wich folder to open when looking for games, and wich one to open when looking for CD image files

-set default options for when you create a new game (such as always set the "quit on exit" checkbox to true)

-re-arrange categories (or even sort them by name), and choose the default view mode for a new category (or apply it to all of them)

-choose if :
AmpShell will prompt you for confirmation when deleting a game or category
AmpShell will remember the window's size, and/or position
AmpShell will show (or not) the status bar, the tool bar, and the menu bar

Limitations (for GNU/Linux users)
--------------------------------
-Avoid spaces in paths

Changelog
---------
See changelog.txt

Future plans

------------
See TODO.txt

License
-------
GPL v3. See 'license.txt' for details.

Contact

-------
xcomcmdr (at) ampshell (dot) tuxfamily (dot) org