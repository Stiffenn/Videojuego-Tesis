using System.Diagnostics;
using UnityEngine;

public class SpeedVisuals : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _speedVisuals;

    [SerializeField]
    private ParticleSystem[] _warpVisuals;

    private readonly Stopwatch _stopwatch = new();

    private void Update()
    {
        RefreshTimer();

        ToggleEffect(_speedVisuals, _stopwatch.Elapsed.Seconds >= 1f);
        ToggleEffect(_warpVisuals, MovementHandler.IsWarping);
    }

    private void ToggleEffect(ParticleSystem[] particles, bool isEnabled)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystem particle = particles[i];

            if (isEnabled && !particle.isPlaying)
            {
                particle.Play();
            }
            else if (!isEnabled && particle.isEmitting)
            {
                particle.Stop();
            }
        }
    }

    private void RefreshTimer()
    {
        if (InputReceiver.WIsPressed && !_stopwatch.IsRunning)
        {
            _stopwatch.Start();
        }
        else if ((!InputReceiver.WIsPressed || MovementHandler.IsWarping) && _stopwatch.IsRunning)
        {
            _stopwatch.Reset();
        }
    }
}