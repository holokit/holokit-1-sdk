# HoloKitSDK v1.0 Manual
# Prefabs
### HoloKitCameraRig
* The most basic component to have your game responds to ARKit tracking. 
* It has a child VideoSeeThroughCamera that renders video seethrough VR experience. There are two extra cameras "Left Eye", "Right Eye" as its children, to render stereo for HoloKit. In "HoloKitCameraRig" object, you can set default see through mode and the key for toggling modes. 

### HoloKitPlaneGenerator
* To generate collidable planes when ARKit detects a plane. Optionally, you can set a Plane Prefab as its property to visualize the generated planes.

### HoloKitPlacementRoot
* A convenient prefab to help you place your objects on the ground. See "How to place your object on the ground" for details.

### HoloKitAmbientLight
* A directional light, which intensity is controlled by ARKit. The intensity will adjust automatically based on the current environment's lighting in reality.

### HoloKitGazeManager
* A convenient prefab for you to emit gaze events. See "How to respond to gaze events" for details.

### PointCloud
* To visualize point clouds that ARKit detects, for debugging purpose. 

### Calibration Canvas
* A debugging UI to help you calibrate HoloKit hardware. See "How to calibrate for HoloKit Hardware" for details.

# Interaction
* You may use `HoloKitInputManager.GetKeyDown(HoloKitKeyCode.<KeyCode>)` to get input event from the player. To get input together with gaze events, see "How to respond to gaze events". 
* `HoloKitInputManager.GetKeyDown` **must be caleld in `Update()` function**. Not in `OnGUI()`, neither `LateUpdate()`. 
* We support multiple ways for interaction, and HoloKitInputManager respects inputs from all of them. 

## Bluetooth keyboard
* You can simply connect a Bluetooth keyboard to your iOS device. Then you use `HoloKitInputManager.GetKeyDown` to get key down events. For example, 

```
    public class MyBehavior : MonoBehavior {
        void Update() {
            if (HoloKitInputManager.GetKeyDown(HoloKitKeyCode.A)) {
                Debug.Log("'A' pressed!");
            }
        }
    }
```

## Utopia bluetooth controller
* [Utopia Bluetooth Controller](http://www.myretrak.com/vr/product.aspx?item_id=1) is a small game controller. Underlying it's nothing more but a Bluetooth keyboard. HoloKitKeyCode defines key mappings for this controller. 
* By default, you can press "B" button on the controller to place an object on the ground, and "C" button to toggle between video seethrough and HoloKit seethrough modes. 

## Another mobile device
* HoloKitSDK allows you to run a controller app on another mobile device, so that you can read key-press events, as long as the other device's orientation and position. 
* [WIP]

## Remote keyboard for debugging
* If you don't have any Bluetooth device in hand, you may also use your computer's keyboard. To do that, make sure your computer and your device are in the same network, and then launch the following command to send UDP packet to the device:

```
    while read -n 1 x; do echo $x > /dev/udp/<your_device_ip>/5555; done
```

# How to...
## Place your object on the ground
## Render plane and point cloud
## Respond to gaze events
## Calibrate for HoloKit Hardware

