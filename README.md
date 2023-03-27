# Unity Lights LOD (Level of Detail) System [![License](https://img.shields.io/badge/License-MIT-lightgrey.svg?style=flat)](http://mit-license.org)
A Unity runtime system to downgrade light (Point, Spot) quality (all the way to completely disabling them or downgrading shadow quality) as the camera gets further (and also based on frustum) in order to improve performance. 

![](/Screenshots/UnityLightsLodSystem_screenshot01.gif)

The tool has been verified on the following versions of Unity:
- 2021.3 (LTS)

*  *  *  *  *

## Features

- Adjustment of light LOD based on player distance
- Disable lights that have areas of influence outside the camera's frustum
- Ability to set custom LOD values for each light in the scene
- Supports different types of lights (recommended only for Point and Spot)
- Easy-to-use user interface and setup

*  *  *  *  *

## Installation

To use the Unity Lights Lod System in your Unity project, simply:

### Option A) Clone or download the repository and drop it in your Unity project.
1. Clone or download the repository
2. Import the `UnityLightsLodSystem` folder into your Unity project's `Assets` folder

### Option B) Add the repository to the package manifest (go in YourProject/Packages/ and open the "manifest.json" file and add "com..." line in the depenencies section). If you don't have Git installed, Unity will require you to install it.
```
{
  "dependencies": {
      ...
      "com.razluta.unitylightslodsystem": "https://github.com/razluta/UnityLightsLodSystem.git"
      ...
  }
}
```
### Option C) Add the repository to the Unity Package Manager using the Package Manager dropdown.
The repository is at: https://github.com/razluta/UnityLightsLodSystem.git

*  *  *  *  *

## Dependencies
While the tool has **no dependencies** outside of the core Unity Editor and Engine, I recommend using the [Unity Lights Audit Tool](https://github.com/razluta/UnityLightsAuditTool) to first sort the lights in the scene in order to be critical about what lights you are targetting.

Alternatively, you can use the default [Unity Search](https://docs.unity3d.com/2022.1/Documentation/Manual/search-overview.html) to sort and examine your lights. 

*  *  *  *  *

## Using the tool
To implement the Unity Lights Lod System in your Unity project, simply:

### Step 01 - Add the LightLodManager.prefab in your scene (or add the 'LightModManager.cs' script to an empty GameObject in your scene)
You can simply drag the LightLodManager.prefab in your scene. The manager acts as a singleton. The main parameter in the UI is the Update Rate, which dictates how often the the LightLodManager will update LODs in the scene. By default, it is set to 0.1f, which means it will run try to run about 10 times per second.

### Step 02 - For each light you want to LOD, add a LightLod.cs script in their GameObject
The LightLod component will be the one to tell the manager what properties we want to LOD on each light.
The "Is Disable After Last Lod" property will disable the light after the last LOD's max distance is surpassed. If the light should be visible at high distances, feel free to keep this flag disabled.
Note: make sure the min and max distances are not overlapping otherwise you might experience unexpected behavior. Include as many LODs as you need.
The screenshot below shows an LOD setup example. 

![](/Screenshots/UnityLightsLodSystem_screenshot02.png)

*  *  *  *  *

## Special notes
*Please note that I highly recommend you only use the LOD component on Point and Spot lights. Any type of LODing optimizations would be very noticeable and jarring on other light types.*

*  *  *  *  *

## Contributing

If you would like to contribute to the Unity Lights LOD System, please:

1. Fork the repository
2. Create a new branch
3. Make your changes
4. Submit a pull request

*  *  *  *  *

## License

The Unity Lights LOD System is licensed under the MIT License. See the `LICENSE` file for details.
