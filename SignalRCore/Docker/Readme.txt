Kubernetes settings

1. Apply Redis + px3 (from redis-k8s folder)
   Run kubectl -f redis.yml
	 Run kubectl -f redis-px3.yml
1. Apply microservices (from MultipleSignalRServers folder)
   Run kubectl -f signalrserver-deployment.yml
	 Run kubectl -f ingress.yml	 