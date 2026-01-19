Project Overview This repository contains a simple "Hello World" style application with two services:

SimpleBackend: A .NET Core Web API.

simple-frontend: A React SPA.

Your Mission We want to see how you handle containerization and infrastructure for a standard web stack.

Requirements:

Dockerize: Create efficient Dockerfiles for both services.

Orchestrate: Create Kubernetes manifests (or a Helm chart) to deploy these services to a cluster.

The Frontend must be accessible via browser.

The Frontend must be able to talk to the Backend.

Pipeline: Create a GitHub Actions workflow that builds the images on push.

Bonus Points:

The React app defaults to localhost:5000. Show us how you configure it to point to the backend service dynamically in the Kubernetes environment.

Ensure the backend container runs as a non-root user.
