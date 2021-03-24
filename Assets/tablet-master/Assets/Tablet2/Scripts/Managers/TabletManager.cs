/* Copyright (C) 2020 IMTEL NTNU - All Rights Reserved
 * Developer: Abbas Jafari
 * Ask your questions by email: a85jafari@gmail.com
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tablet2
{
    /// <summary>
    /// This class is the main class for the tablet
    /// </summary>
    public class TabletManager : MonoBehaviour
    {
        private TabletPosition tabletPos;

        [Header("strings")]
        public string YrkesNavn = "Change this";

        #region Public Variables
        [Header("Canvases")]
  
        public Canvas helpPageCanvas;
        #endregion

        private GameObject[] yrkesTitles;
        public static TabletManager tabletManager;
        public Text canvastext;
        public Text canvastitle;
        public Material tabletMat;
        public Image headerImage;
        private String handlelistetext;
        private String handlekurvtext;
        Camera cam;

        /// <summary>
        /// Runs at the start
        /// </summary>
        private void Start()
        {
            handlekurvtext = "";
            handlelistetext = "Fisk\nMelk\nPizza";
            if (tabletManager == null)
                tabletManager = this;
            else if (tabletManager != this)
                Destroy(gameObject);
            if (!Camera.main)
                cam = GameObject.FindObjectOfType<Camera>();
            else
                cam = Camera.main;
            Debug.Log("All managers can be found under Tablet -> Managers");
            //set Camera.main as all canvases camera in world space if it's not assigned yet
            if (cam)
            {
                if (helpPageCanvas.worldCamera == null)
                    helpPageCanvas.worldCamera = cam;
            }

            tabletPos = transform.parent.transform.parent.gameObject.GetComponent<TabletPosition>();

            //Find all yerkesTitles gameobjects in the scene and set it up
            yrkesTitles = GameObject.FindGameObjectsWithTag("YrkesTitle");
            foreach (GameObject text in yrkesTitles)
                text.GetComponent<Text>().text = YrkesNavn;

            //restart the tablet
            ShowCanvas(helpPageCanvas);
        }

        private void Update()
        {
            int current = tabletPos.getCurrentTablet();
            if (current == 1)
            {
                canvastext.text = handlelistetext;
                canvastitle.text = "Handleliste";
                tabletMat.SetColor("_BaseColor", Color.red);
                headerImage.GetComponent<Image>().color = new Color32(250, 91, 5, 100);
            }
            else if (current == 2)
            {
                canvastext.text = handlekurvtext;
                canvastitle.text = "Handlekurv";
                tabletMat.SetColor("_BaseColor", Color.blue);
                headerImage.GetComponent<Image>().color = new Color32(0, 80, 158, 100);
            }
            else if(current == 3)
            {
                String endtext = "";
                List<String> forgotten = new List<String>();
                List<String> tooMuch = new List<String>();
                String[] handlekurv = handlekurvtext.Split('\n');
                String[] handleliste = handlelistetext.Split('\n');
                foreach (String item in handleliste)
                {
                    if (Array.IndexOf(handlekurv, item) == -1){
                        forgotten.Add(item);
                    }
                }
                foreach (String item in handlekurv)
                {
                    if (Array.IndexOf(handleliste, item) == -1)
                    {
                        tooMuch.Add(item);
                    }
                }
                endtext += "You forgot: \n";
                foreach (String item in forgotten) endtext += item + "\n";
                endtext += "You were not supposed to get: \n";
                foreach (String item in tooMuch) endtext += item + "\n";
                canvastitle.text = "Kvittering";
                tabletMat.SetColor("_BaseColor", Color.white);
                canvastext.text = endtext;
                headerImage.GetComponent<Image>().color = new Color32(100, 130, 130, 100);
            }

        }

        /// <summary>
        /// Open the tablet
        /// </summary>
        /// <param name="status"></param>
        public void OpenTablet(bool status)
        {
            tabletPos.SelectTablet(status);
        }

        /// <summary>
        /// Deactive all canvases
        /// </summary>
        private void HideAllCanvases()
        {
            helpPageCanvas.gameObject.SetActive(false);
        }

        /// <summary>
        /// Active his canvas and deactive the other canvases
        /// </summary>
        /// <param name="canvas"></param>
        public void ShowCanvas(Canvas canvas)
        {

            HideAllCanvases();
            canvas.gameObject.SetActive(true);
        }

        /// <summary>
        /// This method vil close the tablet
        /// </summary>
        public void CloseTablet()
        {
            ShowCanvas(helpPageCanvas);
            OpenTablet(false);
        }

        public void pickUpItem(String itemtext)
        {
                if(!handlekurvtext.Contains(itemtext))
                    handlekurvtext += itemtext + "\n";
            
            
        }

    
    }
}