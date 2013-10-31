Desktop Battle Changelog
Created by Daniel Jost

How to Play:
WASD or Arrow Keys to move
Move mouse and click to shoot
Q to switch weapons

#region NOTES FOR LEIGH:
Milestone 2 Changes
- Improved ToStrings
- Three uses of .Equals in List.cs
- static method GenerateBulletStack in Weapon.cs
- base.LoadContent() calls in Hero, Clippy, Key, and Bullet to Sprite.cs for contructor chaining

Milestone 3 Requirements
- Stack of bullets to recycle the same bullets in memory
- Queue of Enemies to cycle through
- List of bullets and enemies currently onscreen
- Custom List, Stack, and Queue classes

Bonus Points
- GetEnumerator implemented in List
- Serialized XML file I/O
-- To test it, finish the third (last) room and it will call a Load() to put you back in the first
room. It isn't perfectly implemented, but the file I/O part works.

#endregion

Milestone 3 Changelog
Added:
- Save and SaveData classes, used to Save/Load the game using serialized XML
-- The save file is in the game's install location, named savegame.xml
- Crosshair cursor
- Custom List, Stack, and Queue data structures
- Stack of bullets to recycle the same bullets in memory
- Queue of enemies that spawn into the room on an interval
- Remove all bullets onscreen when a new room loads
- Hero and Gun rotate to face the mouse
- New enemy: Key
-- Chases the Hero, has low hp

Changed:
- Moved the onscreen Bullet list and Bullet stack to Hero.cs
- Fixed ToString in Weapon and Sprite so that the names of the objects are properly displayed
- Made Room code able to handle multiple levels
- Enemies spawn farther to the right side of the screen
- The M16 now shoots bullets at an angle
- Improved weapon switching code to prevent crash early in the game
- Made the instructions in the first room easier to comprehend

Removed:
- 
- 