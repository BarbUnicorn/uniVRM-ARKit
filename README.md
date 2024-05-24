# uniVRM ARKit

## Quick-Links
- [Introduction]
- [Requirements]
- [Installation]
- [Usage]
- [BlendShapes Generated]
- [Troubleshooting]

## Introduction

This package will automate the tedious task of creating the `BlendShapeClips` associated to uniVRM. Also, when the various `Mesh` components have the
`BlendShapes` matching the name of the ARKit `blendshapes`, it will assign the maximum value to them.

Also, it will apply constrains to the look `blendshapes` so your model will not look with each eye to a different place.

## Requirements

This package has been tested with [Unity 2019.4.31f](https://unity.com/releases/editor/whats-new/2019.4.31) and [uniVRM v0.89.0](https://github.com/vrm-c/UniVRM/releases/tag/v0.89.0)

As this package has only a dependency on `uniVRM`, it could also with a posterior version of the package. Please be aware of the required Unity version with posterior versions of the `uniVRM` package.

## Installation

There are several methods to install this plugin. The fastest is downloading the artifact and importing it in Unity through `Assets -> Import Package -> Custom Package ...`
and browsing for the file.

Another way is to use the package manager in Unity and adding this repository's URL.

Once the package has been installed, a new menu named `ARKit` should appear on the top of the Editor.

## Usage

The process of generating the `BlendShapes` is quite easy:
1. Select the VRM prefab **IN** your scene
2. Click on `ARKit -> Generate ARKit Blendshape clips`
3. Wait until it is finished
4. Done

In order to check that all the clips have been generated, you can check the `BlendShapeAvatar` component of your VRM model

## BlendShapes Generated

The blendshapes generated should be the following:
- browDownLeft
- browDownRight
- browInnerUpLeft
- browInnerUpRight
- browOuterUpLeft
- browOuterUpRight
- cheekPuff
- eyeBlinkLeft
- eyeBlinkRight
- eyeLookDownLeft
- eyeLookDownRight
- eyeLookInLeft
- eyeLookInRight
- eyeLookOutLeft
- eyeLookOutRight
- eyeLookUpLeft
- eyeLookUpRight
- eyeSquintLeft
- eyeSquintRight
- eyeWideLeft
- eyeWideRight
- jawForward
- jawLeft
- jawOpen
- jawRight
- mouthClose
- mouthDimpleLeft
- mouthDimpleRight
- mouthFrownLeft
- mouthFrownRight
- mouthFunnel
- mouthLeft
- mouthLowerDownLeft
- mouthLowerDownRight
- mouthPressLeft
- mouthPressRight
- mouthPucker
- mouthRight
- mouthRollLower
- mouthRollUpper
- mouthShrugLower
- mouthShrugUpper
- mouthSmileLeft
- mouthSmileRight
- mouthStretchLeft
- mouthStretchRight
- mouthUpperUpLeft
- mouthUpperUpRight
- noseSneerLeft
- noseSneerRight
- tongueOut

Also note that the following blendshapes are constrained to each other:
- eyeLookDownLeft <->   eyeLookDownRight
- eyeLookUpLeft <->     eyeLookUpRight
- eyeLookInLeft <->     eyeLookOutRight
- eyeLookInRight <->    eyeLookOutLeft

## Troubleshooting

The most common cause of failure is to not have selected an object in the scene.
In that case the following error should be printed into the console:
```
No object has been selected in the scene!
UnityEngine.Debug:LogError (object)
VRMARkit.ARKitCreate:GenerateBlendshapeClips () (at Assets/ARKit Create.cs:118)
```

If an object has been selected but it is not a valid VRM object the following error appears:
```
The object GameObject (UnityEngine.Transform) has no BlenderShapes
UnityEngine.Debug:LogErrorFormat (string,object[])
VRMARkit.ARKitCreate:GenerateBlendshapeClips () (at Assets/ARKit Create.cs:125)
```

Other case could be that the `GameObject` has a `VRM Blend Shape Proxy` but there is no `Blend Shape Avatar` associated with it.
Make sure you have imported your VRM into the project and added the prefab into the scene.