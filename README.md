# Discord Rich Presence for Quest Echo VR game

## Description

Simple Discord Rich Presence client app for Quest game Echo VR, using [madmagic007's Oculus-Quest-Presence](https://github.com/madmagic007/Oculus-Quest-Presence) service app for Quest. It starts as a tray icon app and query the Quest service app for currently running app's name. If it matches the Echo VR's name it's updating Discord Rich Presence. After Echo is closed it clears Discord RP status.

There is no Quest api for now, so it's only showing "In game Echo VR" status with static text on details view.

*It was made to show only Echo VR info, but feel free to expand it on Your own to show any app running on Quest. You can use the lang.json file from madmagic007's project, containing already some of the apps, to map app's package names to normal names. You should also provide You own Discord app id in file RichPresenceHandler.cs:*
`_client = new DiscordRpcClient("12345678901234567890");`
*so You can setup different images and app name.*

## Installation

1. Download latest release of [madmagic007's Oculus-Quest-Presence](https://github.com/madmagic007/Oculus-Quest-Presence/releases).
2. Unpack and install the APK file on Quest (using SideQuest for example)
3. Follow [instructions](https://github.com/madmagic007/Oculus-Quest-Presence#on-your-quest) from madmagic007's Readme to run the service on Quest (install Quest App Launcher if needed etc.). You can check if service is working by entering Your Quest Ip address (can be found in SideQuest app) followed by port `:8080`,
for example: `http://192.168.1.100:8080/`
If everything is working it should show You a Json response with stuff like battery level, ip addresses and current app name.
4. Download [latest release of this app](https://github.com/Vadix88/quest-echovr-richpresence/releases).
5. Unpack to desired location.
6. Check the configuration in file *QuestEchoVrRichPresence.exe.config*. You can open it with any text editor. Setup the ip address of Your Quest (it's good to have it configured in Your Wifi router as static address for Quest, so it won't change on next connection) and other parameters if desired.
7. Run the app. It will show in system tray with a notification popup. It will now send requests every 30 sec to service app running on Quest and wait for a response with Echo VR game name in it. Then it will update Discord Rich Presence (and show notification popup if it's enabled).
8. App can be closed from context menu (right click on tray icon) by clicking Exit. Quest service app can be stopped from the app UI (run it from Quest App Launcher).

You can add the app to system autostart, if You want it to be always on. Quest service should start automatically on Quest boot by default.

## Configuration

All configuration is done by editing the *QuestEchoVrRichPresence.exe.config* file (You need to restart app for changes to take effect).
Available parameters:
 - **QuestIpAddress** - Ip address of You Quest. You can check it in SideQuest.
 - **EchoVrPartialAppName** - Beginning of name of Echo VR's package installed on Quest. Version number on the end can change on updates. It's compared to Quest service responses to check if Echo VR is currently running. Don't change it, if You want to track Echo VR status.
 - **DetailsText** - Text displayed on Discord Rich Presence at the first line below game name.
 - **StateText** - Text displayed on Discord Rich Presence at the second line below game name.
 - **ImageHoverText** - Text displayed on Discord Rich Presence after mouse cursor hover above game image.
 - **ShowTrayPopup** - Setting for showing notification popup after each Rich Presence update (on Echo VR start or stop). Any value diffrent from *true* means false.
