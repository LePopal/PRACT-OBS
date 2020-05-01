# PRACT OBS

The **Popal's Rekordbox Analysis and Clean up Tool for OBS** is a program designed to expose the current track or song played in Pioneer DJ software Rekordbox to OBS.

## Author

Axel Pironio ([popal.fr](http://popal.fr)).

## Disclaimer

BACKUP YOUR LIBRARY !!!
-----------------
*It is highly recommended that you backup your Rekordbox library before any use of this program.*

*PRACT_OBS connects to the Rekordbox Database in real time. Although no write actions are performed, it is possible, however unlikely, that corruption occurs. Such corruption would lead to the loss of the totality or parts of the tracks, playlists, analysis, beatgrids, cue points, samples, etc. stored or referenced in the library.*


Here's how to proceed:
1. Go to Rekordbox and click File / Library / Backup library
2. Select the folder where to store the backup which is a zip file
3. When asked if the music files should be backed up as well, click no. 

Keep in mind that you should always keep a backup of your music file by any other mean. But that's another story...

PRACT OBS and its developers won't be responsible for any loss of information or any following damages caused.


-----------------

## Requirements

PRACT OBS is designed to run on Windows 10 and Rekordbox 6.x.

As of April 2020, the following Rekordbox versions are supported:
* Rekordbox 6.0.0
* Up to Rekordbox 6.0.1 Beta

Any version prior to Rekordbox 6 (eg 5.x) is NOT supported and there's no plans to do so.

## Installation

1. Make sure you have installed the [latest .Net Core 3.1 Desktop runtime](https://dotnet.microsoft.com/download/dotnet-core/3.1)
2. Run the installer or unzip the portable version to your desired place on your computer running Rekordbox.
2. Run the program
2. Go the Tools / Options menu and provide the **Encryption Key for the Rekordbox database** and the Output Folder

> The **Output Folder** is the folder where PRACT OBS will export the metadata for OBS to display.

> The **Encryption Key** is not provided with this software and will never be. You must find some way to get it.

## Program Settings Breakdown

You'll find some basic customization in the Tools / Options menu:
* **Key**: the Rekordbox database is encrypted. Please provide the correct key for PRACT OBS to access it
* **Output Folder**: the folder where to write the Artist.txt, Title.txt and Artwork.jpg files that you will map in OBS
* **On screen duration**: the number of seconds for how long the artist, track and artwork must be displayed in OBS. Chose 0 to always display. After the specified amount of seconds, the files will be emptied/deleted so OBS won't display anything.
* **Pooling Period**: how often should the Rekordbox database be queried. In seconds, min value is 0, recommended value is 10.

## How it works

PRACT OBS pools the Rekordbox Histories Playlists. When updated, the program writes 2 or 3 files :
1. Artist.txt is a text file containing the Artist name
2. Title.txt is a text file containing the Title of the song/track
3. Artwork.jpg (if existing) is the cover or art stored in Rekordbox

You then configure OBS and add 1 Text Source (GDI) mapping the Artist.txt file, another one for the Title.txt file and maybe an Image source for the Artwork.jpg file.


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## Project status

PRACT OBS is in active development. Please feel free to open an issue should you encounter any repeatable problem.

## License
[Apache 2.0](https://choosealicense.com/licenses/apache-2.0/)