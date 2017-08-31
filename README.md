#HoloKitSDK v1.0 (Tango Branch)

HoloKitSDK is to build AR apps for HoloKit. Currently, we provide the SDK as Unity package.

- For Google Tango, please checkout "tango" branch.
- For Apple iOS (ARKit), please checkout "ios" branch. 
- For Android (ARCore), please checkout "android" branch. 

For detailed manual, please see [HoloKitSDK Reference Manual](docs/MANUAL.md).

## Prerequists
* A Google Tango device that supports Tango and running Android 7.0.
    * We tested HoloKitSDK with Asus Zenfone AR and Lenovo Phab 2 Pro.
* Unity 5.6.3f1 (Tango SDK cannot run on Unity 2017.x). Make sure you installed Android components.

## Quick Start
1. Import "HoloKitSDK" folder under "Assets" folder into a new Unity project.
2. You might be prompted to switch to Android platform. If so please go ahead and switch. 
3. Open the example scene "HoloKitSDK/Examples/CubeOnTheFloor".
4. Open "File" -> "Build Settings" and click "Build". 
5. After the app runs, you should see a cube and a sphere floating in the air somewhere. You may gaze at the sphere and it'll turn to red. 
    * ![Sample](images/app1.png)
10. The app detects planes, and you may click on the screen to place the cube on the plane. 
    * ![Sample](images/app2.png)
11. You may touch the small "C" button to switch to HoloKit mode. 
    * ![Sample](images/app3.png)

## Create your own experience
1. Create a new scene in Unity. 
2. Drag and drop everything in "HoloKitSDK/StarterPrefabs" to the scene, and delete the default "Main Camera" and "Directional Light". 
    * ![Screenshot](images/new_scene.png)
3. Put anything you like under "HoloKitPlacementRoot", and your model should have a comparable size as "DebugCube". Then feel free to turn off or delete "DebugCube". 
    * ![Screenshot](images/whale.png)
4. Build your scene and run!
5. If you don't like the ambient light, please disable HoloKitAmbientLight in your scene.
  

## Attribution

You shall read the [How to Attribute](https://holokit.io/#develop) section.

App developer shall mark with the words, "Works with HoloKit", or display either of the following two Holokit Logos in your app.

<img src="https://holokit.io/images/HoloKit_Logo1.png" width="250px">

or 
<img src="https://holokit.io/images/HoloKit_Logo2.png" width="90px">


For academic work, please cite Monocular Visual-Inertial State Estimation for Mobile Augmented Reality, P.Li et al (ISMAR 2017, accepted)