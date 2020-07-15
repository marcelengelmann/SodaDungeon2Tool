# SodaDungeon2Tool
## Used Code Snippets
<a href="https://ourcodeworld.com/articles/read/195/capturing-screenshots-of-different-ways-with-c-and-winforms">Screen Capture</a> By <a href="https://ourcodeworld.com/authors/sdkcarlos">Carlos Delgado</a>
<br>
<a href="https://stackoverflow.com/a/35018042">Window Focus Handling</a> By <a href="https://stackoverflow.com/users/3731444/ivan-yurchenko">Ivan Yurchenko</a>
## Purpose
SodaDungeon2Tool, is a Windows Console-Application to notify you when your current Dungeon run has finished.
It is also able to shutdown your PC after your run completed, if you enable this Setting.
## Getting Started
### Either
Got to the <a href="https://github.com/Death-Truction/SodaDungeon2Tool/releases">Releases</a> Page, download the latest release and run it.
### Or
You can Compile the Application yourself using Visual Studio and Run the compiled Application
## Usage
### Main Menu
<img width="700" heigth="450" src="https://raw.githubusercontent.com/Death-Truction/SodaDungeon2Tool/master/Images/mainMenu.png">
Here you can:
* Change the Configuration
* Start the Tool for Monitoring your Dungeon run
* Enable the function to shutdown your PC after the current run completed.
### Configuration Menu
<img width="700" heigth="450" src="https://raw.githubusercontent.com/Death-Truction/SodaDungeon2Tool/master/Images/settingsMenu.png">
Allows you to change the current Configuration. Changes will be saved locally in the config.txt file.
Available Configuration are:
* SleepTimer : The delay between each Check, whether your current run is still in progress
* Notify On Finish: Will Notify you with a Beep-Sound, once your current Run finished
* Number of Notifications: The number of Beep-Sounds you will hear
### Starting
<img width="700" heigth="450" src="https://raw.githubusercontent.com/Death-Truction/SodaDungeon2Tool/master/Images/running.png">
Once you started your run, you will see an update for each Check on your run.
<br>
Either it is still running and displayed in RED
<br> Or your run has finished, which will be displayed in GREEN
