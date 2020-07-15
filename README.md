# SodaDungeon2Tool
## Used Code Snippets
[Screen Capture](https://ourcodeworld.com/articles/read/195/capturing-screenshots-of-different-ways-with-c-and-winforms)By [Carlos Delgado](https://ourcodeworld.com/authors/sdkcarlos)
<br>
[Window Focus Handling](https://stackoverflow.com/a/35018042) By [Ivan Yurchenko](https://stackoverflow.com/users/3731444/ivan-yurchenko)
## Purpose
SodaDungeon2Tool, is a Windows Console-Application to notify you when your current Dungeon run has finished.
It is also able to shutdown your PC after your run completed, if you enable this Setting.
## Getting Started
### Either
Got to the [Releases](https://github.com/Death-Truction/SodaDungeon2Tool/releases) Page, download the latest release and run it.
### Or
You can Compile the Application yourself using Visual Studio and Run the compiled Application
## Usage<br>
### Main Menu
![MainMenu](https://raw.githubusercontent.com/Death-Truction/SodaDungeon2Tool/master/Images/mainMenu.png)
  
  
Here you can:
<br>
* Change the Configuration
* Start the Tool for Monitoring your Dungeon run
* Enable the function to shutdown your PC after the current run completed.
### Configuration Menu
![SettingsMenu](https://raw.githubusercontent.com/Death-Truction/SodaDungeon2Tool/master/Images/settingsMenu.png)
  
  
Allows you to change the current Configuration. Changes will be saved locally in the config.txt file.
Available Configuration are:
* SleepTimer : The delay between each Check, whether your current run is still in progress
* Notify On Finish: Will Notify you with a Beep-Sound, once your current Run finished
* Number of Notifications: The number of Beep-Sounds you will hear
### Starting
![Running](https://raw.githubusercontent.com/Death-Truction/SodaDungeon2Tool/master/Images/running.png)
  
  
Once you started your run, you will see an update for each Check on your run.
<br>
Either it is still running and displayed in RED
<br> Or your run has finished, which will be displayed in GREEN

