using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRM;
using System.IO;
using UniGLTF;

namespace VRMARkit
{
    public class ARKitCreate : MonoBehaviour
    {
#if UNITY_EDITOR
        static readonly string[] blendShapes = {
            "browDownLeft",
            "browDownRight",
            "browInnerUpLeft",
            "browInnerUpRight",
            "browOuterUpLeft",
            "browOuterUpRight",
            "cheekPuff",
            "eyeBlinkLeft",
            "eyeBlinkRight",
            "eyeLookDownLeft",
            "eyeLookDownRight",
            "eyeLookInLeft",
            "eyeLookInRight",
            "eyeLookOutLeft",
            "eyeLookOutRight",
            "eyeLookUpLeft",
            "eyeLookUpRight",
            "eyeSquintLeft",
            "eyeSquintRight",
            "eyeWideLeft",
            "eyeWideRight",
            "jawForward",
            "jawLeft",
            "jawOpen",
            "jawRight",
            "mouthClose",
            "mouthDimpleLeft",
            "mouthDimpleRight",
            "mouthFrownLeft",
            "mouthFrownRight",
            "mouthFunnel",
            "mouthLeft",
            "mouthLowerDownLeft",
            "mouthLowerDownRight",
            "mouthPressLeft",
            "mouthPressRight",
            "mouthPucker",
            "mouthRight",
            "mouthRollLower",
            "mouthRollUpper",
            "mouthShrugLower",
            "mouthShrugUpper",
            "mouthSmileLeft",
            "mouthSmileRight",
            "mouthStretchLeft",
            "mouthStretchRight",
            "mouthUpperUpLeft",
            "mouthUpperUpRight",
            "noseSneerLeft",
            "noseSneerRight",
            "tongueOut"
        };
        static readonly Dictionary<string, string> bsConstrains = new Dictionary<string, string>() {
            { "eyeLookDownLeft",    "eyeLookDownRight"  },
            { "eyeLookDownRight",   "eyeLookDownLeft"   },
            { "eyeLookUpLeft",      "eyeLookUpRight"    },
            { "eyeLookUpRight",     "eyeLookUpLeft"     },
            { "eyeLookInLeft",      "eyeLookOutRight"   },
            { "eyeLookInRight",     "eyeLookOutLeft"    },
            { "eyeLookOutLeft",     "eyeLookInRight"    },
            { "eyeLookOutRight",    "eyeLookInLeft"     }
        };

        static List<BlendShapeBinding> CreateBlendShapeBinding(SkinnedMeshRenderer[] smrs, Transform root, string shapeName)
        {
            List<BlendShapeBinding> shapeBindings = new List<BlendShapeBinding>();
            string constrainShape = "";
            if (bsConstrains.ContainsKey(shapeName))
                constrainShape = bsConstrains[shapeName];

            foreach (SkinnedMeshRenderer smr in smrs)
            {
                Mesh mesh = smr.sharedMesh;
                if (mesh == null)
                {
                    Debug.LogErrorFormat("SkinnedMeshRenderer {1} has no mesh associated to it", smr.name);
                    return null;
                }
             
                for (int i = 0; i < mesh.blendShapeCount; i++)
                {
                    float weight = 0;
                    if (mesh.GetBlendShapeName(i).ToLower() == shapeName.ToLower())
                        weight = 100;
                    else if (mesh.GetBlendShapeName(i).ToLower() == constrainShape.ToLower())
                        weight = 100;

                    shapeBindings.Add(new BlendShapeBinding
                    {
                        Index = i,
                        RelativePath = smr.transform.RelativePathFrom(root),
                        Weight = weight,
                    });
                }
            }
            return shapeBindings;
        }

        [MenuItem("ARKit/Generate ARKit Blendshape clips")]
        static void GenerateBlendshapeClips()
        {
            Transform activeAvatar = Selection.activeTransform;
            if (activeAvatar == null )
            {
                Debug.LogError("No object has been selected in the scene!");
                return;
            }

            VRMBlendShapeProxy proxy = activeAvatar.GetComponent<VRMBlendShapeProxy>();
            if ( proxy == null )
            {
                Debug.LogErrorFormat("The object {0} has no BlenderShapes", activeAvatar);
                return;
            }

            SkinnedMeshRenderer[] skinnedMeshes = activeAvatar.GetComponentsInChildren<SkinnedMeshRenderer>();

            string blendShapesPath = Directory.GetParent(AssetDatabase.GetAssetPath( proxy.BlendShapeAvatar )).ToString();
            Debug.LogFormat("Path to the BlendShapes folder: {0}", blendShapesPath);

            foreach (string shapeName in blendShapes)
            {
                List<BlendShapeBinding> bindings = CreateBlendShapeBinding(skinnedMeshes, activeAvatar, shapeName);
                string path = string.Format("{0}/{1}.asset", blendShapesPath, shapeName);
                BlendShapeClip bsClip = BlendShapeAvatar.CreateBlendShapeClip(path);
                if (bsClip != null)
                {
                    bsClip.name = shapeName;
                    bsClip.Values = bindings.ToArray();
                    proxy.BlendShapeAvatar.Clips.Add(bsClip);
                }
            }
        }
#endif
    }
}
