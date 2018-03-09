using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMover : MonoBehaviour {

    public Transform target;
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    // Use this for initialization
    void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        
    }
	
	// Update is called once per frame
	void Update () {
		particles = new ParticleSystem.Particle[particleSystem.particleCount];
        particleSystem.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++) {
            var particle = particles[i];

            particle.position = Vector3.Lerp(particle.position, target.position, Time.deltaTime*2.0f);
            particles[i] = particle;
        }

        particleSystem.SetParticles(particles, particles.Length);

    }
}
