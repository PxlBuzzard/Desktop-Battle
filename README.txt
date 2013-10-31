Desktop Battle Changelog
Created by Daniel Jost

How to Play:
WASD or Arrow Keys to move
Move mouse and click to shoot
Escape to pause
Q to switch weapons

#region NOTES FOR LEIGH:
Final Submission Requirements
- Custom event (n/a)
- custom FileNotFoundException (Save.cs line 126)
- My XNA menus are pretty much awesome (Menu.cs, and in the Menus folder)
- world to move in (yes)
- items to interact with (guns)
- characters to interact with (enemies)

Bonus Points
- Used XNA
- have sound in the game (pistol.cs line 34, M16.cs line 34,
    Shotgun.cs line 34 Combat.cs line 94)

Special Note: Try using the shotgun, it's so much fun. The quick way to get it is to bump
your total kill count up to 99, it'll unlock at 100.
#endregion

Changelog
Added:
- Menus! Main menu, pause menu, game over, etc
- New weapon: Shotgun
-- Shoots 5 bullets at a time, unlocked after 100 kills
- Sounds for gunfire and when enemies are shot
- Track number of enemies killed
- Loads save file on startup if you've played before

Changed:
- Diagonal speed is capped to match the speed of uni-directional movement.