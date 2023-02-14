using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    public TextMeshProUGUI time, health, finishText;
    float timeRecorder = 20f;
    int healthRecorder = 3;

    public UnityEngine.UI.Button buttonRes;

    bool isContinue = true;
    bool isFinished = false;

    private Rigidbody rb;
    private float speed = 2.75f;
    

    void Start()
    {
        health.text = healthRecorder.ToString();
        rb = GetComponent<Rigidbody>();     // topun �zerindeki rigidbody'e eri�tik
    }

    
    void Update()
    {
        if(isContinue == true && !isFinished)
        {
            timeRecorder -= Time.deltaTime;      // zaman sayac�n� saniyede 1 d���r�r
            time.text = (int)timeRecorder + "";
        }
        else if (isFinished == false)
        {
            finishText.text = "Game Over!";
            buttonRes.gameObject.SetActive(true);
        }
        
        if (timeRecorder < 0)
        {
            isContinue= false;
        }
    }


    private void FixedUpdate()
    {
        if((isContinue==true) && (isFinished != true)) // oyun devam ediyorsa oyuncu kontrol edebilsin
        {
            float yatay = Input.GetAxis("Horizontal");      // yatay input
            float dikey = Input.GetAxis("Vertical");        // dikey input

            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);    // hareket bilgileri   // -dikey dedik ��nk� oyunu oynarken w bas�nca geri gidiyordu
            rb.AddForce(kuvvet * speed);    // rigidbody'e kuvvet (vekt�r do�rultusunda)
        }
        else
        {
            rb.velocity = Vector3.zero;     // oyun bitti�inde top dursun
            rb.angularVelocity= Vector3.zero;   // oyun bitti�inde top d�nmesin
        }
        
    }

    private void OnCollisionEnter(Collision collision)      
    {
        string objName = collision.gameObject.name;
        if (objName.Equals("FloorFinish"))      // biti�e ula��p ula�mad���n�n kontrol�
        {
            //print("Oyun tamamland�");
            isFinished= true;
            finishText.text = "Successful!";
            buttonRes.gameObject.SetActive(true);
        }

        if(collision.gameObject.tag == "Wall")      // duvarla �arp���rsa can� gitsin
        {
            healthRecorder--;
            health.text = healthRecorder.ToString();
            if(healthRecorder==0)   // can� biterse oyun bitsin
            {
                isContinue= false;
            }
        }
    }
}
