using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class drawPixels : MonoBehaviour
{
    public GameObject FloorPlane;
    public Material _floor;
    public Material _wall;
    public Texture2D image;
    public int cubeSize = 1;
    public int height = 3;
    public float offX, offZ;

    void Start()
    {
            offX = FloorPlane.transform.position.x - cubeSize * image.height / 2;
            offZ = FloorPlane.transform.position.z - cubeSize * image.width / 2;

            //print(image.height + " x " + image.width + " / " + image.GetPixels().LongLength);

            for (int i = 0; i < image.height; i++)
            {
                for (int j = 0; j < image.width; j++)
                {
                    int index = i + j * image.width;
                    Color c = image.GetPixels()[index];

                    if (c == Color.black)
                    {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(offX + j * cubeSize, (float)height / 2, offZ + i * cubeSize);
                        cube.transform.localScale = new Vector3(1, height, 1) * cubeSize;
                        cube.GetComponent<Renderer>().material = _wall;
                        cube.transform.SetParent(FloorPlane.transform);
                        cube.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                        cube.GetComponent<Renderer>().receiveShadows = false;

                    }
                }
            }

            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.name = "Floor";
            floor.transform.position = new Vector3(FloorPlane.transform.position.x - 0.5f, 0.8f, FloorPlane.transform.position.z - 0.5f);
            floor.transform.localScale = new Vector3(image.height - 0.5f * cubeSize, 0.2f, image.width - 0.5f * cubeSize);
            floor.GetComponent<Renderer>().material = _floor;
            floor.transform.SetParent(FloorPlane.transform);


            FloorPlane.transform.localScale = Vector3.one * 9;
       
       
    }
}
