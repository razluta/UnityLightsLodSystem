# Unity Lights LOD (Level of Detail) System [![License](https://img.shields.io/badge/License-MIT-lightgrey.svg?style=flat)](http://mit-license.org)
A Unity runtime system to downgrade light (Point, Spot) quality (all the way to completely disabling them or downgrading shadow quality) as the camera gets further (and also based on frustuum) in order to improve performance. 

![](/Screenshots/UnityLightsLodSystem_screenshot01.gif)

The tool has been verified on the following versions of Unity:
- 2022.2

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

### Option B) Option B) Add the repository to the package manifest (go in YourProject/Packages/ and open the "manifest.json" file and add "com..." line in the depenencies section). If you don't have Git installed, Unity will require you to install it.
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

*  *  *  *  *

## Using the tool
(TODO: add details)

*  *  *  *  *

## Special notes
*Please note that I hightly recommend you only use the LOD component on Point and Spot lights. Any type of LODing optimizations would be very noticeable and jarring on other light types.*

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
