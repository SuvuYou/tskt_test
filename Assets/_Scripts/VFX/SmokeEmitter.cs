using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmokeEmitter : MonoBehaviour
{
    [Header("Smoke Settings")]
    [SerializeField] private SmokeParticle[] _smokePrefabs;
    [SerializeField] private Transform _emissionPoint;
    [SerializeField] private Transform _parent;

    [Header("Emission Timing")]
    [SerializeField] private float _minSpawnInterval = 0.3f;
    [SerializeField] private float _maxSpawnInterval = 0.7f;
    [SerializeField] private int _minParticlesPerPuff = 2;
    [SerializeField] private int _maxParticlesPerPuff = 5;
    [SerializeField] private float _delayBetweenParticles = 0.08f;

    [Header("Arc Settings")]
    [SerializeField] private int _resolution = 20;
    [SerializeField] private float _arcLengthMin = 4f;
    [SerializeField] private float _arcLengthMax = 6f;
    [SerializeField] private float _arcHeightMin = 2f;
    [SerializeField] private float _arcHeightMax = 4f;

    [Header("Animation")]
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _scaleOutEndSize = 0f;

    private Timer _emitSmokeTimer;
    private List<ObjectPool<SmokeParticle>> _smokePools;

    private void Start()
    {
        _smokePools = new List<ObjectPool<SmokeParticle>>();
        
        foreach (SmokeParticle randomPrefab in _smokePrefabs)
        {
            _smokePools.Add(new ObjectPool<SmokeParticle>(randomPrefab, 10, _parent));
        }

        _emitSmokeTimer = new Timer(GetRandomSpawnInterval());
        _emitSmokeTimer.Reset();
        _emitSmokeTimer.Start();
    }

    private void Update()
    {
        _emitSmokeTimer.Update(Time.deltaTime);

        if (_emitSmokeTimer.Time <= 0f)
        {
            EmitSmokePuff();
            _emitSmokeTimer.SetBaseTime(GetRandomSpawnInterval());
            _emitSmokeTimer.Start();
        }
    }

    private void EmitSmokePuff()
    {
        int count = Random.Range(_minParticlesPerPuff, _maxParticlesPerPuff + 1);
        StartCoroutine(EmitMultiple(count));
    }

    private IEnumerator EmitMultiple(int count)
    {
        for (int i = 0; i < count; i++)
        {
            EmitSmoke();
            yield return new WaitForSeconds(_delayBetweenParticles);
        }
    }

    private void EmitSmoke()
    {
        ObjectPool<SmokeParticle> smokePool = _smokePools[Random.Range(0, _smokePools.Count)];
        SmokeParticle smoke = smokePool.Get();

        smoke.transform.position = _emissionPoint.position;
        smoke.transform.localScale = Vector3.one;

        float arcLength = Random.Range(_arcLengthMin, _arcLengthMax);
        float arcHeight = Random.Range(_arcHeightMin, _arcHeightMax);
        Vector3[] path = GenerateSquareRootPath(_emissionPoint.position, arcLength, arcHeight);

        smoke.Initialize(path, _duration, _scaleOutEndSize, (SmokeParticle smoke) => smokePool.ReturnToPool(smoke));
    }

    private Vector3[] GenerateSquareRootPath(Vector3 origin, float length, float height)
    {
        List<Vector3> points = new();

        for (int i = 0; i <= _resolution; i++)
        {
            float t = (float)i / _resolution;
            float x = t * length;
            float y = Mathf.Sqrt(t) * height;

            Vector3 point = origin + _emissionPoint.forward * x + Vector3.up * y;
            points.Add(point);
        }

        return points.ToArray();
    }

    private float GetRandomSpawnInterval()
    {
        return Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }
}