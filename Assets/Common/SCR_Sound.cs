using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SoundData {
	public List<AudioClip> clips;
	
	public string	name = "";
	public float 	cooldown = 0;
	public float	maxCooldown = 0;
	public int		iteration = 0;
	
	public SoundData (string n, AudioClip c, float cd) {
		clips = new List<AudioClip>();
		clips.Add(c);
		name = n;
		maxCooldown = cd;
	}
	
	public SoundData (string n, List<AudioClip> c, float cd) {
		clips = c;
		name = n;
		maxCooldown = cd;
	}
	
	public void PlayOneShot(AudioSource source) {
		if (cooldown <= 0) {
			iteration ++;
			if (iteration >= clips.Count) {
				iteration = 0;
			}
			source.PlayOneShot(clips[iteration]);
			cooldown = maxCooldown;
		}
	}
	public void Update(float dt) {
		if (cooldown > 0) cooldown -= dt;
	}
}





public class SCR_Sound : MonoBehaviour {
	public const float DEFAULT_COOLDOWN = 0.07f;
	
	public static SCR_Sound instance;
	public AudioSource 	source;
	public List<SoundData> sounds;
	
    private void Start() {
        instance = this;
		sounds = new List<SoundData>();
		DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        float dt = Time.deltaTime;
		
		for (int i=0; i<sounds.Count; i++) {
			sounds[i].Update (dt);
		}
    }
	
	public void AddSound (string name, AudioClip clip, float cooldown = DEFAULT_COOLDOWN) {
		SoundData temp = new SoundData(name, clip, cooldown);
	}
	
	public void AddSounds (string name, List<AudioClip> clips, float cooldown = DEFAULT_COOLDOWN) {
		SoundData temp = new SoundData(name, clips, cooldown);
	}
	
	public void PlaySound (string name) {
		for (int i=0; i<sounds.Count; i++) {
			if (sounds[i].name == name) {
				sounds[i].PlayOneShot (source);
			}
		}
	}
}

