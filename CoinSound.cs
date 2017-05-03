using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSound : MonoBehaviour {

    //Clips
    public AudioClip coinClip;
    public AudioClip shovelClip;
    public AudioClip jetPackClip;
    public AudioClip walkingClip;
    public AudioClip jumpClip;
    public AudioClip jackHammerClip;
    public AudioClip explosionClip;
    public AudioClip dynimiteSizzleClip;
    public AudioClip lazorClip;
    public AudioClip gatlingClip;
    public AudioClip placeMineClip;
    public AudioClip digClip;
    public AudioClip gunHitClip;
    public AudioClip upBlipClip;
    public AudioClip downBlipClip;
    public AudioClip acceptBlipClip;
    public AudioClip dropShipClip;
    //Coins
    private int playCount;
    private int playCountMax=10;

    private float timer;
    private float soundDelay = .05f;

    
    private float volLowRange = .2f;
    private float volHighRange = .5f;

    private float pitchLowRange = .8f;
    private float pitchHighRange = 1.05f;
    //Beam
    public AudioClip beamWindUp;
    public AudioClip beamHold;
    public AudioClip beamWindDown;
    //Walk
    public bool makeWalkSound = false;
    //Gatling
    private bool makeGatlingSound = false;
    // Use this for initialization
    private AudioSource audioSourceCoin;
    private AudioSource audioSourceWeapon;
    private AudioSource audioSourceWeaponSustained;
    private AudioSource audioSourceJet;
    private AudioSource audioSourceWalk;
    private AudioSource audioSourceJump;
    private AudioSource audioSourceExplosion;
    private AudioSource audioSourceHits;
    private AudioSource audioSourceUI;
    void Start () {
        audioSourceCoin = AddAudio(null,false,false,1);
        audioSourceWeapon = AddAudio(null, false, false, 1);
        audioSourceJet = AddAudio(null, true, false,0.5f);
        audioSourceWalk = AddAudio(null, false, false, 0.5f);
        audioSourceJump = AddAudio(null, false, false, 0.5f);
        audioSourceExplosion = AddAudio(null, false, false, 0.5f);
        audioSourceHits = AddAudio(null, false, false, 0.5f);
        audioSourceUI = AddAudio(null, false, false, 0.5f);
        audioSourceWeaponSustained = AddAudio(null, false, false, 1);
    }

public void AddCoinSound()//call this to add a coin sound 
    {
        if (playCount < playCountMax)
        {
            playCount = playCount + 1;
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            //makeWalkSound = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            //makeWalkSound = false;
        }
        //Walk
        if (makeWalkSound)
        {
            if (!audioSourceWalk.isPlaying)
            {
                StartWalkSound();
            }
        }
        else
        {
            if (audioSourceWalk.isPlaying)
            {
                StopWalkSound();
            }
        }
        //Gatling
        if (makeGatlingSound)
        {
            if (!audioSourceWeapon.isPlaying)
            {
                audioSourceWeaponSustained.clip = gatlingClip;
                audioSourceWeaponSustained.volume = 1;
                audioSourceWeaponSustained.pitch = Random.Range(0.8f, 1.2f);
                audioSourceWeaponSustained.Play();
            }
        }
        //Coin
        if (timer<=0) {
            if (playCount > 0)
            {
                playCount--;
                timer = soundDelay;
                audioSourceCoin.clip = coinClip;
                float vol = Random.Range(volLowRange, volHighRange);
                audioSourceCoin.volume = vol;
                float pitch = Random.Range(pitchLowRange, pitchHighRange);
                audioSourceCoin.pitch = pitch;
                
                audioSourceCoin.Play();
            }
        }
        else
        {
            timer = timer - Time.deltaTime;
        }
    }
    public void PlayUIUp()
    {
        audioSourceUI.clip = upBlipClip;
        audioSourceUI.Play();

    }
    public void PlayUIDown()
    {
        audioSourceUI.clip = downBlipClip;
        audioSourceUI.Play();

    }
    public void PlayUIAccept()
    {
        audioSourceUI.clip = acceptBlipClip;
        audioSourceUI.Play();

    }
    public void PlayDropShip()
    {
        audioSourceUI.clip = acceptBlipClip;
        audioSourceUI.Play();

    }

    public void PlayHit()
    {
        audioSourceHits.clip = gunHitClip;
        audioSourceHits.volume = Random.Range(0.5f, 0.7f);
        audioSourceHits.loop = false;
        audioSourceHits.pitch = Random.Range(0.8f, 1.2f);
        audioSourceHits.Play();
      
    }
    public void PlayLazor()
    {
        audioSourceWeapon.clip = lazorClip;
        audioSourceWeapon.volume = Random.Range(0.4f, 0.6f);
        audioSourceWeapon.loop = false;
        audioSourceWeapon.pitch = Random.Range(0.8f, 1.2f);
        audioSourceWeapon.Play();
    }

    public void PlayShovel()
    {
        audioSourceWeapon.clip = shovelClip;
        audioSourceWeapon.volume = 1;
        audioSourceWeapon.loop = false;
        audioSourceWeapon.pitch = Random.Range(0.9f, 1);
        audioSourceWeapon.Play();
    }
    public void PlayPlaceMine()
    {
        audioSourceWeapon.clip = placeMineClip;
        audioSourceWeapon.volume = .5f;
        audioSourceWeapon.loop = false;
        audioSourceWeapon.pitch = Random.Range(0.8f, 1);
        audioSourceWeapon.Play();
    }
    public void PlayExplosion()
    {
        audioSourceExplosion.clip = explosionClip;
        audioSourceExplosion.pitch = Random.Range(0.8f, 1.2f);
        audioSourceExplosion.Play();
    }

    public void PlayJump()
    {
        audioSourceJump.clip = jumpClip;
        audioSourceJump.pitch = Random.Range(0.8f, 1.2f);
        audioSourceJump.Play();
    }

    public void PlayDynimiteSizzle()
    {
        audioSourceWeapon.clip = dynimiteSizzleClip;
        audioSourceWeapon.volume = .5f;
        audioSourceWeapon.loop = false;
        audioSourceWeapon.pitch = Random.Range(0.8f, 1.2f);
        audioSourceWeapon.Play();
    }

    public void PlayGatling()
    {
        if (!audioSourceWeaponSustained.isPlaying)
        {
            audioSourceWeaponSustained.clip = gatlingClip;
            audioSourceWeaponSustained.volume = 0.3f;
            audioSourceWeaponSustained.pitch = Random.Range(1f, 1.2f);
            audioSourceWeaponSustained.Play();
        }
    }


    public void StartDigSound()
    {
        if (!audioSourceWeaponSustained.isPlaying)
        {
            audioSourceWeaponSustained.clip = digClip;
            audioSourceWeaponSustained.volume = 0.3f;
            audioSourceWeaponSustained.pitch = 1;
            audioSourceWeaponSustained.loop = false;
            audioSourceWeaponSustained.Play();
        }
    }

    public void StartJackHammerSound()
    {
        if (!audioSourceWeaponSustained.isPlaying)
        {
            audioSourceWeaponSustained.clip = jackHammerClip;
            audioSourceWeaponSustained.volume = 1;
            audioSourceWeaponSustained.pitch = 1;
            audioSourceWeaponSustained.loop = false;
            audioSourceWeaponSustained.Play();
        }
    }


    public void StopWeaponSound()
    {
        makeGatlingSound = false;
        audioSourceWeaponSustained.loop = false;
        audioSourceWeaponSustained.Stop();
    }

    public void StartJetSound()
    {
        audioSourceJet.clip = jetPackClip;
        audioSourceJet.loop = true;
        audioSourceJet.Play();
    }
    public void StopJetSound()
    {
        audioSourceJet.Stop();
    }


    void StartWalkSound()//change variable makeWalkSound to call
    {
        audioSourceWalk.clip = walkingClip;
        audioSourceWalk.pitch = Random.Range(0.9f, 1.1f);
        audioSourceWalk.volume = Random.Range(0.2f, 0.4f);
        audioSourceWalk.Play();
    }
    void StopWalkSound()
    {
        audioSourceWalk.Stop();
    }
    //beam sound code
    public void PlayBeamSound()
    {

        audioSourceWeaponSustained.clip = beamHold;
        audioSourceWeaponSustained.pitch = Random.Range(0.9f, 1.1f);
        audioSourceWeaponSustained.volume = 0.2f;
        audioSourceWeaponSustained.loop = false;
        audioSourceWeaponSustained.Play();
    }



    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {

        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;

        return newAudio;

    }
}
