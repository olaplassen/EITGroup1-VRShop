/* Copyright (C) 2020 IMTEL NTNU - All Rights Reserved
 * Developer: Abbas Jafari
 * Ask your questions by email: a85jafari@gmail.com
 */

using System;
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
            }
            else if (current == 2)
            {
                canvastext.text = handlekurvtext;
                canvastitle.text = "Handlekurv";
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
            
                handlekurvtext += itemtext + "\n";
            
            
        }

    
    }
}