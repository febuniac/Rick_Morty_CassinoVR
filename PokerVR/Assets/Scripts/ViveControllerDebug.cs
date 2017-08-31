// Codigo atualizado por Luciano Soares

/*
 * Copyright (c) 2016 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerDebug : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;        // referencia para o controle

    private SteamVR_Controller.Device Controller
    {   // Properties para o controle
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {                                   // recupera referência para o controle
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Exibe o valor X e Y pressionado no trackpad
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        // Exibe se o gatilho do controle foi pressionado
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " gatilho pressionado");
        }

        // Exibe se o gatilho do controle foi solto
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " gatilho solto");
        }

        // Exibe se os botões laterais (Grip) do controle foram pressionados
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip pressionado");
        }

        // Exibe se os botões laterais (Grip) do controle foram soltos
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip solto");
        }

        // Exibe se o botão do touch foi pressionados
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log(gameObject.name + " touch pressionado");
        }

        // Exibe se o botão do menu de aplicações foi pressionados
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log(gameObject.name + " menu pressionado");
        }

    }

}