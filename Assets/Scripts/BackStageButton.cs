﻿using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class BackStageButton : MonoBehaviour {

    public bool tutorialRestartFlg = false;

	public void OnClick(){
		AudioSource[] audioSources = GameObject.Find ("SEController").GetComponents<AudioSource> ();
		audioSources [0].Play ();
        audioSources [12].Stop ();

        if (Application.loadedLevelName == "tutorialKassen") {
            if(tutorialRestartFlg) {
                Application.LoadLevel("tutorialKassen");
            }else { 
                PlayerPrefs.SetInt("tutorialId", 13);
                Application.LoadLevel("tutorialMain");
            }
        } else { 

            Application.LoadLevel("mainStage");

		    bool isAttackedFlg = PlayerPrefs.GetBool ("isAttackedFlg");
		    bool isKessenFlg = PlayerPrefs.GetBool ("isKessenFlg");
		    if (!isAttackedFlg && !isKessenFlg) {
			    PlayerPrefs.SetBool ("fromKassenFlg", true);
            }
        }
        PlayerPrefs.Flush();
    }
}
