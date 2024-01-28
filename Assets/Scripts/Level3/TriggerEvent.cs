using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    private bool _firstContact = true;

    public Transform wall;
    
    // Time when the movement started.
    private float _startTime;

    // Total distance between the markers.
    private float _journeyLength;
    private const float ZMovement = 20f;

    private Vector3 _initialPosition;
    private Vector3 _newPosition;
    private Vector3 _diseaperPosition;
    
    // Movement speed in units per second.
    public float speed = 10.0F;
    public float speedReturn = 0.1F;
    private State _state = State.None;

    [Header("Audio")]
    public AudioClip audio1;
    public AudioClip audio2;
    public float soundWaitTime;
    private AudioSource audioSource;

    private enum State
    {
        None,
        Forward,
        Back
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_state == State.Forward)
        {
            // Distance moved equals elapsed time times speed..
            var distCovered = (Time.time - _startTime) * speed;
            // Fraction of journey completed equals current distance divided by total distance.
            var fractionOfJourney = distCovered / _journeyLength;
            // Set our position as a fraction of the distance between the markers.
            wall.position = Vector3.Lerp(wall.position, _newPosition, fractionOfJourney);
            if (wall.position == _newPosition)
            {
                // Todo: Call the sound and then do the back actions
                _state = State.Back;
                _startTime = Time.time;
                _diseaperPosition = _newPosition;
                _diseaperPosition.y = _newPosition.y - ZMovement;
                _journeyLength = Vector3.Distance(_newPosition, _diseaperPosition);
            }
        }
        if (_state == State.Back)
        {
            // Distance moved equals elapsed time times speed..
            var distCovered = (Time.time - _startTime) * speedReturn / 10000;
            // Fraction of journey completed equals current distance divided by total distance.
            var fractionOfJourney = distCovered / _journeyLength;
            // Set our position as a fraction of the distance between the markers.
            wall.position = Vector3.Lerp(wall.position, _diseaperPosition, fractionOfJourney);
            if (wall.position == _initialPosition)
            {
                _state = State.None;
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _firstContact)
        { 
            _startTime = Time.time;
            _initialPosition = wall.position;
            _newPosition = _initialPosition;
            _newPosition.z = _initialPosition.z - ZMovement;
            _journeyLength = Vector3.Distance(_initialPosition, _newPosition);
            _state = State.Forward;
           _firstContact = false;
           StartCoroutine(AudioCoroutine());
        } 
    }
    
    private IEnumerator AudioCoroutine() {
        audioSource.clip = audio1;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        yield return new WaitForSeconds(soundWaitTime);
        
        audioSource.clip = audio2;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        yield return new WaitForSeconds(soundWaitTime);
        
        audioSource.clip = audio2;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        yield return new WaitForSeconds(soundWaitTime);
        
        audioSource.clip = audio2;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }
}