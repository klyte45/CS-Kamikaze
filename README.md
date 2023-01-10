# CS-Kamikaze
## Cities Skylines: Mod template for self-unsubscription mods that redirects to another mod

Use this template to show an UI to users informing the mod itself is being replaced by another. There will have two options:

- Subscribe to new mod list and unsubscribe the current mod
- Only unsubscribe current mod, no new subscriptions

The game will be closed after the option get chosen, and the actions will be done.

## Where to change to customize this mod

### Kamikaze/Kamikaze.csproj

- Line 11: Change the assembly name. I recommend to use the original mod dll name, but shall work with any name if it doesn't conflicts with any other mod

### Kamikaze/ModInstance.cs

- Line 18: `SimpleName` will be the name shown in the mod list and the title bar of the window
- Line 26: `ModsToReplace` Dictionary: Replace and/or add entries here with the Steam Workshop ID of the new mods as key and a string value to be shown in the UI window. Can be added unlimited entries and them all will be added if the user choose to subscribe to the new mods also. **Mod IDs must be Long** (`L` in the end of the number)

## After compile the mod

The files will be at `Kamikaze` folder in AppData's Mod folder. Copy all content generated and paste replacing all content in the original mod folder in `wokshop/255710/<MOD_ID>` pattern folder. Or just copy them in the staging content page when uploading to workshop.

## Other notes

This mod uses the [Kwytto Utility](https://github.com/klyte45/KwyttoUtility) as base to generate the code under this mod feature.
