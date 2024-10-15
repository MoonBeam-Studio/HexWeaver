mkdir Assets
cd ./Assets
mkdir -p Project/Art/Characters/Player
mkdir -p Project/Art/Characters/Enemies
mkdir -p Project/Art/Characters/NPCs
mdkir -p Project/Art/Enviorment/Levels
mkdir -p Project/Art/Enviorment/Props
mdkir -p Project/Art/UI/Icons
mkdir -p Project/Art/UI/HUD
mkdir -p Project/Art/UI/Menus
mdkir -p Project/Audio/Music
mkdir -p Project/Audio/SFX
mkdir -p Project/Audio/Dialogue
mdkir -p Project/Prefabs/Characters/Player
mkdir -p Project/Prefabs/Characters/Enemies
mdkir -p Project/Prefabs/Enviorment
mkdir -p Project/Prefabs/UI
mdkir -p Project/Animations/Characters/Player
mkdir -p Project/Animations/Characters/Enemies
mkdir -p Project/Animations/Enviorment
mkdir -p Project/Materials
mkdir -p Project/Models/Characters/Player
mkdir -p Project/Models/Characters/Enemies
mkdir -p Project/Scripts/Characters/Player
mkdir -p Project/Scripts/Characters/Enemies
mkdir -p Project/Scripts/Combat
mkdir -p Project/Scripts/Enviorment
mkdir -p Project/Scripts/Puzzles
mkdir -p Project/Scripts/UI
mkdir -p Project/Scripts/Global
mkdir -p Project/Scripts/Enums
mkdir -p Project/Shaders
mkdir -p Project/Scenes/Levels
mkdir -p Project/Scenes/MainMenu
mkdir -p Project/Scenes/TestScenes
mkdir -p Project/Textures/Characters/Player
mkdir -p Project/Textures/Characters/Enemies
mkdir -p Project/Textures/Enviorment
mkdir -p Project/Textures/UI
mkdir -p Plugins
mkdir -p Resources
mkdir -p StreamingAssets

cd ../
mv -r Scenes/* .\Project\Scenes\TestScenes\
rm -rv Scenes