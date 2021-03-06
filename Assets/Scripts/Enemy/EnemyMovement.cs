﻿using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _playerIsh;
        private Seeker _seeker;

        // The calculated path
        public Path path;

        // The AI's speed in meters per second
        public float speed = 10;

        // The max distance from the AI to a waypoint for it to continue to the next waypoint
        public float nextWaypointDistance = 1;

        // The waypoint we are currently moving towards
        private int currentWaypoint = 0;

        // How often to recalculate the path (in seconds)
        public float repathRate = 1f;
        private float lastRepath = -9999;

        private Animator _animator;
        private float _speed = EnemySpawner.DefaultEnemySpeed;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _playerIsh = FindObjectOfType<EnemyTarget>().transform;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Time.time - lastRepath > repathRate && _seeker.IsDone())
            {
                lastRepath = Time.time + UnityEngine.Random.value * repathRate * 0.5f;
                // Start a new path to the targetPosition, call the the OnPathComplete function
                // when the path has been calculated (which may take a few frames depending on the complexity)
                _seeker.StartPath(transform.position, _playerIsh.position, OnPathComplete);
            }

            if (path == null) // path hasn't been calculated yet
                return;

            if (currentWaypoint > path.vectorPath.Count) return;
            if (currentWaypoint == path.vectorPath.Count)
            {
                currentWaypoint++;
                return;
            }

            // Direction to the next waypoint
            var dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            transform.position += dir * Time.deltaTime * _speed;

            if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < 0.2f)
            {
                currentWaypoint++;

                if (currentWaypoint < path.vectorPath.Count)
                    transform.localScale = path.vectorPath[currentWaypoint].x > transform.position.x ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
                return;
            }

            if (path.vectorPath[currentWaypoint].y > transform.position.y + 0.1f)
            {
                _animator.SetBool("Run", false);
                _animator.SetBool("JumpUp", true);
                _animator.SetBool("JumpDown", false);
                _speed = 5.5f;
            }
            else if (path.vectorPath[currentWaypoint].y < transform.position.y - 0.1f)
            {
                _animator.SetBool("Run", false);
                _animator.SetBool("JumpUp", false);
                _animator.SetBool("JumpDown", true);
                _speed = 5.5f;
            }
            else
            {
                _animator.SetBool("Run", true);
                _animator.SetBool("JumpUp", false);
                _animator.SetBool("JumpDown", false);
                _speed = EnemySpawner.DefaultEnemySpeed;
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                // Reset the waypoint counter so that we start to move towards the first point in the path
                currentWaypoint = 0;
            }
        }
    }
}