# DevOps Technical Challenge


##  The Objective

Given a starter code repository containing the following applications:

- **Backend:** A .NET Core API (in the `backend/` folder).
    
- **Frontend:** A React application (in the `frontend/` folder).

 Task: Dockerize these applications, orchestrate them on a local Kubernetes cluster, and ensure the backend has a persistent database connection.

**Time Expectation:** 1 day (8 hours).

## Requirements

### Containerization (Docker)

- Create a `Dockerfile` for the **Backend** (.NET).
    
    - _Requirement:_ Use a **multi-stage build** to keep the final image size small (e.g., compile in an SDK image, run in a Runtime image).
        
- Create a `Dockerfile` for the **Frontend** (React with Vite).
    
    - _Requirement:_ The final container should serve the static files (e.g., using Nginx or similar)


### Orchestration (Local Kubernetes)

- **Target Environment:** Use **Minikube** or **Docker Desktop Kubernetes**.
    
- **Core Services:** Create Kubernetes manifests (YAML) or a Helm Chart to run the Frontend and Backend.
    
    - _Constraint:_ The Frontend must find the Backend dynamically using Kubernetes Service discovery or Environment Variables (no hardcoded `localhost` in the React container).
    
- **Persistence (Redis):**
    
    - The Backend application relies on a **Redis** instance to store a "Page View" counter.
        
    - Deploy a Redis container in your cluster.
        
    - Configure the Backend to connect to Redis via an environment variable named `ConnectionStrings__Redis` (e.g., `redis-service:6379`).
        
    - Maintain Persistence for Redis data across deployments.
        
        - _Test:_ If you delete the Redis pod and it restarts, the "Page View" count must **not** reset to zero.


### CI/CD Pipeline

- Create a GitHub Actions workflow.
    
- **The Logic Challenge:** The pipeline must detect _where_ changes happened.
    
    - If a developer pushes code _only_ to the `frontend/` folder, the pipeline should **only** build and tag the Frontend image.
        
    - If code is pushed to `backend/`, only the Backend should build.
        
    - If both change, both should build.
    
- **The Deployment Stub:**
    
    - Since the Kubernetes cluster is running locally , the GitHub Action cannot actually "deploy" to it.
        
    - Instead, have the "Deploy" step in the pipeline simply print a confirmation message (e.g., `echo "Deploying Frontend Version $GITHUB_SHA..."`).
        

## Deliverables

Please provide a link to a **private GitHub repository** containing:

1. The source code with your Dockerfiles added.
    
2. A `k8s/` folder with your manifests (or Helm charts).
    
3. The `.github/workflows/` YAML file showing the smart trigger logic.
    
4. A `README.md` that explains:
    
    - **How to run it:** The exact commands we need to run to spin up your solution on our own local Minikube/Docker Desktop.
        
    - **Persistence Verification:** A quick instruction on how we can verify the PVC is working (e.g., "Kill the redis pod using `kubectl delete pod...` and refresh the page to see the count persist").

