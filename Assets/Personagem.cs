using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour {

    //Atributos:
    public float distancia = 3.0f;
    public float velocidade = 0.015f;
    public float velocidadeRotacao = 2.0f;

    //Componentes:
    Vector3 destino;


    void Start () {
        //No comeco do jogo, o personagem ficará parado
        destino = transform.position;
	}
	
	void Update ()
    {
        AtualizarPosicaoDestino();

        Movimentar();

        Rotacionar();
    }
   
    private void Movimentar()
    {
        //direção
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    private void Rotacionar()
    {
        var rotacaoDestino = Quaternion.LookRotation(destino);
        var rotacao = Quaternion.Slerp(transform.rotation, rotacaoDestino, velocidadeRotacao * velocidade * Time.deltaTime);
        
        //Zerar os eixos (x e Z)
        rotacao.eulerAngles = new Vector3(0f, rotacao.eulerAngles.y, 0);

        transform.rotation = rotacao;
    }


    //Atualiza a posição [selecionada / clicada]
    private void AtualizarPosicaoDestino()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;

            //Captar a posição - Gera um "Raio"
            Ray ray = (Camera.main.ScreenPointToRay(mousePosition));

            RaycastHit hit;

            //out, a informacao do toque será armazenada em hit
            if (Physics.Raycast(ray, out hit, distancia))
            {
                //Ray tocou em algum ponto
                destino = hit.point;
                destino.y = transform.position.y;
            }
        }
    }
}
