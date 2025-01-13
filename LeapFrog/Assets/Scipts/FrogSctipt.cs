using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class FrogSctipt : MonoBehaviour
{
    [SerializeField] Transform[] rocks;
    private Vector3 nextPosition;
    private Vector3 targetPosition;
    private Vector3 midPoint;
    private Vector3 firstPosition;
    private bool isJump=false;
    int totalJumps=0;
    float jumpSpeed=10;
    int actualRock=0;
    private bool isUp=true;
    private bool isFall=false;
    private Animator animator;
    private int trials=0;
    int allJumps=0;

    void Start()
    {
        animator=GetComponent<Animator>();
        firstPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isJump){
            Debug.Log("asdsad");
            if(isUp){
                transform.position = Vector3.MoveTowards(transform.position, midPoint, jumpSpeed * Time.deltaTime);
                animator.Play("Jump");
                // Si alcanza el punto medio, cambia a la fase de bajada
                if (Vector3.Distance(transform.position, midPoint) < 0.01f)
                {
                    isUp = false;
                    isFall=true;
                }
            }else if(isFall){
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, jumpSpeed * Time.deltaTime);
                animator.Play("Fall");
                // Si alcanza la posición objetivo, termina el salto
                if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
                {
                    isJump = false;
                    isFall=false;
                    isUp=true;
                }
            
            }
        }
        else if(Input.GetKeyDown(KeyCode.Space)){
            if(actualRock==9){
                transform.position=firstPosition;
                actualRock=0;
                totalJumps=0;
                trials++;
            }else{
                targetPosition=transform.position;
                totalJumps++;
                allJumps++;
                int sumRock=Random.Range(1, rocks.Length);
                actualRock+=sumRock;
                if(actualRock>9){
                    actualRock=9;
                }
                Debug.Log("NextROCK "+actualRock);
                nextPosition= rocks[actualRock].position;
                nextPosition.y=-2;
                midPoint = new Vector3((transform.position.x + nextPosition.x) / 2, 0, 0);
                isJump=true;
            }
            
        }
    }

    private void OnGUI() {
           // Crear un estilo de texto
    GUIStyle style = new GUIStyle();
    style.fontSize = 40; // Ajusta el tamaño de la fuente (puedes aumentar o reducir este valor)
    style.normal.textColor = Color.white; 
    // Mostrar las etiquetas con el estilo definido
    GUI.Label(new Rect(10, 10, 200, 20), "Ensayo actual: " + (actualRock + 1), style);
    GUI.Label(new Rect(10, 50, 200, 20), "Saltos en este ensayo: " + totalJumps, style);
    GUI.Label(new Rect(Screen.width - 500, 10, 200, 20), "Ensayos completados: " + trials, style);
    GUI.Label(new Rect(Screen.width - 500, 50, 200, 20), "Saltos totales: " + allJumps, style);
    GUI.Label(new Rect(Screen.width - 500, 90, 200, 20), "Media de saltos: " + (trials > 0 ? (float)allJumps / trials : 0).ToString("F2"), style);

    }
}
