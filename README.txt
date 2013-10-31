Desktop Battle Changelog
Created by Daniel Jost

How to Play:
WASD or Arrow Keys to move
Move mouse and click to shoot
Q to switch weapons
F1 to Save
F2 to Load

#region NOTES FOR LEIGH:
Milestone 4 Requirements
- Moved Hero and Room values into external files
-- hero.xml and rooms.xml are in the Debug folder (output directory)

As I note above, use F1 to save and F2 to load. I've tried to speed up the process
for you by running a Load when the game starts, and it will also do a Save when
you move to a new room.

Bonus Points
- Used XML for majority of file I/O
- Already got serialization bonus points in Milestone 3
#endregion

Changelog
Added:
- Can draw up to 5 custom strings in the middle of the screen
- Enhanced file saving/loading to allow for basic modding

Changed:
- Rooms are now loaded into a list instead of a fixed array